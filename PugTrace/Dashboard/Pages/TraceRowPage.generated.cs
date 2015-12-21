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
    
    #line 2 "..\..\Dashboard\Pages\TraceRowPage.cshtml"
    using PugTrace.Dashboard;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Dashboard\Pages\TraceRowPage.cshtml"
    using PugTrace.Dashboard.Pages;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Dashboard\Pages\TraceRowPage.cshtml"
    using PugTrace.Storage;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    internal partial class TraceRowPage : RazorPage<TraceData>
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");






            
            #line 6 "..\..\Dashboard\Pages\TraceRowPage.cshtml"
  
    Layout = null;
    var rowClass = "trace " + Model.EventType.ToLower();
    var exception = Model.GetException();


            
            #line default
            #line hidden
WriteLiteral("\r\n<tr class=\"");


            
            #line 12 "..\..\Dashboard\Pages\TraceRowPage.cshtml"
      Write(rowClass);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n    <td style=\"width: 100px;\">\r\n        <span class=\"label label-default\">");


            
            #line 14 "..\..\Dashboard\Pages\TraceRowPage.cshtml"
                                     Write(Html.RenderDateTime(Model.UtcDateTime.ToLocalTime()));

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n    </td>\r\n    <td>\r\n        <a href=\"");


            
            #line 17 "..\..\Dashboard\Pages\TraceRowPage.cshtml"
            Write(Url.TraceDetails(Model.TraceId.ToString()));

            
            #line default
            #line hidden
WriteLiteral("\" class=\"btn btn-xs btn-default pull-right\">Details</a>\r\n        ");


            
            #line 18 "..\..\Dashboard\Pages\TraceRowPage.cshtml"
   Write(Model.Message);

            
            #line default
            #line hidden
WriteLiteral("\r\n    </td>\r\n</tr>\r\n\r\n");


            
            #line 22 "..\..\Dashboard\Pages\TraceRowPage.cshtml"
 if (exception != null)
{

            
            #line default
            #line hidden
WriteLiteral("    <tr class=\"detail\" style=\"display: none;\">\r\n        <td colspan=\"2\" style=\"pa" +
"dding-left: 20px; padding-bottom: 20px; background-color: #f5f5f5;\">\r\n          " +
"  <h3>");


            
            #line 26 "..\..\Dashboard\Pages\TraceRowPage.cshtml"
           Write(exception.TypeName);

            
            #line default
            #line hidden
WriteLiteral("</h3>\r\n            <h4>");


            
            #line 27 "..\..\Dashboard\Pages\TraceRowPage.cshtml"
           Write(exception.Message);

            
            #line default
            #line hidden
WriteLiteral("</h4>\r\n            ");


            
            #line 28 "..\..\Dashboard\Pages\TraceRowPage.cshtml"
       Write(Html.RenderExceptionStackTrace(exception.Detail));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n    </tr>\r\n");


            
            #line 31 "..\..\Dashboard\Pages\TraceRowPage.cshtml"
}

            
            #line default
            #line hidden

        }
    }
}
#pragma warning restore 1591