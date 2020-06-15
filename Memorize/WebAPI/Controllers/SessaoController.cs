using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominios.Classes;
using Dominios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicos.ViewModels;

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

        [HttpPost]
        public IActionResult CriarSessao(CriarSessaoViewModel sessao)
        {

            try
            {
                string SequenciaGerada = "";
                for (int i = 0; i < sessao.SequenciaCorreta.Length; i++)
                {

                    SequenciaGerada += sessao.SequenciaCorreta[i];
                    if (sessao.SequenciaCorreta.Length -1 != i)
                    {
                        SequenciaGerada += ";";
                    }
                };

                Sessoes Sessao = new Sessoes() {
                    Fase = sessao.Fase,
                    SequenciaCorreta = SequenciaGerada,
                    SequenciaRecebida = "",
                    Errou = false,
                    PassarDeFase = false
                };

                var resultado = _sessaoRepositorio.criarSessao(Sessao);

                if (resultado == true) return Ok();
                else BadRequest();
            }

            catch (Exception ex)
            {
                return BadRequest();
                throw;
            }
            return BadRequest(new { sucesso = false, mensagem = ex.Message });
        }


        [HttpGet]
        public IActionResult ObterStatus()
        {
            try
            {
                var sessao = _sessaoRepositorio.obterStatus();
                if (sessao == null) return BadRequest(new { sucesso = false, mensagem = "Não existe uma sessão" });
                return Ok(sessao);
            }
            catch (Exception ex)
            {
                return BadRequest(new { sucesso = false, mensagem = ex.Message });
            }
        }

    }
}