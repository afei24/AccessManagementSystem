#pragma checksum "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4d401abdd19de02dd09a71a79c8bb8d93276cfe5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PresetFunction_Index), @"mvc.1.0.view", @"/Views/PresetFunction/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/PresetFunction/Index.cshtml", typeof(AspNetCore.Views_PresetFunction_Index))]
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
#line 1 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\_ViewImports.cshtml"
using AccessManagement;

#line default
#line hidden
#line 2 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\_ViewImports.cshtml"
using AccessManagement.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4d401abdd19de02dd09a71a79c8bb8d93276cfe5", @"/Views/PresetFunction/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3826a4965545f2f0ad57e10c8f472e1ac40b4acd", @"/Views/_ViewImports.cshtml")]
    public class Views_PresetFunction_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<AccessManagementServices.DOTS.PresetFunctionViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(75, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
            BeginContext(118, 15, true);
            WriteLiteral("\r\n<h1>功能</h1>\r\n");
            EndContext();
            BeginContext(628, 84, true);
            WriteLiteral("<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(713, 40, false);
#line 35 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(753, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(809, 40, false);
#line 38 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Code));

#line default
#line hidden
            EndContext();
            BeginContext(849, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(905, 47, false);
#line 41 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Description));

#line default
#line hidden
            EndContext();
            BeginContext(952, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1008, 43, false);
#line 44 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.OpName1));

#line default
#line hidden
            EndContext();
            BeginContext(1051, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1107, 43, false);
#line 47 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.OpName2));

#line default
#line hidden
            EndContext();
            BeginContext(1150, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1206, 43, false);
#line 50 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.OpName3));

#line default
#line hidden
            EndContext();
            BeginContext(1249, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1305, 43, false);
#line 53 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.OpName4));

#line default
#line hidden
            EndContext();
            BeginContext(1348, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1404, 43, false);
#line 56 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.OpName5));

#line default
#line hidden
            EndContext();
            BeginContext(1447, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1503, 43, false);
#line 59 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.OpName6));

#line default
#line hidden
            EndContext();
            BeginContext(1546, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1602, 43, false);
#line 62 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.OpName7));

#line default
#line hidden
            EndContext();
            BeginContext(1645, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1701, 43, false);
#line 65 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.OpName8));

#line default
#line hidden
            EndContext();
            BeginContext(1744, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1800, 43, false);
#line 68 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.OpName9));

#line default
#line hidden
            EndContext();
            BeginContext(1843, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1899, 44, false);
#line 71 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.OpName10));

#line default
#line hidden
            EndContext();
            BeginContext(1943, 86, true);
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
            EndContext();
#line 77 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
            BeginContext(2061, 48, true);
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2110, 39, false);
#line 80 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Name));

#line default
#line hidden
            EndContext();
            BeginContext(2149, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2205, 39, false);
#line 83 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Code));

#line default
#line hidden
            EndContext();
            BeginContext(2244, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2300, 46, false);
#line 86 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Description));

#line default
#line hidden
            EndContext();
            BeginContext(2346, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2402, 42, false);
#line 89 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.OpName1));

#line default
#line hidden
            EndContext();
            BeginContext(2444, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2500, 42, false);
#line 92 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.OpName2));

#line default
#line hidden
            EndContext();
            BeginContext(2542, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2598, 42, false);
#line 95 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.OpName3));

#line default
#line hidden
            EndContext();
            BeginContext(2640, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2696, 42, false);
#line 98 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.OpName4));

#line default
#line hidden
            EndContext();
            BeginContext(2738, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2794, 42, false);
#line 101 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.OpName5));

#line default
#line hidden
            EndContext();
            BeginContext(2836, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2892, 42, false);
#line 104 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.OpName6));

#line default
#line hidden
            EndContext();
            BeginContext(2934, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2990, 42, false);
#line 107 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.OpName7));

#line default
#line hidden
            EndContext();
            BeginContext(3032, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(3088, 42, false);
#line 110 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.OpName8));

#line default
#line hidden
            EndContext();
            BeginContext(3130, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(3186, 42, false);
#line 113 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.OpName9));

#line default
#line hidden
            EndContext();
            BeginContext(3228, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(3284, 43, false);
#line 116 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.OpName10));

#line default
#line hidden
            EndContext();
            BeginContext(3327, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(3383, 49, false);
#line 119 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
           Write(Html.ActionLink("编辑", "Edit", new { id=item.Id }));

#line default
#line hidden
            EndContext();
            BeginContext(3432, 36, true);
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
            EndContext();
#line 122 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\PresetFunction\Index.cshtml"
}

#line default
#line hidden
            BeginContext(3471, 24, true);
            WriteLiteral("    </tbody>\r\n</table>\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<AccessManagementServices.DOTS.PresetFunctionViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
