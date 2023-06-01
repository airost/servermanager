using HotelManager.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelManager.Data;

public class SeedData {
    public static async Task Init(
        IServiceProvider serviceProvider,
        string password = "zaq1@WSX"
    ) {
        await using (var context = new ApplicationDbContext(
                   serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>())
              ) {
            await EnsureUser(serviceProvider, password, "user@example.com");
            
            var manager = await EnsureUser(serviceProvider, password, "manager@example.com");
            await EnsureRole(serviceProvider, manager, ReservationConstants.ReservationManagerRole);
            await EnsureRole(serviceProvider, manager, HotelConstants.HotelManagerRole);

            var administrator = await EnsureUser(serviceProvider, password, "admin@example.com");
            await EnsureRole(serviceProvider, administrator, ReservationConstants.ReservationAdminRole);
            await EnsureRole(serviceProvider, administrator, HotelConstants.HotelAdminRole);
        }
    }

    private static async Task<string> EnsureUser(
        IServiceProvider serviceProvider,
        string initPw, string username) {
        var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

        var user = await userManager.FindByNameAsync(username);

        if (user == null) {
            user = new IdentityUser {
                UserName = username,
                Email = username,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user, initPw);
        }

        if (user == null) throw new Exception("User did not get created, password policy error?");

        return user.Id;
    }

    private static async Task<IdentityResult> EnsureRole(
        IServiceProvider serviceProvider,
        string uid,
        string role
    ) {
        var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

        IdentityResult res;

        if (await roleManager.RoleExistsAsync(role) == false)
            res = await roleManager.CreateAsync(new IdentityRole(role));

        var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

        var user = await userManager.FindByIdAsync(uid);
        if (user == null) throw new Exception("User does not exists");

        res = await userManager.AddToRoleAsync(user, role);

        return res;
    }
}