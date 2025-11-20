using ContextHelpers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myapi.Dtos;
using System.Security.Claims;

namespace myapi;

public static class GenericRoutes
{
    public static void AddGenericCrudRoutes<D, E, M>(this WebApplication app, string controllerName, string IdColumnName)
        where D : class, new()
        where E : class, new()
        where M : IMappperDto<D, E>, new()
    {
        var group = app.MapGroup($"/{controllerName}").RequireAuthorization();
        group.MapPost("/", async (
                [FromServices] IRepository<E> service,
                [FromServices] IServiceProvider sp,
                [FromBody] D item, ClaimsPrincipal user) =>
        {
            var validator = sp.GetRequiredService<IValidator<D>>();
            if (validator is not null)
            {
                var validationResult = await validator.ValidateAsync(item);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }
            }
            item.GetType().GetProperty("UserId")?.SetValue(item, user.FindFirstValue(ClaimTypes.NameIdentifier));
            await service.AddAsync(new M().ToEntity(item));

            return Results.Created($"/{controllerName}/", item);
        })
            .WithOpenApi();

        group.MapGet("/", async ([FromServices] IRepository<E> repository, ClaimsPrincipal user,
                [AsParameters] PageQueryDto dto) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var entities = await repository.FindAllAsync(e => EF.Property<string>(e, "UserId") == userId, dto.Skip, dto.Take, dto.OrderBy);
            return Results.Ok(new M().FromEntityList(entities));
        });
        group.MapGet("/{id}", async ([FromServices] IRepository<E> respository, int id) =>
        {
            var result = await respository.FindAsync(x => EF.Property<int>(x, IdColumnName) == id);
            return result is not null ? Results.Ok(new M().FromEntity(result)) : Results.NotFound();
        });
        group.MapPut("/{id}", async ([FromServices] IRepository<E> crudService, [FromServices] IServiceProvider sp, int id, [FromBody] D item) =>
        {
            var validator = sp.GetRequiredService<IValidator<D>>();
            if (validator is not null)
            {
                var validationResult = await validator.ValidateAsync(item);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }
            }
            await crudService.UpdateAsync(id, new M().ToEntity(item));
            return Results.NoContent();
        });
        group.MapDelete("/{id}", async ([FromServices] IRepository<E> crudService, int id) => await crudService.DeleteAsync(x => EF.Property<int>(x, IdColumnName) == id));
    }
}
