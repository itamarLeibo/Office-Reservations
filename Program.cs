using InterviewAssignment.Data;
using InterviewAssignment.Interfaces;
using InterviewAssignment.Models;
using InterviewAssignment.Repositories;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

var db = new CsvDataTable();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<CsvDataTable>();
builder.Services.AddScoped<DataTable>();
builder.Services.AddScoped<OfficeReservation>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*",
                                              "http://www.contoso.com");
                      });
});

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions()
{
    OnPrepareResponse = (context) =>
    {
        // Disable caching of all static files.
        context.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
        context.Context.Response.Headers["Pragma"] = "no-cache";
        context.Context.Response.Headers["Expires"] = "-1";
    }
});
app.UseDefaultFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
