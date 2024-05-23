using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using modelLayer;

namespace RepostoryLayer.Services
{
    public class AddressRepo:IAddressRepo
    {
        private readonly DataContext _context;
        public AddressRepo(DataContext _context)
        {
            this._context = _context;

        }
        public AddressModel AddAddress(AddressModel model)
        {
            

            using (var conn=_context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("AddAddress_sp", (SqlConnection) conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@FullAddress", model.FullAddress);
                    cmd.Parameters.AddWithValue("@City", model.City);
                    cmd.Parameters.AddWithValue("@State", model.State);
                    cmd.Parameters.AddWithValue("@Pincode", model.Pincode);
                    cmd.Parameters.AddWithValue("@Type", model.Type);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return model;
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


        public List<AddressModel> GetAddresses(int UserId)
        {
            using (var conn = _context.CreateConnection())
            {
                try
                {
                    List<AddressModel> addresses = new List<AddressModel>();
                    SqlCommand cmd = new SqlCommand("GetAddressByUserId_sp", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        AddressModel model = new AddressModel();
                        model.UserId = Convert.ToInt32(dataReader["UserId"]);
                        model.FullAddress = dataReader["FullAddress"].ToString();
                        model.City = dataReader["City"].ToString();
                        model.State = dataReader["State"].ToString();
                        model.Type = dataReader["Type"].ToString();
                        model.Pincode = Convert.ToInt32(dataReader["Pincode"]);
                       // cmd.Parameters.AddWithValue("Pincode", model.Pincode);
                        addresses.Add(model);

                    }
                    return addresses;
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

        public UpdateAddressModel UpdateAddress(UpdateAddressModel model)
        {

            using (var conn =_context.CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("UpdateAddressByUserId_sp", (SqlConnection)conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.UserId);
                    cmd.Parameters.AddWithValue("@AddressId", model.AddressId);
                    cmd.Parameters.AddWithValue("@FullAddress", model.FullAddress);
                    cmd.Parameters.AddWithValue("@City", model.City);
                    cmd.Parameters.AddWithValue("@State", model.State);
                    cmd.Parameters.AddWithValue("@Type", model.Type);
                    cmd.Parameters.AddWithValue("@Pincode",model.Pincode);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return model;
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
