#pragma checksum "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a39cdaa2c49bf55e762172dbf08b01e7d1b526b3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Index), @"mvc.1.0.view", @"/Views/Account/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Account/Index.cshtml", typeof(AspNetCore.Views_Account_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a39cdaa2c49bf55e762172dbf08b01e7d1b526b3", @"/Views/Account/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3826a4965545f2f0ad57e10c8f472e1ac40b4acd", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<AccessManagementServices.DOTS.AccountViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(68, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
            BeginContext(111, 29, true);
            WriteLiteral("\r\n<h1>Index</h1>\r\n\r\n<p>\r\n    ");
            EndContext();
            BeginContext(140, 37, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a39cdaa2c49bf55e762172dbf08b01e7d1b526b33887", async() => {
                BeginContext(163, 10, true);
                WriteLiteral("Create New");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(177, 92, true);
            WriteLiteral("\r\n</p>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(270, 38, false);
#line 16 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Id));

#line default
#line hidden
            EndContext();
            BeginContext(308, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(364, 47, false);
#line 19 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.AccountName));

#line default
#line hidden
            EndContext();
            BeginContext(411, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(467, 44, false);
#line 22 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Password));

#line default
#line hidden
            EndContext();
            BeginContext(511, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(567, 40, false);
#line 25 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(607, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(663, 40, false);
#line 28 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Type));

#line default
#line hidden
            EndContext();
            BeginContext(703, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(759, 42, false);
#line 31 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Status));

#line default
#line hidden
            EndContext();
            BeginContext(801, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(857, 40, false);
#line 34 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.City));

#line default
#line hidden
            EndContext();
            BeginContext(897, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(953, 41, false);
#line 37 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Phone));

#line default
#line hidden
            EndContext();
            BeginContext(994, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1050, 46, false);
#line 40 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.CreateTime));

#line default
#line hidden
            EndContext();
            BeginContext(1096, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1152, 48, false);
#line 43 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.CreateUserId));

#line default
#line hidden
            EndContext();
            BeginContext(1200, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1256, 45, false);
#line 46 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.CompanyId));

#line default
#line hidden
            EndContext();
            BeginContext(1301, 55, true);
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
            EndContext();
            BeginContext(1357, 44, false);
#line 49 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.BranchId));

#line default
#line hidden
            EndContext();
            BeginContext(1401, 86, true);
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
            EndContext();
#line 55 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
            BeginContext(1519, 48, true);
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(1568, 37, false);
#line 58 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Id));

#line default
#line hidden
            EndContext();
            BeginContext(1605, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(1661, 46, false);
#line 61 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.AccountName));

#line default
#line hidden
            EndContext();
            BeginContext(1707, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(1763, 43, false);
#line 64 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Password));

#line default
#line hidden
            EndContext();
            BeginContext(1806, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(1862, 39, false);
#line 67 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Name));

#line default
#line hidden
            EndContext();
            BeginContext(1901, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(1957, 39, false);
#line 70 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Type));

#line default
#line hidden
            EndContext();
            BeginContext(1996, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2052, 41, false);
#line 73 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Status));

#line default
#line hidden
            EndContext();
            BeginContext(2093, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2149, 39, false);
#line 76 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.City));

#line default
#line hidden
            EndContext();
            BeginContext(2188, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2244, 40, false);
#line 79 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Phone));

#line default
#line hidden
            EndContext();
            BeginContext(2284, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2340, 45, false);
#line 82 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.CreateTime));

#line default
#line hidden
            EndContext();
            BeginContext(2385, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2441, 47, false);
#line 85 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.CreateUserId));

#line default
#line hidden
            EndContext();
            BeginContext(2488, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2544, 44, false);
#line 88 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.CompanyId));

#line default
#line hidden
            EndContext();
            BeginContext(2588, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2644, 43, false);
#line 91 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.BranchId));

#line default
#line hidden
            EndContext();
            BeginContext(2687, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(2743, 51, false);
#line 94 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.ActionLink("Edit", "Edit", new { id=item.Id }));

#line default
#line hidden
            EndContext();
            BeginContext(2794, 20, true);
            WriteLiteral(" |\r\n                ");
            EndContext();
            BeginContext(2815, 71, false);
#line 95 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
            EndContext();
            BeginContext(2886, 20, true);
            WriteLiteral(" |\r\n                ");
            EndContext();
            BeginContext(2907, 69, false);
#line 96 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
           Write(Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ }));

#line default
#line hidden
            EndContext();
            BeginContext(2976, 36, true);
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
            EndContext();
#line 99 "C:\项目\AccessManagementSystem\AccessManagement\AccessManagement\Views\Account\Index.cshtml"
}

#line default
#line hidden
            BeginContext(3015, 24, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<AccessManagementServices.DOTS.AccountViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
