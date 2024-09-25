using ChatClient.Service;
using ChatClient.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//htmlbuilderfactory for api
var apiSettings = new ApiSettings();
builder.Configuration.Bind(nameof(ApiSettings), apiSettings);
//Console.WriteLine(apiSettings.BaseAddress);
//builder.Configuration.GetSection(nameof(ApiSettings)).Bind(apiSettings);
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
