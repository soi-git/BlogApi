using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class TagRepository : ITagRepository
    {
        private AdoNetContext _context;
        public TagRepository(AdoNetContext context)
        {
            _context = context;
        }

        public int Create(Tag item)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "INSERT INTO Tags (TagName) VALUES (@tagname)";
                command.Parameters.Add(new SqliteParameter("@tagname", item.TagName));
                command.ExecuteNonQuery();
                command.CommandText = "SELECT last_insert_rowid()";
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Delete(int id)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "DELETE FROM Tags WHERE Id = @id";
                command.Parameters.Add(new SqliteParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }

        public Tag Get(int id)
        {
            Tag item = new Tag();
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Tags WHERE Id = @id";
                command.Parameters.Add(new SqliteParameter("@id", id));
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            item.Id = reader.GetInt32("Id");
                            item.TagName = reader.GetString("TagName");
                        }
                    }
                }
                return item;
            }
        }

        public IQueryable<Tag> GetAll()
        {
            var items = new List<Tag>();
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Tags";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Tag item = new Tag();
                            item.Id = reader.GetInt32("Id");
                            item.TagName = reader.GetString("TagName");
                            items.Add(item);
                        }
                    }
                    return items.AsQueryable();
                }
            }
        }

        public void Update(Tag item)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "UPDATE Tags SET TagName = @tagname, Slug = @slug WHERE Id = @itemid";
                command.Parameters.Add(new SqliteParameter("@itemid", item.Id));
                command.Parameters.Add(new SqliteParameter("@tagname", item.TagName));
                command.ExecuteNonQuery();
            }
        }
    }
}
