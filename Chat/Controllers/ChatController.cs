using Chat.Chat;
using Chat.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ApplicationDbContext _context;        

        public ChatController(ApplicationDbContext context)
        {
            _context = context;            
        }

        [HttpPost]
        [Route("create-group")]
        public async Task<IActionResult> CreateGroup([FromBody] Group model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                var json = new
                {
                    GroupId = model.Id
                };
                return Ok(new Response { Status = "Success", Message = "Group created successfully!", Data = json });
            }
            return Ok(new Response { Status = "Error", Message = "Group creation failed! Please try again" });
        }

        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> SendMessage([FromBody] Message model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();                                
                return Ok(new Response { Status = "Success", Message = "Message sent successfully!" });
            }
            return Ok(new Response { Status = "Error", Message = "Message sent failed! Please try again" });
        }

        [HttpPost]
        [Route("receive")]
        public IActionResult ReceiveMessage([FromBody] Request model)
        {                        
            if (ModelState.IsValid)
            {
                //var messages = _context.Chat.Where(r => r.From.Equals(model.From) && r.To.Equals(model.To) || r.To.Equals(model.From) && r.From.Equals(model.To));
                var messages = from d in _context.Chat where d.From == model.From && d.To == model.To || d.From == model.To && d.To == model.From select d;
                var result = messages.ToList();
                var json = JsonSerializer.Serialize(result);
                return Ok(new Response { Status = "Success", Message = "Messages request successfully!", Data = json });
            }
            return Ok(new Response { Status = "Error", Message = "Messages request failed! Please try again" });
        }
    }
}
