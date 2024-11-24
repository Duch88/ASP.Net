using ChampionsLeagueTeamsApp.Models;

namespace ChampionsLeagueTeamsApp.BusinessLogic
{
    public interface IStadiumService
    {
        Task<IEnumerable<Stadium>> GetAllStadiumsAsync();
        Task<IEnumerable<Stadium>> SearchStadiumsAsync(string searchQuery);
        Task<IEnumerable<Stadium>> GetStadiumsWithPaginationAsync(int pageNumber, int pageSize);
    }
}
