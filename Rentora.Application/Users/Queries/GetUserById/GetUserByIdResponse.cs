using Rentora.Application.Common.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rentora.Application.Users.Queries.GetUserById
{
    public class GetUserByIdResponse 
    {
        public Guid UserId { get; init; }

        public Guid RoleId { get; init; }

        public string RoleName { get; init; } = string.Empty;

        public string FullName { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;

        public string PhoneNumber { get; init; } = string.Empty;
        public bool IsActive { get; init; }
    }
}
