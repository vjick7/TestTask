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
        public List<CheckBoxModel> CheckBoxList { get; set; }

        public GameStudioViewModel(IGameRepository repository)
        {
            Game = new Game();
            StudioSelectList = new SelectList(repository.Studios, "Id", "Title");
            CheckBoxList = repository.Genres.Select(x => new CheckBoxModel { Id = x.Id, Name = x.Title, Checked = false }).ToList();
        }

        public GameStudioViewModel(int gameID, IGameRepository repository)
        {
            Game = repository.Games.FirstOrDefault(x => x.Id == gameID);
            StudioSelectList = new SelectList(repository.Studios, "Id", "Title",Game.StudioID);
            //var a1 = repository
            //        .Genres
            //        .Where(g => g.Games.Where(p => p.GameId == gameID && p.GenreId == 2).Any())
            //        .ToList();

            //            var a2 = repository
            //                    .Genres
            //                    .Where(g => g.Games.All(tag => tag.GameId == gameID && tag.GenreId == 2)).ToList();
            //            var a3 = repository
            //                    .Genres
            //                    .Where(g => g.Games.All(tag => tag.GameId == gameID && tag.GenreId == 3)).ToList();
            //var a4 = repository
            //        .Genres
            //        .FromSql(new RawSqlString($@"select Genres.* from GameGenres
            //inner join Genres on GameGenres.GenreId = Genres.Id
            //where gameId = {gameID} and GenreId = 2"))
                    //.ToList();
            CheckBoxList = repository
                .Genres
                .Select(x => new CheckBoxModel
                {
                    Id = x.Id,
                    Name = x.Title,
                    Checked = repository
                    .Genres
                    .Where(g => g.Games.Where(p => p.GameId == gameID && p.GenreId == x.Id).Any())
                    .Count() == 0 ? false:true
                }).ToList();

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
