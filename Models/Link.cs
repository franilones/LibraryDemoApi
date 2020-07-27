using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryDemoApi.Models
{
    public class Link
    {
        //Link
        public string Href { get; private set; }
        //Description
        public string Rel { get; private set; }
        //Http Action
        public string Mehthod { get; private set; }
        public Link(string href, string rel, string mehthod)
        {
            Href = href;
            Rel = rel;
            Mehthod = mehthod;
        }
    }
}
