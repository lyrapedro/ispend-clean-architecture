namespace iSpend.Web.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
