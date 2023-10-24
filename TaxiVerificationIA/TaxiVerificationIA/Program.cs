using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaxiVerificationIA.Models;

using TaxiVerificationIA.Services.Contract;
using TaxiVerificationIA.Services.Implementation;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = null; // NOTE: set upload limit to unlimited, or specify the limit in number of bytes
});

builder.Services.Configure<FormOptions>(options => options.ValueLengthLimit = int.MaxValue);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddDbContext<TaxiVerificationAiContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLString"));
});

builder.Services.AddScoped<UserService, UserService>();
builder.Services.AddScoped<AgentService, AgentService>();
builder.Services.AddScoped<TaxiDriverService, TaxiDriverService>();
builder.Services.AddScoped<TaxiService, TaxiService>();
builder.Services.AddScoped<VerificationService, VerificationService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Start/SignIn";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    }
);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(
        new ResponseCacheAttribute
        {
            NoStore = true,
            Location = ResponseCacheLocation.None
        }
        );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Start}/{action=SignIn}/{id?}");

app.Run();
