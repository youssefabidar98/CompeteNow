using CompeteNow.Data;
using CompeteNow.Data.Models;
using CompeteNow.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static CompeteNow.Infrastructure.Enumerations;

namespace CompeteNow.Services
{
    public class AccountService : IAccountService
    {
        private readonly IHttpContextAccessor accessor;
        private readonly AppDbContext dbContext;

        public AccountService(IHttpContextAccessor accessor, AppDbContext dbContext) 
        {
            this.accessor = accessor;
            this.dbContext = dbContext;
        }

        public async Task LoginAsync(string email, string password, bool rememberMe)
        {
            //Création de cookie avec claim
            // - Trouver le User dans la bdd
            var user = dbContext.Users
                .Include(u => u.Roles)
                .FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                throw new Exception("Not found");
            }
            if(!await Helpers.IsPasswordCorrect(password, user.HashedPassword))
            {
                throw new Exception("Incorrect credientials");
            }

            // - Créer une liste de Claim
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Gender, user.Genre.ToString()),
                new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString("O")),
                new Claim(ClaimTypes.Email, user.Email)
            };
            foreach(var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
            }

            // - Créer un objet ClaimsIdentity
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // - Créer un objet ClaimsPrincipal
            var principal = new ClaimsPrincipal(identity);

            // - Utiliser HttpContext pour le SIgnIn du principal
            await accessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, 
                new AuthenticationProperties()
            {
                IsPersistent = rememberMe
            });
        }

        public async Task SigninAsync(string email, string password, DateTime birthday, string genre)
        {
            UserGenre genreEnum = UserGenre.Male;

            if (genre == "Female")
                genreEnum = UserGenre.Female;

            var newUser = new User
            {
                Email = email,
                HashedPassword = await Helpers.HashPasswordAsync(password),
                BirthDate = birthday,
                Genre = genreEnum
            };

            dbContext.Users.Add(newUser);
            await dbContext.SaveChangesAsync();
        }

        public async Task LogoutAsync()
        {
            if (accessor.HttpContext!.User.Identity!.IsAuthenticated)
            {
                await accessor.HttpContext.SignOutAsync();
            }
        }
    }
}
