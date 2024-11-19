using ChampionsLeagueTeamsApp.Data;
using ChampionsLeagueTeamsApp.Models;
using Microsoft.EntityFrameworkCore;


namespace ChampionsLeagueTeamsApp.BusinessLogic
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _context;

        public TeamService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team> GetTeamByIdAsync(int id)
        {
            return await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTeamAsync(Team team)
        {
            if (string.IsNullOrWhiteSpace(team.Name))
                throw new ArgumentException("Team name cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(team.Country))
                throw new ArgumentException("Team country cannot be null or empty.");

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTeamAsync(Team team)
        {
            var existingTeam = await _context.Teams.FindAsync(team.Id);
            if (existingTeam == null)
                throw new ArgumentException("Team not found.");

            existingTeam.Name = team.Name;
            existingTeam.Country = team.Country;
            existingTeam.ChampionsLeagueWins = team.ChampionsLeagueWins;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeamAsync(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
                throw new ArgumentException("Team not found.");

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }
    }

}
