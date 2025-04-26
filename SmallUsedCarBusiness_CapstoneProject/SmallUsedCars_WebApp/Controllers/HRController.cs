using Microsoft.AspNetCore.Mvc;
using SmallUsedCars_WebApp.Service;
using SmallUsedCars_WebApp.Models;
using System;
using Microsoft.AspNetCore.Authorization;

namespace SmallUsedCars_WebApp.Controllers
{
    [Authorize]
    [Route("api/hr")]
    [ApiController]
    public class HRController : ControllerBase
    {
        private readonly HRService _hrService;

        public HRController(HRService hrService)
        {
            _hrService = hrService;
        }

        // ✅ 휴가 승인 (PUT)
        [HttpPut("approve-leave/{hrId}")]
        public IActionResult ApproveLeaveRequest(int hrId, [FromBody] string managerId)
        {
            try
            {
                _hrService.ApproveLeaveRequest(hrId, managerId);
                return Ok(new { message = "Leave request approved successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ✅ 휴가 거절 (PUT)
        [HttpPut("deny-leave/{hrId}")]
        public IActionResult DenyLeaveRequest(int hrId, [FromBody] string managerId)
        {
            try
            {
                _hrService.DenyLeaveRequest(hrId, managerId);
                return Ok(new { message = "Leave request denied successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
