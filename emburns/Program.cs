using emburns.Models;
using emburns.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<mokyuContext>(optionsBuilder =>
{
    string connectionString = builder.Configuration.GetConnectionString("mokyuContext");

    optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0)));
});

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    //https://stackoverflow.com/questions/74246482/system-notsupportedexception-serialization-and-deserialization-of-system-dateo

    opt.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Potato Server",
        Description = "Servicio API para emburns",
        TermsOfService = new Uri("https://emburns.fabi.pw/terms"),        
    });
});

var app = builder.Build();

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
