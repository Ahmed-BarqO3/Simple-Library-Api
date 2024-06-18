using LMS.Core.Models;

namespace LMS.Application.Response;

public record BookCopyResponse(int CopyId,int BookId,bool AvailabilityStatus,BookResponse Book);
