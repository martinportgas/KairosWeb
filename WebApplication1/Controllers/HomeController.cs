using KairosWeb.Models;
using KairosWeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection;
using System.Web;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using KairosWeb.Services;

namespace KairosWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _ProductService;
        public HomeController(IProductService productService)
        {
            _ProductService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        public async Task<IActionResult> Edit(string Id)
        {;

            var data = await _ProductService.GetProductById(Convert.ToInt32(Id));
           
            if (data.Rows.Count == 0) 
            {
                return RedirectToAction("Index", "Home");    
            }

            ViewBag.ProductId = data.Rows[0]["ProductId"].ToString();
            ViewBag.ProductCode = data.Rows[0]["ProductCode"].ToString();
            ViewBag.ProductName = data.Rows[0]["ProductName"].ToString();
            ViewBag.ProductQty = data.Rows[0]["ProductQty"].ToString();

            DateTime date = DateTime.ParseExact(data.Rows[0]["ProductDate"].ToString(), "M/d/yyyy h:m:s tt", System.Globalization.CultureInfo.InvariantCulture);
            string formattedDate = date.ToString("yyyy-MM-dd");

            ViewBag.ProductDate = formattedDate;

            return View("Add");
        }
        public IActionResult Delete(string Id)
        {
            int ProductId = Convert.ToInt32(Id);
            _ProductService.Delete(ProductId);

            return RedirectToAction("Index", "Home");
        }
        public async Task<JsonResult> GetData() 
        {
            var data = await _ProductService.GetProduct();
            var list = new List<ProductModel>();
            list = ConvertDataTable<ProductModel>(data);
            
            return Json(list);
        }
        public async Task<JsonResult> Save(string productId, string productCode, string productName, string productQty, string productDate)
        {
            var message = new ResponseModel();

            try
            {
                
                var getProductByProductCode = await _ProductService.GetProductByCode(productCode);
                var getProductByProductName = await _ProductService.GetProductByname(productName);

                if (getProductByProductCode.Rows.Count > 0 && string.IsNullOrEmpty(productId))
                {
                    message.isSucces = false;
                    message.Type = "ProductCode";
                    message.Message = "Product Code is already exists!";
                }
                else if (getProductByProductName.Rows.Count > 0 && string.IsNullOrEmpty(productId))
                {
                    message.isSucces = false;
                    message.Type = "ProductName";
                    message.Message = "Product Name is already exists!";
                }
                else
                {

                    var model = new ProductModel();
                    model.ProductCode = productCode;
                    model.ProductName = productName;
                    model.ProductQty = Convert.ToInt32(productQty);
                    model.ProductDate = Convert.ToDateTime(productDate);

                    if (string.IsNullOrEmpty(productId))
                    {
                        await Task.Run(()=> _ProductService.Save(model));
                    }
                    else
                    {
                        model.ProductId = Convert.ToInt32(productId);
                        await Task.Run(() => _ProductService.Update(model));
                    }

                    message.isSucces = true;
                    message.Type = "Success";
                    message.Message = "";
                }

                return Json(message);
            
            }
            catch (Exception ex) 
            {
                message.isSucces = false;
                message.Type = "Application Error";
                message.Message = ex.Message.ToString();
                return Json(message);
            }
        }
        public async Task<JsonResult> Remove(string productId) 
        {
            int Id = Convert.ToInt32(productId);
            await Task.Run(()=> _ProductService.Delete(Id)); 

            return Json("Success Remove!");
        }
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}
