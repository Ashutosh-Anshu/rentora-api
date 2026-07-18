using System;
using System.Collections.Generic;
using System.Text;

namespace Rentora.Application.Common.Shared.Responses
{
    public sealed record Error(string Code, string Description);
}
