using Microsoft.AspNetCore.Identity;
using ProSpace.DataAcsess.Entites.Supply;
using ProSpace.DataAcsess.Entites.Users;
using ProSpace.Domain.Interfaces.Repositories;
using ProSpace.Domain.Models;

namespace ProSpace.DataAcsess
{
    /// <summary>
    ///  Creates sample data
    /// </summary>
    public static class SeedData
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IUnitOfWork unitOfWork)
        {
            if (roleManager.Roles.Any())
                return;

            var roles = new AppRole[]
            {
                new() { Name = "Manager" },
                new() { Name = "Customer" }
            };

            foreach (var role in roles)
                await roleManager.CreateAsync(role);

            var manager = new AppUser
            {
                Email = "manager@example.com",
                UserName = "Manager",
                EmailConfirmed = true,
                Customer = new()
                {
                    Code = "0000-2025",
                    Name = "Manager"
                }
            };

            var johnDoe = new AppUser
            {
                Email = "johndoe@example.com",
                UserName = "JohnDoe",
                EmailConfirmed = true,
                Customer = new CustomerEntity
                {
                    Code = "0001-2025",
                    Name = "John Doe"
                }
            };

            var janeDoe = new AppUser
            {
                Email = "janedoe@example.com",
                UserName = "JaneDoe",
                EmailConfirmed = true,
                Customer = new CustomerEntity
                {
                    Code = "0002-2025",
                    Name = "Jane Doe"
                }
            };

            await userManager.CreateAsync(manager, "!P@$$w0rd1");
            await userManager.AddToRolesAsync(manager, ["Manager"]);

            await userManager.CreateAsync(johnDoe, "?KA{vrejW3^0=1Trrq");
            await userManager.AddToRolesAsync(johnDoe, ["Customer"]);

            await userManager.CreateAsync(janeDoe, "?KA{vrejW3^0=1Trrq");
            await userManager.AddToRolesAsync(janeDoe, ["Customer"]);


            await unitOfWork.CompleteAsync();
        }

        public static async Task SeedItemsAsync(IUnitOfWork unitOfWork)
        {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var items = new List<ItemEntity>();

            for (var i = 0; i < 10; i++)
            {
                var symbol = alphabet[i];

                var item = ItemModel.Create(Guid.NewGuid(), code: $"{i}{i}-{i}{i}{i}{i}-{symbol}{symbol}{i}{i}",name: $"Item {i}" ,price: i + 1, category: $"Category {i / 5}");

                await unitOfWork.ItemsRepository.CreateAsync(item);
            }

            await unitOfWork.CompleteAsync();
        }
    }
}
