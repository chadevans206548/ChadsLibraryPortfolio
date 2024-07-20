using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.ViewModels.InventoryLogs;
using Microsoft.AspNetCore.Mvc;

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
}
