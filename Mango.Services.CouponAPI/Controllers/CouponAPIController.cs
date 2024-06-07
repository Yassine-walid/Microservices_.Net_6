using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [ApiController]
    [Route("api/coupon")]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private ResponseDto _response;
        private IMapper _imapper;

        public CouponAPIController(AppDbContext appDbContext,IMapper mapper)
        {
            _appDbContext = appDbContext;
            _response = new ResponseDto();
            _imapper = mapper;
        }

        [HttpGet]
        public ResponseDto Get() {
            try
            {
                IEnumerable<Coupon> objList = _appDbContext.Coupons.ToList();
                _response.Result = _imapper.Map<IEnumerable<CouponDto>>(objList);
               
            }
            catch (Exception ex) { 
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
                Coupon obj = _appDbContext.Coupons.FirstOrDefault(u=>u.CouponId==id);
                _response.Result = _imapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon obj = _appDbContext.Coupons.FirstOrDefault(u => u.CouponCode.ToLower() == code.ToLower());
                if(obj == null)
                {
                    _response.IsSuccess=false;
                }
                _response.Result = _imapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _imapper.Map<Coupon>(couponDto);
                _appDbContext.Coupons.Add(obj);
                _appDbContext.SaveChanges();

                _response.Result = _imapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public ResponseDto Pur([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _imapper.Map<Coupon>(couponDto);
                _appDbContext.Coupons.Update(obj);
                _appDbContext.SaveChanges();

                _response.Result = _imapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon obj = _appDbContext.Coupons.FirstOrDefault(u => u.CouponId== id);
                _appDbContext.Coupons.Remove(obj);
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
