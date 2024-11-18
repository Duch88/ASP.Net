﻿using ChampionsLeagueTeamsApp.Data;
using ChampionsLeagueTeamsApp.Models;
using ChampionsLeagueTeamsApp.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionsLeagueTeamsApp.Tests.Services
{
    public class TeamServiceTests
    {
        
        private ApplicationDbContext CreateContext()
        {
            return TestDbContext.GetInMemoryDbContext(); 
        }

       
        [Fact]
        public void AddTeam_ShouldAddTeamToDatabase()
        {
           
            var context = TestDbContext.GetInMemoryDbContext();
            var teamService = new TeamService(context);

            
            var team = new Team
            {
                Name = "Test Team",
                Country = "Test Country"
            };
   
            teamService.AddTeam(team);
        
            var dbTeam = context.Teams.FirstOrDefault(t => t.Name == "Test Team");
            Assert.NotNull(dbTeam);  
            Assert.Equal("Test Team", dbTeam.Name);
            Assert.Equal("Test Country", dbTeam.Country);

        }

       
        [Fact]
        public void GetAllTeams_ShouldReturnAllTeams()
        {
           
            using (var context = CreateContext())
            {
                context.Teams.AddRange(
                    new Team { Name = "Team 1", Country = "Country 1" },
                    new Team { Name = "Team 2", Country = "Country 2" }
                );
                context.SaveChanges();

                var teamService = new TeamService(context);

                
                var teams = teamService.GetAllTeams();

                
                Assert.Equal(2, teams.Count());  
            }
        }

        [Fact]
        public void UpdateTeam_ShouldUpdateTeamInDatabase()
        {
            using (var context = TestDbContext.GetInMemoryDbContext())
            {
                var teamService = new TeamService(context);

 
                var team = new Team
                {
                    Name = "Test Team",
                    Country = "Test Country"
                };

                teamService.AddTeam(team);

                var updatedTeam = new Team
                {
                    Id = team.Id,
                    Name = "Updated Team",
                    Country = "Updated Country",
                    ChampionsLeagueWins = 5
                };

                teamService.UpdateTeam(updatedTeam);

              
                var dbTeam = context.Teams.FirstOrDefault(t => t.Id == team.Id);
                Assert.NotNull(dbTeam);
                Assert.Equal("Updated Team", dbTeam.Name);
                Assert.Equal("Updated Country", dbTeam.Country);
                Assert.Equal(5, dbTeam.ChampionsLeagueWins);
            }
        }

        
        [Fact]
        public void DeleteTeam_ShouldRemoveTeamFromDatabase()
        {
            using (var context = CreateContext())
            {
                var teamService = new TeamService(context);
                var team = new Team
                {
                    Name = "Test Team",
                    Country = "Test Country"
                };

                teamService.AddTeam(team);

                var teamToDelete = context.Teams.First();
                context.Teams.Remove(teamToDelete);
                context.SaveChanges();

                var deletedTeam = context.Teams.FirstOrDefault(t => t.Name == "Test Team");
                Assert.Null(deletedTeam); 
            }
        }

        
        [Fact]
        public void AddTeam_ShouldThrowException_WhenTeamNameIsNull()
        {
            var context = TestDbContext.GetInMemoryDbContext();
            var teamService = new TeamService(context);

            var team = new Team
            {
                Name = null,
                Country = "Test Country"
            };


            var exception = Assert.Throws<ArgumentException>(() => teamService.AddTeam(team));
            Assert.Equal("Name cannot be null or empty.", exception.Message);
        }

        
        [Fact]
        public void AddTeam_ShouldThrowException_WhenTeamCountryIsNull()
        {
            
            var context = TestDbContext.GetInMemoryDbContext();
            var teamService = new TeamService(context);

           
            var team = new Team
            {
                Name = "Test Team",
                Country = null
            };

           
            var exception = Assert.Throws<ArgumentException>(() => teamService.AddTeam(team));
            Assert.Equal("Country cannot be null or empty.", exception.Message);
        }
    }
}
