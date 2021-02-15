using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ChatApp.Constants;
using ChatApp.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ChatApp.ViewComponents
{
    public class RoomViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var chatRooms = new List<ChatRoomDto>();
             using (var httpClient = new HttpClient())
             {                
 
                using (var response = await httpClient.GetAsync(WebApiRouteContans.GetRoomsUrl))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        chatRooms = JsonConvert.DeserializeObject<List<ChatRoomDto>>(apiResponse);
                    }                  
                }
            }
            
            return View(chatRooms);
        }
    }
}