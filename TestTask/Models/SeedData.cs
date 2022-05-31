using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Infrastructure;

namespace TestTask.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if(!context.Studios.Any())
                {
                    context.Studios.AddRange(
                        new Studio { Title= "VironIT" },
                        new Studio { Title = "Playgendary" },
                        new Studio { Title = "Blizard" },
                        new Studio { Title = "HeroCraft" },
                        new Studio { Title = "Playkot" }
                        );
                    context.SaveChanges();
                }

                if(!context.Genres.Any())
                {
                    context.Genres.AddRange(
                        new Genre { Title = "Action"},
                        new Genre { Title = "RPG"},
                        new Genre { Title = "Strategy" },
                        new Genre { Title = "Adventure" }
                        );
                    context.SaveChanges();
                }

                if (!context.Games.Any())
                {
                    context.Games.AddRange(
                        new Game { Title = "Duna", StudioID = 1 },
                        new Game { Title = "Doom", StudioID = 2 },
                        new Game { Title = "CS", StudioID = 1 },
                        new Game { Title = "Quake", StudioID = 3 }
                        );
                    context.SaveChanges();
                }

                if(!context.GameGenres.Any())
                {
                    context.GameGenres.AddRange(
                        new GameGenres { GameId = 1, GenreId = 1},
                        new GameGenres { GameId = 1, GenreId = 2 },
                        new GameGenres { GameId = 1, GenreId = 3 },
                        new GameGenres { GameId = 2, GenreId = 1 },
                        new GameGenres { GameId = 3, GenreId = 1 },
                        new GameGenres { GameId = 4, GenreId = 1 },
                        new GameGenres { GameId = 4, GenreId = 4 }
                        );
                    context.SaveChanges();
                }

            }
        }
    }
}
