using CacheProxyDesignPattern.Interfaces;
using CacheProxyDesignPattern.Services;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Add MemoryCache
builder.Services.AddMemoryCache();

// Register the real data service
builder.Services.AddSingleton<IDataService, RealDataService>();

// Register the proxy data service
builder.Services.AddSingleton<IDataService>(sp =>
{
    var realDataService = sp.GetRequiredService<RealDataService>();
    var memoryCache = sp.GetRequiredService<IMemoryCache>();
    return new CacheProxyDataService(realDataService, memoryCache);
});

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
