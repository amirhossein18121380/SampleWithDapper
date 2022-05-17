
namespace DataModel.Models
{
    public class Receipt
    {
        public long Id { get; set; }
        public decimal TotalCost { get; set; }
        public long ReceiptNumber { get; set; }
        public DateTime ReceiptDate { get; set; }

    }
}