using Microsoft.Data.SqlClient;
using System.Data;
using Generic.DAL.Commands.Implementation;
using Generic.DAL.Commands.Interface;
using Generic.DAL.Repositories.Implementation;
using Generic.DAL.Repositories.Interface;

var builder = WebApplication.CreateBuilder(args);


var con = "data source=172.31.31.2;initial catalog=fbo_Test;persist security info=True;user id=eCertUser;password=Mistral!@3;;MultipleActiveResultSets=FALSE;TrustServerCertificate=True;";
// Add services to the container.
builder.Services.AddControllers(o => { o.AllowEmptyInputInBodyModelBinding = true; });
builder.Services.AddSingleton<IDbConnection>(_ => new SqlConnection(con));
builder.Services.AddSingleton<IUtilCommands, UtilSqlCommands>();
builder.Services.AddScoped<IMethodsRepository, MethodsRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
