//=========Program.cs=========//

using LoginUsingMiddlewares.CustomMiddlewares;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseLogin();

app.Run();
