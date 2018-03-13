using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsServices.Models
{
    public class SearchModel
    {
        public string SearchString { get; set; }

        public PaggingModel Pagging { get; set; }
    }

    public class PaggingModel
    {
        public int TotalItems { get; set; }

        /// <summary>
        /// Current Page
        /// </summary>
        public int Current { get; set; }
        public int ItemPerPage { get; set; }
    }
}
