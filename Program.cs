using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GameWORLD.Data;
using GameWORLD.Repositories;
using GameWORLD.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using GameWORLD.Services.Interfaces;
using GameWORLD.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GameWORLDContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GameWORLDContext") ?? throw new InvalidOperationException("Connection string 'GameWORLDContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "89706967919-cmpqmd6p90afou2vr0si56ghjdl1igpb.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                })
                .AddFacebook(options =>
                {
                    options.ClientId = "6500528903333087";
                    options.ClientSecret = "a482be45d1b9e1e57d1d86b37466794f";
                });

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<GameWORLDContext>(); builder.Services.AddDbContext<GameWORLDContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GameWORLDDb")));

builder.Services.AddScoped<IGameWorldRepository, GameWorldRepository>();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

builder.Services.AddScoped<IUserEmailStore<IdentityUser>, UserStore<IdentityUser, IdentityRole, GameWORLDContext>>();

builder.Services.AddScoped<IUserIdentityService, UserIdendityService>();


builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 6;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;

    // User Settings
    options.User.RequireUniqueEmail = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
