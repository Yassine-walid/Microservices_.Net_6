
using AutoMapper;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [ApiController]
    [Route("api/product")]
    
    public class ProductAPIController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private ResponseDto _response;
        private IMapper _imapper;

        public ProductAPIController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _response = new ResponseDto();
            _imapper = mapper;
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Product> objList = _appDbContext.Products.ToList();
                _response.Result = _imapper.Map<IEnumerable<ProductDto>>(objList);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Product? obj = _appDbContext.Products.FirstOrDefault(u => u.ProductId == id);
                _response.Result = _imapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public ResponseDto GetByName(string name)
        {
            try
            {
                Product? obj = _appDbContext.Products.FirstOrDefault(u => u.Name.ToLower() == name.ToLower());
                if (obj == null)
                {
                    _response.IsSuccess = false;
                }
                _response.Result = _imapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromBody] ProductDto productDto)
        {
            try
            {
                Product obj = _imapper.Map<Product>(productDto);
                _appDbContext.Products.Add(obj);
                _appDbContext.SaveChanges();

                _response.Result = _imapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Pur([FromBody] ProductDto couponDto)
        {
            try
            {
                Product obj = _imapper.Map<Product>(couponDto);
                _appDbContext.Products.Update(obj);
                _appDbContext.SaveChanges();

                _response.Result = _imapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Authorize(Roles = "ADMIN")]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Product? obj = _appDbContext.Products.FirstOrDefault(u => u.ProductId == id);
                _appDbContext.Products.Remove(obj);
                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
