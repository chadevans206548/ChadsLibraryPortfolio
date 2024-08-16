using AutoMapper;
using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.Model.Entities;
using ChadsLibraryPortfolio.Models;
using ChadsLibraryPortfolio.ViewModels.InventoryLogs;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Validation;
using ViewModels.Common;

namespace ChadsLibraryPortfolio.Services;
public class InventoryLogService(LibraryContext libraryContext, IMapper mapper, IBookService bookService) : IInventoryLogService
{
    protected readonly LibraryContext _libraryContext = libraryContext;
    protected readonly IMapper _mapper = mapper;
    protected readonly IBookService _bookService = bookService;
    public async Task<InventoryLogViewModel> AddInventoryLog(AddInventoryLogViewModel addInventoryLogViewModel)
    {
        AddInventoryLogValidator validator = new AddInventoryLogValidator(this._bookService);
        ValidationResult result = await validator.ValidateAsync(addInventoryLogViewModel);

        if (result.IsValid)
        {
            var log = this._mapper.Map<InventoryLog>(addInventoryLogViewModel);
            var entity = await this._libraryContext.AddAsync(log);
            await this._libraryContext.SaveChangesAsync();
            return this._mapper.Map<InventoryLogViewModel>(entity.Entity);
        }
        else
        {
            return new InventoryLogViewModel();
        }
    }

    public async Task<ValidationResultViewModel> ValidateAddInventoryLog(AddInventoryLogViewModel addInventoryLogViewModel)
    {
        AddInventoryLogValidator validator = new AddInventoryLogValidator(this._bookService);
        ValidationResult result = await validator.ValidateAsync(addInventoryLogViewModel);
        return this._mapper.Map<ValidationResultViewModel>(result);
    }
    public async Task<InventoryLogViewModel> EditInventory(EditInventoryLogViewModel editInventoryLogViewModel)
    {
        EditInventoryLogValidator validator = new EditInventoryLogValidator(this._bookService);
        ValidationResult result = await validator.ValidateAsync(editInventoryLogViewModel);

        if (result.IsValid)
        {
            var entity = await this._libraryContext.InventoryLogs
                .FirstOrDefaultAsync(x => x.InventoryLogId == editInventoryLogViewModel.InventoryLogId);
            this._mapper.Map(editInventoryLogViewModel, entity);
            this._libraryContext.Update(entity);
            await this._libraryContext.SaveChangesAsync();
            return this._mapper.Map<InventoryLogViewModel>(entity);
        }
        else
        {
            return new InventoryLogViewModel();
        }
    }

    public async Task<ValidationResultViewModel> ValidateEditInventory(EditInventoryLogViewModel editInventoryLogViewModel)
    {
        EditInventoryLogValidator validator = new EditInventoryLogValidator(this._bookService);
        ValidationResult result = await validator.ValidateAsync(editInventoryLogViewModel);
        return this._mapper.Map<ValidationResultViewModel>(result);
    }

    public async Task<int> Checkout(int bookId)
    {
        var book = await this._libraryContext.Books
            .Include(x => x.InventoryLogs)
            .Include(x => x.Reviews)
            .Where(x => x.BookId == bookId)
            .FirstOrDefaultAsync();

        if (book != null)
        {
            var lastLog = await this._libraryContext.InventoryLogs
                .OrderByDescending(x => x.CheckoutDate)
                .Where(x => x.BookId == bookId && !x.CheckinDate.HasValue)
                .FirstOrDefaultAsync();

            if (lastLog == null) //This book is in stock
            {
                var addLog = new InventoryLog();
                this._mapper.Map(book, addLog);
                addLog.User = "Security.Username"; //from token? abstract user service?
                var entity = await this._libraryContext.AddAsync(addLog);
                await this._libraryContext.SaveChangesAsync();
                return entity.Entity.InventoryLogId;
            }
        }
        return 0;
    }

    public async Task<int> Checkin(int bookId)
    {
        var book = await this._libraryContext.Books
            .Include(x => x.InventoryLogs)
            .Include(x => x.Reviews)
            .Where(x => x.BookId == bookId)
            .FirstOrDefaultAsync();

        if (book != null)
        {
            var lastLog = await this._libraryContext.InventoryLogs
                .OrderByDescending(x => x.CheckoutDate)
                .Where(x => x.BookId == bookId && !x.CheckinDate.HasValue)
                .FirstOrDefaultAsync();

            if (lastLog != null) //This book is checked out
            {
                lastLog.CheckinDate = DateTime.Now;
                this._libraryContext.Entry(lastLog).State = EntityState.Modified;

                await this._libraryContext.SaveChangesAsync();
                return lastLog.InventoryLogId;
            }

        }
        return 0;

    }
}
