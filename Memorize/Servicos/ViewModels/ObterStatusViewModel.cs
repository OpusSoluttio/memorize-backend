using System;
using System.Collections.Generic;
using System.Text;

namespace Servicos.ViewModels
{
    public class ObterStatusViewModel
    {
      
        public int Id { get; set; }

        public int Fase { get; set; }

        public int[] SequenciaCorreta { get; set; }

        public int[] SequenciaRecebida { get; set; }

        public bool Errou { get; set; }

        public bool PassarDeFase { get; set; }

    }
}
