using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUDinMVC.Models
{
    public class DBHandler
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            con = new SqlConnection(constring);
        }
        public void AddCategory(Category cmodel)
        {
            connection();
            using (var cmd = new SqlCommand("Insert into dbo.Category1 (Cid, CName) Values((Select MAX(Cid) from dbo.Category1)+1, @Name)", con))
            {
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.Parameters.AddWithValue("@Name", cmodel.Name);
                //cmd.Parameters.AddWithValue("@Cid", cmodel.Id);
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

        public List<ProductCatelogModel> GetProducts()
        {
            connection();
            //SqlCommand cmd = new SqlCommand("select * from dbo.Category1 where cid=1", con);
            List<ProductCatelogModel> products = new List<ProductCatelogModel>();
            Category c = new Category();
            using (var cmd = new SqlCommand("select * from dbo.Product1 prd Join Category1 ct on prd.cid = ct.cid order by PId Asc", con))
            {
                cmd.CommandType = CommandType.Text;
                con.Open();
                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        var prod = new ProductCatelogModel()
                        {
                            Product = new Product
                            {
                                PName = dr.GetString(dr.GetOrdinal("PName")),
                                Pid = dr.GetInt32(dr.GetOrdinal("PId")),
                                Cid = dr.GetInt32(dr.GetOrdinal("CId")),
                            },
                            
                            Category = new Category
                            {
                                Id = dr.GetInt32(dr.GetOrdinal("CId")),
                                Name = dr.GetString(dr.GetOrdinal("CName"))
                            }
                        };

                        products.Add(prod);
                    }
                    dr.Close();
                }

            }
            con.Close();
            return products;
        }

        public List<Category> GetCategories()
        {
            connection();
            List<Category> categories = new List<Category>();
            
            using (var cmd = new SqlCommand("select * from dbo.Category1", con))
            {
                cmd.CommandType = CommandType.Text;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var category = new Category
                        {
                            Id = dr.GetInt32(dr.GetOrdinal("CId")),
                            Name = dr.GetString(dr.GetOrdinal("CName"))
                        };
                        categories.Add(category);
                    }
                    dr.Close();
                }

            }
            con.Close();
            return categories;
        }

        public Product getProductById(int id)
        {
            connection();
            Product product = new Product();

            try
            {

                using (var cmd = new SqlCommand("select * from dbo.Product1 where PId=@pid", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@pid", id);
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var prod = new Product
                            {
                                Pid = dr.GetInt32(dr.GetOrdinal("PId")),
                                PName = dr.GetString(dr.GetOrdinal("PName")),
                                Cid = dr.GetInt32(dr.GetOrdinal("CId"))
                            };
                            product = prod;
                        }
                        dr.Close();
                    }

                }
                con.Close();
            }
            catch (Exception ex)
            {

            }
            return product;

        }

        public void UpdateCategory(Category cmodel)
        {
            connection();
            try
            {
                using (var cmd = new SqlCommand("Update dbo.Category1 set CName=@cname where CId=@cid", con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    cmd.Parameters.AddWithValue("@cid", cmodel.Id);
                    cmd.Parameters.AddWithValue("@cname", cmodel.Name);
                    cmd.ExecuteNonQuery();

                }
                con.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public void DeleteCategory(int catId)
        {
            connection();
            try
            {
                using (var cmd = new SqlCommand("Delete from dbo.Category1 where cid=@cid", con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    cmd.Parameters.AddWithValue("@cid", catId);
                    cmd.ExecuteNonQuery();

                }
                con.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public Category getCategoryById(int id)
        {
            connection();
            Category category = new Category();

            try
            {

                using (var cmd = new SqlCommand("select * from dbo.Category1 where Cid=@cid", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@cid", id);
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var cat = new Category
                            {
                                Name = dr.GetString(dr.GetOrdinal("CName")),
                                Id = dr.GetInt32(dr.GetOrdinal("Cid"))
                            };
                            category = cat;
                        }
                        dr.Close();
                    }

                }
                con.Close();
            }
            catch (Exception ex)
            {

            }


            return category;
        }

        public void CreateProduct(ProductModel pmodel)
        {
            connection();
            try
            {
                using (var cmd = new SqlCommand("Insert into dbo.Product1 (PName, Cid) Values( @pname, @cid)", con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.Parameters.AddWithValue("@pname", pmodel.Product.PName);
                    cmd.Parameters.AddWithValue("@cid", pmodel.Product.Cid);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            catch(Exception ex)
            {

            }
        }

        public void DeleteProduct(int pid)
        {
            connection();
            try
            {
                using (var cmd = new SqlCommand("Delete from dbo.Product1 where pid=@pid", con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    
                        cmd.Parameters.AddWithValue("@pid", pid);
                        cmd.ExecuteNonQuery();
                    
                }
                con.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public void UpdateProduct(ProductModel pmodel)
        {
            connection();
            try
            {
                using (var cmd = new SqlCommand("Update dbo.Product1 set PName=@pname, CId=@cid where PId=@pid", con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    cmd.Parameters.AddWithValue("@pid", pmodel.Product.Pid);
                    cmd.Parameters.AddWithValue("@cid", pmodel.Product.Cid);
                    cmd.Parameters.AddWithValue("@pname", pmodel.Product.PName);
                    cmd.ExecuteNonQuery();

                }
                con.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}