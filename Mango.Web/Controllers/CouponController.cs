using Mango.Web.Models;
using Mango.Web.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _ProductService;

        public ProductController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto?> list = new();
            ResponseDto? response = await _ProductService.GetAllProductsAsync();
            if (response != null && response.IsSuccess==true)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto ProductDto)
        {
            if (ModelState.IsValid) 
            {
                ResponseDto? response = await _ProductService.CreateProductAsync(ProductDto);
                if (response != null && response.IsSuccess == true)
                {
                    TempData["succes"] = "Product Created Succesfuly";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(ProductDto);
        }

        public async Task<IActionResult> ProductDelete(int ProductId)
        {
            ResponseDto? response = await _ProductService.GetProductByIdAsync(ProductId);
            if (response != null && response.IsSuccess == true)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto ProductDto)
        {
            ResponseDto? response = await _ProductService.DeleteProductByIdAsync(ProductDto.ProductId);
            if (response != null && response.IsSuccess == true)
            {
                TempData["succes"] = "Product Deleted Succesfuly";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(ProductDto);
        }
    }
}
