using Framework;
using Framework.AutoMapper;
using Framework.Middlewares;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Serialization;
using Services.BackgroundServices;
using Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Add Cores
builder.Services.AddCors(c => c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

//Reading from AppSetting.json
var _siteSettings = builder.Configuration.GetSection("SiteSettings").Get<SiteSettings>();
builder.Services.Configure<SiteSettings>(builder.Configuration.GetSection(nameof(SiteSettings)));

//Json Serialization
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
builder.Services.InitializeAutomapper();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// My Application Services
builder.Services.AddServices(builder.Configuration);
builder.Services.AddJwtAuthentication(_siteSettings.JwtSettings);
builder.Services.AddHostedService<CheckPrivilegeHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Photos")),
//    RequestPath = "/Photos"
//});
//app.UseMiddleware<SetConnectionStringMiddleware>();

app.Run();
