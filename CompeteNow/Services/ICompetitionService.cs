using CompeteNow.Models;

namespace CompeteNow.Services
{
    public interface ICompetitionService
    {
        /// <summary>
        /// Création de compétition
        /// </summary>
        /// <param name="sport">Le sport choisi</param>
        /// <param name="startDate">Date de la compétition</param>
        /// <returns>ID de la nouvelle compétition</returns>
        Task<int> CreateCompetitionAsync(string name, int sportId, DateTime startDate);

        Task<IEnumerable<CompetitionListViewModel>> GetNextCompetitionsAsync();

        Task CreateCompetitionEvent(int competitionId, CompetitionEventArguments competitionEventArguments);

        Task <List<(int Id, string Name)>> GetSportsAsync();
    }
}
