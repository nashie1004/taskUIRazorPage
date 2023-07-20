using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using taskui.Contexts;
using taskui.Services;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2V1hhQlJNfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5Ud0xhWXtbdHxQQGNe\r\n");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorPages();

#if DEBUG
var connectionString = "DefaultConnection";
#else
var connectionString = "ProductionConnection";
#endif

builder.Services.AddDbContext<ReleaseNoteContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(connectionString),
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
    });
});

builder.Services.AddScoped<IDataRepository, DataRepository>();
builder.Services.AddScoped<IDataAccess, DataAccess>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
