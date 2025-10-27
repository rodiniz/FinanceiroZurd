using System;
using System.Security.Claims;
using ContextHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myapi.Dtos;

namespace myapi;

public static class GenericRoutes
{
    public static void AddGenericCrudRoutes<D, E,M>(this WebApplication app, string controllerName) 
        where D : class, new()
        where E : class, new()
        where M : IMappperDto<D,E>, new()
    {
        var group = app.MapGroup($"/{controllerName}").RequireAuthorization();
        group.MapPost("/", async (
                [FromServices] IRepository<E> service,
                [FromBody] D item, ClaimsPrincipal user) =>{     
                    item.GetType().GetProperty("UserId")?.SetValue(item, int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!));               
                    await service.AddAsync(new M().ToEntity(item));
                    return Results.Created($"/{controllerName}/", item);
                })
            .WithOpenApi();

        group.MapGet("/", async ([FromServices] IRepository<E> crudService, ClaimsPrincipal user) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var entities = await crudService.FindAllAsync(e => EF.Property<string>(e, "UserId") == userId);
            return Results.Ok(new M().FromEntityList(entities.ToList()));
        });
        group.MapGet("/{id}", async ([FromServices] IRepository<E> crudService, int id) =>{
           var result = await crudService.FindAsync(x => EF.Property<int>(x, "Id") == id);
           return result is not null ? Results.Ok(new M().FromEntity(result)) : Results.NotFound();
        });
        group.MapPut("/{id}", async ([FromServices] IRepository<E> crudService, int id, [FromBody] D item) =>
        {
            await crudService.UpdateAsync(id, new M().ToEntity(item));
            return Results.NoContent();
        });
        group.MapDelete("/{id}", async ([FromServices] IRepository<E> crudService, int id) => await crudService.DeleteAsync(x => EF.Property<int>(x, "Id") == id));
    }
}
