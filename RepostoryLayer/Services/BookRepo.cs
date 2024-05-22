using Microsoft.Extensions.Configuration;
using modelLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepostoryLayer.Entity;
using RepostoryLayer.Interfaces;
using System.Reflection;

namespace RepostoryLayer.Services
{
    public class BookRepo:IBookRepo
    {
        private readonly DataContext _context;

        public BookRepo(DataContext _context)
        {
            this._context = _context;
        }

        public BookModel Add(BookModel model)
        {
            using (var conn = _context.CreateConnection())
            {
                var cmd = new SqlCommand("spaddnotes", (SqlConnection)conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Title", model.Title);
                cmd.Parameters.AddWithValue("@Author", model.Author);
                cmd.Parameters.AddWithValue("@Ratting", model.Ratting);
                cmd.Parameters.AddWithValue("@NumberOfRattings", model.NumberOfRattings);
                cmd.Parameters.AddWithValue("@Originalprice", model.Originalprice);
                cmd.Parameters.AddWithValue("@Discountprice", model.Discountprice);
                cmd.Parameters.AddWithValue("@Description", model.Description);
                cmd.Parameters.AddWithValue("@Quantity", model.Quantity);
                cmd.Parameters.AddWithValue("@Image ", model.Image);


                conn.Open();
                int result = cmd.ExecuteNonQuery();
                conn.Close();

                return result < 0 ? model : null;
            }

        }

        //get al books in list 
        public List<BookEntity> GetAllBooks()
        {
            List<BookEntity> books = new List<BookEntity>();
            using (var conn = _context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spGetAllBooks", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        BookEntity book = new BookEntity();
                        book.Id = Convert.ToInt32(dataReader["Id"]);
                        book.Title = dataReader["Title"].ToString();
                        book.Author = dataReader["Author"].ToString();
                        book.Ratting = Convert.ToInt64(dataReader["Ratting"]);
                        book.NumberOfRattings = (int)dataReader["NumberOfRattings"];
                        book.Originalprice = Convert.ToInt64(dataReader["Originalprice"]);
                        book.Discountprice = Convert.ToInt64(dataReader["Discountprice"]);
                        book.Description = dataReader["Description"].ToString();
                        book.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                        book.Image = dataReader["Image"].ToString();
                        books.Add(book);
                    }
                    return books;
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
        // update book details
        public object EmployeeUpdate( int noteid ,BookModel model)
        {
            using (var conn = _context.CreateConnection())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spUpdateBookDetails", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", noteid);
                   
                   // SqlDataReader dataReader = cmd.ExecuteReader();

                    //while (dataReader.Read())
                    //{
                        //cmd.Parameters.AddWithValue("@Id", noteid);
                        cmd.Parameters.AddWithValue("@Title", model.Title);
                        cmd.Parameters.AddWithValue("@Author", model.Author);
                        cmd.Parameters.AddWithValue("@Ratting", model.Ratting);
                        cmd.Parameters.AddWithValue("@NumberOfRattings", model.NumberOfRattings);
                        cmd.Parameters.AddWithValue("@Originalprice", model.Originalprice);
                        cmd.Parameters.AddWithValue("@Discountprice", model.Discountprice);
                        cmd.Parameters.AddWithValue("@Description", model.Description);
                        cmd.Parameters.AddWithValue("@Quantity", model.Quantity);
                        cmd.Parameters.AddWithValue("@Image ", model.Image);
                        cmd.ExecuteNonQuery();
                        return model;

                   // }
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
        // to delete the employee data 
        public bool DeleteBook(int id)
        {
            using (var conn = _context.CreateConnection())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DeleteDetails", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure; // Set command type for stored procedure

                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                    return true;
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


        // get book by id
        public BookEntity GetBookById(int id)
        {
            using (var conn = _context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("spGetBookById", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        BookEntity book = new BookEntity();
                        book.Id = Convert.ToInt32(dataReader["Id"]);
                        book.Title = dataReader["Title"].ToString();
                        book.Author = dataReader["Author"].ToString();
                        book.Ratting = Convert.ToDecimal(dataReader["Ratting"].ToString());
                        book.NumberOfRattings = (int)(dataReader["NumberOfRattings"]);
                        book.Originalprice = Convert.ToInt64(dataReader["Originalprice"]);
                        book.Discountprice = Convert.ToInt64(dataReader["Discountprice"]);
                        book.Description = dataReader["Description"].ToString();
                        book.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                        book.Image = dataReader["Image"].ToString();
                        return book;

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
                return null;
            }
        }
    }
}
