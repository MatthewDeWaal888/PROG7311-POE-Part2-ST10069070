using AgriEnergyConnect.Models.Tables;
using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AgriEnergyConnect.Controllers
{
    /// <summary>
    /// Callaboration Controller
    /// </summary>
    public class CollaborationController : Controller
    {
        // Data fields.
        private readonly AppDbContext _context;

        /// <summary>
        /// Master constructor.
        /// </summary>
        /// <param name="context"></param>
        public CollaborationController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Forum page.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Forum()
        {
            // Check if the request is a POST method.
            if(this.Request.Method == "POST")
            {
                // Check if the action is PostDiscussion.
                if (this.Request.Headers["ActionName"] == "PostDiscussion")
                {
                    // Wait for the operation to complete asynchronously.
                    await PostDiscussion();
                    // The operation succeeded.
                    this.Response.StatusCode = 1;
                }
            }

            return View(_context.Discussion.ToArray());
        }

        /// <summary>
        /// This method will post a discussion to the AgriEnergyConnect database.
        /// </summary>
        /// <returns></returns>
        private async Task PostDiscussion()
        {
            // Declare and instantiate a StreamReader object.
            var reader = new StreamReader(this.Request.Body);
            // Read the content asynchronously.
            string content = await reader.ReadToEndAsync();
            // Close the StreamReader object.
            reader.Close();

            // Variable declaration.
            string? userName = string.Empty;
            
            // Check if the user is logged in.
            if(Global.UserLoggedIn)
            {
                userName = Global.UserInfo != null ? ((dynamic)Global.UserInfo).UserName : string.Empty;
            }
            else
            {
                // The user is not logged in. Therefore, the user is anonymous.
                userName = "[Anonymous]";
            }

            // Create a discussion object.
            Discussion discussion = new()
            {
                Content = Convert.ToBase64String(Encoding.UTF8.GetBytes(content)),
                UserName = userName
            };

            // Add the discussion to the database.
            _context.Discussion.Add(discussion);
            // Save the changes asynchronously.
            await _context.SaveChangesAsync();
        }
    }
}
