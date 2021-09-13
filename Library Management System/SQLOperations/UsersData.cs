using Library_Management_System.Model;
using Library_Management_System.SQLConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Library_Management_System.SQLOperations
{
    public class UsersData
    {

        public static Int32 Add(User user)
        {
            using (SqlConnection connection = SqlCon.Connection)
            {

                String query = @"INSERT INTO [dbo].[User]([Name],[Email])
                VALUES(@name, @email)";


                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@email", user.Email);

                connection.Open();
                var result = cmd.ExecuteNonQuery();

                return Convert.ToInt32(result);
            }

        }


        public static User GetByEmail(string email)
        {
            User user = new User();
            using (SqlConnection connection = SqlCon.Connection)
            {
                String query = @"SELECT [Id]
                      ,[Name]
                      ,[Email]
                      ,[DateCreated]
                  FROM [dbo].[User] where Email=@email";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@email", email);
                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    while (reader.Read())
                    {
                        user.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        user.Email = email;
                        user.Name = reader.GetString(reader.GetOrdinal("Name"));
                    }
                }
            }
            return user;
        }
        public static DataTable GetAll()
        {
            List<Book> bookList = new List<Book>();
            using (SqlConnection connection = SqlCon.Connection)
            {
                String query = @"SELECT [Id]
                      ,[Title]
                      ,[Available]
                      ,[Quantity]
                      ,[DateCreated]
                  FROM [dbo].[Book]";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = connection;

                connection.Open();

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);

                        return dt;
                        // GridView1.DataSource = dt;
                        // GridView1.DataBind();
                    }
                }


            }
        }

        public static Int32 RentaBook(int bookId, int userId)
        {
            using (SqlConnection connection = SqlCon.Connection)
            {

                String query = @"INSERT INTO [dbo].[UserandBook]
           ([UserId]
           ,[BookId])
     VALUES
           (@userId,@bookId)";


                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.Parameters.AddWithValue("@userId", userId);

                connection.Open();
                var result = cmd.ExecuteNonQuery();

                return Convert.ToInt32(result);
            }
        }

        public static DataTable GetRentBookById(int userId)
        {
            List<Book> bookList = new List<Book>();
            using (SqlConnection connection = SqlCon.Connection)
            {
                String query = @"SELECT UserandBook.[Id]
                  ,[UserId]
                  ,[BookId]
	              ,Book.Title as Title
                  ,[BorrowDate]
                  ,[ReturnDate]
              FROM [dbo].[UserandBook] 
              Join Book ON Book.Id=UserandBook.BookId
              where UserId=@userId and ReturnDate is null";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@userId", userId);

                connection.Open();

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);

                        return dt;
                    
                    }
                }


            }
        }


        public static Int32 RetrunBook(int Id)
        {
            using (SqlConnection connection = SqlCon.Connection)
            {

                String query = @"UPDATE [dbo].[UserandBook]
   SET [ReturnDate] = GETDATE()
 WHERE UserandBook.Id=@id";


                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@id", Id);

                connection.Open();
                var result = cmd.ExecuteNonQuery();

                return Convert.ToInt32(result);
            }
        }


    }
}