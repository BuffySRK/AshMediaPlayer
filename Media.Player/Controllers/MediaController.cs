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
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Media.Player.Storage;

namespace Media.Player.Controllers
{
    public class MediaController : Controller
    {
        private MediaPlayerContext _context;
        private IStorageService _mediaStorage;

        public MediaController(MediaPlayerContext context, IStorageService mediaStorage)
        {
            _context = context;
            _mediaStorage = mediaStorage;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("file not selected");            

            try
            {
                var url = await _mediaStorage.AddToStorage(file);

                var newMedia = new MediaMetadata
                {
                    MediaUrl = url,
                    MediaExtension = Path.GetExtension(file.FileName),
                    FileName = file.FileName,
                };

                await _context.MediaMetadata.AddAsync(newMedia);
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }            

            return RedirectToAction("index");
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
