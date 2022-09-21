using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Dtos
{
    public class CreateTopicPostDto
    {
        public string Body { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public string Thumbnail { get; set; }
    }
}