using FilmDukkani.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.BLL.AbstractService
{
    public interface IMembershipService
    {
        Dictionary<string, decimal> CalculateTotalRevenueByMembershipType();
        decimal CalculateTotalRevenueForAllMemberships();


    }
}
