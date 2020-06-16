using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Servicos.ViewModels
{
    public class CriarSessaoViewModel
    {
        [Required(ErrorMessage = "Informe o número da fase")]
        public int Fase { get; set; }

        [Required(ErrorMessage = "Informe o array com a sequencia correta")]
        public int[] SequenciaCorreta { get; set; }

    }
}
