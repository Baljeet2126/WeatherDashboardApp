using WeatherApp.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather Dashboard v1");
       // c.RoutePrefix = string.Empty;
    });
}
app.UseCors("AllowFrontend");
app.MapControllers();
app.Run();
public partial class Program { }
