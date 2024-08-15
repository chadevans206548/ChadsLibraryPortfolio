using ChadsLibraryPortfolio.ViewModels.InventoryLogs;
using ViewModels.Common;

namespace ChadsLibraryPortfolio.Interfaces;
public interface IInventoryLogService
{
    Task<InventoryLogViewModel> AddInventoryLog(AddInventoryLogViewModel addInventoryLogViewModel);
    Task<InventoryLogViewModel> EditInventory(EditInventoryLogViewModel editInventoryLogViewModel);
    Task<ValidationResultViewModel> ValidateAddInventoryLog(AddInventoryLogViewModel addInventoryLogViewModel);
    Task<ValidationResultViewModel> ValidateEditInventory(EditInventoryLogViewModel editInventoryLogViewModel);

    Task<int> Checkout(int bookId);

    Task<int> Checkin(int bookId);
}
