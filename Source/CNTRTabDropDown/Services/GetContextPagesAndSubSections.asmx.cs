using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Jenzabar.Portal.Framework;
using Jenzabar.Portal.Framework.Facade;
using StructureMap;
using Jenzabar.Portal.Framework.Services.PortalStructure;
using Jenzabar.Common;
using Jenzabar.Common.ApplicationBlocks.ExceptionManagement;
using CNTRTabDropDown.Entities;

namespace CNTRTabDropDown
{
    /// <summary>
    /// Summary description for GetContextPages
    /// </summary>
    [WebService(Namespace = "https://jicsweb-dev.centre.edu/WebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class GetContextPagesAndSubSections : WebService
    {

        [WebMethod(EnableSession = true, CacheDuration = 600)]
        public ContextPageList FindContextPagesSubSections(string path)
        {
            TabDropDownSettingMapperService tabSettingsService = new TabDropDownSettingMapperService();
            IPortalContextService _contextService = ObjectFactoryWrapper.GetInstance<IPortalContextService>();
            List<ContextPage> context_pages = new List<ContextPage>();

            try
            {
                //Retrieve the tab drop down settings, use defaults if null
                TabDropDownSettings tabSettings = tabSettingsService.GetSettings();
                if (tabSettings == null)
                    tabSettings = new TabDropDownSettings();

                //Get the PortalContext by the Tab Name
                PortalContext pc = _contextService.FindByPath(path);

                //If the drop down is disabled or the context is null, then return an empty list
                if (pc == null || !tabSettings.EnableDropDown)
                    return new ContextPageList { Success = false, ContextPages = null };
 
                //If we are displaying page, then grab all the pages for this context
                if (tabSettings.DisplayPages)
                {
                    List<PortalPageInfo> pages = _contextService.GetPagesFor(pc, PortalUser.Current).ToList();
                    foreach (PortalPageInfo p in pages)
                    {
                        //Retrieve the list of portlets on this page
                        List<SubPage> subPortlets = PopulateSubPortlets(p, tabSettings);

                        context_pages.Add(new ContextPage { Name = p.DisplayName, URL = p.URL, SubPageList = new SubPageList(subPortlets.Count() > 1 ? true : false, subPortlets) });
                    }
                }

                //If we are displaying sub sections, then grab all the sub sections for this context
                if (tabSettings.DisplaySubSections && !String.IsNullOrEmpty(path))
                {
                    List<PortalContext> contexts = _contextService.FindChildContextsFor(pc, PortalUser.Current).ToList();
                        
                    foreach (PortalContext c in contexts)
                    {
                        //We only grab sub sections if they are setup to display in the sidebar
                        if (pc.ShowChildNodesInSideBar)
                        {
                            //Sub Sections do not have portlets beneath them
                            //We need to grab pages or more sub sections
                            List<SubPage> subPages = PopulateSubPages(c, tabSettings);

                            context_pages.Add(new ContextPage { Name = c.DisplayName, URL = c.URL, SubPageList = new SubPageList(subPages.Count() > 1 ? true : false, subPages) });
                        }
                    }
                }

                //Sort the list in alphabetical order before returning
                if (tabSettings.DisplayAlphaOrder)
                    context_pages.Sort((x, y) => string.Compare(x.Name, y.Name));

                return new ContextPageList { Success = true, ContextPages = context_pages};
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new ContextPageList { Success = false, ContextPages = null };
            }
        }

        private List<SubPage> PopulateSubPortlets(PortalPageInfo p, TabDropDownSettings tabSettings)
        {
            List<SubPage> subPortlets = new List<SubPage>();

            //Grab portlets on this page if setting is turned on
            if (tabSettings.DisplaySubPagesPortlets)
            {
                var portlets = p.PortletDisplays;
                foreach (var pt in portlets)
                {
                    subPortlets.Add(new Entities.SubPage { Name = pt.DisplayName, URL = pt.URL });
                }
            }

            return subPortlets;
        }

        private List<SubPage> PopulateSubPages(PortalContext c, TabDropDownSettings tabSettings)
        {
            IPortalContextService _contextService = ObjectFactoryWrapper.GetInstance<IPortalContextService>();
            List<SubPage> subPages = new List<SubPage>();

            //Grab pages and sub sections on this sub section if setting is turned on
            if (tabSettings.DisplaySubPagesPortlets)
            {
                var pages = _contextService.GetPagesFor(c, PortalUser.Current).ToList();
                foreach (var pg in pages)
                {
                    subPages.Add(new Entities.SubPage { Name = pg.DisplayName, URL = pg.URL });
                }

                var subsections = _contextService.FindChildContextsFor(c, PortalUser.Current).ToList();
                foreach (var sub in subsections)
                {
                    subPages.Add(new Entities.SubPage { Name = sub.DisplayName, URL = sub.URL });
                }
            }

            return subPages;
        }
    }
}
