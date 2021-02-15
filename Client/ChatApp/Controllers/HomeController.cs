using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Constants;
using ChatApp.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ChatApp.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index() => View();

        public async Task<IActionResult> Chat(int id)
        {
            var chatRoom = new ChatRoomDto();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(WebApiRouteContans.GetRoomById + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        chatRoom = JsonConvert.DeserializeObject<ChatRoomDto>(apiResponse);
                    }
                }
            }
            return View(chatRoom);
        }

        public async Task<IActionResult> CreateMessage(int chatId, string message)
        {
            var messageToSend = new MessageDto
            {
                Name = "Default",
                Text = message,
                Timestamp = DateTime.Now,
                ChatId = chatId
            };

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent
                (JsonConvert.SerializeObject(messageToSend), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(WebApiRouteContans.SendMessage + chatId, content))
                {
                    try
                    {
                        response.EnsureSuccessStatusCode();
                    }
                    catch (HttpRequestException ex)
                    {
                        throw ex;
                    }
                }

                return RedirectToAction("Chat", new {id = chatId});
            }
        }

        public async Task<IActionResult> CreateRoom(string name)
        {
            if (name == null) return RedirectToAction("index");

            var room = new CreateRoomDto
            {
                Name = name,
                isPublic = true,
                UserId = 1
            };

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent
                (JsonConvert.SerializeObject(room), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(WebApiRouteContans.CreateRoomUrl, content))
                {
                    try
                    {
                        response.EnsureSuccessStatusCode();
                    }
                    catch (HttpRequestException ex)
                    {
                        throw ex;
                    }
                }
            }
            return RedirectToAction("index");

        }
    }
}