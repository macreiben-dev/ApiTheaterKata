using BestestTheater.WebApp.Models;
using BestTheater.DontTouchMe.Kata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<BookingRepository>();
builder.Services.AddScoped<BusinessLayerServiceFacade>();

// Enable the session on the site
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// =======
DatabaseBootstrapper.OnStart();
// =======

app.Run();
