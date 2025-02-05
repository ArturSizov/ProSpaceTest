using Microsoft.AspNetCore.Identity;
using ProSpace.Infrastructure.Entites.Supply;
using ProSpace.Infrastructure.Entites.Users;
using ProSpace.Domain.Interfaces.Repositories;
using ProSpace.Domain.Models;

namespace ProSpace.Infrastructure
{
    /// <summary>
    ///  Creates sample data
    /// </summary>
    public static class SeedData
    {
        private static Random _random = new();

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
            var items = await unitOfWork.ItemsRepository.ReadAllAsync();

            if (items == null || items.Count() > 20)
                return;

            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            for (var i = 0; i < 5; i++)
            {
                var symbol = alphabet[i];

                var item = ItemModel.Create(
                    id: Guid.NewGuid(), 
                    code: $"{i}{i}-{i}{i}{i}{i}-{symbol}{symbol}{i}{i}",
                    name: $"Item {i}",
                    price: i + 1, 
                    category: $"Category {i / 5}");

                await unitOfWork.ItemsRepository.CreateAsync(item);
            }

             await unitOfWork.CompleteAsync();
        }

        public static async Task SeedCustomersAsync(IUnitOfWork unitOfWork)
        {
            var customers = await unitOfWork.CustomersRepository.ReadAllAsync();

            if (customers == null || customers.Count() > 20)
                return;

            for (int i = 0; i < _random.Next(1, 5); i++)
            {
                var customer = CustomerModel.Create(
                    id: Guid.NewGuid(),
                    name: $"Customer {i}",
                    code: DateTime.Now.ToString("ddMM-yyyy"),
                    address: $"РТ, Казань, ул.Центральная д.{i}",
                    discount: _random.Next(1, 10) * i);

                await unitOfWork.CustomersRepository.CreateAsync(customer);
            }
        }

        public static async Task SeedOrdersAsync(IUnitOfWork unitOfWork)
        {
            var customers = await unitOfWork.CustomersRepository.ReadAllAsync();

            if (customers == null || customers.Count() > 10)
                return;

            foreach (var customer in customers)
            {
                for (int i = 0; i < _random.Next(1, 5); i++)
                {
                    var order = OrderModel.Create(
                        id: Guid.NewGuid(),
                        customerId: customer.Id,
                        DateOnly.FromDateTime(DateTime.Now),
                        DateOnly.FromDateTime(DateTime.Now.AddDays(_random.Next(1, 6))),
                        orderNumber: i,
                        status: "New");

                    await unitOfWork.OrdersRepository.CreateAsync(order);
                }
            }
        }

        public static async Task SeedOrderItemsAsync(IUnitOfWork unitOfWork)
        {
            var orders = await unitOfWork.OrdersRepository.ReadAllAsync();
            var items = await unitOfWork.ItemsRepository.ReadAllAsync();

            if (orders == null || orders.Count() > 50)
                return;

            if (items == null || items.Count() > 50)
                return;

            for (int i = 0; i < 12; i++)
            {
                var orderItem = OrderItemModel.Create(
                   id: Guid.NewGuid(),
                   orderId: orders.OrderBy(s => _random.NextDouble()).First().Id,
                   itemId: items.OrderBy(s => _random.NextDouble()).First().Id,
                   itemsCount: _random.Next(1, 100),
                   itemPrice: _random.Next(1, 200));

                await unitOfWork.OrderItemsRepository.CreateAsync(orderItem);
            }
        }
    }
}
