using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProSpace.Api.Services;
using ProSpace.DataAcsess;
using ProSpace.DataAcsess.Entites.Users;
using ProSpace.DataAcsess.Repositories;
using ProSpace.Domain.Interfaces.Repositories;
using ProSpace.Domain.Interfaces.Services;
using ProSpace.Domain.Services;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ProSpaceDbContext>(
                        options => {  options.UseSqlite(builder.Configuration.GetConnectionString(nameof(ProSpaceDbContext))); })
                        .AddIdentityApiEndpoints<AppUser>()
                        .AddRoles<AppRole>()
                        .AddEntityFrameworkStores<ProSpaceDbContext>();

        builder.Services.AddAuthorization();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;
        });

        // repositories
        builder.Services
            .AddScoped<IItemsRepository, ItemsRepository>()
            .AddScoped<IOrderItemsRepository, OrderItemsRepository>()
            .AddScoped<IOrdersRepository, OrdersRepository>()
            .AddScoped<ICustomersRepository, CustomerRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>();

        // domain services
        builder.Services
            .AddScoped<IItemsService, ItemsService>()
            .AddScoped<IOtderItemsService, OrderItemsService>()
            .AddScoped<IOrderService, OrdersService>()
            .AddScoped<ICustomersService, CustomersService>();

        // background services
        builder.Services.AddHostedService<InitialService>();


        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.MapIdentityApi<AppUser>();

        await app.RunAsync();
    }
}