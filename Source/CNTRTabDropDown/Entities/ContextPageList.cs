using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNTRTabDropDown.Entities
{
    public class ContextPageList
    {
        public bool Success { get; set; }
        public List<ContextPage> ContextPages { get; set; }
    }

    public class ContextPage
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public SubPageList SubPageList { get; set; }
    }
}