using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.viewModels;
using Bloggie.Web.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        public ITagRepository tagRepository { get; }

        //Constructer injection
        public AdminTagsController(ITagRepository tagRepository )
        {
           this.tagRepository = tagRepository;
        }
        [HttpGet]
        //page show.display the page back to the user.displaying a static html page.
        public IActionResult Add()
        {
            return View();
        }

        //[HttpPost]
        ////Capture the details and post the tag to save to the database
        //public IActionResult Add(AddTagRequest addTagRequest)
        //{
        //    //Mapping AddTagRequest to Tag domain model
        //    var tag = new Tag
        //    {
        //        Name = addTagRequest.Name,
        //        DisplayName = addTagRequest.DisplayName,
        //    };

        //    bloggieDbContext.Tags.Add(tag);
        //    bloggieDbContext.SaveChanges();
            
        //    return RedirectToAction("List");//after click submit button redirect to blog list page
        //}

        [HttpPost]
        //Capture the details and post the tag to save to the database
        //Asynchronous
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //Mapping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            await tagRepository.AddAsync(tag);
            //moved to TagRepository
           //await bloggieDbContext.Tags.AddAsync(tag);
           //await bloggieDbContext.SaveChangesAsync();

            return RedirectToAction("List");//after click submit button redirect to blog list page
        }

        //[HttpGet]
        //[ActionName("List")]//for redirecting to list page
        ////read
        //public IActionResult List()
        //{
        //    //use dbContext to read the tags
        //   var tags = bloggieDbContext.Tags.ToList();
        //    return View(tags);
        //}

        [HttpGet]
        [ActionName("List")]//for redirecting to list page
        //read  //Asynchronous
        public async Task<IActionResult> List()
        {
            //use dbContext to read the tags
            //var tags = await bloggieDbContext.Tags.ToListAsync();
            var tags = await tagRepository.GetAllAsync();
            return View(tags);
        }

        //[HttpGet]
        ////Edit
        //public IActionResult Edit(Guid id)
        //{
        //    //First Method
        //    // var tag = bloggieDbContext.Tags.Find(id);
        //    //Second Method
        //    var tag = bloggieDbContext.Tags.FirstOrDefault(x => x.Id == id);

        //    if (tag != null) 
        //    {
        //        var editTagRequest = new EditTagRequest { Id = tag.Id, Name = tag.Name, DisplayName = tag.DisplayName };
        //        return View(editTagRequest);
        //    }
        //    return View(null);
        //}
        [HttpGet]
        //Edit //Asynchronous
        public async Task<IActionResult> Edit(Guid id)
        {
            //First Method
            //var tag = await bloggieDbContext.Tags.FindAsync(id);
            //Second Method
            //var tag = await bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);

            var tag = await tagRepository.GetAsync(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest { Id = tag.Id, Name = tag.Name, DisplayName = tag.DisplayName };
                return View(editTagRequest);
            }
            return View(null);
        }
        //[HttpPost]
        ////update
        //public IActionResult Edit(EditTagRequest editTagRequest)
        //{
        //    var tag = new Tag
        //    {
        //        Id = editTagRequest.Id,
        //        Name = editTagRequest.Name,
        //        DisplayName = editTagRequest.DisplayName,
        //    };

        //    var existingTag = bloggieDbContext.Tags.Find(tag.Id);

        //    if (existingTag !=null)
        //    {
        //        existingTag.Name = tag.Name;
        //        existingTag.DisplayName = tag.DisplayName;

        //        //save
        //        bloggieDbContext.SaveChanges();
        //        //Show success notification
        //        return RedirectToAction("Edit", new { id = editTagRequest.Id });
        //    }
        //    //show failure notification
        //    return RedirectToAction("Edit", new { id = editTagRequest.Id });
        //}
        [HttpPost]
        //update//Asynchronous
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);

            if (updatedTag != null)
            {
                //success
            }
            else
            {
                //error
            }
            
            
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
        [HttpPost]
        //Delete
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if (deletedTag != null)
            {
                //show success
                return RedirectToAction("List");
            }
            else
            {
                //show error
            }

            //show an error notification
            return RedirectToAction("Edit", new {id = editTagRequest.Id});
        }
    }
}
