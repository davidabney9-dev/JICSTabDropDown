using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNTRTabDropDown.Entities
{
    public class SubPageList
    {
        public bool Success { get; set; }
        public List<SubPage> SubPages { get; set; }

        public SubPageList(bool success, List<SubPage> subpages)
        {
            this.Success = success;
            this.SubPages = subpages;
        }
    }

    public class SubPage
    {
        public string Name { get; set; }
        public string URL { get; set; }
    }
}