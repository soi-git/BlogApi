using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public PostService(IUnitOfWork database)
        {
            _database = database;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, PostModel>().ReverseMap();
                cfg.CreateMap<ChartByDate, ChartByDateModel>();
                cfg.CreateMap<ChartByUser, ChartByUserModel>();
                cfg.CreateMap<ChartByTag, ChartByTagModel>();
            });
            _mapper = config.CreateMapper();
        }

        public int Create(PostModel item)
        {
            var post = _mapper.Map<Post>(item);
            if (post == null) throw new Exception("Отсутствуют данные для записи в базу");
            var insertedId = _database.Posts.Create(post);
            _database.Commit();
            return insertedId;
        }

        public void Delete(int id)
        {
            _database.Posts.Delete(id);
            _database.Commit();
        }

        public PostModel? Get(int id)
        {
            return _mapper.Map<PostModel>(_database.Posts.Get(id));
        }

        public List<PostModel>? GetAll()
        {
            return _mapper.Map<List<PostModel>>(_database.Posts.GetAll());
        }

        public void Update(PostModel item)
        {
            var post = _mapper.Map<Post>(item);
            if (post is null) throw new Exception("Отсутствуют данные для обновления");
            _database.Posts.Update(post);
            _database.Commit();
        }

        public List<PostModel>? GetPostsByTagId(int id)
        {
            var links = _mapper.Map<List<LinkModel>>(_database.Links?.GetAll()?.Where(x => x.TagId == id));
            var posts = new List<PostModel>();
            foreach (var item in links)
            {
                var post = _mapper.Map<PostModel>(_database.Posts.Get(item.PostId));
                posts.Add(post);
            }
            return posts;
        }

        public List<ChartByDateModel> GetChartByDate()
        {
            return _mapper.Map<List<ChartByDateModel>>(_database.Posts.GetChartByDate());
        }

        public List<ChartByUserModel> GetChartByUser()
        {
            return _mapper.Map<List<ChartByUserModel>>(_database.Posts.GetChartByUser());
        }

        public List<ChartByTagModel> GetChartByTag()
        {
            return _mapper.Map<List<ChartByTagModel>>(_database.Posts.GetChartByTag());
        }

    }
}
