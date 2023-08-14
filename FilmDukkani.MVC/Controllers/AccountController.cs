using FilmDukkani.DAL.Context;
using FilmDukkani.Entity.Base;
using FilmDukkani.Entity.Entity;
using FilmDukkani.MVC.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;
using FilmDukkani.MVC.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using FilmDukkani.BLL.AbstractService;

namespace FilmDukkani.MVC.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly FilmDukkaniContext _context;
        private readonly ILogger<AccountController> _logger;
        private readonly IOrderService _orderService;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            FilmDukkaniContext context,
            ILogger<AccountController> logger,
            IOrderService orderService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _logger = logger;
            _orderService = orderService;
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


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var memberships = _context.Memberships.ToList();
            ViewData["Memberships"] = memberships;
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }



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
                    MembershipId = registerDTO.MembershipId
                };

                var membership = _context.Memberships.FirstOrDefault(m => m.Id == registerDTO.MembershipId);
                if (membership != null)
                {
                    user.MovieChange = membership.MovieChange;
                    user.MovieNumber = membership.MovieNumber;
                }



                var result = await _userManager.CreateAsync(user, registerDTO.Password);

                if (result.Succeeded)
                {
                    await AssignUserRole(user, registerDTO.IsEmployee);


                    

                    var creditCard = new CreditCard
                    {
                        CreditCardNumber = HashCreditCardInfo(registerDTO.CreditCardNumber),
                        CvcCode = HashCreditCardInfo(registerDTO.CvcCode.ToString()),
                        CardExpiryDate = HashCreditCardInfo(registerDTO.CardExpiryDate),
                        User = user
                    };



                    _context.CreditCards.Add(creditCard);
                    await _context.SaveChangesAsync();


                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            

            return View(registerDTO);

        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    loginDTO.Username, loginDTO.Password, loginDTO.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in: {Username}", loginDTO.Username);
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    _logger.LogWarning("Failed login attempt for user: {Username}", loginDTO.Username);
                    ModelState.AddModelError("", "Geçersiz giriş denemesi.");
                    ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı.";
                }

            }


            

            return View(loginDTO);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public IActionResult AccessDenied()
        { 
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }


        [Authorize]
        public IActionResult MyOrders()
        {

            var userId = _userManager.GetUserId(User);
            var orders = _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderMovies)
                .ThenInclude(om => om.Movie)
                .ToList();

            var orderDTOs = orders.Select(order => new OrderDTO
            {
                OrderNumber = order.OrderNumber,
                UserId = order.UserId,
                UserName = _userManager.GetUserName(User),
                IsShipped = order.IsShipped,
                IsActive = order.IsActive,
                OrderDate = order.CreatedDate,
                Movies = order.OrderMovies.Select(orderMovie => new MovieDTO
                {
                    Id = orderMovie.Movie.Id,
                    MovieRealName = orderMovie.Movie.MovieRealName
                }).ToList()
            }).ToList();

            return View(orderDTOs);
        }





        [HttpGet]
        [Authorize]
        public IActionResult CancelOrder(string orderId)
        {
            var order = _orderService.GetOrderByOrderNumber(orderId);
            if (order != null && order.UserId == _userManager.GetUserId(User))
            {
                order.IsActive = false;
                _orderService.UpdateOrder(order);
            }

            return RedirectToAction("MyOrders");
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
