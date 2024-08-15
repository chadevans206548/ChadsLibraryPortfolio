using ChadsLibraryPortfolio.Helpers;
using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.ViewModels.InventoryLogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Common;

namespace API.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryLogController(IInventoryLogService inventoryLogService) : ControllerBase
{
    private readonly IInventoryLogService _inventoryLogService = inventoryLogService;

    [HttpPost]
    [Route("AddInventoryLog")]
    public async Task<ActionResult<InventoryLogViewModel>> AddInventoryLog([FromBody] AddInventoryLogViewModel addInventoryLogViewModel)
    {
        return await this._inventoryLogService.AddInventoryLog(addInventoryLogViewModel);
    }

    [HttpPut]
    [Route("EditInventory")]
    public async Task<ActionResult<InventoryLogViewModel>> EditInventory([FromBody] EditInventoryLogViewModel editInventoryLogViewModel)
    {
        return await this._inventoryLogService.EditInventory(editInventoryLogViewModel);
    }

    [HttpPost]
    [Route("ValidateAddInventoryLog")]
    public async Task<ValidationResultViewModel> ValidateAddInventoryLog([FromBody] AddInventoryLogViewModel addInventoryLogViewModel)
    {
        return await this._inventoryLogService.ValidateAddInventoryLog(addInventoryLogViewModel);
    }

    [HttpPost]
    [Route("ValidateEditInventory")]
    public async Task<ValidationResultViewModel> ValidateEditInventory([FromBody] EditInventoryLogViewModel editInventoryLogViewModel)
    {
        return await this._inventoryLogService.ValidateEditInventory(editInventoryLogViewModel);
    }

    [HttpPut]
    [Authorize(Roles = Constants.AuthPolicy.CustomerUser)]
    [Route("Checkout")]
    public async Task<bool> Checkout([FromBody] int bookId)
    {
        return await this._inventoryLogService.Checkout(bookId);
    }

    [HttpPut]
    [Authorize(Roles = Constants.AuthPolicy.LibrarianUser)]
    [Route("Checkin")]
    public async Task<bool> Checkin([FromBody] int bookId)
    {
        return await this._inventoryLogService.Checkin(bookId);
    }
}
