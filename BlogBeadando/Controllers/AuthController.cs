using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Logging;
using BlogBeadando.Models;
using BlogBeadando.Services;
using System;

namespace BlogBeadando.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IMapper _mapper;
        private readonly AuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IMapper mapper, AuthService authService, ILogger<AuthController> logger)
        {
            _mapper = mapper;
            _authService = authService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult<string> Register(RegisterInputModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (userModel.Password != userModel.ConfirmedPassword)
                    {
                        return BadRequest("Passwords do not match!");
                    }
                    if (_authService.DoesUserExist(userModel.Username))
                    {
                        return BadRequest("Username already exists!");
                    }
                    var mappedModel = _mapper.Map<RegisterInputModel, User>(userModel);
                    mappedModel.Role = "User";

                    var user = _authService.RegisterUser(mappedModel);
                    if (user != null)
                    {
                        var token = _authService.GenerateJwtToken(user.Username, mappedModel.Role);
                        return Ok(token);
                    }
                    return BadRequest("Username or password are not correct!");
                }
                return BadRequest(ModelState);
            }
            catch (Exception error)
            {
                _logger.LogError(error.Message);
                return StatusCode(500);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult<string> Login(LoginInputModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_authService.IsAuthenticated(userModel.Username, userModel.Password))
                    {
                        var user = _authService.GetByUsername(userModel.Username);
                        var token = _authService.GenerateJwtToken(userModel.Username, user.Role);
                        return Ok(token);
                    }
                    return BadRequest("Username or password are not correct!");
                }
                return BadRequest(ModelState);
            }
            catch (Exception error)
            {
                _logger.LogError(error.Message);
                return StatusCode(500);
            }
        }
    }
}
