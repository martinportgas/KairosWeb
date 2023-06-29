using KairosWeb.Models;
using System.Data;
using System.Data.SqlClient;

namespace KairosWeb.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private IConfiguration Configuration;
        private string connection;
        public ProductRepository(IConfiguration _configuration) 
        {
            Configuration = _configuration;
            connection = this.Configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<DataTable> GetProduct() 
        {
            var data = new DataTable();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand("sp_produk_get"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    conn.Open();
                    data.Load( await cmd.ExecuteReaderAsync());
                    conn.Close();
                    return data;
                }
            }
        }
        public async Task<DataTable> GetProductById(int Id)
        {
            var data = new DataTable();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand("sp_produk_get_by_id"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Connection = conn;
                    conn.Open();
                    data.Load(await cmd.ExecuteReaderAsync());
                    conn.Close();
                    return data;
                }
            }
        }

        public async Task<DataTable> GetProductByCode(string productCode)
        {
            var data = new DataTable();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand("sp_produk_get_duplicate_code"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductCode", productCode);
                    cmd.Connection = conn;
                    conn.Open();
                    data.Load(await cmd.ExecuteReaderAsync());
                    conn.Close();
                    return data;
                }
            }
        }
        public async Task<DataTable> GetProductByname(string productName)
        {
            var data = new DataTable();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand("sp_produk_get_duplicate_name"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Connection = conn;
                    conn.Open();
                    data.Load(await cmd.ExecuteReaderAsync());
                    conn.Close();
                    return data;
                }
            }
        }

        public async void Save(ProductModel model)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand("sp_produk_insert"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductCode", model.ProductCode);
                    cmd.Parameters.AddWithValue("@ProductName", model.ProductName);
                    cmd.Parameters.AddWithValue("@ProductQty", model.ProductQty);
                    cmd.Parameters.AddWithValue("@ProductDate", model.ProductDate);
                    cmd.Connection = conn;
                    conn.Open();
                    await cmd.ExecuteReaderAsync();
                    conn.Close();
                }
            }
        }
        public async void Update(ProductModel model)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand("sp_produk_update"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductId", model.ProductId);
                    cmd.Parameters.AddWithValue("@ProductCode", model.ProductCode);
                    cmd.Parameters.AddWithValue("@ProductName", model.ProductName);
                    cmd.Parameters.AddWithValue("@ProductQty", model.ProductQty);
                    cmd.Parameters.AddWithValue("@ProductDate", model.ProductDate);
                    cmd.Connection = conn;
                    conn.Open();
                    await cmd.ExecuteReaderAsync();
                    conn.Close();
                }
            }
        }
        public async void Delete(int Id)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand("sp_produk_delete"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductId", Id);
                    cmd.Connection = conn;
                    conn.Open();
                    await cmd.ExecuteReaderAsync();
                    conn.Close();
                }
            }
        }
    }
}
