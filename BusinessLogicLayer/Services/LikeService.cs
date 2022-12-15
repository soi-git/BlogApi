using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public LikeService(IUnitOfWork database)
        {
            _database = database;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Like, LikeModel>().ReverseMap();
            });
            _mapper = config.CreateMapper();
        }

        public int Create(LikeModel item)
        {
            var like = _mapper.Map<Like>(item);
            if (like == null) throw new Exception("Отсутствуют данные для записи в базу");
            var insertedId = _database.Likes.Create(like);
            _database.Commit();
            return insertedId;
        }

        public void Delete(int id)
        {
            _database.Likes.Delete(id);
            _database.Commit();
        }

        public LikeModel? Get(int id)
        {
            var like = _database.Likes.Get(id);
            return _mapper.Map<LikeModel>(like);
        }

        public List<LikeModel>? GetAll()
        {
            var likes = _database.Likes.GetAll();
            return _mapper.Map<List<LikeModel>>(likes);
        }

        public void Update(LikeModel item)
        {
            var like = _mapper.Map<Like>(item);
            if (like is null) throw new Exception("Отсутствуют данные для обновления");
            _database.Likes.Update(like);
            _database.Commit();
        }
    }
}
