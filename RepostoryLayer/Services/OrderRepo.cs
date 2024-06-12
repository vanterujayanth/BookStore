using modelLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepostoryLayer.Entity;

namespace RepostoryLayer.Services
{
    public class OrderRepo:IOrderRepo
    {
        private readonly DataContext _dataContext;
        public OrderRepo(DataContext _dataContext)
        {
            this._dataContext = _dataContext;
        }
        public List<BookEntity> GetOrders(int userid)
        {

            using (var conn = _dataContext.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("GetOrderByUserId_sp", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userid);

                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    List<BookEntity> orders = new List<BookEntity>();

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
                        orders.Add(book);

                    }
                    return orders;
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


        public List<BookEntity> AddToOrder(orderModel model, int UserId)
        {
            using (var conn = _dataContext.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("AddOrder_sp",(SqlConnection) conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@Quantity", model.Quantity);
                    cmd.Parameters.AddWithValue("@BookId", model.BookId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return GetOrders(UserId);
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


        public double GetPriceInOrder(int UserId)
        {
            List<BookEntity> bookList = GetOrders(UserId);
            double totalPrice = 0;
            foreach (var book in bookList)
            {
                totalPrice += (book.Quantity * book.Discountprice);
            }
            return totalPrice;
        }





        //feter

        public List<BookEntity> GetallOrders(int userid)
        {

            using (var conn = _dataContext.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("GetOrdersByUserId_sp", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userid);

                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    List<BookEntity> orders = new List<BookEntity>();

                    while (dataReader.Read())
                    {
                        BookEntity book = new BookEntity();

                        book.UserId = Convert.ToInt32( dataReader["Id"]);
                        book.UserName = dataReader["FullName"].ToString();
                        //book.UserName=
                        book.Id = Convert.ToInt32(dataReader["Id"]);
                        book.Title = dataReader["Title"].ToString();
                        book.Discountprice = Convert.ToInt64(dataReader["Discountprice"]);
                        book.Author = dataReader["Author"].ToString();
                        book.Description = dataReader["Description"].ToString();
                        book.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                        book.Image = dataReader["Image"].ToString();
                        orders.Add(book);

                    }
                    return orders;
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

    }





}
