using Dominios.Classes;
using Servicos.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominios.Interfaces
{
    public interface ISessao
    {
        /// <summary>
        /// Obtem o Status da sessão registrada no banco de dados
        /// </summary>
        /// <returns>Retorna o Status da sessão registrada no banco de dados</returns>
        ObterStatusViewModel obterStatus();

        /// <summary>
        /// Cria uma sessão no banco de dados
        /// </summary>
        /// <param name="SessaoRecebida">Recebe uma sessão com os dados preenchidos</param>
        /// <returns>Cria uma sessão no banco de dados</returns>
        bool criarSessao(Sessoes SessaoRecebida);

        /// <summary>
        /// Adiciona um número na sequência de uma sessão registrada no banco de dados
        /// </summary>
        /// <param name="Recebido">Recebe o número para adicionar na sequência</param>
        /// <returns>Retorna true em caso de sucesso ou false em caso de erro</returns>
        bool adicionarNaSequencia(int Recebido);

        /// <summary>
        /// Deleta a sessão registrada no bando de dados
        /// </summary>
        /// <param name="id">recebe o Id da sessão a ser deletada</param>
        /// <returns>Retorna Ok em caso de sucesso ou Bad Request em caso de erro</returns>
        bool deletarSessao(int id);

        /// <summary>
        /// Método para verificar se existe alguma sessão registrada no banco de dados
        /// </summary>
        /// <returns>Retorna Ok em caso de sucesso ou Bad Request em caso de erro</returns>
        bool existeSessao();

    }
}
