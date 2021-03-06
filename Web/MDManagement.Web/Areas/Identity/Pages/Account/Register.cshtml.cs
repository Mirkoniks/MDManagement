﻿namespace MDManagement.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using MDManagement.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    using static MDManagement.Common.DataValidations.Employee;
    using System.Runtime.CompilerServices;
    using MDManagement.Services.Data;
    using MDManagement.Services.Models.Town;
    using MDManagement.Services.Models.Address;
    using System.Security.Claims;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Employee> _signInManager;
        private readonly UserManager<Employee> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IAddressDataService addressDataService;
        private readonly ITownDataService townDataService;

        public RegisterModel(
            UserManager<Employee> userManager,
            SignInManager<Employee> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IAddressDataService addressDataService,
            ITownDataService townDataService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            this.addressDataService = addressDataService;
            this.townDataService = townDataService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Username")]
            public string UserName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Birth Date")]
            [DataType(DataType.Date)]
            public DateTime BirthDate { get; set; }

            [Required]
            [Display(Name = "First Name")]
            [StringLength(NameMaxLength)]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Middle Name")]
            [StringLength(NameMaxLength)]
            public string MiddleName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            [StringLength(NameMaxLength)]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Phone number")]
            public int PhoneNumber { get; set; }

            [Required]
            [Display(Name = "Town")]
            public string Town { get; set; }

            [Required]
            [Display(Name = "Post Code")]
            public int PostCode { get; set; }

            [Required]
            [Display(Name = "Address")]
            public string AddressText { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                //REGISTERING TOWN AND ADDREES
                //TO DO: 
                var createTownServiceModel = new CreateTownServiceModel()
                {
                    Name = Input.Town,
                    PostCode = Input.PostCode
                };

                townDataService.Create(createTownServiceModel);

                var townId = townDataService.FindByName(Input.Town).Id;

                var createAddressServiceModel = new CreateAddressServiceModel()
                {
                    AddressText = Input.AddressText,
                    TownId = townId
                };

                addressDataService.Create(createAddressServiceModel);

                var addressId = addressDataService.FindByName(Input.AddressText).AddressId;

                var user = new Employee
                {
                    UserName = Input.UserName,
                    Email = Input.Email,
                    Birthdate = Input.BirthDate,
                    FirstName = Input.FirstName,
                    MiddleName = Input.MiddleName,
                    LastName = Input.LastName,
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                      await _signInManager.SignInAsync(user, isPersistent: false);

                        //ADDING employee to an address


                        var addEmployeeToAddressServiceModel = new AddEmployeeToAddressServiceModel()
                        {
                            AddressId = addressId,
                            EmployeeId = user.Id
                        };

                        addressDataService.AddEmployeeToAddress(addEmployeeToAddressServiceModel);   

                        /////////////////////////////////////////
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
