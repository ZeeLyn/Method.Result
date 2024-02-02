using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpPost("test")]
        public IActionResult Test([FromServices] Address address, [FromBody] AddressView data)
        {
            var result = address.Create2(data.Name, data.Address);
            if (result.Succeed)
                return Ok(new
                {
                    id = result.Value,
                });
            return BadRequest(result.ErrorMessage);
        }
    }

    public class AddressView
    {
        public string Name { get; set; }

        public string Address { get; set; }
    }
}