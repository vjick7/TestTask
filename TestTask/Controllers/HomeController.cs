using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTask.Models;
using TestTask.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestTask.Controllers
{
    /// <summary>
    /// 
    ///«Здравствуйте, Дмитрий Васильевич,
    ///
    ///Для первоначальной оценки компетентности Прошу Вас выполнить тестовое задание:
    ///Сделать web api для взаимодействия с базой данных, в которой хранятся данные о видеоиграх, реализовать CRUD операции с ней, а также метод для получения списка игр определённого жанра.
    ///Информация о игре: название, студия разработчик, несколько жанров, которым соответствует игра.
    ///Используя.NET core, entity framework.
    ///Действуя согласно SOLID MVC MVVM.
    ///Сделать минимум 3 слоя абстракций, а контроллеры "тонкими".
    /// </summary>
    public class HomeController : Controller
    {
        IGameRepository repository;
        public HomeController(IGameRepository repo) => repository = repo;

        public IActionResult Index()
        {
            return View(
                repository
                .Games
                .Include(s => s.Studio)
                );
        }

        [HttpGet]
        public ViewResult Details(int Id)
        {
            var viewModel = new Models.ViewModels.GameDetailsViewModel(repository,Id);

            return View(viewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            var viewModel = new Models.ViewModels.GameStudioViewModel(repository);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Game game, int[] GenreIds)
        {
            if (ModelState.IsValid)
            {
                repository.SaveChanges(game);
                repository.GenreAssign(game.Id, GenreIds);
                TempData["Success"] = "Row was added!";
                return RedirectToAction("Index");
            }
            else
            {
                ///Прописать корректную передачу при ошибке валиации
                var viewModel = new Models.ViewModels.GameStudioViewModel(repository, GenreIds);
                return View(viewModel);
            }
        }

        [HttpGet]
        public ViewResult Edit(int Id)
        {
            var viewModel = new Models.ViewModels.GameStudioViewModel(Id, repository);
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(Game game, int[] GenreIds)
        {
            if (ModelState.IsValid)
            {
                repository.SaveChanges(game);
                repository.GenreAssign(game.Id, GenreIds);
                TempData["Success"] = "Игра обновлена";
                return RedirectToAction("Index");
            }
            else
            {
                ///Прописать корректную передачу при ошибке валиации
                var viewModel = new Models.ViewModels.GameStudioViewModel(game.Id, repository,GenreIds);
                return View(viewModel);
            }
        }

        public IActionResult Delete(int id)
        {
            repository.Remove(id);
            TempData["Success"] = "Игра успешно удалена";
            return RedirectToAction("Index");
        }
    }
}
