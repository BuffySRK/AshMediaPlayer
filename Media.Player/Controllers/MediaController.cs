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
using System.Net;

namespace Media.Player.Controllers
{
    public class MediaController : Controller
    {
        private MediaPlayerContext _context;

        public MediaController(MediaPlayerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, MediaModel model)
        {
            string extension = Path.GetExtension(file.FileName);

            if (file == null || file.Length == 0)
                return Content("file not selected");
            else
            {
                var path = $@"wwwroot\media\{file.FileName}";

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            var newMedia = new MediaMetadata
            {
                MediaUrl = WebUtility.HtmlEncode($"/media/{file.FileName}"),
                MediaExtension = Path.GetExtension(file.FileName),
                FileName = file.FileName,
                Information = model.Information,
                MediaArtUrl = model.MediaArtUrl
            };

            await _context.MediaMetadata.AddAsync(newMedia);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> GetMediaUrl(int id)
        {
            var mediaMeta = await _context.MediaMetadata.Where(x => x.MediaMetadataId == id).Select(x => x.MediaUrl).FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(mediaMeta))
                return NotFound();

            return Ok(mediaMeta);       
        }

        public async Task<IActionResult> GetAllMedia()
        {
            return Ok(await _context.MediaMetadata.ToListAsync());
        }

        public class MediaViewModel
        {
            public MediaModel Media { get; set; }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
