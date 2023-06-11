using CompeteNow.Data;
using CompeteNow.Services;
using Microsoft.EntityFrameworkCore;

namespace CompeteNow.Tests
{
    public class CompetitionServiceTests
    {
        private readonly AppDbContext db;
        private readonly CompetitionService svc;

        public CompetitionServiceTests()
        {
            var opt = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "CompeteNow")
                .Options;
            db = new AppDbContext(opt);
            svc = new CompetitionService(db);
        }

        [Fact]
        public async Task CreateCompetitionAsync_creates_competition()
        {
            //Arrange
            //préparer les données
            var sportId = 1;
            var name = "test";
            var startDate = DateTime.Now.AddDays(2);
            //Act
            //executer la méthode testée
            var result = await svc.CreateCompetitionAsync(name, sportId, startDate);
            var competition = db.Competitions.FirstOrDefault();
            //Assert
            //valider le résultat de la méthode testée
            Assert.True(result > 0);
            Assert.NotNull(competition);
            Assert.Equal(name, competition.Name);
            Assert.Equal(startDate, competition.Date);
            Assert.Equal(sportId, competition.SportId);
        }

        [Fact]
        public void CreateCompetitionDataIsValid_validates_data()
        {

            //Arrange
            //préparer les données
            var sportId = 1;
            var name = "test";
            var startDate = DateTime.Now.AddDays(2);
            //Act
            //executer la méthode testée
            var result = svc.CreateCompetitionDataIsValid(name, sportId, startDate);
            //Assert
            //valider le résultat de la méthode testée
            Assert.True(result);
        }

        [Theory]
        [InlineData(0, "test", "2020-01-01")]
        [InlineData(-1, "test", "2024-01-01")]
        [InlineData(-1, "", "2024-01-01")]
        [InlineData(1, "test", "2020-01-01")]
        [InlineData(99, "test", "2020-01-01")]
        [InlineData(99, "", "2020-01-01")]
        public void CreateCompetitionDataIsValid_detectes_errors(int sportId, string name, string date)
        {
            //Arrange
            //préparer les données
            var startDate = DateTime.Parse(date);
            //Act
            //executer la méthode testée
            var result = svc.CreateCompetitionDataIsValid(name, sportId, startDate);
            //Assert
            //valider le résultat de la méthode testée
            Assert.False(result);
        }
    }
}