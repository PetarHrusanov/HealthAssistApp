namespace HealthAssistApp.Web.ViewModels.Administration.AdminitrationTextFilesViewModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using System.Text.RegularExpressions;

    using Ganss.XSS;
    using HealthAssistApp.Data.Models;
    using HealthAssistApp.Services.Mapping;

    public class AdminTextFilesDetailsViewModels : IMapFrom<TextFilesForAdministration>
    {
        public int Id { get; set; }

        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Modified On")]
        public DateTime ModifiedOn { get; set; }

        [DisplayName("Deleted On")]
        public DateTime DeletedOn { get; set; }

        [DisplayName("Is Deleted")]
        public bool IsDeleted { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(8000)]
        public string Content { get; set; }

        [DisplayName("Content")]
        public string SanitizedContent
           => new HtmlSanitizer().Sanitize(this.Content);

        [DisplayName("Content")]
        public string ShortContent
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Content, @"<[^>]+>", string.Empty));
                return content.Length > 300
                        ? content.Substring(0, 300) + "..."
                        : content;
            }
        }
    }
}
