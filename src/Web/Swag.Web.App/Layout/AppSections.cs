using MudBlazor;

namespace Swag.Web.App.Layout
{
    /// <summary>Разделы приложения — общий список для меню и главной страницы.</summary>
    public static class AppSections
    {
        public sealed record Section(string Href, string Title, string Icon);

        public static readonly IReadOnlyList<Section> All =
        [
            new("persons", "Люди", Icons.Material.Filled.People),
            new("groups", "Группы", Icons.Material.Filled.Groups),
            new("courses", "Направления", Icons.Material.Filled.MenuBook),
        ];
    }
}
