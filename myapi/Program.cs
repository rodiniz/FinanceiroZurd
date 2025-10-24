using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Npgsql;
using myapi;
using myapi.Infrastructure;
using ContextHelpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddCors();
builder.AddServiceDefaults();

builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<NeondbContext>()
    .AddRoleManager<RoleManager<IdentityRole>>();

builder.Services.AddEndpointsApiExplorer();

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
var dataSourceBuilder = new NpgsqlDataSourceBuilder(conn);
dataSourceBuilder.EnableDynamicJson();
var dataSource = dataSourceBuilder.Build();
builder.Services.AddDbContext<NeondbContext>(options =>
    options.UseNpgsql(dataSource));

builder.Services.AddScoped(new Func<IServiceProvider, IUnitOfWork>(sp =>
{
    var context = sp.GetRequiredService<NeondbContext>();
    return new UnitOfWork(context, sp);
}));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.AddGenericCrudRoutes<Category>("Category");
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin()
);
app.MapIdentityApi<ApplicationUser>();


app.Run();


