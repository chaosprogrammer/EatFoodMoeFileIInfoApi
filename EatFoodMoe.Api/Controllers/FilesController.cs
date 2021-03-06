﻿using EatFoodMoe.Api.Data;
using EatFoodMoe.Api.Entitles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.IO.File;


namespace EatFoodMoe.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IHostEnvironment _environment;
        private readonly FileDbContext _dbContext;

        public FilesController(IHostEnvironment environment, FileDbContext dbContext)
        {
            _environment = environment;
            _dbContext = dbContext;
        }

        [HttpPost("file")]
        public async Task<IActionResult> UpLoad([FromForm] EatFoodIFilesInfo eatFoodIFilesInfo)
        {
            string projectid = eatFoodIFilesInfo.ProjectId ?? Const.Default.ProjectId;
            if (Guid.TryParse(projectid, out var projectGuid) is false)
            {
                return Problem();
            }

            char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
            if (eatFoodIFilesInfo.File.FileName.Any( c => invalidFileNameChars.Any(ic => ic == c)))
            {
                return Problem();
            }

            using var stream = eatFoodIFilesInfo.File.OpenReadStream();
            var filePath = Path.Combine(_environment.ContentRootPath, "wwwroot", eatFoodIFilesInfo.File.FileName);
            using var file = Create(filePath);
            await stream.CopyToAsync(file);

            await _dbContext.EatFoodFiles.AddAsync(new EatFoodFile
            {
                Id = Guid.NewGuid(),
                Name = eatFoodIFilesInfo.File.FileName,
                FileSize = new FileInfo(filePath).Length,
                Path = filePath,
                LastModityTIme = DateTime.UtcNow,
                ProjectId = projectGuid
            });
            _ = await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("file")]
        public async Task<IActionResult> Download([FromQuery][Required] string id)
        {
            if (Guid.TryParse(id, out var fileGuid) is false)
            {
                return Problem();
            }

            EatFoodFile fileInfo = await _dbContext.EatFoodFiles.FirstOrDefaultAsync(f => f.Id.Equals(fileGuid));
            if (fileInfo is null)
            {
                return NotFound();
            }
            // var filePath = Path.Combine(_environment.ContentRootPath, "wwwroot", fileInfo.Name);

            var filePath = fileInfo.Path;
            if (Exists(filePath) is false)
            {
                return NotFound();
            }

            var fileExtensionContentTypeProvider= new FileExtensionContentTypeProvider();
            if (fileExtensionContentTypeProvider.TryGetContentType(
                Path.GetExtension(filePath), out var contextType) is false)
            {
                return NotFound();
            }

            return PhysicalFile(filePath, contextType);
        }

        [HttpDelete("file")]
        public async Task<IActionResult> DeleteFile([FromQuery][Required] string id)
        {
            if (Guid.TryParse(id, out var fileGuid) is false)
            {
                return Problem();
            }

            EatFoodFile fileInfo = await _dbContext.EatFoodFiles.FirstOrDefaultAsync(f => f.Id.Equals(fileGuid));
            if (fileInfo is null)
            {
                return NotFound();
            }

            _dbContext.EatFoodFiles.Remove(fileInfo);
            _ = await _dbContext.SaveChangesAsync();

            Delete(fileInfo.Path);
            return Ok();
        }
    }
}
