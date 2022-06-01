using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class Game
    {
        public int Id { get; set; }
        [Required,Display(Name = "Название"), MaxLength(200, ErrorMessage = "Длина Названия не должна превышать 200 симоволов")]
        public string Title { get; set; }
        public int StudioID { get; set; }
        public Studio Studio { get; set; }
        public ICollection<GameGenres> Genres { get; set; }

        

    }
}
