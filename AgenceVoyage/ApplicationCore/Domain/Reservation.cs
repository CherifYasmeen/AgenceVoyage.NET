using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Domain
{
    public class Reservation
    {
        [DataType(DataType.DateTime)]
        public DateTime DateReservation { get; set; }
        [Range(1,4,ErrorMessage ="valeur entre 0 et 4")]
        public int NbPersonnes { get; set; }
        public virtual Client Client { get; set; }
        [ForeignKey("Client")]
        public int ClientFk { get; set; }
        public virtual Pack Pack { get; set; }
        [ForeignKey("Pack")]
        public int PackFk { get; set; }
    }
}
