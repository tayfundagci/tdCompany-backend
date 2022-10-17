﻿using Microsoft.AspNetCore.Mvc;
using MovieApp.Dto;
using MovieApp.Entities;
using MovieApp.Message.Request;
using tdCompany.Entities;

namespace MovieApp.Controllers
{
    [Route("api/userdetail")]
    [ApiController]
    public class UserDetailControllerController : ControllerBase
    {
        private readonly IUserDetailRepository _userDetailRepo;

        public UserDetailControllerController(IUserDetailRepository userDetailRepo)
        {
            _userDetailRepo = userDetailRepo;
        }

        [AllowAnonymous]
        [HttpGet("{userDetailId}")]
        public async Task<IActionResult> GetUserDetail(Guid UserDetailId)
        {
            try
            {
                var userDetail = await _userDetailRepo.GetUserDetail(UserDetailId);

                if (userDetail == null)
                    return NotFound();

                var response = new Message.Response.UserDetailResponse()
                {
                    Code = 200,
                    Message = "Success",
                    UserDetail = userDetail
                };

                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUserDetail(UserDetailForCreationDto userDetail)
        {
            try
            {
                var createdUserDetail = await _userDetailRepo.CreateUserDetail(userDetail);
                var response = new Message.Response.UserDetailResponse()
                {
                    Code = 200,
                    Message = "A new user detail was created",
                    UserDetail = createdUserDetail
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
