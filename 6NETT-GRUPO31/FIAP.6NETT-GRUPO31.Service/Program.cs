using FIAP._6NETT_GRUPO31.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices();
builder.Services.AddApConfiguration(builder.Configuration);

var app = builder.Build();

app.UseApiConfiguration(app.Environment);

app.Run();

public partial class Program { }