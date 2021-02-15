using System.Threading.Tasks;
using API.Dtos;
using API.Hubs;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller
    {

        private readonly IMessageService _messageService;
        private readonly IHubContext<ChatHub> _chat;

        public ChatController(IMessageService messageService, IHubContext<ChatHub> chat)
        {
            _chat = chat;
            _messageService = messageService;

        }

        [Route("{roomName}", Name = "send")]
        [HttpPost]
        public async Task<IActionResult> SendMessage(MessageDto messageDto, string roomName)
        {
            await _messageService.SendMessage(messageDto);
            await _chat.Clients.Group(roomName).SendAsync("MessageReceived", messageDto);
            return StatusCode(201);

        }

        [HttpGet("ChatRommMessages")]
        public async Task<IActionResult> GetChatRoomMessage(int chatRoom)
        {
            var response = await _messageService.GetChatRoomMessages(chatRoom);
            return Ok(response);
        }

        [HttpPost("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> JoinChatRoom(string connectionId, string roomName)
        {
            await _chat.Groups.AddToGroupAsync(connectionId, roomName);
            return Ok();
        }

        [HttpPost("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> LeaveCharRoom(string connectionId, string roomName)
        {
            await _chat.Groups.RemoveFromGroupAsync(connectionId, roomName);
            return Ok();
        }
    }
}