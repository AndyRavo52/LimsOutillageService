using LimsOutillageService.Dtos;
using LimsOutillageService.Services;
using LimsUtils.Api; // Supposé existant, comme dans l'exemple
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsOutillageService.Controllers
{
    [ApiController]
    [Route("api/entree-outillages")]
    public class EntreeOutillageController : ControllerBase
    {
        private readonly IEntreeOutillageService _entreeOutillageService;

        // Injection du service via le constructeur
        public EntreeOutillageController(IEntreeOutillageService entreeOutillageService)
        {
            _entreeOutillageService = entreeOutillageService;
        }

        // Récupère le nombre total d'entrées d'outillage
        [HttpGet("total")]
        public async Task<ActionResult<ApiResponse>> GetTotalEntreeOutillages()
        {
            int total = await _entreeOutillageService.CountEntreeOutillagesAsync();
            return Ok(new ApiResponse
            {
                Data = total,
                ViewBag = null,
                IsSuccess = true,
                Message = "Total des entrées d'outillage récupéré avec succès.",
                StatusCode = 200
            });
        }

        // Récupère une liste paginée d'entrées d'outillage
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetEntreeOutillages(int position = 1, int pageSize = 5)
        {
            if (position < 1) position = 1;
            if (pageSize < 1) pageSize = 5;

            var entreeOutillages = await _entreeOutillageService.GetEntreeOutillagesAsync(position, pageSize);
            int total = await _entreeOutillageService.CountEntreeOutillagesAsync();
            var viewBag = new Dictionary<string, object>
            {
                { "nbrPerPage", pageSize },
                { "TotalCount", total },
                { "nbrLinks", (int)Math.Ceiling((double)total / pageSize) },
                { "position", position }
            };

            return Ok(new ApiResponse
            {
                Data = entreeOutillages,
                ViewBag = viewBag,
                IsSuccess = true,
                Message = "Entrées d'outillage récupérées avec succès.",
                StatusCode = 200
            });
        }

        // Récupère une entrée d'outillage par son ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetEntreeOutillage(int id)
        {
            try
            {
                var entreeOutillage = await _entreeOutillageService.GetEntreeOutillageByIdAsync(id);
                return Ok(new ApiResponse
                {
                    Data = entreeOutillage,
                    ViewBag = null,
                    IsSuccess = true,
                    Message = "Entrée d'outillage récupérée avec succès.",
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
                    Message = ex.Message, // Ex: "Entrée outillage non trouvée"
                    StatusCode = 404
                });
            }
        }

        // Crée une nouvelle entrée d'outillage
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateEntreeOutillage([FromBody] EntreeOutillageDto entreeOutillageDto)
        {
            try
            {
                var createdEntreeOutillage = await _entreeOutillageService.CreateEntreeOutillageAsync(entreeOutillageDto);
                return CreatedAtAction(
                    nameof(GetEntreeOutillage),
                    new { id = createdEntreeOutillage.IdEntreeOutillage },
                    new ApiResponse
                    {
                        Data = createdEntreeOutillage,
                        ViewBag = null,
                        IsSuccess = true,
                        Message = "Entrée d'outillage créée avec succès.",
                        StatusCode = 201
                    });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = ex.Message, // Ex: "L'outillage spécifié n'existe pas."
                    StatusCode = 400
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = $"Erreur lors de la création de l'entrée d'outillage : {ex.Message}",
                    StatusCode = 500
                });
            }
        }
    }
}