using Microsoft.AspNetCore.Mvc;
using System.IO;
using backendtask.Repo;
using backendtask.Model;

namespace backendtask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo repo;
        private const string ImageDirectory = @"C:\AImages\";

        public ProductController(IProductRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var _list = await this.repo.GetAll();
            if (_list != null)
            {
                return Ok(_list);
            }
            else
            {
                return NotFound();
            }
        }

        // Action to serve images
 [HttpGet("images/{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            var imagePath = Path.Combine(ImageDirectory, imageName);
            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound(); // Image not found
            }

            // Read the image file into a byte array
            var imageBytes = System.IO.File.ReadAllBytes(imagePath);

            // Return the image file in the response
            return File(imageBytes, "image/jpeg"); // Change content type accordingly
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            var createdProduct = await repo.Create(product);
            return Ok(createdProduct);
        }
    }
}
