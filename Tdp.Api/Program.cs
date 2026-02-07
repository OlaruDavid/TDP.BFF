using Tdp.Application;
using Tdp.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplicationDependencies();

builder.Services.AddPersistanceDependencies();


builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", p =>
        p.AllowAnyHeader()
         .AllowAnyMethod()
         .AllowAnyOrigin()
    );
});

var app = builder.Build();

app.UseCors("frontend");

app.UseAuthorization();
app.MapControllers();

app.Run();
