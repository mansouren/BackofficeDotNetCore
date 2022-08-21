
using Framework;
using Framework.AutoMapper;

using Monitoring.Hubs;
using Monitoring.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.InitializeAutomapper();
builder.Services.AddSignalR();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IStock, Stock>();
//builder.Services.AddHostedService<StockServiceBackgroundCaller>();
builder.Services.AddServices(builder.Configuration);

builder.Services.AddHostedService<TxnRespHistoryBackgroundCaller>();
//builder.Services.AddHostedService<TxnRevHistoryBackgroundCaller>();
//builder.Services.AddHostedService<TxnHistoryByPosBackgroundCaller>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MonitoringHub>("/monitoringHub");
    
    
});
app.Run();
