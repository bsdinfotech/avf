#pragma checksum "C:\Users\BSD\source\repos\AskForVets\CoreMVCApplication\Views\Patients\Ph.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9a41ff9405d19dc919f498a059a3b18d39b86a16"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Patients_Ph), @"mvc.1.0.view", @"/Views/Patients/Ph.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a41ff9405d19dc919f498a059a3b18d39b86a16", @"/Views/Patients/Ph.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"691fac788864ab06db875c83426f9fcf76cb915b", @"/Views/_ViewImports.cshtml")]
    public class Views_Patients_Ph : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\BSD\source\repos\AskForVets\CoreMVCApplication\Views\Patients\Ph.cshtml"
  
    ViewData["Title"] = "Ph";
    Layout = "~/Views/Shared/_GlobalLawLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9a41ff9405d19dc919f498a059a3b18d39b86a163533", async() => {
                WriteLiteral(@"
    <meta name=""viewport"" content=""width=device-width, initial-scale=1, maximum-scale=1, user-scalable=yes, minimum-scale=1"">
    <title>Patient</title>
    <link rel=""stylesheet"" href=""css/style.css"">
    <link rel=""stylesheet"" href=""css/bootstrap.min.css"">
    <link rel=""stylesheet"" href=""css/dataTables.bootstrap.css"">
    <link rel=""stylesheet"" href=""css/dataTables.responsive.css"">

");
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9a41ff9405d19dc919f498a059a3b18d39b86a164914", async() => {
                WriteLiteral("\r\n    <div class=\"askforvets\">\r\n");
                WriteLiteral("        <main>\r\n");
                WriteLiteral(@"            <div class=""dashbord"">
                <!-- Dashboard -->
                <div class=""frame-heading only-data-grid"">
                    <div class=""heading flex-grid"">
                        <div class=""title"">Pharmacy </div>
                        <div class=""link"">
                            <div class=""f-input""> <input type=""text"" placeholder=""Re-Issue Pharmacy"" /></div>

                            <div class=""f-input ""> <input type=""text"" placeholder=""Dispense Pharmacy"" /></div>
                            <a href=""javascript:void(0)"" class=""addbutton modelLink "" data-id=""appointment"">New Request</a>
                        </div>
                    </div>
                    <table id=""patient"" class=""datatable table table-striped table-bordered"">
                        <thead>
                            <tr>
                                <th>Prescription Date</th>
                                <th>Patient Name</th>
                                <th>Appointment ");
                WriteLiteral(@"Type</th>
                                <th>Pharmacy</th>
                                <th>Quantity (Pcs.)</th>
                                <th class=""sortingnone""> </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr class=""gradeX"">
                                <td>22-05-2021</td>
                                <td> Fluffy </td>
                                <td>TELEMEDICINE</td>
                                <td>Anthrax-D23</td>
                                <td class=""center"">500</td>

                                <td>
                                    <div class=""actionbtn"">
                                        <a href=""javascript:void(0)"" class=""update fm""><i class=""fa fa-pencil-square-o"" aria-hidden=""true""></i></a>
                                        <a href=""javascript:void(0)"" data-id=""delete_patient"" class=""delete fm modelLink""><i class=""fa fa-trash-o"" aria-hidden=""true""");
                WriteLiteral(@"></i></a>

                                    </div>
                                </td>
                            </tr>
                            <tr class=""gradeX"">
                                <td>22-05-2021</td>
                                <td> Fluffy </td>
                                <td>TELEMEDICINE</td>
                                <td>Anthrax-D23</td>
                                <td class=""center"">25</td>

                                <td>
                                    <div class=""actionbtn"">
                                        <a href=""javascript:void(0)"" class=""update fm""><i class=""fa fa-pencil-square-o"" aria-hidden=""true""></i></a>
                                        <a href=""javascript:void(0)"" data-id=""delete_patient"" class=""delete fm modelLink""><i class=""fa fa-trash-o"" aria-hidden=""true""></i></a>

                                    </div>
                                </td>
                            </tr>
                         ");
                WriteLiteral(@"   <tr class=""gradeX"">
                                <td>22-05-2021</td>
                                <td> Fluffy </td>
                                <td>TELEMEDICINE</td>
                                <td>Anthrax-D23</td>
                                <td class=""center"">80</td>

                                <td>
                                    <div class=""actionbtn"">
                                        <a href=""javascript:void(0)"" class=""update fm""><i class=""fa fa-pencil-square-o"" aria-hidden=""true""></i></a>
                                        <a href=""javascript:void(0)"" data-id=""delete_patient"" class=""delete fm modelLink""><i class=""fa fa-trash-o"" aria-hidden=""true""></i></a>

                                    </div>
                                </td>
                            </tr>
                            <tr class=""gradeX"">
                                <td>22-05-2021</td>
                                <td> Fluffy </td>
                            ");
                WriteLiteral(@"    <td>TELEMEDICINE</td>
                                <td>Anthrax-D23</td>
                                <td class=""center"">30</td>

                                <td>
                                    <div class=""actionbtn"">
                                        <a href=""javascript:void(0)"" class=""update fm""><i class=""fa fa-pencil-square-o"" aria-hidden=""true""></i></a>
                                        <a href=""javascript:void(0)"" data-id=""delete_patient"" class=""delete fm modelLink""><i class=""fa fa-trash-o"" aria-hidden=""true""></i></a>

                                    </div>
                                </td>
                            </tr>

                        </tbody>
                    </table>
                </div>
                <!-- End Dashboard -->
            </div>
        </main>
        <div id=""popupBox"" class=""overlay box-model""> </div>
    </div>

    <script src=""js/jquery-3.6.0.min.js""></script>

    <script src=""js/jquery.dataTables.mi");
                WriteLiteral(@"n.js""></script>
    <script src=""js/dataTables.bootstrap.js""></script>
    <script src=""js/dataTables.responsive.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js""></script>
    <script src=""js/custom.js""></script>


    <script>$(function () {
var chart;
$(document).ready(function() {
    // $.getJSON(""data.php"", function(json) {

        chart = new Highcharts.Chart({
            chart: {
                renderTo: 'container',
                type: 'line',
                marginRight: 130,
                marginBottom: 25
            },
            title: {
                text: 'Disease',
                x: -20 //center
            },
            subtitle: {
                text: '',
                x: -20
            },
            xAxis: {
                categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
            },
            yAxis: {
                title: {
          ");
                WriteLiteral(@"          text: 'Patient Count'
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                formatter: function() {
                        return '<b>'+ this.series.name +'</b><br/>'+
                        this.x +': '+ this.y;
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -10,
                y: 100,
                borderWidth: 0
            },
            series: [{
                name: 'Jane',
                data: [1, 0, 4]
            }, {
                name: 'John',
                data: [5, 7, 3]
            }]
        });
//  });

});
});</script>
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
            WriteLiteral("\r\n</html>\r\n");
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
