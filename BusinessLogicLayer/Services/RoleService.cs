using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork database)
        {
            _database = database;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Role, RoleModel>().ReverseMap();
            });
            _mapper = config.CreateMapper();
        }

        public int Create(RoleModel item)
        {
            var role = _mapper.Map<Role>(item);
            if (role == null) throw new Exception("Отсутствуют данные для записи в базу");
            var insertedId = _database.Roles.Create(role);
            _database.Commit();
            return insertedId;
        }

        public void Delete(int id)
        {
            _database.Roles.Delete(id);
            _database.Commit();
        }

        public RoleModel? Get(int id)
        {
            var role = _database.Roles.Get(id);
            return _mapper.Map<RoleModel>(role);
        }

        public List<RoleModel>? GetAll()
        {
            var roles = _database.Roles.GetAll();
            return _mapper.Map<List<RoleModel>>(roles);
        }

        public void Update(RoleModel item)
        {
            var role = _mapper.Map<Role>(item);
            if (role is null) throw new Exception("Отсутствуют данные для обновления");
            _database.Roles.Update(role);
            _database.Commit();
        }
    }
}
