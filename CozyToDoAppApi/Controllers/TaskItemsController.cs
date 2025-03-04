using CozyToDoAppApi.Data;
using CozyToDoAppApi.Models;
using CozyToDoAppApi.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace CozyToDoAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        private readonly DataContext _data;

        public TaskItemsController(DataContext data)
        {
            _data = data;
        }

        [HttpGet("get-tasks")]
        public async Task<IActionResult> GetTaskList()
        {
            var tasks = await _data.TaskItems
                    .OrderByDescending(t => t.CreatedAt)
                    .ToListAsync();

            return Ok(tasks);
        }

        [HttpGet("task/{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _data.TaskItems.FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(CreateTaskItemDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                CreatedAt = DateTime.Now
            };

            _data.TaskItems.Add(task);
            await _data.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskById), new { Id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask (int id, UpdateTaskItemDto updateTaskDto)
        {
            var task = await _data.TaskItems.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            task.Title = updateTaskDto.Title;
            task.Description = updateTaskDto.Description;
            task.IsCompleted = updateTaskDto.IsCompleted;
            task.UpdatedAt = DateTime.Now;

            await _data.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskById), new { Id = id }, task);
        }

        [HttpDelete("deleteTask/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _data.TaskItems.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _data.TaskItems.Remove(task);
            await _data.SaveChangesAsync();

            return Ok("Task removed.");
        }
       
    }
}
