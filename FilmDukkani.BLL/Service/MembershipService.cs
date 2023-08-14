using FilmDukkani.BLL.AbstractService;
using FilmDukkani.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmDukkani.BLL.Service
{
    public class MembershipService : IMembershipService
    {
        private readonly FilmDukkaniContext _context;

        public MembershipService(FilmDukkaniContext context)
        {
            _context = context;
        }

        public Dictionary<string, decimal> CalculateTotalRevenueByMembershipType()
        {
            var revenueByMembershipType = new Dictionary<string, decimal>();

            var users = _context.Users.Include(u => u.Membership).ToList();

            foreach (var user in users)
            {
                var membership = user.Membership;

                if (membership != null && membership.Price > 0)
                {
                    if (revenueByMembershipType.ContainsKey(membership.Name))
                    {
                        revenueByMembershipType[membership.Name] += membership.Price;
                    }
                    else
                    {
                        revenueByMembershipType[membership.Name] = membership.Price;
                    }
                }
            }

            return revenueByMembershipType;
        }

        public decimal CalculateTotalRevenueForAllMemberships()
        {
            var totalRevenue = 0m;
            var users = _context.Users.Include(u => u.Membership).ToList();

            foreach (var user in users)
            {
                var membership = user.Membership;

                if (membership != null && membership.Price > 0)
                {
                    totalRevenue += membership.Price;
                }
            }

            return totalRevenue;
        }

        











    }
}
