using ChampionsLeagueTeamsApp.Models;
using ChampionsLeagueTeamsApp.Data;

namespace ChampionsLeagueTeamsApp.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            
            if (!context.Teams.Any())
            {
                SeedTeams(context);
            }

           
            if (!context.Titles.Any())
            {
                SeedTitles(context);
            }

            if (!context.Coaches.Any())
            {
                SeedCoaches(context);
            }

            if (!context.Players.Any())
            {
                SeedPlayers(context);
            }

            {
                SeedStadiums(context);
            }
        }

        private static void SeedTeams(ApplicationDbContext context)
        {
            var teams = new[]
            {
                new Team { Name = "Real Madrid", Country = "Spain", ChampionsLeagueWins = 15 },
                new Team { Name = "Milan", Country = "Italy", ChampionsLeagueWins = 7 },
                new Team { Name = "Bayern Munich", Country = "Germany", ChampionsLeagueWins = 6 },
                new Team { Name = "Liverpool", Country = "England", ChampionsLeagueWins = 6 },
                new Team { Name = "Barcelona", Country = "Spain", ChampionsLeagueWins = 5 },
                new Team { Name = "Ajax", Country = "Netherland", ChampionsLeagueWins = 4 },
                new Team { Name = "Inter Milan", Country = "Italy", ChampionsLeagueWins = 3 },
                new Team { Name = "Manchester United", Country = "England", ChampionsLeagueWins = 3 },
                new Team { Name = "Juventus", Country = "Italy", ChampionsLeagueWins = 2 },
                new Team { Name = "Benfica", Country = "Portugal", ChampionsLeagueWins = 2 }
            };

            context.Teams.AddRange(teams);
            context.SaveChanges();
        }

        private static void SeedTitles(ApplicationDbContext context)
        {
            if (!context.Titles.Any())
            {
                var teams = context.Teams.ToList();

                context.Titles.AddRange(new[]
                {
                    new Title { Year = 1956, TeamId = teams.First(t => t.Name == "Real Madrid").Id },
                    new Title { Year = 1963, TeamId = teams.First(t => t.Name == "Milan").Id },
                    new Title { Year = 1974, TeamId = teams.First(t => t.Name == "Bayern Munich").Id },
                    new Title { Year = 1977, TeamId = teams.First(t => t.Name == "Liverpool").Id },
                    new Title { Year = 1992, TeamId = teams.First(t => t.Name == "Barcelona").Id },
                    new Title { Year = 1995, TeamId = teams.First(t => t.Name == "Ajax").Id },
                    new Title { Year = 1964, TeamId = teams.First(t => t.Name == "Inter Milan").Id },
                    new Title { Year = 1968, TeamId = teams.First(t => t.Name == "Manchester United").Id },
                    new Title { Year = 1985, TeamId = teams.First(t => t.Name == "Juventus").Id },
                    new Title { Year = 1961, TeamId = teams.First(t => t.Name == "Benfica").Id },
                });

                context.SaveChanges();
            }
        }

        public static void SeedCoaches(ApplicationDbContext context)
        {
            var teams = context.Teams.ToList();

            if (!context.Coaches.Any())
            {
                context.Coaches.AddRange(new[]
                {
            new Coach { Name = "Zinedine Zidane", Experience = 3, Team = teams.FirstOrDefault(t => t.Name == "Real Madrid"),},
            new Coach { Name = "Carlo Ancelotti", Experience = 8, Team = teams.FirstOrDefault(t => t.Name == "Milan"),},
            new Coach { Name = "Hansi Flick", Experience = 3, Team = teams.FirstOrDefault(t => t.Name == "Bayern Munich"),},
            new Coach { Name = "Jurgen Klopp", Experience = 9, Team = teams.FirstOrDefault(t => t.Name == "Liverpool"),},
            new Coach { Name = "Pep Guardiola", Experience = 4, Team = teams.FirstOrDefault(t => t.Name == "Barcelona"),},
            new Coach { Name = "Erik ten Hag", Experience = 5, Team = teams.FirstOrDefault(t => t.Name == "Ajax"), },
            new Coach { Name = "Jose Mourinho", Experience = 2, Team = teams.FirstOrDefault(t => t.Name == "Inter Milan"), },
            new Coach { Name = "Alex Ferguson", Experience = 27, Team = teams.FirstOrDefault(t => t.Name == "Manchester United"), },
            new Coach { Name = "Antonio Conte", Experience = 3, Team = teams.FirstOrDefault(t => t.Name == "Juventus"), },
            new Coach { Name = "Sven-Goran Eriksson", Experience = 2, Team = teams.FirstOrDefault(t => t.Name == "Benfica"),}
                 });

                context.SaveChanges();
            }
        }

        public static void SeedPlayers(ApplicationDbContext context)
        {
            var teams = context.Teams.ToList();

            if (!context.Players.Any()) 
            {
                context.Players.AddRange(new[]
                {
            new Player { Name = "Cristiano Ronaldo", Position = "Forward", Goals = 311, Team = teams.FirstOrDefault(t => t.Name == "Real Madrid"),},
            new Player { Name = "Kaka", Position = "Midfielder", Goals = 77, Team = teams.FirstOrDefault(t => t.Name == "Milan"),},
            new Player { Name = "Oliver Kahn", Position = "Goalkeeper", Goals = 0, Team = teams.FirstOrDefault(t => t.Name == "Bayern Munich"),},
            new Player { Name = "Steven Gerrard", Position = "Midfielder", Goals = 120, Team = teams.FirstOrDefault(t => t.Name == "Liverpool"),},
            new Player { Name = "Lionel Messi", Position = "Forward", Goals = 474, Team = teams.FirstOrDefault(t => t.Name == "Barcelona"),},
            new Player { Name = "Johan Cruyff", Position = "Forward", Goals = 207, Team = teams.FirstOrDefault(t => t.Name == "Ajax"),},
            new Player { Name = "Ronaldo Nazario", Position = "Striker", Goals = 49, Team = teams.FirstOrDefault(t => t.Name == "Inter Milan"),},
            new Player { Name = "Wayne Rooney", Position = "Forward", Goals = 183, Team = teams.FirstOrDefault(t => t.Name == "Manchester United"),},
            new Player { Name = "Alessandro Del Piero", Position = "Forward", Goals = 208, Team = teams.FirstOrDefault(t => t.Name == "Juventus"),},
            new Player { Name = "Euzebio", Position = "Striker", Goals = 317, Team = teams.FirstOrDefault(t => t.Name == "Benfica"),}
        });

                context.SaveChanges();
            }
        }

        public static void SeedStadiums(ApplicationDbContext context)
        {
            var teams = context.Teams.ToList();

            if (!context.Stadiums.Any()) 
            {
                context.Stadiums.AddRange(new[]
                {
            new Stadium { Name = "Santiago Bernabeu", Capacity = 78297, Location = "Madrid, Spain", Team = teams.FirstOrDefault(t => t.Name == "Real Madrid") },
            new Stadium { Name = "San Siro", Capacity = 75817, Location = "Milan, Italy", Team = teams.FirstOrDefault(t => t.Name == "Milan") },
            new Stadium { Name = "Allianz Arena", Capacity = 75024, Location = "Munich, Germany", Team = teams.FirstOrDefault(t => t.Name == "Bayern Munich") },
            new Stadium { Name = "Anfield", Capacity = 61276, Location = "Liverpool, England", Team = teams.FirstOrDefault(t => t.Name == "Liverpool") },
            new Stadium { Name = "Camp Nou", Capacity = 105000, Location = "Barcelona, Spain", Team = teams.FirstOrDefault(t => t.Name == "Barcelona") },
            new Stadium { Name = "Johan Cruyff Arena", Capacity = 55865, Location = "Amsterdam, Netherlands", Team = teams.FirstOrDefault(t => t.Name == "Ajax") },
            new Stadium { Name = "San Siro", Capacity = 75817, Location = "Milan, Italy", Team = teams.FirstOrDefault(t => t.Name == "Inter Milan") },
            new Stadium { Name = "Old Trafford", Capacity = 74310, Location = "Manchester, England", Team = teams.FirstOrDefault(t => t.Name == "Manchester United") },
            new Stadium { Name = "Juventus Stadium", Capacity = 41507, Location = "Turin, Italy", Team = teams.FirstOrDefault(t => t.Name == "Juventus") },
            new Stadium { Name = "Estadio de Luz", Capacity = 65592, Location = "Lisbon, Portugal", Team = teams.FirstOrDefault(t => t.Name == "Benfica") }
                 });

                context.SaveChanges();
            }
        }
    }
}
