using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private AdoNetContext _context;
        public LikeRepository(AdoNetContext context)
        {
            _context = context;
        }

        public int Create(Like item)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "INSERT INTO Likes (PostId, UserId, CreateDate) VALUES (@postid, @userid, date('now'))";
                command.Parameters.Add(new SqliteParameter("@postid", item.PostId));
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
                command.CommandText = "DELETE FROM Likes WHERE Id = @id";
                command.Parameters.Add(new SqliteParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }

        public Like Get(int id)
        {
            Like item = new Like();
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Likes WHERE Id = @id";
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
                        }
                    }
                }
                return item;
            }
        }

        public IQueryable<Like> GetAll()
        {
            var items = new List<Like>();
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Likes";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Like item = new Like();
                            item.Id = reader.GetInt32("Id");
                            item.PostId = reader.GetInt32("PostId");
                            item.UserId = reader.GetInt32("UserId");
                            item.CreateDate = reader.GetString("CreateDate");
                            items.Add(item);
                        }
                    }
                    return items.AsQueryable();
                }
            }
        }

        public void Update(Like item)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "UPDATE Likes SET PostId = @postid, UserId = @userid, CreateDate = @createdate WHERE Id = @itemid";
                command.Parameters.Add(new SqliteParameter("@itemid", item.Id));
                command.Parameters.Add(new SqliteParameter("@postid", item.PostId));
                command.Parameters.Add(new SqliteParameter("@userid", item.UserId));
                command.Parameters.Add(new SqliteParameter("@createdate", item.CreateDate));
                command.ExecuteNonQuery();
            }
        }
    }
}
