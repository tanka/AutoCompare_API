using AutoCompare_API.Data;
using AutoCompare_API.Models;
using AutoCompare_API.Models.Dto;
using AutoCompare_API.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AutoCompare_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopServiceController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly ApiResponse _response;

        public ShopServiceController(ApplicationDBContext db)
        {
            _db = db;
            _response = new ApiResponse();
        }

        [HttpGet]
        public ActionResult<ApiResponse> GetShopServices(int shopId = 0, int masterServiceId = 0)
        {
            IQueryable<ShopService> query = _db.ShopServices
                .Include(s => s.Shop)
                .Include(s => s.MasterService)
                .OrderBy(s => s.sortOrder);

            if (shopId > 0)
            {
                query = query.Where(s => s.shopId == shopId);
            }

            if (masterServiceId > 0)
            {
                query = query.Where(s => s.masterServiceId == masterServiceId);
            }

            _response.Result = query.ToList();
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet("{shopServiceId:int}")]
        public ActionResult<ApiResponse> GetShopService(int shopServiceId)
        {
            if (shopServiceId == 0)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Invalid shop service Id");
                return BadRequest(_response);
            }

            ShopService? shopService = _db.ShopServices
                .Include(s => s.Shop)
                .Include(s => s.MasterService)
                .FirstOrDefault(s => s.shopServiceId == shopServiceId);

            if (shopService == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("Shop service not found");
                return NotFound(_response);
            }

            _response.Result = shopService;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost]
        public ActionResult<ApiResponse> CreateShopService([FromBody] ShopServiceCreateDTO shopServiceCreateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(_response);
                }

                if (!_db.Shops.Any(s => s.Id == shopServiceCreateDTO.shopId))
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Shop not found");
                    return BadRequest(_response);
                }

                if (!_db.MasterServices.Any(m => m.masterServiceId == shopServiceCreateDTO.masterServiceId))
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Master service not found");
                    return BadRequest(_response);
                }

                ShopService shopService = new()
                {
                    shopId = shopServiceCreateDTO.shopId,
                    masterServiceId = shopServiceCreateDTO.masterServiceId,
                    name = shopServiceCreateDTO.name,
                    price = shopServiceCreateDTO.price,
                    priceType = shopServiceCreateDTO.priceType,
                    durationMin = shopServiceCreateDTO.durationMin,
                    etaMin = shopServiceCreateDTO.etaMin,
                    isAtShop = shopServiceCreateDTO.isAtShop,
                    isMobile = shopServiceCreateDTO.isMobile,
                    isRoadside = shopServiceCreateDTO.isRoadside,
                    active = shopServiceCreateDTO.active,
                    sortOrder = shopServiceCreateDTO.sortOrder
                };

                _db.ShopServices.Add(shopService);
                _db.SaveChanges();

                _response.Result = shopService;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtAction(nameof(GetShopService), new { shopServiceId = shopService.shopServiceId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }

        [HttpPut("{shopServiceId:int}")]
        public ActionResult<ApiResponse> UpdateShopService(int shopServiceId, [FromBody] ShopServiceUpdateDTO shopServiceUpdateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(_response);
                }

                if (shopServiceId != shopServiceUpdateDTO.shopServiceId)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Invalid Id");
                    return BadRequest(_response);
                }

                ShopService? shopServiceFromDb = _db.ShopServices.FirstOrDefault(s => s.shopServiceId == shopServiceId);
                if (shopServiceFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Shop service not found");
                    return NotFound(_response);
                }

                if (!_db.Shops.Any(s => s.Id == shopServiceUpdateDTO.shopId))
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Shop not found");
                    return BadRequest(_response);
                }

                if (!_db.MasterServices.Any(m => m.masterServiceId == shopServiceUpdateDTO.masterServiceId))
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Master service not found");
                    return BadRequest(_response);
                }

                shopServiceFromDb.shopId = shopServiceUpdateDTO.shopId;
                shopServiceFromDb.masterServiceId = shopServiceUpdateDTO.masterServiceId;
                shopServiceFromDb.name = shopServiceUpdateDTO.name;
                shopServiceFromDb.price = shopServiceUpdateDTO.price;
                shopServiceFromDb.priceType = shopServiceUpdateDTO.priceType;
                shopServiceFromDb.durationMin = shopServiceUpdateDTO.durationMin;
                shopServiceFromDb.etaMin = shopServiceUpdateDTO.etaMin;
                shopServiceFromDb.isAtShop = shopServiceUpdateDTO.isAtShop;
                shopServiceFromDb.isMobile = shopServiceUpdateDTO.isMobile;
                shopServiceFromDb.isRoadside = shopServiceUpdateDTO.isRoadside;
                shopServiceFromDb.active = shopServiceUpdateDTO.active;
                shopServiceFromDb.sortOrder = shopServiceUpdateDTO.sortOrder;

                _db.SaveChanges();

                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }

        [HttpDelete("{shopServiceId:int}")]
        public ActionResult<ApiResponse> DeleteShopService(int shopServiceId)
        {
            try
            {
                if (shopServiceId == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Invalid shop service Id");
                    return BadRequest(_response);
                }

                ShopService? shopServiceFromDb = _db.ShopServices.FirstOrDefault(s => s.shopServiceId == shopServiceId);
                if (shopServiceFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Shop service not found");
                    return NotFound(_response);
                }

                _db.ShopServices.Remove(shopServiceFromDb);
                _db.SaveChanges();

                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }
    }
}
