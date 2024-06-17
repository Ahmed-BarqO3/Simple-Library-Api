using LMS.Application.Response;
using Mediator;

namespace LMS.Application.Users.Commands;
public class CreateUserCommand : IRequest<UserResponse>
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string ContactInformation { get; set; }

    // Generate a random 4 char for the LibraryCardNumber (deomnstration purposes only)
    public readonly string LibraryCardNumber = Guid.NewGuid().ToString().Substring(0, 4);
}
