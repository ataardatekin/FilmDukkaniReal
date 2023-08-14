using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.Entity.Entity
{
    public class CreditCard
    {
        public int Id { get; set; }
        public string CreditCardNumber { get; set; }
        public string CardExpiryDate { get; set; }
        public string CvcCode { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
