﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PugTrace.Dashboard.Pages
{
    
    #line 2 "..\..\Dashboard\Pages\DataObjectPage.cshtml"
    using System;
    
    #line default
    #line hidden
    using System.Collections.Generic;
    
    #line 3 "..\..\Dashboard\Pages\DataObjectPage.cshtml"
    using System.Linq;
    
    #line default
    #line hidden
    using System.Text;
    
    #line 6 "..\..\Dashboard\Pages\DataObjectPage.cshtml"
    using Newtonsoft.Json;
    
    #line default
    #line hidden
    
    #line 7 "..\..\Dashboard\Pages\DataObjectPage.cshtml"
    using Newtonsoft.Json.Linq;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Dashboard\Pages\DataObjectPage.cshtml"
    using PugTrace.Dashboard;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Dashboard\Pages\DataObjectPage.cshtml"
    using PugTrace.Dashboard.Pages;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    internal partial class DataObjectPage : RazorPage
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");









            
            #line 9 "..\..\Dashboard\Pages\DataObjectPage.cshtml"
  
    JObject obj = Data;


            
            #line default
            #line hidden
WriteLiteral(@"<div class=""table-responsive"" style=""margin-bottom: 20px;"">
    <table class=""table table-striped table-bordered table-condensed"" style=""margin-bottom: 0;"">
        <thead>
            <tr class=""success"">
                <th>Name</th>
                <th>Value</th>
            </tr>
        </thead>
        <tbody>
");


            
            #line 21 "..\..\Dashboard\Pages\DataObjectPage.cshtml"
             foreach (var prop in obj)
            {
                if (prop.Value.Type == JTokenType.Object)
                {
                    var dict = prop.Value.Value<JObject>();


            
            #line default
            #line hidden
WriteLiteral("                    <tr>\r\n                        <td colspan=\"2\" class=\"info\"><s" +
"trong>");


            
            #line 28 "..\..\Dashboard\Pages\DataObjectPage.cshtml"
                                                        Write(prop.Key);

            
            #line default
            #line hidden
WriteLiteral("</strong></td>\r\n                    </tr>\r\n");


            
            #line 30 "..\..\Dashboard\Pages\DataObjectPage.cshtml"

                    foreach (var item in dict)
                    {

            
            #line default
            #line hidden
WriteLiteral("                        <tr>\r\n                            <td>");


            
            #line 34 "..\..\Dashboard\Pages\DataObjectPage.cshtml"
                           Write(item.Key);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                            <td style=\"overflow: auto;\">");


            
            #line 35 "..\..\Dashboard\Pages\DataObjectPage.cshtml"
                                                   Write(item.Value.ToString());

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                        </tr>\r\n");


            
            #line 37 "..\..\Dashboard\Pages\DataObjectPage.cshtml"
                    }
                }
                else
                {
                    var value = prop.Value.ToString();


            
            #line default
            #line hidden
WriteLiteral("                    <tr>\r\n                        <td>");


            
            #line 44 "..\..\Dashboard\Pages\DataObjectPage.cshtml"
                       Write(prop.Key);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                        <td style=\"overflow: auto;\">\r\n");


            
            #line 46 "..\..\Dashboard\Pages\DataObjectPage.cshtml"
                             if (value.Contains(Environment.NewLine))
                            {

            
            #line default
            #line hidden
WriteLiteral("                                <pre style=\"border: none; padding: 0;\"><code clas" +
"s=\"csharp\">");


            
            #line 48 "..\..\Dashboard\Pages\DataObjectPage.cshtml"
                                                                                       Write(value);

            
            #line default
            #line hidden
WriteLiteral("</code></pre>\r\n");


            
            #line 49 "..\..\Dashboard\Pages\DataObjectPage.cshtml"
                            }
                            else
                            {
                                
            
            #line default
            #line hidden
            
            #line 52 "..\..\Dashboard\Pages\DataObjectPage.cshtml"
                           Write(value);

            
            #line default
            #line hidden
            
            #line 52 "..\..\Dashboard\Pages\DataObjectPage.cshtml"
                                      
                            }

            
            #line default
            #line hidden
WriteLiteral("                        </td>\r\n                    </tr>\r\n");


            
            #line 56 "..\..\Dashboard\Pages\DataObjectPage.cshtml"
                }
            }

            
            #line default
            #line hidden
WriteLiteral("        </tbody>\r\n    </table>\r\n</div>");


        }
    }
}
#pragma warning restore 1591
