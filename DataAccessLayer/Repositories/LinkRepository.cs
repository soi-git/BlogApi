using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class LinkRepository : ILinkRepository
    {
        private AdoNetContext _context;
        public LinkRepository(AdoNetContext context)
        {
            _context = context;
        }

        public int Create(Link item)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "INSERT INTO Links (PostId, TagId) VALUES (@postid, @tagid)";
                command.Parameters.Add(new SqliteParameter("@postid", item.PostId));
                command.Parameters.Add(new SqliteParameter("@tagid", item.TagId));
                command.ExecuteNonQuery();
                command.CommandText = "SELECT last_insert_rowid()";
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Delete(int id)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "DELETE FROM Links WHERE Id = @id";
                command.Parameters.Add(new SqliteParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }

        public Link Get(int id)
        {
            Link item = new Link();
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Links WHERE Id = @id";
                command.Parameters.Add(new SqliteParameter("@id", id));
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            item.Id = reader.GetInt32("Id");
                            item.PostId = reader.GetInt32("PostId");
                            item.TagId = reader.GetInt32("TagId");
                        }
                    }
                }
                return item;
            }
        }

        public IQueryable<Link> GetAll()
        {
            var items = new List<Link>();
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Links";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Link item = new Link();
                            item.Id = reader.GetInt32("Id");
                            item.PostId = reader.GetInt32("PostId");
                            item.TagId = reader.GetInt32("TagId");
                            items.Add(item);
                        }
                    }
                    return items.AsQueryable();
                }
            }
        }

        public void Update(Link item)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "UPDATE Links SET PostId = @postid, TagId = @tagid WHERE Id = @itemid";
                command.Parameters.Add(new SqliteParameter("@itemid", item.Id));
                command.Parameters.Add(new SqliteParameter("@postid", item.PostId));
                command.Parameters.Add(new SqliteParameter("@tagid", item.TagId));
                command.ExecuteNonQuery();
            }
        }
    }
}
