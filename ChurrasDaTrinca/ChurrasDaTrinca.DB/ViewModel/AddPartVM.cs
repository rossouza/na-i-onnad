using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurrasDaTrinca.DB.ViewModel
{
    public class AddPartVM
    {

        public int Id { get; set; }

        public int IdChurrasco { get; set; }

        [Required(ErrorMessage = "Favor selecionar um participante")]
        [DisplayName("Participante")]
        public int IdParticipante { get; set; }

        [DisplayName("Valor da Contribuição")]
        public decimal? Contribuicao { get; set; }

        [DisplayName("Bebida?")]
        public bool? CheckBebida { get; set; }

        public string strCheckBebida { get; set; }

        [DisplayName("Pago?")]
        public bool? CheckPago { get; set; }

        public string strCheckPago { get; set; }

        [StringLength(140, ErrorMessage = "Digitar até 140 caracteres na observação")]
        [DisplayName("Observação")]
        public string Obs { get; set; }

        public List<ParticipanteVM> partVMList { get; set; }

    }
}
