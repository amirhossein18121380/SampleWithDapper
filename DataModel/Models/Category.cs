
namespace DataModel.Models
{
    public class Category 
    {
        public long Id { get; set; }
        public long ParentId { get; set; }
        public string Title { get; set; }
        public DateTime Createon { get; set; }
    }
}