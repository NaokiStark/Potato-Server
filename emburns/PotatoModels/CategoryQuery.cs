using emburns.Models;

namespace emburns.PotatoModels
{
    public class CategoryQuery
    {
        public string Icon { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Shortname { get; set; } = null!;
        public string CatImage { get; set; } = null!;
        public CategoryQuery(PostCategory category) {
            Icon = category.Icon;
            Name = category.Name;
            Shortname = category.Shortname;
            CatImage = category.CatImage;
        }
    }
}
