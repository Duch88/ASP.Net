using ChampionsLeagueTeamsApp.Data;
using ChampionsLeagueTeamsApp.Models;
using ChampionsLeagueTeamsApp.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        [Fact]
        public void AddTeam_ShouldEscapeHtmlCharacters()
        {
            var team = new Team
            {
                Name = "<script>alert('XSS')</script>",
                Country = "Test Country"
            };

            var encodedName = WebUtility.HtmlEncode(team.Name);
            var encodedCountry = WebUtility.HtmlEncode(team.Country);

            Assert.Equal("&lt;script&gt;alert(&#39;XSS&#39;)&lt;/script&gt;", encodedName);
            Assert.Equal("Test Country", encodedCountry);
        }

        [Fact]
        public void AddTeam_ShouldAddValidTeamToDatabase()
        {
            var context = TestDbContext.GetInMemoryDbContext();
            var teamService = new TeamService(context);

            var team = new Team
            {
                Name = "Valid Team",
                Country = "Valid Country"
            };

            teamService.AddTeam(team);

            var dbTeam = context.Teams.FirstOrDefault(t => t.Name == "Valid Team");
            Assert.NotNull(dbTeam);
            Assert.Equal("Valid Team", dbTeam.Name);
            Assert.Equal("Valid Country", dbTeam.Country);
        }

        [Fact]
        public void AddTeam_ShouldThrowException_WhenTeamIsNull()
        {
            var context = TestDbContext.GetInMemoryDbContext();
            var teamService = new TeamService(context);

            Team team = null;

            var exception = Assert.Throws<ArgumentException>(() => teamService.AddTeam(team));
            Assert.Equal("Team cannot be null.", exception.Message);
        }

        [Fact]
        public void AddTeam_ShouldEscapeHtml()
        {
          
            var context = TestDbContext.GetInMemoryDbContext();
            var teamService = new TeamService(context);

            var team = new Team
            {
                Name = "<script>alert('XSS')</script>",
                Country = "Test Country"
            };

         
            teamService.AddTeam(team);

           
            var dbTeam = context.Teams.FirstOrDefault(t => t.Name.Contains("&lt;script&gt;"));
            Assert.NotNull(dbTeam);

          
            var expectedEncodedName = WebUtility.HtmlEncode("<script>alert('XSS')</script>");
            Assert.Equal(expectedEncodedName, dbTeam.Name);
        }

        [Fact]
        public void UpdateTeam_ShouldUpdateExistingTeam()
        {
            var context = TestDbContext.GetInMemoryDbContext();
            var teamService = new TeamService(context);

            var team = new Team { Name = "Old Name", Country = "Old Country" };
            context.Teams.Add(team);
            context.SaveChanges();

            var updatedTeam = new Team { Id = team.Id, Name = "New Name", Country = "New Country" };

            teamService.UpdateTeam(updatedTeam);

            var dbTeam = context.Teams.FirstOrDefault(t => t.Id == team.Id);
            Assert.NotNull(dbTeam);
            Assert.Equal("New Name", dbTeam.Name);
            Assert.Equal("New Country", dbTeam.Country);
        }

        [Fact]
        public void UpdateTeam_ShouldThrowException_WhenTeamNotFound()
        {
            var context = TestDbContext.GetInMemoryDbContext();
            var teamService = new TeamService(context);

            var nonExistentTeam = new Team { Id = 999, Name = "Non-Existent", Country = "Nowhere" };

            var exception = Assert.Throws<ArgumentException>(() => teamService.UpdateTeam(nonExistentTeam));
            Assert.Equal("Team not found.", exception.Message);
        }

        [Fact]
        public void AddTeam_ShouldThrowException_WhenWinsIsNegative()
        {
            var context = TestDbContext.GetInMemoryDbContext();
            var teamService = new TeamService(context);

            var team = new Team { Name = "Invalid Team", Country = "Invalid Country", ChampionsLeagueWins = -1 };

            var exception = Assert.Throws<ArgumentException>(() => teamService.AddTeam(team));
            Assert.Equal("Champions League Wins must be a non-negative number.", exception.Message);
        }

        [Fact]
        public void DeleteTeam_ShouldRemoveTeam_WhenTeamExists()
        {
            var context = TestDbContext.GetInMemoryDbContext();
            var teamService = new TeamService(context);

            var team = new Team { Name = "Team to Delete", Country = "Country" };
            teamService.AddTeam(team);

            teamService.DeleteTeam(team.Id);

            Assert.Null(context.Teams.FirstOrDefault(t => t.Id == team.Id));
        }

        [Fact]
        public void DeleteTeam_ShouldThrowException_WhenTeamDoesNotExist()
        {
            var context = TestDbContext.GetInMemoryDbContext();
            var teamService = new TeamService(context);

            var exception = Assert.Throws<ArgumentException>(() => teamService.DeleteTeam(-1));
            Assert.Equal("Team not found.", exception.Message);
        }
    }
}

