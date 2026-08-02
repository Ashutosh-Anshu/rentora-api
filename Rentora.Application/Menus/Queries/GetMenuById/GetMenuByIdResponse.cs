namespace Rentora.Application.Menus.Queries.GetMenus
{
    public sealed class GetMenuByIdResponse
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public int OrderNum { get; set; }

        public List<GetMenuByIdResponse> Children { get; set; } = [];
    }
}
