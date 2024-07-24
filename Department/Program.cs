using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Department.Data;
using Department.Services;
using MySqlConnector;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DepartmentContext");
builder.Services.AddDbContext<DepartmentContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddTransient<EmailService>(provider => new EmailService(
        "Email:SmtpServer",
        int.Parse("10"),
        "Email:SmtpUser",
        "Email:SmtpPass"
    ));
//Add services to the container.
builder.Services.AddHostedService<ReminderEmailService>();

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
