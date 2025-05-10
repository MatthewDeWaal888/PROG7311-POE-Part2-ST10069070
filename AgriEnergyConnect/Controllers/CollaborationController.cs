using AgriEnergyConnect.Models.Tables;
using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgriEnergyConnect.Controllers
{
    public class CollaborationController : Controller
    {
        private readonly AppDbContext _context;

        public CollaborationController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Forum()
        {
            if(this.Request.Method == "POST")
            {
                if (this.Request.Headers["ActionName"] == "PostDiscussion")
                {
                    await PostDiscussion();

                    this.Response.StatusCode = 1;
                }
            }

            return View(_context.Discussion.ToArray());
        }

        private async Task PostDiscussion()
        {
            var reader = new StreamReader(this.Request.Body);
            string content = await reader.ReadToEndAsync();
            reader.Close();

            string? userName = string.Empty;
            
            if(Global.UserLoggedIn)
            {
                userName = Global.UserInfo != null ? ((dynamic)Global.UserInfo).UserName : string.Empty;
            }
            else
            {
                userName = "[Anonymous]";
            }

            Discussion discussion = new()
            {
                Content = content,
                UserName = userName
            };

            _context.Discussion.Add(discussion);
            await _context.SaveChangesAsync();
        }
    }
}
