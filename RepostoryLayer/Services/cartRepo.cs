using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepostoryLayer.Entity;
using modelLayer;

namespace RepostoryLayer.Services
{
    public class CartRepo:ICartRepo
    {
        private readonly DataContext _dataContext;
        public CartRepo(DataContext _dataContext)
        {

           this. _dataContext =_dataContext;

        }
        public List<BookEntity> GetCartBooks(int UserId)
        {

            using (var conn =_dataContext.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("GetAllCart", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    List<BookEntity> list = new List<BookEntity>();

                    while (dataReader.Read())
                    {
                        BookEntity book = new BookEntity();
                        book.Id = Convert.ToInt32(dataReader["Id"]);
                        book.Title = dataReader["Title"].ToString();
                        book.Discountprice = Convert.ToInt64(dataReader["Discountprice"]);
                        book.Author = dataReader["Author"].ToString();
                        book.Description = dataReader["Description"].ToString();
                        book.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                        book.Image = dataReader["Image"].ToString();
                        list.Add(book);

                    }
                    return list;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }

        public List<BookEntity> AddToCart(CartModel model, int UserId)
        {
            using (var conn=_dataContext.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("AddCart_sp", (SqlConnection) conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@Quantity", model.Quantity);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return GetCartBooks(UserId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }

        //public double GetPrice(int UserId)
        //{
        //    List<Book> list = GetCartBooks(UserId);
        //    double totalPrice = 0;
        //    foreach (var book in list)
        //    {
        //        totalPrice += (book.Quantity * book.Price);
        //    }
        //    return totalPrice;
        //}

        public CartModel UpdateQuantity(int UserId, CartModel model)
        {
            using (var conn = _dataContext.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("UpdateCart_sp", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@Quantity", model.Quantity);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);
                    conn.Open();
                    int rowseefected = cmd.ExecuteNonQuery();
                    if (rowseefected > 0)
                    {
                        return model;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally { conn.Close(); }
                return null;

            }
        }

        public bool DeleteCart(DeleteCartModel model)
        {
            using (var conn=_dataContext.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("DeleteCart_sp",(SqlConnection) conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.Id);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);


                    conn.Open();
                    int rowseefected = cmd.ExecuteNonQuery();
                    if (rowseefected > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally { conn.Close(); }
                return false;

            }
        }
    }
}
