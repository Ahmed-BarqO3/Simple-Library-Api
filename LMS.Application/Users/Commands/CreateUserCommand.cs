using LMS.Application.Response;
using Mediator;

namespace LMS.Application.Users.Commands;
public class CreateUserCommand : IRequest<UserResponse>
{
    // Generate a random 4 digit number for the LibraryCardNumber (deomnstration purposes only)
    public int UserId { get; set; }
    public string Name { get; set; }
    public string ContactInformation { get; set; }
    public readonly string LibraryCardNumber = Guid.NewGuid().ToString().Substring(0, 4);
}
