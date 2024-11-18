using ChampionsLeagueTeamsApp.Data;
using ChampionsLeagueTeamsApp.Models;
using ChampionsLeagueTeamsApp.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTeamsApp.Tests.Services
{
    public class TeamService
    {
        private readonly ApplicationDbContext _context;

        public TeamService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddTeam(Team team)
        {
            if (string.IsNullOrEmpty(team.Name))
            {
                throw new ArgumentException("Name cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(team.Country))
            {
                throw new ArgumentException("Country cannot be null or empty.");
            }

            _context.Teams.Add(team); 
            _context.SaveChanges();
        } 
        public IEnumerable<Team> GetAllTeams()
        {
            return _context.Teams.ToList();
        }

        public void UpdateTeam(Team team)
        {
            var existingTeam = _context.Teams.FirstOrDefault(t => t.Id == team.Id);
            if (existingTeam == null)
            {
                throw new ArgumentException("Team not found.");
            }

            existingTeam.Name = team.Name;
            existingTeam.Country = team.Country;
            existingTeam.ChampionsLeagueWins = team.ChampionsLeagueWins;
            existingTeam.Titles = team.Titles;

            _context.SaveChanges();
        }
    }


}


