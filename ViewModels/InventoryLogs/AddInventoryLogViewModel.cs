namespace ChadsLibraryPortfolio.ViewModels.InventoryLogs;
public class AddInventoryLogViewModel
{
    public AddInventoryLogViewModel()
    {
        this.BookId = 0;
        this.User = string.Empty;
        this.CheckoutDate = null;
        this.CheckinDate = null;
        this.DueDate = null;
    }
    public int BookId { get; set; }
    public string User { get; set; }
    public DateTime? CheckoutDate { get; set; }
    public DateTime? CheckinDate { get; set; }
    public DateTime? DueDate { get; set; }
}
