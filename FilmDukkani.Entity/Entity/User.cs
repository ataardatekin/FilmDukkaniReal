using FilmDukkani.Entity.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.Entity.Entity
{
    public class User:IdentityUser
    {
        public User()
        {
            CreatedDate = DateTime.Now;
        }
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool IsEmployee { get; set; }
        public string Address { get; set; }

        public DateTime? CreatedDate { get; set; }
        public decimal? MembershipTotalPayment { get; set; }

        public int MovieChange { get; set; }
        public int MovieNumber { get; set; }


        public List<CreditCard> CreditCards { get; set; } = new List<CreditCard>();


        [ForeignKey("Membership")]
        public int MembershipId { get; set; }
        public Membership Membership { get; set; }



        public List<Order> Orders { get; set; } = new List<Order>();




    }
}
