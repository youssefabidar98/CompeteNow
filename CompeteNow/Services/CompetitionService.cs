using CompeteNow.Data;
using CompeteNow.Data.Models;
using CompeteNow.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CompeteNow.Tests")]
namespace CompeteNow.Services
{
    public class CompetitionService : ICompetitionService
    {
        private readonly AppDbContext _dbContext;
        public CompetitionService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateCompetitionAsync(string name, int sportId, DateTime startDate)
        {
            if (!CreateCompetitionDataIsValid(name, sportId, startDate))
            {
                throw new InvalidDataException("Données invalides");
            }

            var comp = new Competition()
            {
                Name = name,
                SportId = sportId,
                Date = startDate,
            };

            var entry = await _dbContext.AddAsync(comp);
            await _dbContext.SaveChangesAsync();

            return entry.Entity.Id;
        }

        internal bool CreateCompetitionDataIsValid(string name, int sportId, DateTime startDate)
        {
            //valider l'id du sport
            if (sportId <= 0)
            {
                return false;
            }
            if (!_dbContext.Sports.Any(s => s.Id == sportId))
            {
                return false;
            }
            //valider name
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            //valider startDate
            if (startDate < DateTime.Now.AddDays(1))
            {
                return false;
            }

            return true;
        }

        public Task CreateCompetitionEvent(int competitionId, CompetitionEventArguments competitionEventArguments)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CompetitionListViewModel>> GetNextCompetitionsAsync()
        {
            var items = await _dbContext.Competitions
                .Include(c => c.Sport) // include = inner join
                .Where(c => c.Date.Date > DateTime.Now.Date)
                .ToListAsync();

            var results = items.Select(c => new CompetitionListViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                Date = c.Date,
                SportName = c.Sport.SportName
            });

            return results;
        }

        public async Task<List<(int Id, string Name)>> GetSportsAsync()
        {
            var sports = await _dbContext.Sports.ToListAsync();
            var results = sports
                .Select(s => (s.Id, s.SportName)).ToList();

            return results;
        }
    }
}
