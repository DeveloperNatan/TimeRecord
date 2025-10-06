using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegistrarPonto.Models;
using RegistrarPonto.Services;

// namespace RegistrarPonto.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class markingsController : ControllerBase
//     {

//         private readonly MarkingsService _markingsService;

//         public markingsController(MarkingsService markingsservice)
//         {
//             _markingsService = markingsservice;
//         }

//         [HttpPost]
//         public async Task<IActionResult> CreateUser(Marking markings)
//         {
//             var CreateUserService = await _markingsService.FindAll();
//             return Ok(CreateUserService);
//         }
//     }
// }
