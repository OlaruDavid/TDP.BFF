namespace Tdp.Domain.Dtos.Board;

public class MoveSubtaskRequest
{
    public string Column { get; set; } = string.Empty;
    public int Position { get; set; }
     public Guid UserId { get; set; }
}