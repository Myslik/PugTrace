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
    
    #line 2 "..\..\Dashboard\Pages\HomePage.cshtml"
    using PugTrace.Dashboard;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Dashboard\Pages\HomePage.cshtml"
    using PugTrace.Dashboard.Pages;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Dashboard\Pages\HomePage.cshtml"
    using PugTrace.Storage;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    internal partial class HomePage : TracesPage
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");






            
            #line 6 "..\..\Dashboard\Pages\HomePage.cshtml"
  
    Layout = new LayoutPage();


            
            #line default
            #line hidden
WriteLiteral("\r\n<div class=\"panel panel-default\">\r\n    <div class=\"panel-heading\">\r\n        <di" +
"v class=\"row\">\r\n            <div class=\"col-lg-6\">\r\n                ");


            
            #line 14 "..\..\Dashboard\Pages\HomePage.cshtml"
           Write(Html.Filter(this.Pager));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n            <div class=\"col-lg-6\">\r\n                <div cl" +
"ass=\"pull-right \">\r\n                    ");


            
            #line 18 "..\..\Dashboard\Pages\HomePage.cshtml"
               Write(Html.PerPageSelector(this.Pager));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div id=\"se" +
"arch-bar\" class=\"row hide\">\r\n            <div class=\"col-md-12\">\r\n              " +
"  ");


            
            #line 24 "..\..\Dashboard\Pages\HomePage.cshtml"
           Write(Html.Search(this.Pager));

            
            #line default
            #line hidden
WriteLiteral(@"
            </div>
        </div>
    </div>
    <div class=""table-responsive"">
        <table class=""table table-condensed"">
            <thead>
                <tr>
                    <th>Timestamp</th>
                    <th>User</th>
                    <th>Message</th>
                </tr>
            </thead>
            <tbody>
");


            
            #line 38 "..\..\Dashboard\Pages\HomePage.cshtml"
                 if (this.Count == 0)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <tr>\r\n                        <td colspan=\"3\">No traces.</td>" +
"\r\n                    </tr>\r\n");


            
            #line 43 "..\..\Dashboard\Pages\HomePage.cshtml"
                }
                else
                {
                    foreach (var trace in this.Model)
                    {
                        
            
            #line default
            #line hidden
            
            #line 48 "..\..\Dashboard\Pages\HomePage.cshtml"
                   Write(Html.TraceRow(trace));

            
            #line default
            #line hidden
            
            #line 48 "..\..\Dashboard\Pages\HomePage.cshtml"
                                             
                    }
                }

            
            #line default
            #line hidden
WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n    <div class=\"panel-footer\"" +
">\r\n        ");


            
            #line 55 "..\..\Dashboard\Pages\HomePage.cshtml"
   Write(Html.Paginator(this.Pager));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n</div>\r\n");


        }
    }
}
#pragma warning restore 1591
