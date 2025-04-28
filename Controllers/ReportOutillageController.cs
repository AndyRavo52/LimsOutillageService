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
    [Route("api/report-outillage")]
    public class ReportOutillageController : ControllerBase
    {
        private readonly IReportOutillageService _reportOutillageService;

        public ReportOutillageController(IReportOutillageService reportOutillageService)
        {
            _reportOutillageService = reportOutillageService;
        }

        [HttpGet("total")]
        public async Task<ActionResult<ApiResponse>> GetTotalReportOutillages()
        {
            int total = await _reportOutillageService.CountReportOutillagesAsync();
            return Ok(new ApiResponse
            {
                Data = total,
                ViewBag = null,
                IsSuccess = true,
                Message = "Total des reports outillage récupéré avec succès.",
                StatusCode = 200
            });
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetReportOutillages(int position = 1, int pageSize = 10)
        {
            if (position < 1) position = 1;
            if (pageSize < 1) pageSize = 10;

            var reportOutillages = await _reportOutillageService.GetReportOutillagesAsync(position, pageSize);
            int total = await _reportOutillageService.CountReportOutillagesAsync();
            var viewBag = new Dictionary<string, object>
            {
                { "nbrPerPage", pageSize },
                { "TotalCount", total },
                { "nbrLinks", (int)Math.Ceiling((double)total / pageSize) },
                { "position", position }
            };

            return Ok(new ApiResponse
            {
                Data = reportOutillages,
                ViewBag = viewBag,
                IsSuccess = true,
                Message = "Reports outillage récupérés avec succès.",
                StatusCode = 200
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetReportOutillage(int id)
        {
            try
            {
                var reportOutillage = await _reportOutillageService.GetReportOutillageByIdAsync(id);
                return Ok(new ApiResponse
                {
                    Data = reportOutillage,
                    ViewBag = null,
                    IsSuccess = true,
                    Message = "Report outillage récupéré avec succès.",
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
        public async Task<ActionResult<ApiResponse>> CreateReportOutillage([FromBody] ReportOutillageDto reportOutillageDto)
        {
            var createdReportOutillage = await _reportOutillageService.CreateReportOutillageAsync(reportOutillageDto);
            return CreatedAtAction(nameof(GetReportOutillage), new { id = createdReportOutillage.IdReportOutillage }, new ApiResponse
            {
                Data = createdReportOutillage,
                ViewBag = null,
                IsSuccess = true,
                Message = "Report outillage créé avec succès.",
                StatusCode = 201
            });
        }
    }
}