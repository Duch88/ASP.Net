using ChampionsLeagueTeamsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChampionsLeagueTeamsApp.BusinessLogic
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetAllTeamsAsync();  
        Task<Team> GetTeamByIdAsync(int id);           
        Task AddTeamAsync(Team team);                  
        Task UpdateTeamAsync(Team team);               
        Task DeleteTeamAsync(int id);
    }
}
