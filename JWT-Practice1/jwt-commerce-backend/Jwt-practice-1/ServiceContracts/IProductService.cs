using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IProductService
    {
        Task<ProductResponse> addProduct(ProductRequest request, Guid userID);
        Task<List<ProductResponse>> getAvailableProducts();

        Task<bool> deleteProduct(Guid productID);
        Task<ProductResponse?> getProduct(Guid productID);

        Task<List<ProductResponse>> searchProduct(string search);
    }
}
