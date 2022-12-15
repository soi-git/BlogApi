namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository Posts { get; }
        ICommentRepository Comments { get; }
        ITagRepository Tags { get; }
        ILikeRepository Likes { get; }
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        ILinkRepository Links { get; }
        void Commit();
    }
}
