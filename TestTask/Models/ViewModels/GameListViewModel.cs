using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Repositories.Interfaces;

namespace TestTask.Models.ViewModels
{
    public class GameListViewModel
    {
        public IQueryable<Game> Games { get; set; }
        public int SelectedGenreId { get; set; }

        public GameListViewModel(IGameRepository repository, int genreId)
        {
            Games = repository
                .Games
                .Include(s => s.Studio).Where(g => genreId == 0 || g.Genres.Where(tag => tag.GenreId == genreId).Any());
            SelectedGenreId = genreId;

        }
    }
}
