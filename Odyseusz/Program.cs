using Hangfire;
using Microsoft.EntityFrameworkCore;
using Odyseusz.bll;
using Odyseusz.bll.Services.Implementations;
using Odyseusz.bll.Services.Interfaces;
using Odyseusz.dal;
using Odyseusz.dal.Repositories;
using Odyseusz.dal.Repositories.Implementations;
using Odyseusz.dal.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<DatabaseLayoutFilter>();
});

builder.Services.AddDbContext<OdyseuszDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
builder.Services.AddScoped<DatabaseLayoutFilter>();
builder.Services.AddScoped<IWarningRepository, WarningRepository>();
builder.Services.AddScoped<WarningService>();

// Dodanie Hangfire i konfiguracja z SQL Server
builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();


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

// U¿ycie dashboard Hangfire (opcjonalne)
app.UseHangfireDashboard();

// Przyk³adowa konfiguracja zadania cyklicznego
var serviceProvider = app.Services.CreateScope().ServiceProvider;
var warningService = serviceProvider.GetRequiredService<WarningService>();
string warningsFilePath = "..\\Odyseusz.dal\\warnings.xml";
BackgroundJob.Enqueue(() => warningService.UpdateWarningsFromXml(warningsFilePath));

// Mo¿na równie¿ dodaæ zadanie cykliczne, np. codziennie
RecurringJob.AddOrUpdate(
    "UpdateWarnings",
    () => warningService.UpdateWarningsFromXml(warningsFilePath),
    Cron.Daily);
    //Cron.MinuteInterval(5));
    //"*/1 * * * *");

app.Run();
