using MySql.Data.MySqlClient;
using Owner.models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Owner.controllers
{
    class AdminController
    {
        private string connString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        public (string, List<Admin>) Index()
        {
            string methodResult = "unknown";
            List<Admin> admins = new List<Admin>();

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = "SELECT * FROM admins";
                    MySqlDataReader reader = sql.ExecuteReader();
                    while (reader.Read())
                    {
                        Admin admin = new Admin();
                        admin.Id = (int)reader["id"];
                        admin.Name = (string)reader["name"];
                        admin.Password = (string)reader["password"];
                        admin.Salt = (string)reader["salt"];
                    }
                    methodResult = "ok";
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(Index));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
                finally
                {
                    conn.Close();
                }
            }                   
            return (methodResult, admins);
        }
        public (string, Admin) Show(int id)
        {
            string methodResult = "unknown";
            Admin admin = new Admin();

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = "SELECT * FROM admins WHERE id = @id";
                    sql.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = sql.ExecuteReader();
                    if (reader.Read())
                    {
                        admin.Id = (int)reader["id"];
                        admin.Name = (string)reader["name"];
                        admin.Password = (string)reader["password"];
                        admin.Salt = (string)reader["salt"];
                    }
                    methodResult = "ok";
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(Show));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
            return (methodResult, admin);
        }
        public string Create(Admin admin)
        {
            string methodResult = "unknown";
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = "INSERT INTO admins (name, password, salt) VALUES (@name, @password, @salt)";
                    sql.Parameters.AddWithValue("@name", admin.Name);
                    sql.Parameters.AddWithValue("@password", admin.Password);
                    sql.Parameters.AddWithValue("@salt", admin.Salt);
                    if(sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = "ok";
                    }
                    else
                    {
                        methodResult = "Not ok :(";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(Create));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
            return methodResult;
        }
        public string Update(Admin admin)
        {
            string methodResult = "unknown";
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = "UPDATE admins SET name = @name, password = @password, salt = @salt WHERE id = @id";
                    sql.Parameters.AddWithValue("@name", admin.Name);
                    sql.Parameters.AddWithValue("@password", admin.Password);
                    sql.Parameters.AddWithValue("@salt", admin.Password);
                    sql.Parameters.AddWithValue("@id", admin.Id);
                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = "ok";
                    }
                    else
                    {
                        methodResult = "Not ok :(";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(Update));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
            return methodResult;
        }
        public string Delete(int id)
        {
            string methodResult = "unknown";
            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = "DELETE FROM admins WHERE id = @id";
                    sql.Parameters.AddWithValue("@id", id);
                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = "ok";
                    }
                    else
                    {
                        methodResult = "Not ok :(";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(Delete));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
            return methodResult;
        }
    }
}
