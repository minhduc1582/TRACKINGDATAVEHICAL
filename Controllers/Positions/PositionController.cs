using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop_api.Controllers;
using eshop_api.Services.Positions;
using Microsoft.AspNetCore.Mvc;
using eshop_pbl6.Helpers.Identities;
using eshop_pbl6.Models.DTO.Positions;
using System.IdentityModel.Tokens.Jwt;
using eshop_api.Authorization;
using eshop_pbl6.Helpers.Common;

namespace eshop_pbl6.Controllers.Positions
{
    
    public class PositionController : BaseController
    {
        private readonly IPositionService _positionService;
        
        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpPost("add-position")]
        // [Authorize(EshopPermissions.UserPermissions.Add)]
        public async Task<IActionResult> AddPosition(CreateUpdatePosition createUpdatePosition)
        {
            try
            {
                string remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                //var serId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var username = jwtSecurityToken.Claims.First(claim => claim.Type == "nameid").Value;
                var result = await _positionService.AddPositionByUser(createUpdatePosition, username);
                return Ok(CommonReponse.CreateResponse(ResponseCodes.Ok, "thêm dữ liệu thành công", result));
            }
            catch(Exception ex)
            {
                return Ok(CommonReponse.CreateResponse(ResponseCodes.ErrorException, ex.Message, "null"));
            }  
        }

        [HttpGet("Get-Position")]
        [Authorize(EshopPermissions.UserPermissions.Get)]
        public async Task<IActionResult> GetPosition()
        {
            try
            {
                var result = await _positionService.GetPosition();
                return Ok(CommonReponse.CreateResponse(ResponseCodes.Ok, "thêm dữ liệu thành công", result));
            }
            catch(Exception ex)
            {
                return Ok(CommonReponse.CreateResponse(ResponseCodes.ErrorException, ex.Message, "null"));
            } 
        }

        [HttpGet("Get-Position-By-User")]
        [Authorize(EshopPermissions.UserPermissions.Get)]
        public async Task<IActionResult> GetPositionByUser()
        {
             try
            {
                string remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                //var serId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var username = jwtSecurityToken.Claims.First(claim => claim.Type == "nameid").Value;
                var result = await _positionService.GetPositionByUser(username);
                return Ok(CommonReponse.CreateResponse(ResponseCodes.Ok, "thêm dữ liệu thành công", result));
            }
            catch(Exception ex)
            {
                return Ok(CommonReponse.CreateResponse(ResponseCodes.ErrorException, ex.Message, "null"));
            } 
        }

        [HttpGet("Get-Position-By-Time")]
        [Authorize(EshopPermissions.UserPermissions.Get)]
        public async Task<IActionResult> GetPositionByTime(int day, int month)
        {
            try
            {
                var result = await _positionService.GetPositionByDate(day, month);
                return Ok(CommonReponse.CreateResponse(ResponseCodes.Ok, "thêm dữ liệu thành công", result));
            }
            catch(Exception ex)
            {
                return Ok(CommonReponse.CreateResponse(ResponseCodes.ErrorException, ex.Message, "null"));
            } 
        }

        [HttpGet("Get-Position-By-Date-Month")]
        [Authorize(EshopPermissions.UserPermissions.Get)]
        public async Task<IActionResult> GetPositionByDateMonth(DateTime begin, DateTime end)
        {
            try
            {
                int result1 = DateTime.Compare(begin, end);
                if(result1 > 0) return Ok(CommonReponse.CreateResponse(ResponseCodes.Ok, "ngày bắt đầu muộn hơn ngày kết thúc", result1));
                else if(result1 == 0)
                {
                    int day = begin.Day;
                    int month = begin.Month;
                    var result2 = await _positionService.GetPositionByDate(day, month);
                    return Ok(CommonReponse.CreateResponse(ResponseCodes.Ok, "thêm dữ liệu thành công", result2));
                }
                else
                {
                    var result = await _positionService.GetPositionByDateMonth(begin, end);
                    return Ok(CommonReponse.CreateResponse(ResponseCodes.Ok, "thêm dữ liệu thành công", result));
                }
            }
            catch(Exception ex)
            {
                return Ok(CommonReponse.CreateResponse(ResponseCodes.ErrorException, ex.Message, "null"));
            } 
        }
        [HttpGet("Daily-Report")]
        public async Task<IActionResult> GetDailyReport(DateTime date)
        {
            try
            {
                var result = await _positionService.DailyDriveStatistics(date);
                    return Ok(CommonReponse.CreateResponse(ResponseCodes.Ok, "thêm dữ liệu thành công", result));
            }
            catch(Exception ex)
            {
                return Ok(CommonReponse.CreateResponse(ResponseCodes.ErrorException, ex.Message, "null"));
            }
        }
        [HttpGet("Monthly-Report")]
        [Authorize(EshopPermissions.UserPermissions.Get)]
        public async Task<IActionResult> GetMonthlyReport(int month)
        {
            try
            {
                string remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                //var serId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var username = jwtSecurityToken.Claims.First(claim => claim.Type == "nameid").Value;
                var result = await _positionService.MonthlyDriveStatistics(username, month);
                    return Ok(CommonReponse.CreateResponse(ResponseCodes.Ok, "thêm dữ liệu thành công", result));
            }
            catch(Exception ex)
            {
                return Ok(CommonReponse.CreateResponse(ResponseCodes.ErrorException, ex.Message, "null"));
            }
        }
    }
}