using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private AdoNetContext _context;
        public CommentRepository(AdoNetContext context)
        {
            _context = context;
        }

        public int Create(Comment item)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "INSERT INTO Comments (PostId, UserId, CreateDate, ContentText) VALUES (@postid, @userid, date('now'), @contenttext)";
                command.Parameters.Add(new SqliteParameter("@postid", item.PostId));
                command.Parameters.Add(new SqliteParameter("@userid", item.UserId));
                command.Parameters.Add(new SqliteParameter("@contenttext", item.ContentText));
                command.ExecuteNonQuery();
                command.CommandText = "SELECT last_insert_rowid()";
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Delete(int id)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "DELETE FROM Comments WHERE Id = @id";
                command.Parameters.Add(new SqliteParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }

        public Comment Get(int id)
        {
            Comment item = new Comment();
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Comments WHERE Id = @id";
                command.Parameters.Add(new SqliteParameter("@id", id));
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            item.Id = reader.GetInt32("Id");
                            item.PostId = reader.GetInt32("PostId");
                            item.UserId = reader.GetInt32("UserId");
                            item.CreateDate = reader.GetString("CreateDate");
                            item.ContentText = reader.GetString("ContentText");
                        }
                    }
                }
                return item;
            }
        }

        public IQueryable<Comment> GetAll()
        {
            var items = new List<Comment>();
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Comments";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Comment item = new Comment();
                            item.Id = reader.GetInt32("Id");
                            item.PostId = reader.GetInt32("PostId");
                            item.UserId = reader.GetInt32("UserId");
                            item.CreateDate = reader.GetString("CreateDate");
                            item.ContentText = reader.GetString("ContentText");
                            items.Add(item);
                        }
                    }
                    return items.AsQueryable();
                }
            }
        }

        public void Update(Comment item)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "UPDATE Comments SET PostId = @postid, UserId = @userid, CreateDate = @createdate, ContextText = @contexttext WHERE Id = @itemid";
                command.Parameters.Add(new SqliteParameter("@itemid", item.Id));
                command.Parameters.Add(new SqliteParameter("@postid", item.PostId));
                command.Parameters.Add(new SqliteParameter("@userid", item.UserId));
                command.Parameters.Add(new SqliteParameter("@createdate", item.CreateDate));
                command.Parameters.Add(new SqliteParameter("@contexttext", item.ContentText));
                command.ExecuteNonQuery();
            }
        }
    }
}
