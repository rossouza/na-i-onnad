using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurrasDaTrinca.DB.ViewModel
{
    public class DetailsVM
    {

        public ChurrascoVM churrasco { get; set; }

        public List<ChurrascoParticipanteVM> cpVMList { get; set; }

        public List<ParticipanteVM> partVMList { get; set; }

    }
}
