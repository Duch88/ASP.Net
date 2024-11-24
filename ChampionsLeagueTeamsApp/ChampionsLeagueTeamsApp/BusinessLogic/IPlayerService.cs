using ChampionsLeagueTeamsApp.Models;


namespace ChampionsLeagueTeamsApp.BusinessLogic
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetAllPlayersAsync();
        Task<IEnumerable<Player>> SearchPlayersAsync(string searchQuery);
        Task<IEnumerable<Player>> GetPlayersWithPaginationAsync(int pageNumber, int pageSize);
    }
}
