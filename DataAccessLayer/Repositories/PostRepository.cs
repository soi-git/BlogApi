using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class PostRepository : IPostRepository
    {
        private AdoNetContext _context;
        public PostRepository(AdoNetContext context)
        {
            _context = context;
        }

        public int Create(Post item)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "INSERT INTO Posts (Title, Summary, ContentText, CreateDate, LastUpdateDate, UserId) VALUES (@title, @summary, @contenttext, date('now'), null, @userid)";
                command.Parameters.Add(new SqliteParameter("@title", item.Title));
                command.Parameters.Add(new SqliteParameter("@summary", item.Summary));
                command.Parameters.Add(new SqliteParameter("@contenttext", item.ContentText));
                command.Parameters.Add(new SqliteParameter("@userid", item.UserId));
                command.ExecuteNonQuery();
                command.CommandText = "SELECT last_insert_rowid()";
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Delete(int id)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "DELETE FROM Posts WHERE Id = @id";
                command.Parameters.Add(new SqliteParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }

        public Post Get(int id)
        {
            Post item = new Post();
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Posts WHERE Id = @Id";
                command.Parameters.Add(new SqliteParameter("@Id", id));
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            item.Id = reader.GetInt32("Id");
                            item.Title = reader.GetString("Title");
                            item.Summary = reader["Summary"] != DBNull.Value ? reader.GetString("Summary") : null;
                            item.ContentText = reader["ContentText"] != DBNull.Value ? reader.GetString("ContentText") : null;
                            item.CreateDate = reader.GetString("CreateDate");
                            item.LastUpdateDate = reader["LastUpdateDate"] != DBNull.Value ? reader.GetString("LastUpdateDate") : null;
                            item.UserId = reader.GetInt32("UserId");
                        }
                    }
                }
                return item;
            }
        }

        public IQueryable<Post> GetAll()
        {
            var items = new List<Post>();
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Posts";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Post item = new Post();
                            item.Id = reader.GetInt32("Id");
                            item.Title = reader.GetString("Title");
                            item.Summary = reader["Summary"] != DBNull.Value ? reader.GetString("Summary") : null;
                            item.ContentText = reader["ContentText"] != DBNull.Value ? reader.GetString("ContentText") : null;
                            item.CreateDate = reader.GetString("CreateDate");
                            item.LastUpdateDate = reader["LastUpdateDate"] != DBNull.Value ? reader.GetString("LastUpdateDate") : null;
                            item.UserId = reader.GetInt32("UserId");
                            items.Add(item);
                        }
                    }
                    return items.AsQueryable();
                }
            }
        }

        public void Update(Post item)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "UPDATE Posts SET Title = @title, Summary = @summary, ContentText = @contenttext, CreateDate = @createdate, LastUpdateDate = date('now'),  UserId = @userid WHERE Id = @itemid";
                command.Parameters.Add(new SqliteParameter("@itemid", item.Id));
                command.Parameters.Add(new SqliteParameter("@title", item.Title));
                command.Parameters.Add(new SqliteParameter("@summary", item.Summary));
                command.Parameters.Add(new SqliteParameter("@contenttext", item.ContentText));
                command.Parameters.Add(new SqliteParameter("@createdate", item.CreateDate));
                command.Parameters.Add(new SqliteParameter("@userid", item.UserId));
                command.ExecuteNonQuery();
            }
        }

        public IQueryable<ChartByDate> GetChartByDate()
        {
            var items = new List<ChartByDate>();
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "SELECT CreateDate AS PostDate, Count(*) AS PostCount FROM Posts GROUP BY CreateDate";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ChartByDate item = new ChartByDate();
                            item.PostDate = reader.GetString("PostDate");
                            item.PostsCount = reader.GetInt32("PostCount");
                            items.Add(item);
                        }
                    }
                    return items.AsQueryable();
                }
            }
        }

        public IQueryable<ChartByUser> GetChartByUser()
        {
            var items = new List<ChartByUser>();
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "SELECT UserName AS PostUser, Count(*) AS PostCount from Posts P JOIN Users U ON U.id = P.UserId GROUP BY UserId";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ChartByUser item = new ChartByUser();
                            item.PostUser = reader.GetString("PostUser");
                            item.PostsCount = reader.GetInt32("PostCount");
                            items.Add(item);
                        }
                    }
                    return items.AsQueryable();
                }
            }
        }

        public IQueryable<ChartByTag> GetChartByTag()
        {
            var items = new List<ChartByTag>();
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "SELECT TagName as PostTag, Count(*) AS PostCount FROM Tags T JOIN Links L ON T.Id = L.TagId JOIN Posts P ON L.PostId = P.Id group by TagId";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ChartByTag item = new ChartByTag();
                            item.PostTag = reader.GetString("PostTag");
                            item.PostsCount = reader.GetInt32("PostCount");
                            items.Add(item);
                        }
                    }
                    return items.AsQueryable();
                }
            }
        }

    }
}
