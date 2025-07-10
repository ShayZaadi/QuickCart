using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;
using QuickCart.Inventory;

namespace QuickCart.CartService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        [HttpGet("check/{productId}")]
        public async Task<IActionResult> CheckAvailability(int productId)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5001"); // כתובת InventoryService
            var client = new Inventory.Inventory.InventoryClient(channel);

            var response = await client.CheckAvailabilityAsync(new ProductRequest { ProductId = productId });

            return Ok(new { productId, available = response.Available });
        }
    }
}
