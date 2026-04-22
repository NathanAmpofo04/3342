using Microsoft.AspNetCore.Mvc;
using ProfileServiceAPI.Models;

namespace ProfileServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        ProfileManager pm = new ProfileManager();

        // 1. GET api/Profile/5
        [HttpGet("{id}")]
        public Account Get(int id)
        {
            return pm.GetProfile(id);
        }

        // 2. POST api/Profile (Main Update)
        [HttpPost]
        public bool Post([FromBody] Account updatedAccount)
        {
            if (updatedAccount == null) return false;
            return pm.UpdateProfile(updatedAccount);
        }

        // 3. PUT api/Profile/Visibility/5/true (Toggle Hide)
        [HttpPut("Visibility/{id}/{shouldHide}")]
        public bool SetVisibility(int id, bool shouldHide)
        {
            return pm.SetProfileVisibility(id, shouldHide);
        }

        // 4. POST api/Profile/UploadImage (Image Gallery)
        [HttpPost("UploadImage")]
        public bool UploadImage([FromBody] ImageUploadRequest img)
        {
            return pm.AddImageToGallery(img.UserId, img.Title, img.Data, img.Type, img.Length);
        }
    }

    // This helper class allows the API to receive the complex image data
    public class ImageUploadRequest
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public byte[] Data { get; set; }
        public string Type { get; set; }
        public long Length { get; set; }
    }
}