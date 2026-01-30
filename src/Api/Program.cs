using Application.Reservations;
using Application.Reservations.CreateReservation;
using Infrastructure.Reservations;
using Application.Reservations.CreateReservation.Command;
using Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Infrastructure.Persistence;
using Infrastructure.Reservations;
using Microsoft.EntityFrameworkCore;
using Application.Reservations;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Application.Reservations.CreateReservation.Command.CreateReservationCommand).Assembly));
// FluentValidation: registra todos los validators del assembly de Application
builder.Services.AddValidatorsFromAssembly(typeof(Application.Reservations.CreateReservation.Command.CreateReservationCommand).Assembly);

// MediatR pipeline: validación antes del handler
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));



//builder.Services.AddSingleton<IReservationRepository, InMemoryReservationRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlite("Data Source=app.db"));

builder.Services.AddScoped<IReservationRepository, EfReservationRepository>();

builder.Services.AddScoped<CreateReservationHandler>();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature =
            context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();

        var ex = exceptionHandlerPathFeature?.Error;

        if (ex is FluentValidation.ValidationException vex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/problem+json";

            var errors = vex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            var problem = new
            {
                type = "https://httpstatuses.com/400",
                title = "Validation failed",
                status = 400,
                errors
            };

            await context.Response.WriteAsJsonAsync(problem);
            return;
        }

        // fallback genérico
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync("Unexpected error.");
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
