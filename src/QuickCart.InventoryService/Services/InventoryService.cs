using Grpc.Core;
using QuickCart.Inventory;

namespace QuickCart.InventoryService.Services
{
    public class InventoryService : QuickCart.Inventory.Inventory.InventoryBase
    {
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(ILogger<InventoryService> logger)
        {
            _logger = logger;
        }

        public override Task<AvailabilityResponse> CheckAvailability(ProductRequest request, ServerCallContext context)
        {
            var isAvailable = request.ProductId % 2 == 0; 

            return Task.FromResult(new AvailabilityResponse
            {
                Available = isAvailable
            });
        }
    }
}
