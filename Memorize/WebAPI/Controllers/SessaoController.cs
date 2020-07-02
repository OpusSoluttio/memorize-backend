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

        /// <summary>
        /// Cria sessão
        /// </summary>
        /// <param name="sessao">Recebe o número da fase e a sequencia correta</param>
        /// <returns>Retorna Ok se conseguir criar ou bad request caso haja algum erro</returns>
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
                return BadRequest(new { sucesso = false, mensagem = ex.Message });
             
            }
            return BadRequest(new { sucesso = false, mensagem = "ocorreu um erro inesperado" });

        }


        /// <summary>
        /// Obter Status da Sessao Iniciada
        /// </summary>
        /// <returns>Retorna as informações da sessão iniciada caso tudo ocorra corretamente, caso não, retorna erro</returns>
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


        /// <summary>
        /// Adiciona o número recebido na sequência
        /// </summary>
        /// <param name="adicionarNaSequencia">Recebe o número para adicionar na sequência e uma senha</param>
        /// <returns>Retorna Ok em caso de sucesso ou Bad Request em caso de erro</returns>
        [HttpPut]
        public IActionResult AdicionarNaSequencia(AdicionarNaSequenciaViewModel adicionarNaSequencia)
        {
            if (adicionarNaSequencia.Senha != "memorizeAdd") return Unauthorized();

            if(_sessaoRepositorio.existeSessao() == false) return BadRequest(new { sucesso = false, mensagem = "Não existe uma sessão" });

            try
            {
                var resultado = _sessaoRepositorio.adicionarNaSequencia(adicionarNaSequencia.NumeroAdicionar);
                if (resultado == true) return Ok();
                else return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new { sucesso = false, mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Deleta a Sessão registrada no banco de dados
        /// </summary>
        /// <param name="deletarSessao">Recebe a senha</param>
        /// <returns>Retorna Ok em caso de sucesso ou Bad Request em caso de erro</returns>
        [HttpDelete]
        public IActionResult DeletarSessao(DeletarSessaoViewModel deletarSessao)
        {

            if (deletarSessao.Senha != "memorizeDelete") return Unauthorized();

            var sessao = _sessaoRepositorio.obterStatus();
            if (sessao == null) return BadRequest(new { sucesso = false, mensagem = "Não existe uma sessão" });

            try
            {
                var resultado = _sessaoRepositorio.deletarSessao(sessao.Id);
                if (resultado == true) return Ok();
                else return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpPut]
        [Route("passarfase")]
        public IActionResult PassarFase(PassarFaseViewModel passarFase)
        {
            if (_sessaoRepositorio.existeSessao() == false) return BadRequest(new { sucesso = false, mensagem = "Não existe uma sessão" });


            try
            {
                
                return Ok(_sessaoRepositorio.passarFase(passarFase));

            }
            catch (Exception ex)
            {
                return BadRequest(new { sucesso = false, mensagem = ex.Message });

            }

        }

    }
}