using AutoMapper;
using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.Model.Entities;
using ChadsLibraryPortfolio.Models;
using ChadsLibraryPortfolio.ViewModels.InventoryLogs;
using Microsoft.EntityFrameworkCore;

namespace ChadsLibraryPortfolio.Services;
public class InventoryLogService(LibraryContext libraryContext, IMapper mapper) : IInventoryLogService
{
    protected readonly LibraryContext _libraryContext = libraryContext;
    protected readonly IMapper _mapper = mapper;
    public async Task<InventoryLogViewModel> AddInventoryLog(AddInventoryLogViewModel addInventoryLogViewModel)
    {
        var log = this._mapper.Map<InventoryLog>(addInventoryLogViewModel);
        var entity = await this._libraryContext.AddAsync(log);
        await this._libraryContext.SaveChangesAsync();
        return this._mapper.Map<InventoryLogViewModel>(entity.Entity);
    }
    public async Task<InventoryLogViewModel> EditInventory(EditInventoryLogViewModel editInventoryLogViewModel)
    {
        var entity = await this._libraryContext.InventoryLogs
            .FirstOrDefaultAsync(x => x.InventoryLogId == editInventoryLogViewModel.InventoryLogId);
        this._mapper.Map(editInventoryLogViewModel, entity);
        this._libraryContext.Update(entity);
        await this._libraryContext.SaveChangesAsync();
        return this._mapper.Map<InventoryLogViewModel>(entity);
    }
}
