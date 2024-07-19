namespace ChadsLibraryPortfolio.Model.Entities;

public class InventoryLog
{
    public InventoryLog()
    {
        this.InventoryLogId = 0;
        this.BookId = 0;
        this.User = string.Empty;
        this.CheckoutDate = null;
        this.CheckinDate = null;
        this.DueDate = null;
    }
    public int InventoryLogId { get; set; }
    public int BookId { get; set; }
    public Book? Book { get; set; }
    public string User { get; set; }
    public DateTime? CheckoutDate { get; set; }
    public DateTime? CheckinDate { get; set; }
    public DateTime? DueDate { get; set; }
}
