using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Repositories.Interfaces;

namespace TestTask.Components
{
    public class GenresMenuViewComponent: ViewComponent
    {
        private IGameRepository repository;
        public GenresMenuViewComponent(IGameRepository repo) => repository = repo;
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedGenreId = RouteData?.Values["genreId"];
            return View(repository.Genres);
        }
            
    }
}
