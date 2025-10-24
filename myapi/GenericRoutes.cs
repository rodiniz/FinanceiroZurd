using System;
using ContextHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace myapi;

public static class GenericRoutes
{
    public static void AddGenericCrudRoutes<T>(this WebApplication app, string controllerName) where T : class, new()
    {
        var group = app.MapGroup($"/{controllerName}").RequireAuthorization();
        group.MapPost("/", async (
                [FromServices] IRepository<T> service,
                [FromBody] T item) => await service.AddAsync(item))
            .WithOpenApi();

        group.MapGet("/", ([FromServices] IRepository<T> crudService) => crudService.FindAllAsync(e => true));
        group.MapGet("/{id}", async ([FromServices] IRepository<T> crudService, int id) => await crudService.FindAsync(x => EF.Property<int>(x, "Id") == id));
        group.MapPut("/{id}", async ([FromServices] IRepository<T> crudService, int id, [FromBody] T item) => await crudService.UpdateAsync(id, item));
        group.MapDelete("/{id}", async ([FromServices] IRepository<T> crudService, int id) => await crudService.DeleteAsync(x => EF.Property<int>(x, "Id") == id));
    }
}
