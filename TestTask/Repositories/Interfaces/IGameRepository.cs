using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask.Repositories.Interfaces
{
    public interface IGameRepository
    {
        IQueryable<Game> Games { get; }
        IQueryable<Genre> Genres { get; }
        IQueryable<Studio> Studios { get; }

        void SaveChanges(Game game);
        void GenreAssign(int gameId, int genreId);
        void Remove(int Id);
        //string GenresString(int gameId);
    }
}
