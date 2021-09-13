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
    public class BookData
    {
        //Add book data to database
        public static Int32 Add(Book book)
        {
            using (SqlConnection connection = SqlCon.Connection)
            {

                String query = @"INSERT INTO [dbo].[Book]
           ([Title]
           ,[Available]
           ,[Quantity])
     VALUES
           (@title,@available,@quantity)  SELECT SCOPE_IDENTITY() ";


                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@title", book.Title);
                cmd.Parameters.AddWithValue("@quantity", book.Quantity);
                cmd.Parameters.AddWithValue("@available", book.Quantity);

                connection.Open();
                var result = cmd.ExecuteScalar();

                return Convert.ToInt32(result);
            }

        }

        //Add book author in database by book id
        public static Int32 AddBookAuthor(int authorId, int bookId)
        {
            using (SqlConnection connection = SqlCon.Connection)
            {

                String query = @"INSERT INTO [dbo].[BookandAuthor]
           ([BookId]
           ,[AuthorId])
     VALUES
           (@bookId,
           @authorId)";


                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.Parameters.AddWithValue("@authorId", authorId);

                connection.Open();
                var result = cmd.ExecuteNonQuery();

                return Convert.ToInt32(result);
            }

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

                //using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                //{

                //    while (reader.Read())
                //    {
                //        Book book = new Book();
                //        book.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                //        book.Title = reader.GetString(reader.GetOrdinal("Title"));
                //        book.Available = reader.GetInt32(reader.GetOrdinal("Available"));
                //        book.Quantity = reader.GetInt32(reader.GetOrdinal("Available"));
                //        bookList.Add(book);
                //    }
                //}
            }
            //  return null;
        }

        public static Book GetbyId(int BookId)
        {
            Book book = new Book();
            using (SqlConnection connection = SqlCon.Connection)
            {
                String query = @"SELECT [Id]
                      ,[Title]
                      ,[Available]
                      ,[Quantity]
                      ,[DateCreated]
                  FROM [dbo].[Book] where Id=@Id";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@Id", BookId);
                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    while (reader.Read())
                    {
                        book.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        book.Available = reader.GetInt32(reader.GetOrdinal("Available"));
                        book.Quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                    }
                }
            }
            return book;


        }


        //get Author by book Id from database
        public static List<Author> GetAuthorsbyBookId(int Id)
        {
            List<Author> authorList = new List<Author>();
            using (SqlConnection connection = SqlCon.Connection)
            {
                String query = @"SELECT 
                       BookandAuthor.Id,
                  [BookId]
                  ,[AuthorId],
	              Author.[Name],
	              Author.[Email]
                  FROM BookandAuthor
                 Join Author ON BookandAuthor.AuthorId=Author.Id where BookId=@bookId";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@bookId", Id);
                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    while (reader.Read())
                    {
                        Author author = new Author();
                        author.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        author.Email = reader.GetString(reader.GetOrdinal("Email"));
                        author.Name = reader.GetString(reader.GetOrdinal("Name"));
                        author.NameEmail = author.Name + "(" + author.Email + ")";
                        authorList.Add(author);
                    }
                }
            }



            return authorList;
        }

        public static List<User> GetUsersbyBookId(int Id)
        {
            List<User> userList = new List<User>();
            using (SqlConnection connection = SqlCon.Connection)
            {
                String query = @"SELECT 
                       [UserandBook].Id,
                  [BookId]
                  ,[UserId],
	              [User].[Name],
	              [User].[Email]
                  FROM UserandBook
                 Join [User] ON [User].Id=[UserandBook].UserId where BookId=@bookId and [UserandBook].ReturnDate is null";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@bookId", Id);
                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    while (reader.Read())
                    {
                        User user = new User();
                        user.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        user.Email = reader.GetString(reader.GetOrdinal("Email"));
                        user.Name = reader.GetString(reader.GetOrdinal("Name"));
                        user.NameEmail = user.Name + " (" + user.Email + ")";
                        userList.Add(user);
                    }
                }
            }



            return userList;
        }

        public static int Delete(int BookId)
        {
            using (SqlConnection connection = SqlCon.Connection)
            {

                String query = @"DELETE FROM Book WHERE Id=@id ";


                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@id", BookId);

                connection.Open();
                var result = cmd.ExecuteNonQuery();

                // connection.Close();
                query = @"DELETE FROM BookandAuthor WHERE BookId=@id ";


                cmd = new SqlCommand(query);
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@id", BookId);

                //connection.Open();
                result = cmd.ExecuteNonQuery();

                return Convert.ToInt32(result);
            }
        }

        public static int Update(int BookId, int quantity, int availablility)
        {
            using (SqlConnection connection = SqlCon.Connection)
            {
               

                //book.Quantity = book.Quantity;
                String query = @"UPDATE [dbo].[Book] SET [Quantity] = @quantity, [Available]=@availability WHERE Id=@id";


                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@id", BookId);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@availability", availablility);

                connection.Open();
                var result = cmd.ExecuteNonQuery();

                return Convert.ToInt32(result);
            }
        }



    }
}