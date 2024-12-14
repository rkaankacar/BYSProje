using BYSProje.Services;
using BYSProje.Models; 
using BYSProje.Repositorys;
using Microsoft.EntityFrameworkCore;
using BYSProje.DBContext.Entity;

var builder = WebApplication.CreateBuilder(args);

 builder.Services.AddDbContext<BYSContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBYSRepository<Students>, BYSRepository<Students>>();
builder.Services.AddScoped<IBYSService<Students>, BYSService<Students>>();

builder.Services.AddScoped<IBYSRepository<Instructors>, BYSRepository<Instructors>>();
builder.Services.AddScoped<IBYSService<Instructors>, BYSService<Instructors>>();

builder.Services.AddScoped<IBYSService<Student_Courses>, BYSService<Student_Courses>>();
builder.Services.AddScoped<IBYSRepository<Student_Courses>, BYSRepository<Student_Courses>>();

builder.Services.AddScoped<IBYSRepository<Courses>, BYSRepository<Courses>>();
builder.Services.AddScoped<IBYSService<Courses>, BYSService<Courses>>();

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
