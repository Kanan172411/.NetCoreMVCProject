#pragma checksum "E:\BE\p220_be_21_backendproject-Kanan172411\EduHomeBackendProject\EduHomeBackendProject\Areas\Manage\Views\ContactMessage\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "19bbddef95b6b49c0675570d9d1a97c8d9149725"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Manage_Views_ContactMessage_Details), @"mvc.1.0.view", @"/Areas/Manage/Views/ContactMessage/Details.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\BE\p220_be_21_backendproject-Kanan172411\EduHomeBackendProject\EduHomeBackendProject\Areas\Manage\Views\_ViewImports.cshtml"
using EduHomeBackendProject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\BE\p220_be_21_backendproject-Kanan172411\EduHomeBackendProject\EduHomeBackendProject\Areas\Manage\Views\_ViewImports.cshtml"
using EduHomeBackendProject.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\BE\p220_be_21_backendproject-Kanan172411\EduHomeBackendProject\EduHomeBackendProject\Areas\Manage\Views\_ViewImports.cshtml"
using EduHomeBackendProject.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\BE\p220_be_21_backendproject-Kanan172411\EduHomeBackendProject\EduHomeBackendProject\Areas\Manage\Views\_ViewImports.cshtml"
using EduHomeBackendProject.Areas.Manage.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19bbddef95b6b49c0675570d9d1a97c8d9149725", @"/Areas/Manage/Views/ContactMessage/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b160bbc7b078de1272fb836be84816df7d533e38", @"/Areas/Manage/Views/_ViewImports.cshtml")]
    public class Areas_Manage_Views_ContactMessage_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ContactMessages>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "E:\BE\p220_be_21_backendproject-Kanan172411\EduHomeBackendProject\EduHomeBackendProject\Areas\Manage\Views\ContactMessage\Details.cshtml"
  
    ViewData["Title"] = "Details";
    Layout = "~/Areas/Manage/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container-fluid\">\r\n    <h1>Details</h1>\r\n    <div class=\"card\" style=\"width: 100rem;\">\r\n        <ul class=\"list-group list-group-flush\">\r\n");
#nullable restore
#line 11 "E:\BE\p220_be_21_backendproject-Kanan172411\EduHomeBackendProject\EduHomeBackendProject\Areas\Manage\Views\ContactMessage\Details.cshtml"
             if (Model.AppUserId == null)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li class=\"list-group-item\">UserName :");
#nullable restore
#line 13 "E:\BE\p220_be_21_backendproject-Kanan172411\EduHomeBackendProject\EduHomeBackendProject\Areas\Manage\Views\ContactMessage\Details.cshtml"
                                                  Write("              "+Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n                <li class=\"list-group-item\">Email :");
#nullable restore
#line 14 "E:\BE\p220_be_21_backendproject-Kanan172411\EduHomeBackendProject\EduHomeBackendProject\Areas\Manage\Views\ContactMessage\Details.cshtml"
                                               Write("              "+Model.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n");
#nullable restore
#line 15 "E:\BE\p220_be_21_backendproject-Kanan172411\EduHomeBackendProject\EduHomeBackendProject\Areas\Manage\Views\ContactMessage\Details.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li class=\"list-group-item\">UserName :");
#nullable restore
#line 18 "E:\BE\p220_be_21_backendproject-Kanan172411\EduHomeBackendProject\EduHomeBackendProject\Areas\Manage\Views\ContactMessage\Details.cshtml"
                                                  Write("              "+Model.appUser.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n                <li class=\"list-group-item\">Email :");
#nullable restore
#line 19 "E:\BE\p220_be_21_backendproject-Kanan172411\EduHomeBackendProject\EduHomeBackendProject\Areas\Manage\Views\ContactMessage\Details.cshtml"
                                               Write("              "+Model.appUser.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n");
#nullable restore
#line 20 "E:\BE\p220_be_21_backendproject-Kanan172411\EduHomeBackendProject\EduHomeBackendProject\Areas\Manage\Views\ContactMessage\Details.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li class=\"list-group-item\">SendedAt :");
#nullable restore
#line 21 "E:\BE\p220_be_21_backendproject-Kanan172411\EduHomeBackendProject\EduHomeBackendProject\Areas\Manage\Views\ContactMessage\Details.cshtml"
                                              Write("         "+Model.SendedAt.ToString("HH:mm dd.MM.yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n            <li class=\"list-group-item\">Subject :");
#nullable restore
#line 22 "E:\BE\p220_be_21_backendproject-Kanan172411\EduHomeBackendProject\EduHomeBackendProject\Areas\Manage\Views\ContactMessage\Details.cshtml"
                                             Write("        "+Model.Subject);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n            <li class=\"list-group-item\">Message :");
#nullable restore
#line 23 "E:\BE\p220_be_21_backendproject-Kanan172411\EduHomeBackendProject\EduHomeBackendProject\Areas\Manage\Views\ContactMessage\Details.cshtml"
                                             Write("                "+Model.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n        </ul>\r\n    </div>\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ContactMessages> Html { get; private set; }
    }
}
#pragma warning restore 1591
