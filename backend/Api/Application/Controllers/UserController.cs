using APITemplate.Domain.Entities;
using APITemplate.Dto;
using Api.Base.Pagination.Base;
using Api.Base.ExceptionFilter.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using APITemplate.Domain.Services.Interfaces;

namespace APITemplate.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IMapper mapper,
            Domain.Services.Interfaces.IUserService userService,
            ILogger<UserController> logger)
        {
            _mapper = mapper;
            _userService = userService;
            if (userService == null) logger.LogInformation("userservice: ", "NULL");
            _logger = logger;
        }

        [HttpGet]
        public string GetUserInfo(){
            string a = "";
            try
            {
                 a += $"\nname\t: {HttpContext.User.Identity.Name}";
            }
            catch (System.Exception)
            {
                
            }
           
            foreach (var claim in User.Claims)
            {
                a += $"\n{claim.Type}\t: {claim.Value}";
            }
            return a;
        }

        [HttpGet]
        public Task<IPage<User>> GetUserWithCondition([FromQuery] Pageable pageable, [FromQuery] UserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            return _userService.GetUserByCondition(user, pageable);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync([FromQuery] Pageable pageable)
        {
            if (pageable == null || pageable.PageSize == 0)
                throw new DataInvalidException("Dữ liệu phân trang không đúng");
            var users = await _userService.FindAll(pageable);
            var pageResult = users.Content.Select(x => _mapper.Map<UserDto>(x)).ToList();
            return Ok(pageResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var user = await _userService.FindOne(id);
            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserDto userDto)
        {
            if (userDto.Id != 0) throw new Exception();
            User user = _mapper.Map<User>(userDto);
            await _userService.Save(user);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserDto userDto)
        {
            if (userDto.Id == 0) throw new Exception();
            var user = _mapper.Map<User>(userDto);
            await _userService.Save(user);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] long id)
        {
            await _userService.Delete(id);
            return Ok();
        }
    }
}
