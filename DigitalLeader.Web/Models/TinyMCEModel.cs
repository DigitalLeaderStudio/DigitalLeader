using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DigitalLeader.Web.Models
{
    public class TinyMCEModel
	{
        [AllowHtml]
        [UIHint("tinymce_basic_compressed")]
        public string Basic { get; set; }
		
		[AllowHtml]
        [UIHint("tinymce_classic_compressed")]
        public string Classic { get; set; }
		
        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        public string Full { get; set; }
    }
}