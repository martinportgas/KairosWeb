using KairosWeb.Models;
using KairosWeb.Repositories;
using System.Data;
using System.Reflection;

namespace KairosWeb.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _ProductRepository;
        public ProductService(IProductRepository productRepository)
        {
            _ProductRepository = productRepository;
        }
        public async void Delete(int Id)
        {
            await Task.Run(() => _ProductRepository.Delete(Id));
        }

        public async Task<DataTable> GetProduct()
        {
            return await _ProductRepository.GetProduct();
        }

        public async Task<DataTable> GetProductByCode(string productCode)
        {
            return await _ProductRepository.GetProductByCode(productCode);
        }

        public async Task<DataTable> GetProductById(int Id)
        {
            return await _ProductRepository.GetProductById(Id);
        }

        public Task<DataTable> GetProductByname(string productName)
        {
            return _ProductRepository.GetProductByname(productName);
        }

        public async void Save(ProductModel model)
        {
            await Task.Run(()=> _ProductRepository.Save(model)); 
        }

        public async void Update(ProductModel model)
        {
            await Task.Run(() => _ProductRepository.Update(model));
        }
    }
}
