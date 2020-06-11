using Jenzabar.Portal.Framework.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CNTRTabDropDown
{
    public partial class Default_View : PortletViewBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnViewSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect(ParentPortlet.GetNextScreenURL("Main"));
        }
    }
}