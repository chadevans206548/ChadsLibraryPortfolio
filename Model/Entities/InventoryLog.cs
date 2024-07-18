namespace ChadsLibraryPortfolio.Model.Entities
{
    public class InventoryLog
    {
        public InventoryLog()
        {
            InventoryLogId = 0;
            BookId = 0;
            User = string.Empty;
            CheckoutDate = null;
            CheckinDate = null;
            DueDate = null;
        }
        public int InventoryLogId { get; set; }
        public int BookId { get; set; }
        public string User { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public DateTime? CheckinDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
