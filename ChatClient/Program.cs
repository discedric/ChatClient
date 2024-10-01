using ChatClient.Core;
using ChatClient.Service;
using ChatClient.Settings;
using Microsoft.Azure.SignalR;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// HttpClientFactory for API
var apiSettings = new ApiSettings();
builder.Configuration.Bind(nameof(ApiSettings), apiSettings);
builder.Services.AddHttpClient("TalkApi", options =>
{
    options.BaseAddress = new Uri(apiSettings.BaseAddress);
});

// Add services to the container.
builder.Services.AddScoped<TalkApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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