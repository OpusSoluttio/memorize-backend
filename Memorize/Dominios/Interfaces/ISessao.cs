using Dominios.Classes;
using Servicos.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominios.Interfaces
{
    public interface ISessao
    {
        Sessoes obterStatus();

        bool criarSessao(Sessoes SessaoRecebida);

        bool criarSessao(int Recebido);

        bool deletarSessao();

    }
}
