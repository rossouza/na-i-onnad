using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurrasDaTrinca.DB.ViewModel
{
    public class ChurrascoVM
    {
        public int? Id { get; set; }

        [DisplayName("Data")]
        [Required(ErrorMessage = "Selecionar uma data")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime Data { get; set; }

        [DisplayName("Razão")]
        [StringLength(50,ErrorMessage = "Digitar até 50 caracteres na razão")]
        [Required(ErrorMessage = "Você deve escrever uma razão")]
        public string Razao { get; set; }

        [DisplayName("Observação")]
        [StringLength(140,ErrorMessage = "Digitar até 140 caracteres na observação")]
        public string Obs { get; set; }
    }
}
