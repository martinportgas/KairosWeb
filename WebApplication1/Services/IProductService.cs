using KairosWeb.Models;
using System.Data;

namespace KairosWeb.Services
{
    public interface IProductService
    {
        Task<DataTable> GetProduct();
        Task<DataTable> GetProductById(int Id);
        Task<DataTable> GetProductByCode(string productCode);
        Task<DataTable> GetProductByname(string productName);
        void Save(ProductModel model);
        void Update(ProductModel model);
        void Delete(int Id);
    }
}
