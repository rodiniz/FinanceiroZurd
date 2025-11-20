namespace myapi.Dtos;

public record PageQueryDto(int Skip, int Take, string OrderBy);