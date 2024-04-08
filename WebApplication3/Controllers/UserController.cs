using MessageBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Contracts;
using WebApplication3.Data;
using WebApplication3.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMessagingBus _messagingBus;

        public UserController(IUserRepository userRepository, IConfiguration configuration, IMessagingBus messagingBus)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _messagingBus = messagingBus;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            return Ok(await _userRepository.GetAll());
        }

        [HttpPost("addUser")]
        public async Task<IActionResult> AddUser([FromBody]AddUserDTO userDto)
        {
           await _userRepository.CreateUserAsync(userDto);
            return Ok("User Created Successfully");
        }

        

        [HttpPost("emailrequest")]
        public async Task<IActionResult> EmailRequest([FromBody] AddUserDTO userDto)
        {
            await _messagingBus.PublishMessage(userDto, _configuration.GetValue<string>("TopicAndQueueNames:Emailnotificationservicequeuename"), _configuration["Emailnotificationserviceconnectionstring:ConnectionString"]);
            return Ok("Message successfully sent to the queue");
        }
    }
}
