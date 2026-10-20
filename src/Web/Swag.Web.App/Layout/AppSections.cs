using MudBlazor;

namespace Swag.Web.App.Layout
{
    /// <summary>Разделы приложения — общий список для меню и главной страницы.</summary>
    public static class AppSections
    {
        public sealed record Section(string Href, string Title, string Icon);

        public static readonly IReadOnlyList<Section> All =
        [
            new("students", "Студенты", Icons.Material.Filled.School),
            new("teachers", "Преподаватели", Icons.Material.Filled.Person),
            new("groups", "Группы", Icons.Material.Filled.Groups),
            new("courses", "Направления", Icons.Material.Filled.MenuBook),
        ];
    }
}
