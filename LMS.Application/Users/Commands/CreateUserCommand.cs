using LMS.Application.Response;
using Mediator;

namespace LMS.Application.Users.Commands;
public class CreateUserCommand : IRequest<UserResponse>
{
    public CreateUserCommand() =>

        LibraryCardNumber = Guid.NewGuid().ToString().Substring(0, 4);
    public int UserId { get; set; }
    public string Name { get; set; }
    public string ContactInformation { get; set; }
    public readonly string LibraryCardNumber;
}
