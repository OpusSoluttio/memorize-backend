using Dominios.Classes;
using Servicos.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominios.Interfaces
{
    public interface ISessao
    {
        ObterStatusViewModel obterStatus();

        bool criarSessao(Sessoes SessaoRecebida);

        bool adicionarNaSequencia(int Recebido);

        bool deletarSessao(int id);

        bool existeSessao();

    }
}
