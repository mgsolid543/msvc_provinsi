using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("[provinsi]")]
    [ApiController]
    [Produces("application/json")]
    public class ProvinsiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // Implement your logic to retrieve data
            return Ok("This is the Get method of YourControllerNameController.");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Implement your logic to retrieve data by ID
            return Ok($"This is the GetById method of YourControllerNameController with ID: {id}");
        }

    }
}
