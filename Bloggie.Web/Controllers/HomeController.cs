using Bloggie.Web.Models;
using Bloggie.Web.Models.viewModels;
using Bloggie.Web.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bloggie.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public HomeController(ILogger<HomeController> logger,ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            _logger = logger;
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }

        public async Task<IActionResult> Index()
        {
            //getting all blogs
            var blogPosts = await blogPostRepository.GetAllAsync();

            //getting all tags
            var tags = await tagRepository.GetAllAsync();

            var model = new HomeViewModel
            {
                BlogPosts = blogPosts,
                Tags = tags
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}