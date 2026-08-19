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
    public class MasterServiceController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly ApiResponse _response;

        public MasterServiceController(ApplicationDBContext db)
        {
            _db = db;
            _response = new ApiResponse();
        }

        [HttpGet]
        public ActionResult<ApiResponse> GetMasterServices()
        {
            _response.Result = _db.MasterServices
                .OrderBy(m => m.name)
                .ToList();
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet("{masterServiceId:int}")]
        public ActionResult<ApiResponse> GetMasterService(int masterServiceId)
        {
            if (masterServiceId == 0)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Invalid master service Id");
                return BadRequest(_response);
            }

            MasterService? masterService = _db.MasterServices
                .FirstOrDefault(m => m.masterServiceId == masterServiceId);

            if (masterService == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("Master service not found");
                return NotFound(_response);
            }

            _response.Result = masterService;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost]
        public ActionResult<ApiResponse> CreateMasterService([FromBody] MasterServiceCreateDTO masterServiceCreateDTO)
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

                MasterService masterService = new()
                {
                    name = masterServiceCreateDTO.name,
                    serviceType = masterServiceCreateDTO.serviceType,
                    category = masterServiceCreateDTO.category,
                    icon = masterServiceCreateDTO.icon,
                    active = masterServiceCreateDTO.active
                };

                _db.MasterServices.Add(masterService);
                _db.SaveChanges();

                _response.Result = masterService;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtAction(nameof(GetMasterService), new { masterServiceId = masterService.masterServiceId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }

        [HttpPut("{masterServiceId:int}")]
        public ActionResult<ApiResponse> UpdateMasterService(int masterServiceId, [FromBody] MasterServiceUpdateDTO masterServiceUpdateDTO)
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

                if (masterServiceId != masterServiceUpdateDTO.masterServiceId)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Invalid Id");
                    return BadRequest(_response);
                }

                MasterService? masterServiceFromDb = _db.MasterServices.FirstOrDefault(m => m.masterServiceId == masterServiceId);
                if (masterServiceFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Master service not found");
                    return NotFound(_response);
                }

                masterServiceFromDb.name = masterServiceUpdateDTO.name;
                masterServiceFromDb.serviceType = masterServiceUpdateDTO.serviceType;
                masterServiceFromDb.category = masterServiceUpdateDTO.category;
                masterServiceFromDb.icon = masterServiceUpdateDTO.icon;
                masterServiceFromDb.active = masterServiceUpdateDTO.active;

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

        [HttpDelete("{masterServiceId:int}")]
        public ActionResult<ApiResponse> DeleteMasterService(int masterServiceId)
        {
            try
            {
                if (masterServiceId == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Invalid master service Id");
                    return BadRequest(_response);
                }

                MasterService? masterServiceFromDb = _db.MasterServices.FirstOrDefault(m => m.masterServiceId == masterServiceId);
                if (masterServiceFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Master service not found");
                    return NotFound(_response);
                }

                _db.MasterServices.Remove(masterServiceFromDb);
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
