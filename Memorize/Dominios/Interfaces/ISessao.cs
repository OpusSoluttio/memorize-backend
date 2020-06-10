using Dominios.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominios.Interfaces
{
    public interface ISessao
    {
        Sessoes obterStatus();

        bool criarSessao( int[] SequenciaCorreta );

        bool criarSessao(int Recebido);

        bool deletarSessao();

    }
}
