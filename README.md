# JICSTabDropDown
Displays a drop down list when you hover your cursor over the tabs of a JICS portal.

# Function
This custom will display a list of pages in a popup for the tab that the user has their mouse over.  The user can click a page in the list to be taken directly to that URL, rather than having to click on the tab first. In version 1.2, the tab drop down can display a second list of portlets, pages, or subsections. In addition, this custom works by calling a JQuery script when the user hovers over a tab, a AJAX Post is sent to a web service file in the CNTR Tab Drop Down Portlet that returns the list of page names and URLs based on the user's role.  Javascript must be enabled in the browser you are using for this custom to work. The CNTR Tab Drop Down Portlet contains a settings page that the administrator can use to set if the tab drop down list displays pages and/or sub-sections and if those items are displayed in alphabetical order.

# New Installation
1. Unzip the contents of the archive into a location of your choosing.
2. Set [[Resync Operations|SyncGlobalOperationsOnStart]] to true in Program Files\Jenzabar\ICS.NET\Portal\web.config
3. Run Compiled\BuildTable.sql and Compiled\FWK_VersionInfo.sql against your ICS_NET database
4. Edit or create Program Files\Jenzabar\ICS.NET\Portal\ClientConfig\[[CustomSessionFactory_xml|CustomSessionFactory.xml]]. If the file doesn't exist, [[CustomSessionFactory_xml|follow instructions to create the file]], making sure it has the following entry:
    <SessionFactory>CNTRTabDropDown.NHibernateFactory, CNTRTabDropDown</SessionFactory>
5. Copy Compiled\ICS.NET\ into Program Files\Jenzabar\ICS.NET\
6. Copy the CNTRTabDropDown.ldf and CNTRTabDropDown.vbs to your JICS server and execute CNTRTabDropDown.vbs there -- this should create two entries in AD LDS representing the PortletTemplate for this portlet
7. You will most likely need to go to the Portal\Portlets\CUS\ICS\CNTRTabDropDown\css\CNTRTabDropDown.css file and update the css to match the theme of your portal.
8. Add these two lines to your Portal\ClientConfig\Controls\CustomHead.ascx file and then refresh your browser cache. You will need to change /ICS/ if your root is different.
    <style type="text/css" media="all">@import url( /ICS/Portlets/CUS/ICS/CNTRTabDropDown/css/CNTRTabDropDown.css );</style>
    <script src="/ics/Portlets/CUS/ICS/CNTRTabDropDown/js/CNTRTabDropDown.js"></script>
* Visit the JICS site in a browser to cause the site to load
* Once the portlet is setup, set [[Resync Operations|SyncGlobalOperationsOnStart]] to false in Program Files\Jenzabar\ICS.NET\Portal\web.config

# Important JICS 9 Notes
The portlet part of the CNTRTabDropDown is responsive, but the drop down lists are not designed for mobile devices and are only designed to display in the desktop mode of the portal.  So, the drop down list only displays when the screen width is greater than or equal to 1025px.  If you have a theme where this needs to be different, then you need to edit the Portlets/CUS/ICS/CNTRTabDropDown/js/CNTRTabDropDown.js file to reflect the different screen size.  In addition, the drop downs are designed to work with the Jenzabar Atlantic theme.  If you use a different theme or custom theme, then you will need to modify the CNTRTabDropDown.css file.

