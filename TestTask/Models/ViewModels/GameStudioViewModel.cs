using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Repositories.Interfaces;

namespace TestTask.Models.ViewModels
{
    public class GameStudioViewModel
    {
        public Game Game { get; set; }
        public SelectList StudioSelectList { get; set; }
        public IEnumerable<CheckBoxModel> CheckBoxList { get; set; }

        public GameStudioViewModel(IGameRepository repository, int[] genreIds = null)
        {
            Game = new Game();
            StudioSelectList = new SelectList(repository.Studios, "Id", "Title");
            CheckBoxList = genreIds is null?
                repository.Genres.Select(x => new CheckBoxModel { Id = x.Id, Name = x.Title, Checked = false }):
                repository.Genres.Select(x => new CheckBoxModel
                {
                    Id = x.Id,
                    Name = x.Title,
                    Checked = genreIds.Contains(x.Id) ? true : false
                });

        }

        public GameStudioViewModel(int gameID, IGameRepository repository, int[] genreIds = null)
        {
            Game = repository.Games.FirstOrDefault(x => x.Id == gameID);
            StudioSelectList = new SelectList(repository.Studios, "Id", "Title",Game.StudioID);

            if (genreIds is null)
            {
                CheckBoxList = repository
                    .Genres
                    .Select(x => new CheckBoxModel
                    {
                        Id = x.Id,
                        Name = x.Title,
                        Checked = repository
                        .Genres
                        .Where(g => g.Games.Where(p => p.GameId == gameID && p.GenreId == x.Id).Any())
                        .Count() == 0 ? false : true
                    });
            }
            else
            {
                CheckBoxList = repository.Genres.Select(x => new CheckBoxModel
                {
                    Id = x.Id,
                    Name = x.Title,
                    Checked = genreIds.Contains(x.Id) ? true : false
                });
            }

        }
    }

    public class CheckBoxModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}
