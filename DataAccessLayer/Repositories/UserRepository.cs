using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AdoNetContext _context;
        public UserRepository(AdoNetContext context)
        {
            _context = context;
        }

        public int Create(User item)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "INSERT INTO Users (UserName, Email, PasswordHash, RoleId) VALUES (@username, @email, @passwordhash, @roleid)";
                command.Parameters.Add(new SqliteParameter("@username", item.UserName));
                command.Parameters.Add(new SqliteParameter("@email", item.Email));
                command.Parameters.Add(new SqliteParameter("@passwordhash", item.PasswordHash));
                command.Parameters.Add(new SqliteParameter("@roleid", item.RoleId));
                command.ExecuteNonQuery();
                command.CommandText = "SELECT last_insert_rowid()";
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void Delete(int id)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "DELETE FROM Users WHERE Id = @id";
                command.Parameters.Add(new SqliteParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }

        public User Get(int id)
        {
            User item = new User();
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Users WHERE Id = @id";
                command.Parameters.Add(new SqliteParameter("@id", id));
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            item.Id = reader.GetInt32("Id");
                            item.UserName = reader.GetString("UserName");
                            item.Email = reader.GetString("Email");
                            item.PasswordHash = reader.GetString("PasswordHash");
                            item.RoleId = reader.GetInt32("RoleId");
                        }
                    }
                }
                return item;
            }
        }

        public IQueryable<User> GetAll()
        {
            var items = new List<User>();
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Users";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            User item = new User();
                            item.Id = reader.GetInt32("Id");
                            item.UserName = reader.GetString("UserName");
                            item.Email = reader.GetString("Email");
                            item.PasswordHash = reader.GetString("PasswordHash");
                            item.RoleId = reader.GetInt32("RoleId");
                            items.Add(item);
                        }
                    }
                    return items.AsQueryable();
                }
            }
        }

        public void Update(User item)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET UserName = @username, Email = @email, PasswordHash = @passwordhash, RoleId =@roleid WHERE Id = @itemid";
                command.Parameters.Add(new SqliteParameter("@itemid", item.Id));
                command.Parameters.Add(new SqliteParameter("@username", item.UserName));
                command.Parameters.Add(new SqliteParameter("@email", item.Email));
                command.Parameters.Add(new SqliteParameter("@passwordhash", item.PasswordHash));
                command.Parameters.Add(new SqliteParameter("@roleid", item.RoleId));
                command.ExecuteNonQuery();
            }
        }
    }
}
