using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoEmTresCamadas.Pizzaria.Mvc.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjetoEmTresCamadas.Pizzaria.Mvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public LoginController(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]       
        public IActionResult Index()
        {

            // If user is already authenticated, redirect to another page
            if (User.Identity.IsAuthenticated)
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

                if (role.Value == "simples")
                {
                    return RedirectToAction("Index", "Pizzas");
                }
                if (role.Value == "manager")
                {
                    return RedirectToAction("Index", "Admin");
                }
            }

            return View(new Login());
        }

        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to the login page or another page after logout
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> Index([Bind("email,password")]Login login)
        {
            if(ModelState.IsValid is false)
            {
                return View(login);
            }
            string Email = login.email;
            string Password = login.password;

            // Make a request to your authentication API to validate user credentials
            var apiEndpoint = _configuration["AuthenticationApiEndpoint"];
            var requestBody = $"{{\"email\": \"{Email}\", \"password\": \"{Password}\"}}";
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync(apiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    // Read the token from the API response
                    var tokenString = await response.Content.ReadAsStringAsync();

                    // Validate and decode the JWT token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.ReadToken(tokenString) as JwtSecurityToken;

                    if (token != null)
                    {

                        // Extract claims from the JWT token
                        var jwtClaims = token.Claims.ToList();
                        var nameClaim = jwtClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

                        if (nameClaim == null)
                        {
                            nameClaim = jwtClaims.FirstOrDefault(c => c.Type.Contains("name"));

                            jwtClaims.Add(new Claim(ClaimTypes.Name, nameClaim.Value));
                        }

                        var roleClaim = jwtClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

                        if (roleClaim == null)
                        {
                            roleClaim = jwtClaims.FirstOrDefault(c => c.Type.Contains("role"));

                            jwtClaims.Add(new Claim(ClaimTypes.Role, roleClaim.Value));
                        }


                        // Create claims identity
                        var claimsIdentity = new ClaimsIdentity(jwtClaims, JwtBearerDefaults.AuthenticationScheme);

                        // Create claims principal
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        // Set the JWT token as a cookie
                        var cookieOptions = new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true, // Set to true if using HTTPS
                            SameSite = SameSiteMode.Strict,
                            Expires = DateTime.Now.AddHours(1) // Set the expiration time as needed
                        };

                        Response.Cookies.Append("JwtCookie", tokenString, cookieOptions);

                        var authProperties = new AuthenticationProperties
                        {
                            // You can set additional properties if needed
                            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                            IsPersistent = true,
                        };

                        // Sign in the user with the combined claims
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

                        // Redirect to another page or return success
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Problema com api de autenticação.");
                        return View(login);
                    }
                }
                else
                {
                    // Authentication failed
                    ModelState.AddModelError(string.Empty, "Dados invalidos");
                    
                    return View(login);
                }
            }


        }

        public IActionResult Denied()
        {
            return View();
        }
    }
}
