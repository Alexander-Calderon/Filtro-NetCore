using System.Reflection;
using API.Extensions;
using API.Helpers.Errors;
using AspNetCoreRateLimit;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();

builder.Logging.AddSerilog(logger);
// Add services to the container.
builder.Services.AddControllers(options => 
{
    options.RespectBrowserAcceptHeader = true;
    options.ReturnHttpNotAcceptable = true;
})/* .AddXmlSerializerFormatters() */;  

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); });
builder.Services.ConfigureCors(); // aplica el cors de serviceExtension
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly()); // aplica automapper
builder.Services.AddJwt(builder.Configuration); //Aplicacion de JWT
builder.Services.ConfigureRateLimiting();
builder.Services.ConfigureApiVersioning();  
builder.Services.AddAplicacionServices(); // para que aplique el archivo de extensions
builder.Services.AddDbContext<DbAppContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("ConexMysql");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using(var scope= app.Services.CreateScope()){
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try{
    var context = services.GetRequiredService<DbAppContext>();
    await context.Database.MigrateAsync();
    }
    catch(Exception ex){
    var _logger = loggerFactory.CreateLogger<Program>();
    _logger.LogError(ex,"Ocurrió un error durante la migración");
    }
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy"); //- le decimos que use el cors "CorsPolicy"
app.UseAuthorization();
app.UseIpRateLimiting();
app.MapControllers();
app.Run();