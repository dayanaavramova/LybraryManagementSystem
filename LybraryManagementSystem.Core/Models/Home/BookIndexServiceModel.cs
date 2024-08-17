using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Models.Home
{
    public class BookIndexServiceModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
    }
}
