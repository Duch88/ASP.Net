using ChampionsLeagueTeamsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChampionsLeagueTeamsApp.BusinessLogic
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetAllTeamsAsync();
        Task<IEnumerable<Team>> SearchTeamsAsync(string searchQuery);
        Task<IEnumerable<Team>> GetTeamsWithPaginationAsync(int pageNumber, int pageSize);
        Task<int> GetTotalTeamsCountAsync();
    }
}
