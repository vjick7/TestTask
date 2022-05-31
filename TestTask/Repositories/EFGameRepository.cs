using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Infrastructure;
using TestTask.Models;
using TestTask.Repositories.Interfaces;

namespace TestTask.Repositories
{
    public class EFGameRepository : IGameRepository
    {
        ApplicationDbContext context;
        public EFGameRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        IQueryable<Game> IGameRepository.Games => context.Games;

        IQueryable<Genre> IGameRepository.Genres => context.Genres;

        IQueryable<Studio> IGameRepository.Studios => context.Studios;


        void IGameRepository.Remove(int Id)
        {
            try
            {
                context.Games.Remove(context.Games.Where(x => x.Id == Id).FirstOrDefault());
                context.SaveChanges();
            }
            catch (DataException)
            { }
        }

        void IGameRepository.SaveChanges(Game game)
        {
            if (game.Id == 0)
            {
                game.Studio = context.Studios.Where(x => x.Id == game.StudioID).FirstOrDefault();
                context.Games.Add(game);
                context.SaveChanges();
            }
            else
            {
                var dbEntity = context.Games.Where(x => x.Id == game.Id).FirstOrDefault();
                if (dbEntity == null)
                    throw new KeyNotFoundException("EF: Game not found");
                dbEntity.Title = game.Title;
                dbEntity.StudioID = game.StudioID;
                dbEntity.Studio = context.Studios.Where(studio => studio.Id == game.StudioID).FirstOrDefault();
                context.SaveChanges();
            }
        }

        


        void  IGameRepository.GenreAssign(int gameId, int[] genreIds)
        {
            int [] existGenreIds = context.GameGenres.Where(gg => gg.GameId == gameId).Select(s => s.GenreId).ToArray();
            
            foreach (int genreId in genreIds)
            {
                if (!existGenreIds.Contains(genreId))
                {
                    context.GameGenres.Add(new GameGenres { GameId = gameId, GenreId = genreId });
                    
                }
            }
            foreach(int existGenre in existGenreIds)
            {
                if(!genreIds.Contains(existGenre))
                {
                    context
                        .GameGenres
                        .Remove(context
                                .GameGenres
                                .FirstOrDefault(gg => gg.GameId == gameId && gg.GenreId == existGenre));
                }
            }
            context.SaveChanges();
        }
    }
}
