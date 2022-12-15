using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public TagService(IUnitOfWork database)
        {
            _database = database;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Tag, TagModel>().ReverseMap();
            });
            _mapper = config.CreateMapper();
        }

        public int Create(TagModel item)
        {
            var tag = _mapper.Map<Tag>(item);
            if (tag == null) throw new Exception("Отсутствуют данные для записи в базу");
            var insertedId = _database.Tags.Create(tag);
            _database.Commit();
            return insertedId;
        }

        public void Delete(int id)
        {
            _database.Tags.Delete(id);
            _database.Commit();
        }

        public TagModel? Get(int id)
        {
            var tag = _database.Tags.Get(id);
            return _mapper.Map<TagModel>(tag);
        }

        public List<TagModel>? GetAll()
        {
            var tags = _database.Tags.GetAll();
            return _mapper.Map<List<TagModel>>(tags);
        }

        public void Update(TagModel item)
        {
            var tag = _mapper.Map<Tag>(item);
            if (tag is null) throw new Exception("Отсутствуют данные для обновления");
            _database.Tags.Update(tag);
            _database.Commit();
        }
    }
}
