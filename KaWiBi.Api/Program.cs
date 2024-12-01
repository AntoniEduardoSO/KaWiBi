using KaWiBi.Api;
using KaWiBi.Api.Common.Api;
using KaWiBi.Api.Common.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentarion();
builder.AddServices();

var app = builder.Build();

if(app.Environment.IsDevelopment())
  app.ConfigureDevEnvironment();

app.UseCors(ApiConfiguration.CorsPolicyName);
app.UseSecurity();
app.MapEndpoints();

app.Run();