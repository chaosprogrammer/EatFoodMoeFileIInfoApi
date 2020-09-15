﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EatFoodMoe.Api.Entitles
{
    public class EatFoodIFilesInfo
    {
        public string FileName { get; set; }

        [Required]
        public IFormFile File { get; set; }
}
}
