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
    [Route("api/reforme-outillage")]
    public class ReformeOutillageController : ControllerBase
    {
        private readonly IReformeOutillageService _reformeOutillageService;

        public ReformeOutillageController(IReformeOutillageService reformeOutillageService)
        {
            _reformeOutillageService = reformeOutillageService;
        }

        [HttpGet("total")]
        public async Task<ActionResult<ApiResponse>> GetTotalReformeOutillages()
        {
            int total = await _reformeOutillageService.CountReformeOutillagesAsync();
            return Ok(new ApiResponse
            {
                Data = total,
                ViewBag = null,
                IsSuccess = true,
                Message = "Total des réformes outillage récupéré avec succès.",
                StatusCode = 200
            });
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetReformeOutillages(int position = 1, int pageSize = 10)
        {
            if (position < 1) position = 1;
            if (pageSize < 1) pageSize = 10;

            var reformeOutillages = await _reformeOutillageService.GetReformeOutillagesAsync(position, pageSize);
            int total = await _reformeOutillageService.CountReformeOutillagesAsync();
            var viewBag = new Dictionary<string, object>
            {
                { "nbrPerPage", pageSize },
                { "TotalCount", total },
                { "nbrLinks", (int)Math.Ceiling((double)total / pageSize) },
                { "position", position }
            };

            return Ok(new ApiResponse
            {
                Data = reformeOutillages,
                ViewBag = viewBag,
                IsSuccess = true,
                Message = "Réformes outillage récupérées avec succès.",
                StatusCode = 200
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetReformeOutillage(int id)
        {
            try
            {
                var reformeOutillage = await _reformeOutillageService.GetReformeOutillageByIdAsync(id);
                return Ok(new ApiResponse
                {
                    Data = reformeOutillage,
                    ViewBag = null,
                    IsSuccess = true,
                    Message = "Réforme outillage récupérée avec succès.",
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

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateReformeOutillage([FromBody] ReformeOutillageDto reformeOutillageDto)
        {
            var createdReformeOutillage = await _reformeOutillageService.CreateReformeOutillageAsync(reformeOutillageDto);
            return CreatedAtAction(nameof(GetReformeOutillage), new { id = createdReformeOutillage.IdReformeOutillage }, new ApiResponse
            {
                Data = createdReformeOutillage,
                ViewBag = null,
                IsSuccess = true,
                Message = "Réforme outillage créée avec succès.",
                StatusCode = 201
            });
        }
    }
}