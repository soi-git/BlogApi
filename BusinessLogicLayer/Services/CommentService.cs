using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;


namespace BusinessLogicLayer.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork database)
        {
            _database = database;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Comment, CommentModel>().ReverseMap();
            });
            _mapper = config.CreateMapper();
        }

        public int Create(CommentModel item)
        {
            var comment = _mapper.Map<Comment>(item);
            if (comment == null) throw new Exception("Отсутствуют данные для записи в базу");
            var insertedId = _database.Comments.Create(comment);
            _database.Commit();
            return insertedId;
        }

        public void Delete(int id)
        {
            _database.Comments.Delete(id);
            _database.Commit();
        }

        public CommentModel? Get(int id)
        {
            var comment = _database.Comments.Get(id);
            return _mapper.Map<CommentModel>(comment);
        }

        public List<CommentModel>? GetAll()
        {
            var comments = _database.Comments.GetAll();
            return _mapper.Map<List<CommentModel>>(comments);
        }

        public void Update(CommentModel item)
        {
            var comment = _mapper.Map<Comment>(item);
            if (comment is null) throw new Exception("Отсутствуют данные для обновления");
            _database.Comments.Update(comment);
            _database.Commit();
        }
    }
}
