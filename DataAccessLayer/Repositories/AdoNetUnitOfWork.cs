using DataAccessLayer.Context;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories
{
    public class AdoNetUnitOfWork : IUnitOfWork
    {
        const string ConnectionStringName = "Data Source=..\\blog.db;Foreign Keys=true";
        private readonly AdoNetContext _context;
        private UserRepository? _userRepository;
        private CommentRepository? _commentRepository;
        private PostRepository? _postRepository;
        private TagRepository? _tagRepository;
        private LikeRepository? _likeRepository;
        private LinkRepository? _linkRepository;
        private RoleRepository? _roleRepository;

        private bool disposedValue = false;

        public AdoNetUnitOfWork()
        {
            _context = new AdoNetContext(ConnectionStringName, true);
        }
        
        public IUserRepository Users => _userRepository ?? (_userRepository = new UserRepository(_context));
        public ICommentRepository Comments => _commentRepository ?? (_commentRepository = new CommentRepository(_context));
        public IPostRepository Posts => _postRepository ?? (_postRepository = new PostRepository(_context));
        public ITagRepository Tags => _tagRepository ?? (_tagRepository = new TagRepository(_context));
        public ILikeRepository Likes => _likeRepository ?? (_likeRepository = new LikeRepository(_context));
        public ILinkRepository Links => _linkRepository ?? (_linkRepository = new LinkRepository(_context));
        public IRoleRepository Roles => _roleRepository ?? (_roleRepository = new RoleRepository(_context));


        public void Commit()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
