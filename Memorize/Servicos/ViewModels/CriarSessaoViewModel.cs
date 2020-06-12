using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Servicos.ViewModels
{
    public class CriarSessaoViewModel
    {
        [Required]
        public int Fase { get; set; }

        [Required]
        public int[] SequenciaCorreta { get; set; }

    }
}
