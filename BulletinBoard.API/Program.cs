using BulletinBoard.API.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
var app = builder.Build();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

var appController = new AppController();
app.Run(async (ctx) => await appController.Run(ctx));

app.Run();
