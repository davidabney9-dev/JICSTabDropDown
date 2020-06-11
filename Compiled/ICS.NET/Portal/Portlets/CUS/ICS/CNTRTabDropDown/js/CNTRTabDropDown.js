$(document).ready(function () {
    InitializeTabDropDown();
});

$(window).resize(function () {
    InitializeTabDropDown();
});

function InitializeTabDropDown() {
    if ($(window).width() >= 1025) {
        $('#main-nav > li[class*="tab_"]').on('mouseenter', function () {
            var tab = $(this);
            var offset = tab.offset();
            var pageList = $(this).children('ul.pageContentList');
            //If pageList exists then display it
            if (pageList.length != 0) {
                pageList.css('display', 'block');
            }
                //If pageList doesn't exist then create it and display it
            else {
                var tab_name = $.trim(tab.children('a').text());
                //If this is the home tab then leave tab_name blank
                if (tab_name == "Home") { tab_name = ""; }

                var pageList = $('<ul class="pageContentList" />');
                $.ajax({
                    type: "POST",
                    url: "/ics/portlets/cus/ics/CNTRTabDropDown/Services/GetContextPagesAndSubSections.asmx/FindContextPagesSubSections",
                    data: "{path:'" + tab_name + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d.Success) {
                            $.each(data.d.ContextPages, function (index, contextpage) {
                                if (contextpage.SubPageList.Success) {
                                    var glyphicon = "";
                                    var subPageList = $('<ul class="subPageList" />');
                                    $.each(contextpage.SubPageList.SubPages, function (index, subpage) {
                                        subPageList.append('<li><a href="' + subpage.URL + '">' + subpage.Name + '</a></li>');
                                    });
                                    subPageList.css('display', 'none');
                                    if ($(window).width() > (offset.left + 700)) {
                                        subPageList.css('margin-left', '350px');
                                        glyphicon = '<span class="glyphicon glyphicon-menu-right pull-right" aria-hidden="true"></span>';
                                    }
                                    else {
                                        subPageList.css('margin-left', '-350px');
                                        glyphicon = '<span class="glyphicon glyphicon-menu-left pull-left" aria-hidden="true"></span>';
                                    }
                                    pageList.append($('<li><a href="' + contextpage.URL + '">' + contextpage.Name + glyphicon + '</a></li>').append(subPageList));
                                }
                                else { pageList.append('<li><a href="' + contextpage.URL + '">' + contextpage.Name + '</a></li>'); }
                            });
                        }
                    }
                });
                tab.append(pageList);
                pageList.css('display', 'block');
            }
        });

        $('#main-nav > li[class*="tab_"]').on('mouseenter', 'ul.pageContentList > li', function () {
            $(this).children('ul.subPageList').css('display', 'block');
        });

        $('#main-nav > li[class*="tab_"]').on('mouseleave', 'ul.pageContentList > li', function () {
            $(this).children('ul.subPageList').css('display', 'none');
        });

        $('#main-nav > li[class*="tab_"]').on('mouseleave', function () {
            $(this).children('ul.pageContentList').css('display', 'none');
        });
    }
    else {
        $('#main-nav > li[class*="tab_"]').on('mouseenter', function () {
            $(this).children('ul.pageContentList').css('display', 'none');
        });

        $('#main-nav > li[class*="tab_"]').each(function () {
            var pageList = $(this).children('ul.pageContentList')
            if (pageList.length) {
                pageList.remove();
            }
        });
    }
}