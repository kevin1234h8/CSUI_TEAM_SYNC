using CSUI_Teams_Sync;
using CSUI_Teams_Sync.Components.Commons;
using CSUI_Teams_Sync.Components.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var appConfig = AppSettings.Build();
var services = builder.Services;
builder.Services.Configure<CfgDatabase>(appConfig.GetSection("Database"));
builder.Services.Configure<LoggerConfig>(appConfig.GetSection("Logger"));
services.Configure<EmailConfig>(appConfig.GetSection("Email"));
services.Configure<ConnectionConfig>(appConfig.GetSection("Connection"));
services.Configure<TeamsGraphAPIConfig>(appConfig.GetSection("TeamsGraphAPI"));
services.AddSingleton<LogManagerCustom>();
services.AddSingleton<CryptographyCore>();
services.AddSingleton<SecureInfo>();
var app = builder.Build();
LogManagerCustom logManager = app.Services.GetRequiredService<LogManagerCustom>();
logManager.InitializeLogger();
var logger = logManager.logger;
logger.Info("application started");
/*LogManager.LoadConfiguration("nlog.config");*//**/
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
