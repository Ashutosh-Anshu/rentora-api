using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rentora.Application.Common.Interfaces;
using Rentora.Application.Common.Shared.Responses;
using Rentora.Application.Menus.Queries.GetMenus;
using Rentora.Domain.Entities.Authentication;

namespace Rentora.Application.Menus.Queries.GetMenuById
{
    public sealed class GetMenuByIdQueryHandler
    : IRequestHandler<GetMenuByIdQuery, Result<List<GetMenuByIdResponse>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public GetMenuByIdQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _currentUser = currentUser;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Result<List<GetMenuByIdResponse>>> Handle(GetMenuByIdQuery request, CancellationToken ct)
        {
            var userId = request.userId;

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null)
            {
                return Result<List<GetMenuByIdResponse>>.Fail(
                    "User not found.");
            }

            if (!user.IsActive)
            {
                return Result<List<GetMenuByIdResponse>>.Fail(
                    "User is inactive.");
            }

            // Get user roles
            var roleNames = await _userManager.GetRolesAsync(user);

            if (!roleNames.Any())
            {
                return Result<List<GetMenuByIdResponse>>.Ok([], "No menus found.");
            }

            // Get role ids
            var roleIds = await _roleManager.Roles
                .Where(x => roleNames.Contains(x.Name!))
                .Select(x => x.Id)
                .ToListAsync(ct);

            if (!roleIds.Any())
            {
                return Result<List<GetMenuByIdResponse>>.Ok([], "No menus found.");
            }

            // Menu ids the user has permission to access
            var permittedMenuIds = await _context.RolePermissions
                .Where(rp => roleIds.Contains(rp.RoleId))
                .Select(rp => rp.ActionPermission.MenuId)
                .Distinct()
                .ToListAsync(ct);

            if (!permittedMenuIds.Any())
            {
                return Result<List<GetMenuByIdResponse>>.Ok([], "No menus found.");
            }

            // Load all active menus
            var allMenus = await _context.Menus
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.OrderNum)
                .ToListAsync(ct);

            var menuLookup = allMenus.ToDictionary(x => x.Id);

            // Final menu ids (including parents)
            var finalMenuIds = permittedMenuIds.ToHashSet();

            foreach (var menuId in permittedMenuIds)
            {
                if (!menuLookup.TryGetValue(menuId, out var menu))
                    continue;

                var parentId = menu.ParentId;

                while (parentId.HasValue)
                {
                    if (!menuLookup.TryGetValue(parentId.Value, out var parent))
                        break;

                    if (!finalMenuIds.Add(parent.Id))
                        break;

                    parentId = parent.ParentId;
                }
            }

            // Filter menus
            var menus = allMenus
                .Where(x => finalMenuIds.Contains(x.Id))
                .OrderBy(x => x.OrderNum)
                .ToList();

            // Convert to response objects
            var responseLookup = menus
                .Select(x => new GetMenuByIdResponse
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    Name = x.Name,
                    DisplayName = x.DisplayName,
                    Route = x.Route ?? string.Empty,
                    Icon = x.Icon ?? string.Empty,
                    OrderNum = x.OrderNum,
                    Children = []
                })
                .ToDictionary(x => x.Id);

            var result = new List<GetMenuByIdResponse>();

            foreach (var menu in responseLookup.Values.OrderBy(x => x.OrderNum))
            {
                if (menu.ParentId.HasValue &&
                    responseLookup.TryGetValue(menu.ParentId.Value, out var parent))
                {
                    parent.Children.Add(menu);
                }
                else
                {
                    result.Add(menu);
                }
            }

            return Result<List<GetMenuByIdResponse>>.Ok(result);
        }

    }
}
