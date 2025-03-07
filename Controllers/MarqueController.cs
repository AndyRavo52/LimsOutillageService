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
    [Route("api/marques")]
    public class MarqueController : ControllerBase
    {
        private readonly IMarqueService _marqueService;

        // Injection du service via le constructeur
        public MarqueController(IMarqueService marqueService)
        {
            _marqueService = marqueService;
        }

        // Récupère le nombre total de marques
        [HttpGet("total")]
        public async Task<ActionResult<ApiResponse>> GetTotalMarques()
        {
            int total = await _marqueService.CountMarquesAsync();
            return Ok(new ApiResponse
            {
                Data = total,
                ViewBag = null,
                IsSuccess = true,
                Message = "Total marques retrieved successfully.",
                StatusCode = 200
            });
        }

        // Récupère une liste paginée de marques
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetMarques(int position = 1, int pageSize = 10)
        {
            if (position < 1) position = 1; // Position minimale : 1
            if (pageSize < 1) pageSize = 10; // Taille de page minimale : 1, valeur par défaut : 10

            var marques = await _marqueService.GetMarquesAsync(position, pageSize);
            int total = await _marqueService.CountMarquesAsync();

            var viewBag = new Dictionary<string, object>
            {
                { "nbrPerPage", pageSize },
                { "TotalCount", total },
                { "nbrLinks", (int)Math.Ceiling((double)total / pageSize) },
                { "position", position }
            };

            return Ok(new ApiResponse
            {
                Data = marques,
                ViewBag = viewBag,
                IsSuccess = true,
                Message = "Marques retrieved successfully.",
                StatusCode = 200
            });
        }

        // Récupère une marque par son ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetMarque(int id)
        {
            var marque = await _marqueService.GetMarqueByIdAsync(id);
            if (marque == null)
            {
                return NotFound(new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = "Marque not found.",
                    StatusCode = 404
                });
            }

            return Ok(new ApiResponse
            {
                Data = marque,
                ViewBag = null,
                IsSuccess = true,
                Message = "Marque retrieved successfully.",
                StatusCode = 200
            });
        }
    }
}
