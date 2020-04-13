using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIDev.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIDev.Controller
{
    [Route("~/api/{Controller}/{Action}")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly DefaultDbContext _context;

        public StudentsController(DefaultDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("~/api/students/GetALL")]
        public IActionResult GetALL()
        {
            try
            {
                var list = new { error = 0, data = _context.Students.ToList() };

                return new JsonResult(list)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch(Exception ex)
            {
                var list = new { error = 1, message = ex.Message, stacktrace = ex.StackTrace };
                return new JsonResult(list) 
                { 
                    StatusCode = StatusCodes.Status500InternalServerError 
                };
            }
        }
        
        [HttpGet("{id}")]
        public IActionResult GetByID(int? id)
        { 
            try
            {
                var data = new { error = 0, obj = _context.Students.Find(id) };
                return new JsonResult(data) {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                var data = new { error = 0, message = ex.Message, details = ex.StackTrace.ToString() };
                return new JsonResult(data)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody]Student model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.Students.Add(model);
                await _context.SaveChangesAsync();
                var data = new { error = 0, message = "Successfully created", obj = model };
                return new JsonResult(data)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch(Exception ex)
            {
                var data = new { error = 1, message = ex.Message, trace = ex.StackTrace.ToString() };
                return new JsonResult(data)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody]Student model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                model.Id = id;
                _context.Update(model);
                await _context.SaveChangesAsync(); 
                var data = new { error = 0, message = "Successfully Updated", obj = model };
                return new JsonResult(data)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                var data = new { error = 1, message = ex.Message, trace = ex.StackTrace.ToString() };
                return new JsonResult(data)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            
        }

    }
}