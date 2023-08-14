using FilmDukkani.Entity.Enum;
using FilmDukkani.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.Entity.Base
{
    public abstract class BaseEntity 
    {

        public BaseEntity()
        {
            IsActive = true;
            Status = Status.Created;
            CreatedDate = DateTime.Now;
            CreatedComputerName = System.Environment.MachineName;
            CreatedAdUsername = System.Environment.UserName;
            CreatedIpAddress = IpAddressFinder.GetHostName();
        }

        public int Id { get; set; }
        


        //Created
        public DateTime? CreatedDate { get; set; }
        [StringLength(255)]
        public string? CreatedComputerName { get; set; }
        [StringLength(255)]
        public string? CreatedAdUsername { get; set; }
        [StringLength(255)]
        public string? CreatedIpAddress { get; set; }

        //Updated
        public DateTime? UpdatedDate { get; set; }
        [StringLength(255)]
        public string? UpdatedComputerName { get; set; }
        [StringLength(255)]
        public string? UpdatedAdUsername { get; set; }
        [StringLength(255)]
        public string? UpdatedIpAddress { get; set; }
        public bool IsActive { get; set; }
        public Status Status { get; set; }

    }
}
