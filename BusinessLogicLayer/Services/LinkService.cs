using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class LinkService : ILinkService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public LinkService(IUnitOfWork database)
        {
            _database = database;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Like, LikeModel>().ReverseMap();
            });
            _mapper = config.CreateMapper();
        }

        public int Create(LinkModel item)
        {
            var link = _mapper.Map<Link>(item);
            if (link == null) throw new Exception("Отсутствуют данные для записи в базу");
            var insertedId = _database.Links.Create(link);
            _database.Commit();
            return insertedId;
        }

        public void Delete(int id)
        {
            _database.Links.Delete(id);
            _database.Commit();
        }

        public LinkModel? Get(int id)
        {
            var link = _database.Links.Get(id);
            return _mapper.Map<LinkModel>(link);
        }

        public List<LinkModel>? GetAll()
        {
            var links = _database.Links.GetAll();
            return _mapper.Map<List<LinkModel>>(links);
        }

        public void Update(LinkModel item)
        {
            var link = _mapper.Map<Link>(item);
            if (link is null) throw new Exception("Отсутствуют данные для обновления");
            _database.Links.Update(link);
            _database.Commit();
        }
    }
}
