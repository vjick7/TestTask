using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class GameGenres
    {
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        //public string GenresString()
        //{
        //    if (Genres.Count() == 0)
        //        return "Genre not selected";
        //    StringBuilder str = new StringBuilder();
        //    foreach (GameGenres gameGenre in Genres)
        //    {
        //        str.Append($"{gameGenre.Genre.Title}<br />");
        //    }
        //    return str.ToString();
        //}
    }
}
