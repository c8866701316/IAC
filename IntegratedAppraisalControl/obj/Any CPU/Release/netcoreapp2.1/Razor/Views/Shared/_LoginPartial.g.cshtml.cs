#pragma checksum "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Shared\_LoginPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1dd15e6c7fe6df7e8f4cd12ce8aaa5f2ee5617a4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__LoginPartial), @"mvc.1.0.view", @"/Views/Shared/_LoginPartial.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/_LoginPartial.cshtml", typeof(AspNetCore.Views_Shared__LoginPartial))]
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
#line 1 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\_ViewImports.cshtml"
using IntegratedAppraisalControl;

#line default
#line hidden
#line 2 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\_ViewImports.cshtml"
using IntegratedAppraisalControl.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1dd15e6c7fe6df7e8f4cd12ce8aaa5f2ee5617a4", @"/Views/Shared/_LoginPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0e529617ff9fadcf5455a429964ecf77932fb74b", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__LoginPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Content/images/profileImg.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("profile image"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Shared\_LoginPartial.cshtml"
 if (User.Identity.IsAuthenticated)
{

#line default
#line hidden
            BeginContext(42, 20, true);
            WriteLiteral("    <p>\r\n        <i>");
            EndContext();
            BeginContext(62, 65, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "bca5fa615cec45e3b9cd1ef437946c2b", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(127, 20, true);
            WriteLiteral("</i>\r\n        <span>");
            EndContext();
            BeginContext(148, 18, false);
#line 6 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Shared\_LoginPartial.cshtml"
         Write(User.Identity.Name);

#line default
#line hidden
            EndContext();
            BeginContext(166, 51, true);
            WriteLiteral(" </span>\r\n    </p>\r\n    <div class=\"logoutDiv\">\r\n\r\n");
            EndContext();
#line 10 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Shared\_LoginPartial.cshtml"
         using (Html.BeginForm("LogOff", "Account", FormMethod.Post, new { id = "logoutForm" }))
        {
            

#line default
#line hidden
            BeginContext(339, 23, false);
#line 12 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Shared\_LoginPartial.cshtml"
       Write(Html.AntiForgeryToken());

#line default
#line hidden
            EndContext();
            BeginContext(364, 65, true);
            WriteLiteral("            <ul>\r\n                <li>\r\n                    <p>\r\n");
            EndContext();
#line 16 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Shared\_LoginPartial.cshtml"
                         if (ViewBag.BaseClientId > 0)
                        {

#line default
#line hidden
            BeginContext(512, 54, true);
            WriteLiteral("                            <strong>File No:</strong> ");
            EndContext();
            BeginContext(567, 20, false);
#line 18 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Shared\_LoginPartial.cshtml"
                                                 Write(ViewBag.BaseFileName);

#line default
#line hidden
            EndContext();
            BeginContext(587, 101, true);
            WriteLiteral("<br />\r\n                            <strong>Client Name:</strong>\r\n                            <span>");
            EndContext();
            BeginContext(689, 22, false);
#line 20 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Shared\_LoginPartial.cshtml"
                             Write(ViewBag.BaseClientName);

#line default
#line hidden
            EndContext();
            BeginContext(711, 15, true);
            WriteLiteral("</span><br />\r\n");
            EndContext();
#line 21 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Shared\_LoginPartial.cshtml"
                        }

#line default
#line hidden
            BeginContext(753, 51, true);
            WriteLiteral("                        <strong>Username:</strong> ");
            EndContext();
            BeginContext(805, 18, false);
#line 22 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Shared\_LoginPartial.cshtml"
                                              Write(User.Identity.Name);

#line default
#line hidden
            EndContext();
            BeginContext(823, 51, true);
            WriteLiteral("\r\n                    </p>\r\n                </li>\r\n");
            EndContext();
#line 25 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Shared\_LoginPartial.cshtml"
                 if (ViewBag.BaseIsLocationChangeAllowed == true)
                {

#line default
#line hidden
            BeginContext(960, 42, true);
            WriteLiteral("                    <li class=\"setting\"><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1002, "\"", 1046, 1);
#line 27 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Shared\_LoginPartial.cshtml"
WriteAttributeValue("", 1009, Url.Action("ClientSelection","Home"), 1009, 37, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1047, 20, true);
            WriteLiteral(">Settings</a></li>\r\n");
            EndContext();
#line 28 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Shared\_LoginPartial.cshtml"
                }

#line default
#line hidden
            BeginContext(1086, 181, true);
            WriteLiteral("\r\n                <li class=\"logout\">\r\n                    <a href=\"javascript:document.getElementById(\'logoutForm\').submit()\">Logout</a>\r\n                </li>\r\n            </ul>\r\n");
            EndContext();
#line 34 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Shared\_LoginPartial.cshtml"
        }

#line default
#line hidden
            BeginContext(1278, 12, true);
            WriteLiteral("    </div>\r\n");
            EndContext();
#line 36 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Shared\_LoginPartial.cshtml"
}
else
{

#line default
#line hidden
            BeginContext(1302, 69, true);
            WriteLiteral("    <ul id=\"css3menu1\" class=\"topmenu\">\r\n        <li class=\"topmenu\">");
            EndContext();
            BeginContext(1372, 156, false);
#line 40 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Shared\_LoginPartial.cshtml"
                       Write(Html.ActionLink("Register", "Register", "Account", routeValues: null, htmlAttributes: new { id = "registerLink", @style = "height:32px;line-height:32px;" }));

#line default
#line hidden
            EndContext();
            BeginContext(1528, 35, true);
            WriteLiteral("</li>\r\n        <li class=\"topmenu\">");
            EndContext();
            BeginContext(1564, 148, false);
#line 41 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Shared\_LoginPartial.cshtml"
                       Write(Html.ActionLink("Log in", "LogOn", "Account", routeValues: null, htmlAttributes: new { id = "loginLink", @style = "height:32px;line-height:32px;" }));

#line default
#line hidden
            EndContext();
            BeginContext(1712, 18, true);
            WriteLiteral("</li>\r\n    </ul>\r\n");
            EndContext();
#line 43 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Shared\_LoginPartial.cshtml"
}

#line default
#line hidden
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
