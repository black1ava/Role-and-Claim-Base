using Microsoft.EntityFrameworkCore;
using IdentityAppFirstAttempt.Data;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

String connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddAuthentication("TestingCookie")
    .AddCookie("TestingCookie", options => {
        options.Cookie.Name = "TestingCookie";
        options.LoginPath = "/Account/Signin";
    });

builder.Services.AddAuthorization(options => {
    options.AddPolicy("Admin", policy => {
        policy.RequireClaim(ClaimTypes.Role, "Admin");
    });
    options.AddPolicy("Teacher", policy => {
        policy.RequireClaim(ClaimTypes.Role, "Teacher");
    });
    options.AddPolicy("Student", policy => {
        policy.RequireClaim(ClaimTypes.Role, "Student");
    });
});

// Add services to the container.
builder.Services.AddControllersWithViews();

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

// add authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
