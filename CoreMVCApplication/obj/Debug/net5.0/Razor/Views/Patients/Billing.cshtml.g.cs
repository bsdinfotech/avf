#pragma checksum "C:\Users\BSD\source\repos\AskForVets\CoreMVCApplication\Views\Patients\Billing.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1bd0506f6b68418ef7c093e47f2a69e8800f654f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Patients_Billing), @"mvc.1.0.view", @"/Views/Patients/Billing.cshtml")]
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
#line 1 "C:\Users\BSD\source\repos\AskForVets\CoreMVCApplication\Views\_ViewImports.cshtml"
using CoreMVCApplication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\BSD\source\repos\AskForVets\CoreMVCApplication\Views\_ViewImports.cshtml"
using CoreMVCApplication.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1bd0506f6b68418ef7c093e47f2a69e8800f654f", @"/Views/Patients/Billing.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"691fac788864ab06db875c83426f9fcf76cb915b", @"/Views/_ViewImports.cshtml")]
    public class Views_Patients_Billing : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("submit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("background: #652B88; width: 100%;color:white; padding: 6px 7px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn "), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "NewInvoice", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Patients", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\BSD\source\repos\AskForVets\CoreMVCApplication\Views\Patients\Billing.cshtml"
  
    ViewData["Title"] = "Billing";
    Layout = "~/Views/Shared/_GlobalLawLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1bd0506f6b68418ef7c093e47f2a69e8800f654f6055", async() => {
                WriteLiteral(@"
    <style>
        body {
            font-family: Arial;
        }

        /* Style the tab */
        .tab {
            overflow: hidden;
            border-bottom: 1px solid #ccc;
            background-color: #f1f1f100;
        }

            /* Style the buttons inside the tab */
            .tab button {
                background-color: inherit;
                float: left;
                border: none;
                outline: none;
                cursor: pointer;
                padding: 14px 16px;
                transition: 0.3s;
                font-size: 17px;
            }

                /* Change background color of buttons on hover */
                .tab button:hover {
                    background-color: #ddd;
                }

                /* Create an active/current tablink class */
                .tab button.active {
                    background-color: #ccc0;
                    border-bottom: 3px solid;
                }

        /* Styl");
                WriteLiteral("e the tab content */\r\n        .tabcontent {\r\n            display: none;\r\n            border-top: none;\r\n        }\r\n    </style>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1bd0506f6b68418ef7c093e47f2a69e8800f654f8218", async() => {
                WriteLiteral("\r\n    <div class=\"PetAppointment\">\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1bd0506f6b68418ef7c093e47f2a69e8800f654f8522", async() => {
                    WriteLiteral(@"
            <div class=""col-md-12"">
                <div class=""row"">
                    <div class=""col-md-8"">
                        <h5>Invoices</h5>
                    </div>
                    <div class=""col-md-2"">
                        <button type=""button"" style="" width: 100%; border: 1px solid #D8D8DC; padding: 6px 7px;""  class=""btn "">+Add Deposite</button>
                    </div>
                    <div class=""col-md-2"">
                        ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1bd0506f6b68418ef7c093e47f2a69e8800f654f9281", async() => {
                        WriteLiteral("+New Invoice");
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Action = (string)__tagHelperAttribute_3.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral(@"
                    </div>
                </div>
            </div>
            <br />
            <div class=""clearfix""></div>
            <div class=""tab"">
                <button type=""button"" style=""color: #652B88;"" class=""tablinks"" onclick=""openInvoice(event, 'AllInvoice')"" id=""defaultOpen"">All Invoice</button>
                <button type=""button"" style=""color: #652B88;"" class=""tablinks"" onclick=""openInvoice(event, 'Billed')"">Billed</button>
                <button type=""button"" style=""color: #652B88;"" class=""tablinks"" onclick=""openInvoice(event, 'Drafts')"">Drafts</button>
                <button type=""button"" style=""color: #652B88;"" class=""tablinks"" onclick=""openInvoice(event, 'Paid')"">Paid</button>
            </div>
            <br />
            <div id=""AllInvoice"" class=""tabcontent"">
                <div class=""col-md-12 main-table"">
                    <table class=""table"" style=""width:100%"">
                        <thead>
                            <tr style=""background: #F5");
                    WriteLiteral("F5FA !important;\">\r\n");
                    WriteLiteral(@"                                <th>Invoice No.</th>
                                <th>Bill Date</th>
                                <th>Status</th>
                                <th>Patient</th>
                                <th>Total</th>
                                <th>Balance Due</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody class=""InvoicetbodyData""></tbody>
                    </table>
                </div>
            </div>

            <div id=""Billed"" class=""tabcontent"">
                <div class=""col-md-12 main-table"">
                    <table class=""table"" style=""width:100%"">
                        <thead>
                            <tr style=""background: #F5F5FA !important;"">
");
                    WriteLiteral(@"                                <th>Invoice No.</th>
                                <th>Bill Date</th>
                                <th>Status</th>
                                <th>Patient</th>
                                <th>Total</th>
                                <th>Balance Due</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody class=""BilledtbodyData""></tbody>
                    </table>
                </div>
            </div>

            <div id=""Drafts"" class=""tabcontent"">
                <div class=""col-md-12 main-table"">
                    <table class=""table"" style=""width:100%"">
                        <thead>
                            <tr style=""background: #F5F5FA !important;"">
");
                    WriteLiteral(@"                                <th>Invoice No.</th>
                                <th>Bill Date</th>
                                <th>Status</th>
                                <th>Patient</th>
                                <th>Total</th>
                                <th>Balance Due</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody class=""DraftstbodyData""></tbody>
                    </table>
                </div>
            </div>
            <div id=""Paid"" class=""tabcontent"">
                <div class=""col-md-12 main-table"">
                    <table class=""table"" style=""width:100%"">
                        <thead>
                            <tr style=""background: #F5F5FA !important;"">
");
                    WriteLiteral(@"                                <th>Invoice No.</th>
                                <th>Bill Date</th>
                                <th>Status</th>
                                <th>Patient</th>
                                <th>Total</th>
                                <th>Balance Due</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody class=""PaidtbodyData""></tbody>
                    </table>
                </div>
            </div>
        ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
        </div>
        <script>
            function openInvoice(evt, Invoices) {
                var i, tabcontent, tablinks;
                tabcontent = document.getElementsByClassName(""tabcontent"");
                for (i = 0; i < tabcontent.length; i++) {
                    tabcontent[i].style.display = ""none"";
                }
                tablinks = document.getElementsByClassName(""tablinks"");
                for (i = 0; i < tablinks.length; i++) {
                    tablinks[i].className = tablinks[i].className.replace("" active"", """");
                }
                document.getElementById(Invoices).style.display = ""block"";
                evt.currentTarget.className += "" active"";
            }
            document.getElementById(""defaultOpen"").click();
        </script>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>\r\n\r\n");
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
