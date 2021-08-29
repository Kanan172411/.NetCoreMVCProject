using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Models
{
    public class Setting
    {
        public int Id { get; set; }
        [StringLength(maximumLength:100)]
        public string HeaderLogo { get; set; }
        [StringLength(maximumLength: 50)]
        [Required]
        public string ContactPhone1 { get; set; }
        [StringLength(maximumLength: 100)]
        public string FooterLogo { get; set; }
        [Required]
        [StringLength(maximumLength: 300)]
        public string FooterDescription { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Address { get; set; }
        [StringLength(maximumLength: 50)]
        [Required]
        public string ContactPhone2 { get; set; }
        [StringLength(maximumLength: 50)]
        [Required]
        public string Website { get; set; }
        [StringLength(maximumLength: 50)]
        [Required]
        public string Email { get; set; }
        [StringLength(maximumLength: 300)]
        [Required]
        public string FirstBannerTextPart { get; set; }
        [StringLength(maximumLength: 300)]
        [Required]
        public string SecondBannerText { get; set; }
        [StringLength(maximumLength: 300)]
        [Required]
        public string BannerinAboutText { get; set; }
        [StringLength(maximumLength:500)]
        public string BannerImage { get; set; }
        [NotMapped]
        public IFormFile BannerImageFile { get; set; }
        [NotMapped]
        public IFormFile FooterImage { get; set; }
        [NotMapped]
        public IFormFile HeaderImage { get; set; }

    }
}
