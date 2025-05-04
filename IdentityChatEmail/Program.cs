using IdentityChatEmail.Context;
using IdentityChatEmail.Entities;
using IdentityChatEmail.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// DbContext s�n�f�n� ekler. Bu, veritaban� ile ileti�im kuran s�n�f� yap�land�r�r.
builder.Services.AddDbContext<EmailContext>();

// Identity yap�land�rmas�n� ekler. Kullan�c� kimlik do�rulama ve yetkilendirme i�lemleri i�in 
// AppUser s�n�f� ve IdentityRole s�n�f�n� kullanarak, veritaban� i�lemlerini EmailContext �zerinden yapar.
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<EmailContext>()
    .AddErrorDescriber<CustomIdentityValidator>();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
