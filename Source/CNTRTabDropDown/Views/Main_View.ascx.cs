using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Jenzabar.Portal.Framework;
using Jenzabar.Portal.Framework.Web.UI;
using System.Data;
using Jenzabar.Common.ApplicationBlocks.ExceptionManagement;
using StructureMap;
using Jenzabar.Portal.Framework.Facade;
using Jenzabar.ICS;
using Jenzabar.Portal.Framework.Services.PortalStructure;
using Jenzabar.Common;

namespace CNTRTabDropDown
{
    public partial class Main_View : PortletViewBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsFirstLoad)
            {
                PopulateSettings();
                PopulateTabNames();
            }
        }

        private void PopulateSettings()
        {
            TabDropDownSettingMapperService tabSettingsService = new TabDropDownSettingMapperService();
            TabDropDownSettings tabSettings = tabSettingsService.GetSettings();
            if (tabSettings != null)
            {
                cbxEnable.Checked = tabSettings.EnableDropDown;
                cbxPages.Checked = tabSettings.DisplayPages;
                cbxSubSections.Checked = tabSettings.DisplaySubSections;
                cbxSubPagesPortlets.Checked = tabSettings.DisplaySubPagesPortlets;
                cbxAlphaOrder.Checked = tabSettings.DisplayAlphaOrder;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                TabDropDownSettingMapperService tabSettingsService = new TabDropDownSettingMapperService();
                TabDropDownSettings tabSettings = tabSettingsService.GetSettings();

                if (tabSettings == null)
                {
                    tabSettings = new TabDropDownSettings();
                }

                tabSettings.EnableDropDown = cbxEnable.Checked;
                tabSettings.DisplayPages = cbxPages.Checked;
                tabSettings.DisplaySubSections = cbxSubSections.Checked;
                tabSettings.DisplaySubPagesPortlets = cbxSubPagesPortlets.Checked;
                tabSettings.DisplayAlphaOrder = cbxAlphaOrder.Checked;
                tabSettingsService.Save(tabSettings);
                ParentPortlet.ShowFeedback(FeedbackType.Message, "The settings were successfully saved.");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                ParentPortlet.ShowFeedback(FeedbackType.Error, "The system encountered an error saving the setting, please contact your ITS administrator.");
            }
        }

        private void PopulateTabNames()
        {
            IPortalContextService _contextService = ObjectFactoryWrapper.GetInstance<IPortalContextService>();
            //Get Home tab
            PortalContext pc = _contextService.FindByPath(String.Empty);

            //Add all other tabs to the table
            foreach (PortalContext context in _contextService.FindChildContextsFor(pc, PortalUser.Current))
            {
                ltrlTabNames.Text += "<tr><td>" + Server.HtmlEncode(context.DisplayName) + "</td><td>" + Server.HtmlEncode(context.Name) + "</td></td>";
            }
        }
    }
}