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
    public class ShopController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly ApiResponse _response;

        public ShopController(ApplicationDBContext db)
        {
            _db = db;
            _response = new ApiResponse();
        }

        [HttpGet]
        public ActionResult<ApiResponse> GetShops()
        {
            IQueryable<Shop> query = _db.Shops.Include(s => s.Owner).OrderByDescending(s => s.CreatedAt);

            _response.Result = query.ToList();
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }



        [HttpGet("ByOwnerId")]
        public ActionResult<ApiResponse> GetShopsByOwnerId(string ownerId)
        {
            IQueryable<Shop> query = _db.Shops.Include(s => s.Owner).OrderByDescending(s => s.CreatedAt);

            if (ownerId!=null)
            {
                query = query.Where(s => s.OwnerId == ownerId);
            }

            _response.Result = query.ToList();
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ApiResponse> GetShop(int id)
        {
            if (id == 0)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Invalid shop Id");
                return BadRequest(_response);
            }

            Shop? shop = _db.Shops.Include(s => s.Owner).FirstOrDefault(s => s.Id == id);
            if (shop == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("Shop not found");
                return NotFound(_response);
            }

            _response.Result = shop;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost]
        public ActionResult<ApiResponse> CreateShop([FromBody] ShopCreateDTO shopCreateDTO)
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

                Shop shop = new()
                {
                    OwnerId = shopCreateDTO.OwnerId,
                    Name = shopCreateDTO.Name,
                    Category = shopCreateDTO.Category,
                    Address = shopCreateDTO.Address,
                    City = shopCreateDTO.City,
                    Province = shopCreateDTO.Province,
                    PostalCode = shopCreateDTO.PostalCode,
                    Lat = shopCreateDTO.Lat,
                    Lng = shopCreateDTO.Lng,
                    Phone = shopCreateDTO.Phone,
                    Email = shopCreateDTO.Email,
                    IsOpen = shopCreateDTO.IsOpen,
                    WorkHours = shopCreateDTO.WorkHours,
                    Bio = shopCreateDTO.Bio,
                    CreatedAt = DateTime.UtcNow
                };

                _db.Shops.Add(shop);
                _db.SaveChanges();

                _response.Result = shop;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtAction(nameof(GetShop), new { id = shop.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult<ApiResponse> UpdateShop(int id, [FromBody] ShopUpdateDTO shopUpdateDTO)
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

                if (id != shopUpdateDTO.Id)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Invalid Id");
                    return BadRequest(_response);
                }

                Shop? shopFromDb = _db.Shops.FirstOrDefault(s => s.Id == id);
                    if (shopFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Shop not found");
                    return NotFound(_response);
                }

                if (!string.IsNullOrEmpty(shopUpdateDTO.OwnerId))
                    shopFromDb.OwnerId = shopUpdateDTO.OwnerId;
                //if(string.IsNullOrEmpty(shopUpdateDTO.Name))
                //    shopFromDb.Name = shopUpdateDTO.Name;
               // if(string.IsNullOrEmpty(shopUpdateDTO.Category))
               //     shopFromDb.Category = shopUpdateDTO.Category;
                
               // if(string.IsNullOrEmpty(shopUpdateDTO.Address))
               //     shopFromDb.Address = shopUpdateDTO.Address;
               //if(string.IsNullOrEmpty(shopUpdateDTO.City))
               //     shopFromDb.City = shopUpdateDTO.City;
               // if(string.IsNullOrEmpty(shopUpdateDTO.Province))
               //     shopFromDb.Province = shopUpdateDTO.Province;
               // if(string.IsNullOrEmpty(shopUpdateDTO.PostalCode))
               //     shopFromDb.PostalCode = shopUpdateDTO.PostalCode;
               // if(shopUpdateDTO.Lat != 0)
               //     shopFromDb.Lat = shopUpdateDTO.Lat;
               // if(shopUpdateDTO.Lng != 0)
               //     shopFromDb.Lng = shopUpdateDTO.Lng;
               //if(string.IsNullOrEmpty(shopUpdateDTO.Phone)) 
               // shopFromDb.Phone = shopUpdateDTO.Phone;
               //if(string.IsNullOrEmpty(shopUpdateDTO.Email))
               // shopFromDb.Email = shopUpdateDTO.Email;
               // if (shopUpdateDTO.IsOpen != null)
               //     shopFromDb.IsOpen = shopUpdateDTO.IsOpen;
               // if(string.IsNullOrEmpty(shopUpdateDTO.WorkHours))
               //     shopFromDb.WorkHours = shopUpdateDTO.WorkHours;
               //if(string.IsNullOrEmpty(shopUpdateDTO.Bio))
               //     shopFromDb.Bio = shopUpdateDTO.Bio;

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

        [HttpDelete("{id:int}")]
        public ActionResult<ApiResponse> DeleteShop(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Invalid shop Id");
                    return BadRequest(_response);
                }

                Shop? shopFromDb = _db.Shops.FirstOrDefault(s => s.Id == id);
                if (shopFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Shop not found");
                    return NotFound(_response);
                }

                _db.Shops.Remove(shopFromDb);
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
