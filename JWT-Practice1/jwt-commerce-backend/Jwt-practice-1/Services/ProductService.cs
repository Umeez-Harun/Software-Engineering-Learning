using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductResponse> addProduct(ProductRequest request, Guid userID)
        {
            if (!InputValidator.validateInput(request))
            {
                throw new ArgumentException("Please fill all required fields correctly");
            }
            if(_dbContext.products.Any(temp => temp.sku.Trim() == request.sku.Trim()))
            {
                throw new InvalidOperationException("A Product with same SKU exists");
            }

            Product product = request.convertToProduct();
            product.seller_id = userID;

            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return product.convertToProductResponse();
        }

        public async Task<bool> deleteProduct(Guid productID)
        {
            Product? product = await _dbContext.products.FirstOrDefaultAsync(temp => temp.id == productID);
            if (product == null)
            {
                throw new InvalidOperationException("Product could not be found");
            }
            product.isDeleted = true;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<ProductResponse>> getAvailableProducts()
        {
            return await _dbContext.products.Where(temp => temp.isDeleted == false)
                .OrderByDescending(temp => temp.createdAt)
                .Select(temp => temp.convertToProductResponse())
                .ToListAsync();
        }

        public async Task<ProductResponse?> getProduct(Guid productID)
        {
            Product? product = await _dbContext.products.FirstOrDefaultAsync(temp => temp.id == productID);
            return product?.convertToProductResponse();
        }

        public async Task<List<ProductResponse>> searchProduct(string search)
        {
            bool isNumber = Decimal.TryParse(search, out decimal result);

            if (isNumber)
            {
            return await _dbContext.products.Where(temp =>
               (temp.title.Contains(search)
            || temp.description.Contains(search)
            || temp.category.Contains(search)
            || temp.sku.Contains(search)
            || temp.quantity == result
            || temp.price == result)
            && temp.isDeleted == false
            ).Select(temp => temp.convertToProductResponse()).ToListAsync();
            }

            return await _dbContext.products.Where(temp =>
               (temp.title.Contains(search)
            || temp.description.Contains(search)
            || temp.category.Contains(search)
            || temp.sku.Contains(search))
            && temp.isDeleted == false
            ).Select(temp => temp.convertToProductResponse()).ToListAsync();
        }
    }
}
