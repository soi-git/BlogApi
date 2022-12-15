using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    public interface IPostRepository : IBlogBaseRepository<Post>
    {
        IQueryable<ChartByDate> GetChartByDate();
        IQueryable<ChartByUser> GetChartByUser();
        IQueryable<ChartByTag> GetChartByTag();
    }
}
