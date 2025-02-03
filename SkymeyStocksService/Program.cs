using SkymeyLibs;
using SkymeyLibs.Data;
using SkymeyLibs.Interfaces.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.SetBasePath(builder.Configuration.GetSection("ConfigPath").Value)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Services.Configure<MainSettings>(builder.Configuration.GetSection("MainSettings"));
builder.Services.AddScoped<IMongoRepository, MongoPostRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
