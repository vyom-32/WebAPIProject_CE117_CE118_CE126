using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogManageentSystem.Data
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfUpload { get; set; }
    }
}