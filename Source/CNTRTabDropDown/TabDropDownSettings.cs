using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Xml.Linq;
using Jenzabar.Portal.Framework.NHibernateFWK;  //Added for NHibernate
using Jenzabar.Portal.Framework.NHibernateFWK.Base; //Added for NHibernate
using NHibernate; //Added for NHibernate
using NHibernate.Criterion; //Added for NHibernate

namespace CNTRTabDropDown
{
    public class TabDropDownSettings: JICSBase
    {
        public virtual Guid SettingID { get; set; }
        public virtual bool EnableDropDown { get; set; }
        public virtual bool DisplayPages { get; set; }
        public virtual bool DisplaySubSections { get; set; }
        public virtual bool DisplaySubPagesPortlets { get; set; }
        public virtual bool DisplayAlphaOrder { get; set; }

        public TabDropDownSettings()
        {
            this.EnableDropDown = false;
            this.DisplayPages = false;
            this.DisplaySubSections = false;
            this.DisplaySubPagesPortlets = false;
            this.DisplayAlphaOrder = false;
        }
    }

    public class TabDropDownSettingMapperService : JICSBaseFacade<TabDropDownSettings>
    {
        public TabDropDownSettings GetSettings()
        {
            return GetQuery().Where(x => x.SettingID != null).FirstOrDefault();
        }
    }
}