# IE Fix for version 1.2
The subpage list doesn't display next to the drop down list in Internet Explorer. Please change the javascript in the CNTRTabDropDown.js file between lines 36 and 50 to the following to fix the issue:
    var glyphicon = "";
    var pixelAdjust = 0; //pixel amount to adjust IE margins
    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");				
    								
    //If IE adjust the subpage list by 350px
    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) { 
        pixelAdjust = 350; 
    }
    
    var subPageList = $('<ul class="subPageList" />');
    $.each(contextpage.SubPageList.SubPages, function (index, subpage) {
        subPageList.append('&lt;li><a href="' + subpage.URL + '">' + subpage.Name + '</a>&lt;/li>');
    });
    subPageList.css('display', 'none');
    if ($(window).width() > (offset.left + 700)) {
        subPageList.css('margin-left', 350-pixelAdjust+'px');
        glyphicon = '<span class="glyphicon glyphicon-menu-right pull-right" aria-hidden="true"></span>';
    }
    else {
        subPageList.css('margin-left', -350-pixelAdjust+'px');
        glyphicon = '<span class="glyphicon glyphicon-menu-left pull-left" aria-hidden="true"></span>';
    }
    pageList.append($('&lt;li><a href="' + contextpage.URL + '">' + contextpage.Name + glyphicon + '</a>&lt;/li>').append(subPageList));

# More Tab Issues in 1.0 and 1.1
If you are using the more tab in your theme then you will have to modify the theme's javascript file to keep the More tab from pulling in the page drop down lists by removing the page drop down lists when the More tab is created. For example, these are directions if you are using the Aegean theme in JICS:
1. Open the Portal\UI\Themes\Aegean\javascript.js file (this file will be different depending on what theme you are using)
2.  Add '''removePageLists();''' to the first line of the '''csTabOverflow''' function
3.  Add this function to the bottom of the javascript.js file
    function removePageLists() {
        $('#headerTabs ul li').each(function(){
            //If pageList exists then remove it
            var pageList = $(this).children('ul.pageList');
            if(pageList.length) {
                pageList.remove();
            }
         });
    }
* This change will have to be added each time JICS is updated, unless you are using a custom theme'

Hide the page drop downs that display when you hover over tabs on the More tab
* Add these lines to the CNTRTabDropDown.css file:
    #headerTabs #moreList ul.pageList {
        display: none !important;
    }

# Portlet Settings
There are 5 settings you can control in the portlet, which are stored the ICS_NET database.  You must enter and save these settings first, before the tab drop down menu will display.

# Tab Drop Down Not Displaying on Selected Tab
As the process is currently setup, the drop down menu will not display on a tab that is selected by the user.  If you would like to display the drop down menu anyway, you can do so by removing this section of CSS from the Portlets/CUS/ICS/CNTRTabDropDown/css/CNTRTabDropDown.css file:
    #headerTabs li.selected ul.pageList
    {
        display: none !important;
    }

# Certain Tabs Not Displaying Drop Down
You may notice certain tabs do not display a drop down list. In the javascript, the system uses the header title to find the corresponding context in JICS.  If you don't have the tab name and url set to match, then the system will not get the correct context and not display the popup.  This means that in the Context Manager screen, on the properties tab, you must have the "Change URL" checkbox checked and click Save Changes.

WARNING: You may not want to change the URL of the tab for certain reasons.  If that is the case then I would suggest editing the CNTRTabDropDown.js to where it will look for a specific tab title and hard code in the correct name you need to send.  The tab table underneath the portlet settings will help you identify any tabs that their display name does not match their corresponding identifying value.

# Known Issues
Problem
The drop down displays on the left side of the screen and not under the tab that the mouse is hovering over.

Resolution
This is a CSS issue and can usually be fixed by adding '''float: left;''' to the '''headertabs li''' property in your portal''s CSS theme file. Please test this CSS change first, it may not work with all themes.

Problem
If you use special characters in the tab name, such as /,\,+,*,&,?, or ; then the drop down will not display for that tab.

Temporary Resolution
Add an "else if" statement to the Portal/Portlets/CUS/ICS/CNTRTabDropDown/js/CNTRTabDropDown.js file at line 16 to change the tab name to be what is stored in JICS.  For example:
    else if (tab_name == 'Tab / Name') {
        tab_name = "Tab  Name";
    }

# Enhancement Request List
* Allow administrator to choose if drop down menu is displayed on selected tab or not.
