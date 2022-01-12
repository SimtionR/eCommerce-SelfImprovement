﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Self_Improve_eCommerce.Configuration;
using Self_Improve_eCommerce.Models.DomainObjects;
using Self_Improve_eCommerce.Models.RequestObjects;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.Controllers
{
    public class IdentityController :ApiController
    {
        private readonly UserManager<User> userManager;
        private readonly IOptions<ApplicationSettings> appSettings;

        public IdentityController(UserManager<User> userManager, IOptions<ApplicationSettings> appSettings)
        {
            this.userManager = userManager;
            this.appSettings = appSettings;
        }
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterRequestModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if(result.Succeeded)
            {
                return Ok();
            }


            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login(LoginRequestModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.UserName);
            if(user == null)
            {
                return Unauthorized();
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);

            if(!passwordValid)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.appSettings.Value.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)


            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }
    }
}