using Grand.Business.Core.Interfaces.System.Installation;
using Grand.Domain.Shipping;

namespace Grand.Business.System.Services.Installation
{
    public partial class InstallationService : IInstallationService
    {
        protected virtual async Task InstallShippingMethods()
        {
            var shippingMethods = new List<ShippingMethod>
                                {
                                    new ShippingMethod
                                        {
                                            Name = "Ground",
                                            Description ="Compared to other shipping methods, ground shipping is carried out closer to the earth",
                                            DisplayOrder = 1
                                        }                                    
                                };
            await _shippingMethodRepository.InsertAsync(shippingMethods);
        }
    }
}
