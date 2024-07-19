using ChadsLibraryPortfolio.ViewModels.InventoryLogs;

namespace ChadsLibraryPortfolio.Interfaces;
public interface IInventoryLogService
{
    Task<InventoryLogViewModel> AddInventoryLog(AddInventoryLogViewModel addInventoryLogViewModel);
    Task<InventoryLogViewModel> EditInventory(EditInventoryLogViewModel editInventoryLogViewModel);
}
