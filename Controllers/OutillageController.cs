using LimsOutillageService.Dtos;
using LimsOutillageService.Services;
using LimsUtils.Api;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsOutillageService.Controllers
{
    [ApiController]
    [Route("api/outillages")]
    public class OutillageController : ControllerBase
    {
        private readonly IOutillageService _outillageService;

        // Injection du service via le constructeur
        public OutillageController(IOutillageService outillageService)
        {
            _outillageService = outillageService;
        }

        // Récupère le nombre total d'outillages
        [HttpGet("total")]
        public async Task<ActionResult<ApiResponse>> GetTotalOutillages()
        {
            int total = await _outillageService.CountOutillagesAsync();
            return Ok(new ApiResponse
            {
                Data = total,
                ViewBag = null,
                IsSuccess = true,
                Message = "Total outillages retrieved successfully.",
                StatusCode = 200
            });
        }

        // Récupère une liste paginée d'outillages
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetOutillages(int position = 1, int pageSize = 10)
        {
            if (position < 1) position = 1;
            if (pageSize < 1) pageSize = 10;

            var outillages = await _outillageService.GetOutillagesAsync(position, pageSize);
            int total = await _outillageService.CountOutillagesAsync();

            var viewBag = new Dictionary<string, object>
            {
                { "nbrPerPage", pageSize },
                { "TotalCount", total },
                { "nbrLinks", (int)Math.Ceiling((double)total / pageSize) },
                { "position", position }
            };

            return Ok(new ApiResponse
            {
                Data = outillages,
                ViewBag = viewBag,
                IsSuccess = true,
                Message = "Outillages retrieved successfully.",
                StatusCode = 200
            });
        }

        // Récupère un outillage par son ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetOutillage(int id)
        {
            try
            {
                var outillage = await _outillageService.GetOutillageByIdAsync(id);
                return Ok(new ApiResponse
                {
                    Data = outillage,
                    ViewBag = null,
                    IsSuccess = true,
                    Message = "Outillage retrieved successfully.",
                    StatusCode = 200
                });
            }
            catch (Exception)
            {
                return NotFound(new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = "Outillage not found.",
                    StatusCode = 404
                });
            }
        }

        // Crée un nouvel outillage
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateOutillage([FromBody] OutillageDto outillageDto)
        {
            var createdOutillage = await _outillageService.CreateOutillageAsync(outillageDto);
            return CreatedAtAction(nameof(GetOutillage), new { id = createdOutillage.IdOutillage }, new ApiResponse
            {
                Data = createdOutillage,
                ViewBag = null,
                IsSuccess = true,
                Message = "Outillage created successfully.",
                StatusCode = 201
            });
        }

        // Met à jour un outillage existant
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateOutillage(int id, [FromBody] OutillageDto outillageDto)
        {
            if (id != outillageDto.IdOutillage)
            {
                return BadRequest(new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = "ID mismatch.",
                    StatusCode = 400
                });
            }

            try
            {
                var updatedOutillage = await _outillageService.UpdateOutillageAsync(id, outillageDto);
                return Ok(new ApiResponse
                {
                    Data = updatedOutillage,
                    ViewBag = null,
                    IsSuccess = true,
                    Message = "Outillage updated successfully.",
                    StatusCode = 200
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = 404
                });
            }
        }

        // Supprime un outillage
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteOutillage(int id)
        {
            bool result = await _outillageService.DeleteOutillageAsync(id);
            if (!result)
            {
                return NotFound(new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = "Outillage not found.",
                    StatusCode = 404
                });
            }

            return NoContent();
        }
    }
}
