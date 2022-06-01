using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Repositories.Interfaces;

namespace TestTask.Models.ViewModels
{
    public class GameDetailsViewModel
    {
        public Game Game { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public GameDetailsViewModel(IGameRepository repository, int id)
        {
            Game = repository
                .Games
                .Include(s => s.Studio).FirstOrDefault(g=>g.Id==id);
            Genres = repository.Genres.Where(genre => genre.Games.Where(g => g.GameId == id).Any()).Select(s=>s.Title);
                
        }
    }
}
