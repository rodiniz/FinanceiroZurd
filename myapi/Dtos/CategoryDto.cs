using System;
using System.Security.Claims;

namespace myapi.Dtos;

public class CategoryDto()
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string UserId { get; set; }
   
}
