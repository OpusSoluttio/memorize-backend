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
        [HttpGet]
        [Route("arduino/{id}")]
        public IActionResult AdicionarNaSequencia(int id)
        {
            
            if(_sessaoRepositorio.existeSessao() == false) return BadRequest(new { sucesso = false, mensagem = "Não existe uma sessão" });

            try
            {
                var resultado = _sessaoRepositorio.adicionarNaSequencia(id);
                if (resultado == true) return Ok();
                else return BadRequest("Ocorreu um erro inesperado, verifique se a sessão não terminou.");
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

        /// <summary>
        /// Edita os dados da sessão mantendo o ip
        /// </summary>
        /// <param name="passarFase">Recebe a nova sequencia, numero da nova fase e id</param>
        /// <returns>Retorna Ok com os dados da nova sessao ou BadRequest com descrição do erro.</returns>
        [HttpPut]
        [Route("passarfase")]
        public IActionResult PassarFase(PassarFaseViewModel passarFase)
        {
            var a = passarFase;
            if (_sessaoRepositorio.existeSessao() == false) return BadRequest(new { sucesso = false, mensagem = "Não existe uma sessão" });

            try
            {
                var sessao = _sessaoRepositorio.passarFase(passarFase);   
                return Ok(sessao);
            }
            catch (Exception ex)
            {
                return BadRequest(new { sucesso = false, mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Requisição de teste
        /// </summary>
        /// <param name="inf">Recebe qualquer tipo de dado</param>
        /// <returns>Retorna ok com o que recebeu</returns>
        [HttpGet]
        [Route("teste")]
        public IActionResult Teste([FromQuery(Name = "inf")] string inf)
        {
            if (inf != null)
            {
                return Ok(inf);
            }
            else
            {
                return BadRequest("erro");
            }
        }

    }
}