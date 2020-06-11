using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;  //Required for PortletBase (SecuredPortletBase, LinkablePortletBase, SecuredLinkablePortletBase)
using Jenzabar.Portal.Framework.Web.UI;  //Required for PortletBase (SecuredPortletBase, LinkablePortletBase, SecuredLinkablePortletBase)
using Jenzabar.Portal.Framework.Security.Authorization;  //Added for portlet opertaion
using Jenzabar.Common.Web.UI.Controls;  //Added for toolbar
using Jenzabar.Portal.Framework.Web.UI.Controls.MetaControls.Attributes; //Added for portlet preferences and settings
using Jenzabar.Portal.Framework;  //Added for preferences and settings check
using Jenzabar.ICS.Web.Portlets.Common; //Added for preferences and settings check

namespace CNTRTabDropDown
{
    public class TabDropDown : SecuredLinkablePortletBase
    {
        protected override PortletViewBase GetCurrentScreen()
        {
            PortletViewBase screen = null;

            switch (this.CurrentPortletScreenName)
            {
                case "default":
                default:
                    screen = this.LoadPortletView("/ICS/CNTRTabDropDown/Views/Default_View.ascx");
                    break;

                case "Main":
                    screen = this.LoadPortletView("/ICS/CNTRTabDropDown/Views/Main_View.ascx");
                    break;
            }

            return screen;
        }
    }
}
