using ChampionsLeagueTeamsApp.Models;

namespace ChampionsLeagueTeamsApp.BusinessLogic
{
    public interface ICoachService
    {
        Task<IEnumerable<Coach>> GetAllCoachesAsync();
        Task<IEnumerable<Coach>> SearchCoachesAsync(string searchQuery);
        Task<IEnumerable<Coach>> GetCoachesWithPaginationAsync(int pageNumber, int pageSize);
    }
}
