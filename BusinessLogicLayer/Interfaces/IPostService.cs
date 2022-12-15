using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces
{
    public interface IPostService : IBlogBaseService<PostModel>
    {
        List<PostModel>? GetPostsByTagId(int id);
        List<ChartByDateModel> GetChartByDate();
        List<ChartByUserModel> GetChartByUser();
        List<ChartByTagModel> GetChartByTag();
    }
}
