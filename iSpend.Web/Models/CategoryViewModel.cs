namespace iSpend.Web.Models
{
    public class CategoryViewModel: Entity
    {
        public string? UserId { get; private set; }
        public string Name { get; private set; }
        public string Color { get; private set; }
    }
}
