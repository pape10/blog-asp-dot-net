using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using blog.Models;
using Microsoft.AspNetCore.Hosting;
using blog.Services;
using blog.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace blog.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class HomeController : Controller
    {
        private IHostingEnvironment _environment;
        private IBlogData _blogData;

        public HomeController(IBlogData blogData, IHostingEnvironment environment)
        {
            _environment = environment;
            _blogData = blogData;
        }
        [Route("")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = _blogData.GetAll();
            return View(model);
        }
        [Route("[action]/{id}")]
        [AllowAnonymous]
        public IActionResult Detail(int id)
        {
            var model = _blogData.Get(id);
            if (model != null)
            {
                return View(model);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
        [Route("[action]")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Route("[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogEditModel blog)
        {
            if (ModelState.IsValid)
            {
                var newBlog = new Blog();
                var files = HttpContext.Request.Form.Files;
                //return files[0].FileName;
                var uploads = Path.Combine(_environment.WebRootPath, "uploads\\img\\");


                var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(files[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                {
                    await files[0].CopyToAsync(fileStream);
                    newBlog.Image = fileName;
                }

                newBlog.Name = blog.Name;
                newBlog.Details = blog.Details;
                newBlog.Author = blog.Author;

                newBlog = _blogData.Add(newBlog);
                return RedirectToAction(nameof(Detail), new { id = newBlog.Id });
            }
            else
            {
                return View();
            }
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _blogData.Get(id);
            if (model != null)
                return View(model);
            else
                return RedirectToAction(nameof(Index));

        }
        [Route("[action]/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogEditModel blog, int id, string Imageprev)
        {

            if (ModelState.IsValid)
            {
                var newBlog = new Blog();
                
                var files = HttpContext.Request.Form.Files;
                //return files[0].FileName;
                if (files[0].Length > 0)
                {
                    var uploads = Path.Combine(_environment.WebRootPath, "uploads\\img\\");



                    System.IO.File.Delete(Path.Combine(_environment.WebRootPath, "uploads\\img\\" + Imageprev));



                    var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(files[0].FileName);
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    {
                        await files[0].CopyToAsync(fileStream);
                        newBlog.Image = fileName;
                    }
                }
                else
                {
                    newBlog.Image = Imageprev;
                }
                newBlog.Id = id;
                newBlog.Name = blog.Name;
                newBlog.Details = blog.Details;
                newBlog.Author = blog.Author;
                newBlog = _blogData.Update(newBlog);
                return RedirectToAction(nameof(Detail), new { id = newBlog.Id });
            }
            else
            {
                //return "no";
                return View();
            }
        }
        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult Delete(int id)
        {
            return RedirectToAction(nameof(ConfirmDelete), new { BlogId = id });
        }
        [HttpGet]
        [Route("[action]/{BlogId}")]
        public IActionResult ConfirmDelete(int BlogId)
        {
            var model = BlogId;
            return View(model);
        }
        [HttpGet]
        [Route("[action]/{Id}")]
        public IActionResult SaveDelete(int Id)
        {
            
            string s = _blogData.Delete(Id);
            System.IO.File.Delete(Path.Combine(_environment.WebRootPath, "uploads\\img\\" + s));
            return RedirectToAction(nameof(Index));
        }
    }
}
