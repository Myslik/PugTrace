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
    
    #line 2 "..\..\Dashboard\Pages\Cleanup.cshtml"
    using PugTrace.Dashboard;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Dashboard\Pages\Cleanup.cshtml"
    using PugTrace.Dashboard.Pages;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Dashboard\Pages\Cleanup.cshtml"
    using PugTrace.Storage;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    internal partial class Cleanup : RazorPage
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");






            
            #line 6 "..\..\Dashboard\Pages\Cleanup.cshtml"
   
    Layout = new LayoutPage();
    using (var connection = this.Storage.GetConnection())
    {
        connection.Cleanup();
    }


            
            #line default
            #line hidden
WriteLiteral("<h3>Cleanup</h3>\r\n<p>Done.</p>");


        }
    }
}
#pragma warning restore 1591