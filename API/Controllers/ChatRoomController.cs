using System.Threading.Tasks;
using API.Dtos;
using API.Repositories.UoW;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomService _chatRoomService;
        public ChatRoomController(IChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;

        }

        [HttpGet]
        public async Task<IActionResult> GetChatRooms()
        {
            var response = await _chatRoomService.GetRooms();
            return Ok(response);
        }

        [HttpGet("{chatRoomId}", Name = "GetRoom")]
        public async Task<IActionResult> GetChatRoom(int chatRoomId)
        {
            var response = await _chatRoomService.GetRoomById(chatRoomId);
            return Ok(response);
        }


        [HttpPost("CreateRoom")]
        public async Task<IActionResult> SaveChatRoom(CreateRoomDto chatRoom)
        {
            await _chatRoomService.CreateRoom(chatRoom);
            return StatusCode(201);
        }

        [HttpPost("JoinChat")]
        public async Task<IActionResult> JoinChatRoom(JoinChatRoomDto joinChatRoomDto)
        {
            await _chatRoomService.JoinRoom(joinChatRoomDto);
            return StatusCode(201);
        }

        [HttpGet("RoomMembers")]
        public async Task<IActionResult> GetRoomMembers(int chatRoomId)
        {
            var response = await _chatRoomService.GetChatRoomMembers(chatRoomId);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult DeleteRooms()
        {
            _chatRoomService.DeleteRooms();
            return StatusCode(200);
        }
    }
}