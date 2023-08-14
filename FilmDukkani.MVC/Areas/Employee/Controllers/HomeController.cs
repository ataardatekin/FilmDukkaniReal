using FilmDukkani.BLL.AbstractService;
using FilmDukkani.DAL.Context;
using FilmDukkani.Entity.Entity;
using FilmDukkani.MVC.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace FilmDukkani.MVC.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class HomeController : Controller
    {
        private readonly IMembershipService _membershipService;
        private readonly FilmDukkaniContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(IMembershipService membershipService, FilmDukkaniContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _membershipService = membershipService;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            string Username = user.UserName;

            ViewBag.Username = Username;

            return View();
        }

        public IActionResult ShowRevenue()
        {
            var revenueByMembershipType = _membershipService.CalculateTotalRevenueByMembershipType();

            decimal totalRevenue = revenueByMembershipType.Values.Sum();
            ViewBag.TotalRevenueForAllMemberships = totalRevenue;


            return View(revenueByMembershipType);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {



            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = registerDTO.Username,
                    Email = registerDTO.MailAddress,
                    Firstname = registerDTO.FirstName,
                    Lastname = registerDTO.LastName,
                    Address = registerDTO.Address,
                    PhoneNumber = registerDTO.PhoneNumber,
                    MembershipId = 7,
                    IsEmployee = true,
                    MovieChange = 1,
                    MovieNumber = 1
                    
                };

                



                var result = await _userManager.CreateAsync(user, registerDTO.Password);

                if (result.Succeeded)
                {
                    await AssignUserRole(user, true);




                    var creditCard = new CreditCard
                    {
                        CreditCardNumber = HashCreditCardInfo("0123456789101112"),
                        CvcCode = HashCreditCardInfo("2111019876543210"),
                        CardExpiryDate = HashCreditCardInfo("1211109876543210"),
                        User = user
                    };



                    _context.CreditCards.Add(creditCard);
                    await _context.SaveChangesAsync();


                    return RedirectToAction("Home", "Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }


            return View(registerDTO);

        }









        private async Task AssignUserRole(User user, bool isEmployee)
        {

            user.IsEmployee = isEmployee;

            if (isEmployee)
            {
                if (!await _roleManager.RoleExistsAsync("Employee"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Employee"));
                }

                await _userManager.AddToRoleAsync(user, "Employee");
            }
        }





        private string HashCreditCardInfo(string card)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(card);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }


}





