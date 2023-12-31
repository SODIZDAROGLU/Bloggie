﻿using Bloggie.Web.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace Bloggie.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
       
        private readonly IImageRepositary imageRepository;
        //Constructor  cominication to database
        public ImagesController(IImageRepositary imageRepository)
        {
            this.imageRepository = imageRepository;
        }


        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            //call a repository
           var imageURL = await imageRepository.UploadAsync(file);

            if (imageURL == null)
            {
                return Problem("Something went wrong", null, (int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult( new { link = imageURL });
        }

        
    }
}
