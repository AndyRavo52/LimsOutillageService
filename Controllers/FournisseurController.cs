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
    [Route("api/fournisseur")]
    public class FournisseurController : ControllerBase
    {
        private readonly IFournisseurService _fournisseurService;

        // Injection du service via le constructeur
        public FournisseurController(IFournisseurService fournisseurService)
        {
            _fournisseurService = fournisseurService;
        }

        // Récupère le nombre total de fournisseurs
        [HttpGet("total")]
        public async Task<ActionResult<ApiResponse>> GetTotalFournisseurs()
        {
            int total = await _fournisseurService.CountFournisseursAsync();
            return Ok(new ApiResponse
            {
                Data = total,
                ViewBag = null,
                IsSuccess = true,
                Message = "Total des fournisseurs récupéré avec succès.",
                StatusCode = 200
            });
        }

        // Récupère une liste paginée de fournisseurs
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetFournisseurs(int position = 1, int pageSize = 10)
        {
            if (position < 1) position = 1; // Position minimale : 1
            if (pageSize < 1) pageSize = 10; // Taille de page minimale : 1, valeur par défaut : 10

            var fournisseurs = await _fournisseurService.GetFournisseursAsync(position, pageSize);
            int total = await _fournisseurService.CountFournisseursAsync();

            var viewBag = new Dictionary<string, object>
            {
                { "nbrPerPage", pageSize },
                { "TotalCount", total },
                { "nbrLinks", (int)Math.Ceiling((double)total / pageSize) },
                { "position", position }
            };

            return Ok(new ApiResponse
            {
                Data = fournisseurs,
                ViewBag = viewBag,
                IsSuccess = true,
                Message = "Fournisseurs récupérés avec succès.",
                StatusCode = 200
            });
        }

        // Récupère un fournisseur par son ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetFournisseur(int id)
        {
            try
            {
                var fournisseur = await _fournisseurService.GetFournisseurByIdAsync(id);
                return Ok(new ApiResponse
                {
                    Data = fournisseur,
                    ViewBag = null,
                    IsSuccess = true,
                    Message = "Fournisseur récupéré avec succès.",
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
                    Message = ex.Message, // Ex: "Fournisseur non trouvé"
                    StatusCode = 404
                });
            }
        }
    }
}