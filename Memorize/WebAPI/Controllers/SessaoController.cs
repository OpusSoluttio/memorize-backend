using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessaoController : ControllerBase
    {

        private ISessao _sessaoRepositorio;

        public SessaoController(ISessao sessaoRepositorio)
        {
            _sessaoRepositorio = sessaoRepositorio;
        }

        public IActionResult teste()
        {
            return Ok("oi");
        }

    }
}