﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PugTrace.Dashboard.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    #line 2 "..\..\Dashboard\Pages\_Filter.cshtml"
    using PugTrace.Dashboard;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    internal partial class Filter : RazorPage<Pager>
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\n");



WriteLiteral("\n<div class=\"btn-group pull-left\">\n    ");


            
            #line 6 "..\..\Dashboard\Pages\_Filter.cshtml"
Write(Html.Link("All", Model.TypeFilterUrl(null), "btn btn-sm btn-default", Model.TypeFilter == null));

            
            #line default
            #line hidden
WriteLiteral("\n    ");


            
            #line 7 "..\..\Dashboard\Pages\_Filter.cshtml"
Write(Html.Link("Information", Model.TypeFilterUrl("Information"), "btn btn-sm btn-default", Model.TypeFilter == "Information"));

            
            #line default
            #line hidden
WriteLiteral("\n    ");


            
            #line 8 "..\..\Dashboard\Pages\_Filter.cshtml"
Write(Html.Link("Critical", Model.TypeFilterUrl("Critical"), "btn btn-sm btn-default", Model.TypeFilter == "Critical"));

            
            #line default
            #line hidden
WriteLiteral("\n    ");


            
            #line 9 "..\..\Dashboard\Pages\_Filter.cshtml"
Write(Html.Link("Error", Model.TypeFilterUrl("Error"), "btn btn-sm btn-default", Model.TypeFilter == "Error"));

            
            #line default
            #line hidden
WriteLiteral("\n</div>\n\n<div class=\"btn-group pull-left\" style=\"margin-left: 10px;\">\n    <button" +
" id=\"search-button-on\" class=\"btn btn-sm btn-default\">Search</button>\n    <a id=" +
"\"search-button-off\" class=\"btn btn-sm btn-success hide\" href=\"");


            
            #line 14 "..\..\Dashboard\Pages\_Filter.cshtml"
                                                                   Write(Url.Home());

            
            #line default
            #line hidden
WriteLiteral(@""">
        Search
    </a>
</div>

<script>
    var searchButtonOn = document.getElementById('search-button-on');
    var searchButtonOff = document.getElementById('search-button-off');

    Date.prototype.addHours = function (h) {
        this.setHours(this.getHours() + h);
        return this;
    }

    searchButtonOn.onclick = function () {
        console.log(""show first search"");
        var searchBar = document.getElementById('search-bar');
            searchButtonOn.classList.add('hide');
            searchBar.classList.remove('hide');
            searchButtonOff.classList.remove('hide');
            document.getElementById('search-from').valueAsDate = new Date();
            document.getElementById('search-to').valueAsDate = new Date().addHours(24);
            document.getElementById('search-value').focus();
    }
</script>");


        }
    }
}
#pragma warning restore 1591
