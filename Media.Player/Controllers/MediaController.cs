using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Media.Player.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Media.Player.Domain;
using Microsoft.EntityFrameworkCore;

namespace Media.Player.Controllers
{
    public class MediaController : Controller
    {
        private MediaPlayerContext _context;

        public MediaController(MediaPlayerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var newSong = new MediaMetadata
            {
                Title = "Never going to give you up"
            };

            await _context.MediaMetadata.AddAsync(newSong);

            await _context.SaveChangesAsync();

            ViewBag.SongTitle = await _context.MediaMetadata.Select(x => x.Title).Where(x => x.Contains("Never")).FirstOrDefaultAsync();

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName);


            if (file == null || file.Length == 0)
                return Content("file not selected");

            if (extension == ".mp3" || extension == ".wma")
            {
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot/media/audio",
                    file.FileName);


                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            else if (extension == ".mp4" || extension == ".ogg")
            {
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot/media/video",
                    file.FileName);


                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
