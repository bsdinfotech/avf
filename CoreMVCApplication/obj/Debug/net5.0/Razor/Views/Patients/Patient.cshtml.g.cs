#pragma checksum "C:\Users\BSD\source\repos\AskForVets\CoreMVCApplication\Views\Patients\Patient.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6fcb2986e4e08c7383098aff3bc6790c2f2265f8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Patients_Patient), @"mvc.1.0.view", @"/Views/Patients/Patient.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6fcb2986e4e08c7383098aff3bc6790c2f2265f8", @"/Views/Patients/Patient.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"691fac788864ab06db875c83426f9fcf76cb915b", @"/Views/_ViewImports.cshtml")]
    public class Views_Patients_Patient : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString(" width: 35%; background: #652B88;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("submit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "addpatient", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Patients", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("formmethod", new global::Microsoft.AspNetCore.Html.HtmlString("post"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("addbutton btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-row"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\BSD\source\repos\AskForVets\CoreMVCApplication\Views\Patients\Patient.cshtml"
  
    ViewData["Title"] = "Patient";
    Layout = "~/Views/Shared/_GlobalLawLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6fcb2986e4e08c7383098aff3bc6790c2f2265f87161", async() => {
                WriteLiteral("\r\n");
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6fcb2986e4e08c7383098aff3bc6790c2f2265f88127", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6fcb2986e4e08c7383098aff3bc6790c2f2265f88389", async() => {
                    WriteLiteral(@"
        <div class=""main-paients"">
            <div class=""col-md-12"">
                <div class=""row"">
                    <div class=""col-md-7"">
                        <h5>Patients</h5>
                    </div>
                    <div class=""col-md-5"" style=""text-align:right"">
                        <div class=""frame-heading only-data-grid"">
                            <div class=""link"">
                                <div class=""f-select"">
                                    <select class=""form-control inputText required"" id=""ddlPetBreed"">
                                        ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6fcb2986e4e08c7383098aff3bc6790c2f2265f89283", async() => {
                        WriteLiteral("Pet Breeds");
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\r\n");
#nullable restore
#line 23 "C:\Users\BSD\source\repos\AskForVets\CoreMVCApplication\Views\Patients\Patient.cshtml"
                                         if (ViewBag.PetBreedList != null)
                                        {
                                            foreach (var item in ViewBag.PetBreedList as List<SelectListItem>)
                                            {

#line default
#line hidden
#nullable disable
                    WriteLiteral("                                                ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6fcb2986e4e08c7383098aff3bc6790c2f2265f811106", async() => {
#nullable restore
#line 27 "C:\Users\BSD\source\repos\AskForVets\CoreMVCApplication\Views\Patients\Patient.cshtml"
                                                                       Write(item.Text);

#line default
#line hidden
#nullable disable
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                    BeginWriteTagHelperAttribute();
#nullable restore
#line 27 "C:\Users\BSD\source\repos\AskForVets\CoreMVCApplication\Views\Patients\Patient.cshtml"
                                                   WriteLiteral(item.Value);

#line default
#line hidden
#nullable disable
                    __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                    __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\r\n");
#nullable restore
#line 28 "C:\Users\BSD\source\repos\AskForVets\CoreMVCApplication\Views\Patients\Patient.cshtml"
                                            }
                                        }

#line default
#line hidden
#nullable disable
                    WriteLiteral(@"                                    </select>
                                </div>
                                <div class=""f-select"">
                                    <select class=""form-control inputText required"" id=""ddlPetBreed"">
                                        ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6fcb2986e4e08c7383098aff3bc6790c2f2265f813693", async() => {
                        WriteLiteral("All Status");
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\r\n\r\n                                    </select>\r\n                                </div>\r\n                                ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6fcb2986e4e08c7383098aff3bc6790c2f2265f815112", async() => {
                        WriteLiteral("\r\n                                    +New Patient\r\n                                ");
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Action = (string)__tagHelperAttribute_3.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
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
                </div>
            </div>
            <br />
            <div class=""col-md-12 main-table"">
                <table class=""table"" style=""width:100%"">
                    <thead>
                        <tr style=""background: #F5F5FA !important;"">
");
                    WriteLiteral(@"                            <th style=""display:none"">Pet ID</th>
                            <th>Pet's Parent Name</th>
                            <th>Pet Name</th>
                            <th>Pet Gender</th>
                            <th>Breed</th>
                            <th></th>
                            <th></th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody class=""tbodyData""></tbody>
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
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
    <script type=""text/javascript"" src=""https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js""></script>
    <script type=""text/javascript"">
        function ShowDataInTable() {
            var item;
            $.ajax({
                url: ""/Patients/ShowPatientRecord"",
                data: JSON.stringify(item),
                dataType: 'JSON',
                type: 'GET',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    var html = '';
                    var index = 1;
                    $.each(data, function (key, item) {
                        html += '<tr>';
                        // html += '<td>' + index + '</td>';
                        //html += '<td>' + item.petParent_Id + '</td>';
                        html += '<td style=""display:none"">' + item.pet_id + '</td>';
                        html += '<td>' + item.fullName + '</td>';
                        html += '<td>' + item.petName +");
                WriteLiteral(@" '</td>';
                        html += '<td>' + item.petGender + '</td>';
                        html += '<td>' + item.breed + '</td>';
                        html += '<td><a class=""btn btn-sm"" asp-area="""" href=""/Patients/addpatient?pet_id=' + item.pet_id + '""><i class=""fa fa-edit""></i></a></td>';
                        html += '<td><a class=""btn btn-sm"" href=""#"" onclick=""return DeletePatient(' + item.pet_id + ')""><i class=""fa fa-trash-o""></i></a></td>';
                        html += '<td><a class=""btn btn-sm"" href=""/Patients/PatientDetalis?petParent_Id=' + item.petParent_Id + '""><i class=""fa fa-eye""></i></a></td>';
                        html += '</tr>';
                        index++;
                    });
                    $('.tbodyData').html(html);
                }
            });
        }
        function DeletePatient(pet_id) {
            var checkstr = confirm('Are You Sure You Want To Delete This?');
            var PetObj = JSON.stringify({ pet_id: pet_id });
       ");
                WriteLiteral(@"     // var item;
            if (checkstr == true) {
                $.ajax({
                    url: ""/Patients/DeletePatientById"",
                    data: JSON.parse(PetObj),
                    dataType: 'JSON',
                    type: 'POST',
                    //contentType: 'application/json; charset=utf-8',
                    success: function (result) {
                        if (result.msg == ""Delete Successfull!!"") {
                            alert(""Delete Success"");
                            ShowDataInTable();
                        }
                    },
                    error: function (errormessage) {
                        alert(errormessage.responseText);
                    }
                });
            }
            else {
                return false;
            }

        }
        function Editbyid(pet_id) {
            var PetObj = JSON.stringify({ pet_id: pet_id });
            $.ajax({
                url: ""/Patients/EditByPatientId"",");
                WriteLiteral(@"
                data: JSON.parse(PetObj),
                dataType: 'JSON',
                type: 'POST',
                success: function (result) {
                    //$('#id').val(result.appointment_id);
                    //var PetParentId = result.petParent_Id;
                    //PopulatePetParentId(PetParentId);
                    //$(""#ddlPetParent"").val(PetParentId).attr(""selected"", ""true"");
                    $('#txtFullName').val(result.fullName);
                    $('#txtMobile').val(result.mobile_no);
                    $('#txtEmail').val(result.email);
                    $('#txtAddress').val(result.address);
                    $('#txtParentNotes').val(result.notes);
                    $('#txtPetName').val(result.petName);
                    $('#breed').val(result.breed);
                    $('#petType').val(result.petType);
                    $('#petGender').val(result.petGender);
                    $('#txtPetDob').val(result.pet_DOB);
                    $(");
                WriteLiteral(@"'#txtWeight').val(result.pet_Weight);
                    $('#petColour').val(result.colour);
                    $('#txtAdopted').val(result.adoptedOn);
                    $('#txtMicrochipNo').val(result.microchipNo);
                    $('#txtdateofchipping').val(result.dateOfChipping);
                    alert(result.data);
                    $('#txtSpay').val(result.spay_Meutered);
                    $('#txtLocchipping').val(result.loc_Chipping);
                    $('#txtIncurance').val(result.insurance);
                    $('#txtPetNotes').val(result.petNotes);
                    ShowDataInTable();

                },
                error: function () {
                    alert(""Data Not Found  !!"");
                }
            });

        }
        ShowDataInTable();
    </script>
    <script type=""text/javascript"" src=""https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/js/select2.min.js""></script>
    <script>
        $(document).ready(function () {
             $(""");
                WriteLiteral("#ddlPetBreed\").select2();\r\n            // $(\"#ddlPetName\").select2();\r\n        });\r\n    </script>\r\n");
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
