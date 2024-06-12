using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepostoryLayer.Entity;
using modelLayer;
using RepostoryLayer.Interfaces;

namespace RepostoryLayer.Services
{
    public class wishlistRepo :IwishlistRepo
    {

        private readonly DataContext dataContext;
       public wishlistRepo(DataContext dataContext)
        {
            this.dataContext = dataContext;
       }
       public List<BookEntity> GetWhishListBooks(int userid)
        {

            using (var conn =dataContext.CreateConnection())
           {
                try
                {

                    SqlCommand cmd = new SqlCommand("GetWishListByUserId_sp",(SqlConnection)conn);
                   cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userid);

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

        public List<BookEntity> AddToWishList(AddWhishlist model)
        {
            using (var conn =dataContext.CreateConnection())
            {
               try
               {

                    SqlCommand cmd = new SqlCommand("AddWishList", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.Id);
                    cmd.Parameters.AddWithValue("@BookId", model.Bookid);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return GetWhishListBooks(model.Id);
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


        public bool DeleteWhishlist(DeleteCartModel model)
        {
            using (var conn = dataContext.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("deleteWishList_sp", (SqlConnection)conn);
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

                finally
                {
                    conn.Close();

                }
                return false;

            }
        }


    }
}
