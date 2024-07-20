using ChadsLibraryPortfolio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestDataController(ITestDataService testDataService) : ControllerBase
{
    private readonly ITestDataService _testDataService = testDataService;

    [HttpPost]
    [Route("AddTestData")]
    public async Task<ActionResult<bool>> AddTestData()
    {
        return await this._testDataService.AddTestData();
    }
}
