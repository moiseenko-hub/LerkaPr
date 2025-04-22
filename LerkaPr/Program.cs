using System;
using LerkaPr;
using LerkaPr.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ProjectDbContext>(o => o.UseSqlServer(
        ProjectDbContext.CONNECTION_STRING
    ));

builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<RoomRepository>();
builder.Services.AddScoped<ServiceRepository>();
builder.Services.AddScoped<StudentRepository>();

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
    pattern: "{controller=Student}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();
    DbSeeder.Seed(db);
}

app.Run();
