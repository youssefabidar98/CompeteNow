using CompeteNow.Models;
using CompeteNow.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CompeteNow.Controllers
{
    public class CompetitionController : Controller
    {
        private readonly ICompetitionService competitionService;
        public CompetitionController(ICompetitionService competitionService)
        {
            this.competitionService = competitionService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<CompetitionListViewModel> result =
                await competitionService.GetNextCompetitionsAsync();
            return View(result);
        }

        //GET
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create() 
        {
            var model = new CreateCompetitionViewModel()
            { 
                Date = DateTime.Now,
                Sports = await competitionService.GetSportsAsync(),
            };

            return View(model);
        }

        //POST
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create(string name, DateTime date, int sport)
        {
            try
            {
                //creation de la compétition
                await competitionService.CreateCompetitionAsync(name, sport, date);

                //redirection vers la liste des compétitions
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                //méthode erreur 400
                //return BadRequest(e.Message);

                //méthode validation du formulaire
                ModelState.AddModelError("", e.Message);
                var model = new CreateCompetitionViewModel()
                {
                    Name = name,
                    Date = date,
                    Sports = await competitionService.GetSportsAsync(),
                };
                return View();
            }
        }

        [HttpGet("{id:int}/CreateEvent")]
        public async Task<IActionResult> CreateEvent(int id)
        {
            var model = new AddCompetitionEventViewModel()
            {
                CompetitionId = default,
                CompetitionEvents = default,
                CompetitionName = default,
                Genres = default
            };

            return View(model);
        }
    }
}
