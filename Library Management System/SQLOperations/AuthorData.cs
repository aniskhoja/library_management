using Library_Management_System.Model;
using Library_Management_System.SQLConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Library_Management_System.SQLOperations
{
    public class AuthorData
    {
       public static List<Author> GetAll()
        {
            List<Author> authorList = new List<Author>();
            using (SqlConnection connection = SqlCon.Connection)
            {
                String query = @"SELECT [Id]
                      ,[Name]
                      ,[Email]
                      ,[DateCreated]
                  FROM [dbo].[Author]";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = connection;
              
                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    while (reader.Read())
                    {
                        Author author = new Author();
                        author.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        author.Email = reader.GetString(reader.GetOrdinal("Email"));
                        author.Name = reader.GetString(reader.GetOrdinal("Name"));
                        author.NameEmail = author.Name + " (" + author.Email+")";
                        authorList.Add(author);
                    }
                }
            }

            
            
            return authorList;
        }
        public static Int32 Add(Author author)
        {
            using (SqlConnection connection = SqlCon.Connection)
            {

                String query = @"INSERT INTO [dbo].[Author]
           ([Name]
           ,[Email])
           VALUES(@name, @email)";


                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@name", author.Name);
                cmd.Parameters.AddWithValue("@email", author.Email);

                connection.Open();
                var result = cmd.ExecuteNonQuery();

                return Convert.ToInt32(result);
            }

        }
    }
}