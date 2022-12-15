using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork database)
        {
            _database = database;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserModel>().ReverseMap();
            });
            _mapper = config.CreateMapper();
        }


        public int Create(UserModel item)
        {
            var user = _mapper.Map<User>(item);
            if (user == null) throw new Exception("Отсутствуют данные для записи в базу");
            var insertedId = _database.Users.Create(user);
            _database.Commit();
            return insertedId;
        }

        public void Delete(int id)
        {
            _database.Users.Delete(id);
            _database.Commit();
        }

        public UserModel? Get(int id)
        {
            var user = _database.Users.Get(id);
            return _mapper.Map<UserModel>(user);
        }

        public List<UserModel>? GetAll()
        {
            var users = _database.Users.GetAll();
            return _mapper.Map<List<UserModel>>(users);
        }

        public void Update(UserModel item)
        {
            var user = _mapper.Map<User>(item);
            if (user is null) throw new Exception("Отсутствуют данные для обновления");
            _database.Users.Update(user);
            _database.Commit();
        }
    }
}
