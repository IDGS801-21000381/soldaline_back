using Microsoft.EntityFrameworkCore;
using soldaline_back.Models;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Obtenemos la cadena de conexion
var connectionString = builder.Configuration.GetConnectionString("cadenaSQL");


//Agregamos la configuracion para SQLSERVER
builder.Services.AddDbContext<SoldalineBdContext>(options => options.UseSqlServer(connectionString));

//Definimos la nueva politica CORS(CROSS-ORIGIN Resource Sharing) para la API
builder.Services.AddCors(options =>
{
	options.AddPolicy("NuevaPolitica", app =>
	{
		app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("NuevaPolitica");
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
