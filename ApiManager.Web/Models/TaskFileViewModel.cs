
using ApiManager.Logic.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ApiManager.Web.Models
{
    public class TaskFileViewModel
    {
        [Display(Name = "Shares")]
        public virtual IEnumerable<Share> Shares { get; set; }
        public ICollection<int> SelectedShares { get; set; }

        [Display(Name = "Formats")]
        public IEnumerable<SelectListItem> Formats { get; set; }
        public int[] SelectedFormats { get; set; }
    }
}