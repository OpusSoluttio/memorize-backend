using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Servicos.ViewModels
{
    public class PassarFaseViewModel
    {

        [Required(ErrorMessage = "Informe a nova sequencia")]
        public int[] NovaSequencia { get; set; }

        [Required(ErrorMessage = "Informe a nova fase")]
        public int NovaFase { get; set; }

        [Required(ErrorMessage = "Informe o Id da sessao")]
        public int Id { get; set; }

    }
}
