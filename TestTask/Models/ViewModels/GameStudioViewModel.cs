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
        public IQueryable<CheckBoxModel> Genres { get; set; }

        public GameStudioViewModel(IGameRepository repository)
        {
            Game = new Game();
            StudioSelectList = new SelectList(repository.Studios, "Id", "Title");
            Genres = repository.Genres.Select(x => new CheckBoxModel { Id = x.Id, Name = x.Title, Checked = false });
        }

        public GameStudioViewModel(int gameID, IGameRepository repository)
        {
            Game = repository.Games.FirstOrDefault(x => x.Id == gameID);
            StudioSelectList = new SelectList(repository.Studios, "Id", "Title",Game.StudioID);
            Genres = repository
                .Genres
                .Select(x => new CheckBoxModel
                {
                    Id = x.Id,
                    Name = x.Title,
                    Checked = repository
                    .Genres
                    .Where(g => g.Games.All(tag =>tag.GameId == gameID && tag.GenreId == x.Id))
                    .Count() == 0 ? false:true

                });
        }

        private void SetChecked(CheckBoxModel check, int[] ids)
        {
            foreach (int id in ids)
            {
                if (check.Id == id)
                {
                    check.Checked = true;
                    break;
                }
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
