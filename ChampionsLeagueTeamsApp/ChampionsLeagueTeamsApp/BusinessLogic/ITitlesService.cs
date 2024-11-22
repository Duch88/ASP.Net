using ChampionsLeagueTeamsApp.Models;

namespace ChampionsLeagueTeamsApp.BusinessLogic
{
    public interface ITitlesService
    {
        Task<IEnumerable<Title>> GetAllTitlesAsync();  
        Task<IEnumerable<Title>> GetTitlesWithPaginationAsync(int pageNumber, int pageSize);  
        Task<IEnumerable<Title>> SearchTitlesAsync(string searchQuery); 
    }
}
