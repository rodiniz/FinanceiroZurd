namespace myapi.Dtos;

public record CategoryDto()
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string UserId { get; set; }

}
