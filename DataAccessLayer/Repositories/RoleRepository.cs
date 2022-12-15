using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private AdoNetContext _context;
        public RoleRepository(AdoNetContext context)
        {
            _context = context;
        }

        public int Create(Role item)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "INSERT INTO Roles (RoleName) VALUES (@rolename)";
                command.Parameters.Add(new SqliteParameter("@rolename", item.RoleName));
                command.ExecuteNonQuery();
                command.CommandText = "SELECT last_insert_rowid()";
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Delete(int id)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "DELETE FROM Roles WHERE Id = @id";
                command.Parameters.Add(new SqliteParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }

        public Role Get(int id)
        {
            Role item = new Role();
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Roles WHERE Id = @id";
                command.Parameters.Add(new SqliteParameter("@id", id));
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            item.Id = reader.GetInt32("Id");
                            item.RoleName = reader.GetString("RoleName");
                        }
                    }
                }
                return item;
            }
        }

        public IQueryable<Role> GetAll()
        {
            var items = new List<Role>();
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Roles";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Role item = new Role();
                            item.Id = reader.GetInt32("Id");
                            item.RoleName = reader.GetString("RoleName");
                            items.Add(item);
                        }
                    }
                    return items.AsQueryable();
                }
            }
        }

        public void Update(Role item)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "UPDATE Roles SET RoleName = @rolename, Slug = @slug WHERE Id = @itemid";
                command.Parameters.Add(new SqliteParameter("@itemid", item.Id));
                command.Parameters.Add(new SqliteParameter("@rolename", item.RoleName));
                command.ExecuteNonQuery();
            }
        }
    }
}
