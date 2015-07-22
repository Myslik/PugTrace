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
    
    #line 2 "..\..\Dashboard\Pages\HomePage.cshtml"
    using System;
    
    #line default
    #line hidden
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    #line 3 "..\..\Dashboard\Pages\HomePage.cshtml"
    using PugTrace.Dashboard;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Dashboard\Pages\HomePage.cshtml"
    using PugTrace.Dashboard.Pages;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    internal partial class HomePage : RazorPage
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");






            
            #line 6 "..\..\Dashboard\Pages\HomePage.cshtml"
  
    Layout = new LayoutPage();
    Initialize();


            
            #line default
            #line hidden
WriteLiteral("<div class=\"btn-toolbar btn-toolbar-top\">\r\n    ");


            
            #line 11 "..\..\Dashboard\Pages\HomePage.cshtml"
Write(Html.PerPageSelector(this.Pager));

            
            #line default
            #line hidden
WriteLiteral(@"
</div>
<div class=""table-responsive"">
    <table class=""table"">
        <thead>
            <tr>
                <th>Application</th>
                <th>Source</th>
                <th>Id</th>
                <th>Event Type</th>
                <th>Timestamp</th>
                <th>Machine</th>
                <th>Message</th>
            </tr>
        </thead>
        <tbody>
");


            
            #line 27 "..\..\Dashboard\Pages\HomePage.cshtml"
             foreach (var trace in this.Rows)
            {
                var klass = string.Empty;

                if (trace.EventType == "Error")
                {
                    klass = "danger";
                }


            
            #line default
            #line hidden
WriteLiteral("                <tr class=\"");


            
            #line 36 "..\..\Dashboard\Pages\HomePage.cshtml"
                      Write(klass);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n                    <td>");


            
            #line 37 "..\..\Dashboard\Pages\HomePage.cshtml"
                   Write(trace.ApplicationName);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                    <td>");


            
            #line 38 "..\..\Dashboard\Pages\HomePage.cshtml"
                   Write(trace.Source);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                    <td>");


            
            #line 39 "..\..\Dashboard\Pages\HomePage.cshtml"
                   Write(trace.Id);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                    <td>");


            
            #line 40 "..\..\Dashboard\Pages\HomePage.cshtml"
                   Write(trace.EventType);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                    <td>");


            
            #line 41 "..\..\Dashboard\Pages\HomePage.cshtml"
                   Write(trace.UtcDateTime.ToString("f"));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                    <td>");


            
            #line 42 "..\..\Dashboard\Pages\HomePage.cshtml"
                   Write(trace.MachineName);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                    <td>");


            
            #line 43 "..\..\Dashboard\Pages\HomePage.cshtml"
                   Write(trace.Message);

            
            #line default
            #line hidden
WriteLiteral(" <a href=\"");


            
            #line 43 "..\..\Dashboard\Pages\HomePage.cshtml"
                                           Write(Url.TraceDetails(trace.TraceId.ToString()));

            
            #line default
            #line hidden
WriteLiteral("\">Details...</a></td>\r\n                </tr>\r\n");


            
            #line 45 "..\..\Dashboard\Pages\HomePage.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n");


            
            #line 49 "..\..\Dashboard\Pages\HomePage.cshtml"
Write(Html.Paginator(this.Pager));

            
            #line default
            #line hidden

        }
    }
}
#pragma warning restore 1591
