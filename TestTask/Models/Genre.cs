using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public IQueryable<GameGenres> Games { get; set; } 
    }
}
