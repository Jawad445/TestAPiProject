using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDev.Models
{
    public class DefaultDbContextInitializer : IDefaultDbContextInitializer
    {
        private readonly DefaultDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DefaultDbContextInitializer(DefaultDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }

        public bool EnsureCreated()
        {
            return _context.Database.EnsureCreated();
        }

        public async Task Seed()
        {
            var email = "admin@test.com";
            if (await _userManager.FindByEmailAsync(email) == null)
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    GivenName = "Jawad Hassan"
                };

                await _userManager.CreateAsync(user, "P@ssw0rd!");
            }

            if (_context.Students.Any())
            {
                foreach (var u in _context.Students)
                {
                    _context.Remove(u);
                }
            }

            _context.Students.Add(new Student() { LastName = "Hassan", FirstName = "Jawad", Phone = "03218839010", Email = "test@test.com" });
            _context.Students.Add(new Student() { LastName = "Haziq", FirstName = "Muhammad", Phone = "03217892456", Email = "test2@test.com" });
            _context.SaveChanges();
        }
    }

    public interface IDefaultDbContextInitializer
    {
        bool EnsureCreated();
        Task Seed();
    }
}