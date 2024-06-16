using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Application.Response
{
    public record UeerResponse(
        int UserId,
        string Name,
        string ContactInformation,
        string LibraryCardNumber);

}
