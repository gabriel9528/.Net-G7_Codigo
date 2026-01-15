using StoreProcedure_EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace StoreProcedure_EntityFramework.DAL
{
    public class Product_DAL
    {
        string connectioString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        //GetAllProducts
        public List<ProductModel> GetAllProducts()
        {
            var listProducts = new List<ProductModel>();
            using(SqlConnection connection = new SqlConnection(connectioString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_get_all_products";

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();

                connection.Open();
                adapter.Fill(dtProducts);
                connection.Close();

                foreach (DataRow item in dtProducts.Rows)
                {
                    ProductModel product = new ProductModel();
                    product.Id = Convert.ToInt32(item["Id"]);
                    product.Name = item["Product"].ToString();
                    product.Price = Convert.ToDecimal(item["Price"]);
                    product.Quantity = Convert.ToInt32(item["Quantity"]);
                    product.Remark = item["Remark"].ToString();

                    listProducts.Add(product);
                }
            }

            return listProducts;
        }

        //GetProductByID
        public ProductModel GetProductById(int id)
        {
            ProductModel product = new ProductModel();
            using (SqlConnection connection = new SqlConnection(connectioString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_get_product_by_id";

                command.Parameters.AddWithValue("@id", id);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();

                connection.Open();
                adapter.Fill(dtProducts);
                connection.Close();

                foreach (DataRow item in dtProducts.Rows)
                {
                    product.Id = Convert.ToInt32(item["Id"]);
                    product.Name = item["Product"].ToString();
                    product.Price = Convert.ToDecimal(item["Price"]);
                    product.Quantity = Convert.ToInt32(item["Quantity"]);
                    product.Remark = item["Remark"].ToString();
                }
            }

            return product;
        }

        //InsertProducts
        public bool InsertProduct(ProductModel product)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(connectioString))
            {
                SqlCommand command = new SqlCommand("sp_insert_products", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@product_name", product.Name);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@quantity", product.Quantity);
                command.Parameters.AddWithValue("@remarks", product.Remark);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();

                if(id > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //UpdateProducts
        public bool UpdateProduct(ProductModel product)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(connectioString))
            {
                SqlCommand command = new SqlCommand("sp_update_product", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id", product.Id);
                command.Parameters.AddWithValue("@product", product.Name);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@quantity", product.Quantity);
                command.Parameters.AddWithValue("@remarks", product.Remark);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();

                if (id > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //DeleteProducts
        public string DeleteProduct(int id)
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection(connectioString))
            {
                SqlCommand command = new SqlCommand("sp_delete_product", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id", id);

                command.Parameters.Add("@ReturnMessage", SqlDbType.VarChar, 255)
                    .Direction = ParameterDirection.Output;

                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@ReturnMessage"].Value.ToString();
                connection.Close();
            }

            return result;
        }



    }
}