#pragma checksum "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Location\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e033b9c9a59d621bcc0d1380c5ddaa567e5cab8b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Location_Index), @"mvc.1.0.view", @"/Views/Location/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Location/Index.cshtml", typeof(AspNetCore.Views_Location_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e033b9c9a59d621bcc0d1380c5ddaa567e5cab8b", @"/Views/Location/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0e529617ff9fadcf5455a429964ecf77932fb74b", @"/Views/_ViewImports.cshtml")]
    public class Views_Location_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IntegratedAppraisalControl.Models.DTO.TblClientsDTO>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Location\Index.cshtml"
  
    ViewBag.Title = "Location List";

#line default
#line hidden
            BeginContext(105, 125, true);
            WriteLiteral("\r\n<div class=\"panel-group main\" id=\"accordion\">\r\n    <div class=\"panel panel-primary\">\r\n        <div class=\"panel-heading\">\r\n");
            EndContext();
#line 9 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Location\Index.cshtml"
             if (!(ViewBag.BaseReadOnly))
            {

#line default
#line hidden
            BeginContext(288, 48, true);
            WriteLiteral("            <div class=\"btn-group pull-right\">\r\n");
            EndContext();
            BeginContext(467, 101, true);
            WriteLiteral("                <button class=\"btn btn-default btn-sm\" id=\"btnAdd\">Add</button>\r\n            </div>\r\n");
            EndContext();
#line 15 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Location\Index.cshtml"
            }

#line default
#line hidden
            BeginContext(583, 54, true);
            WriteLiteral("            <h4 class=\"panel-title\">\r\n                ");
            EndContext();
            BeginContext(638, 13, false);
#line 17 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Location\Index.cshtml"
           Write(ViewBag.Title);

#line default
#line hidden
            EndContext();
            BeginContext(651, 3310, true);
            WriteLiteral(@"
            </h4>
        </div>
        <div id=""Locationsections"" class=""panel-collapse collapse in"">
            <div class=""panel-body"">

                <div class=""row"">
                    <div class=""col-md-12"">
                        <div class=""table-responsive"" style=""overflow-x:scroll"">
                            <table id=""tblLocationList"">
                                <thead>
                                    <tr>
                                        <th>
                                            Name
                                        </th>
                                        <th>
                                            File No
                                        </th>
                                        <th>
                                            Address1
                                        </th>
                                        <th>
                                            Address2
                                    ");
            WriteLiteral(@"    </th>
                                        <th>
                                            City
                                        </th>
                                        <th>
                                            State
                                        </th>
                                        <th>
                                            Zip Code
                                        </th>
                                        <th>
                                            Point Of Contact
                                        </th>
                                        <th>
                                            Telephone
                                        </th>
                                        <th>
                                            Report Year
                                        </th>
                                        <th>
                                            Accounting Year
                   ");
            WriteLiteral(@"                     </th>
                                        <th>
                                            Aquisition Cost Cutoff
                                        </th>
                                        <th>
                                            Last Updated
                                        </th>
                                        <th>
                                            Annual Depreciation
                                        </th>
                                        <th>
                                            First year Depreciation
                                        </th>
                                        <th>
                                            Status
                                        </th>
                                        <th>
                                            Edit
                                        </th>
                                    </tr>
                                <");
            WriteLiteral("/thead>\r\n                                <tbody></tbody>\r\n                            </table>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            EndContext();
#line 92 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Location\Index.cshtml"
 if (!(ViewBag.BaseReadOnly))
{

#line default
#line hidden
            BeginContext(3995, 899, true);
            WriteLiteral(@"    <div class=""modal fade"" id=""modalAddLoaction"" tabindex=""-1"" role=""dialog"" aria-hidden=""true"">
        <div class=""modal-dialog modal-lg"" style=""width: 1140px;"">
            <div class=""modal-content"">
                <!-- Modal Header -->
                <div class=""modal-header"">
                    <button type=""button"" class=""close""
                            data-dismiss=""modal"">
                        <span aria-hidden=""true"">&times;</span>
                        <span class=""sr-only"">Close</span>
                    </button>
                    <h4 class=""modal-title"">
                        Add/Edit Location
                    </h4>
                </div>
                <!-- Modal Body -->
                <div class=""modal-body"" id=""popupAdd"">
                </div>
                <!-- Modal Footer -->
            </div>
        </div>
    </div>
");
            EndContext();
#line 115 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Location\Index.cshtml"
}

#line default
#line hidden
            BeginContext(4897, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(4918, 120, true);
                WriteLiteral("\r\n    <script type=\"text/javascript\">\r\n\r\n        function LoadTaleData() {\r\n            $.ajax({\r\n                url: \'");
                EndContext();
                BeginContext(5039, 38, false);
#line 123 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Location\Index.cshtml"
                 Write(Url.Action("LocationList", "Location"));

#line default
#line hidden
                EndContext();
                BeginContext(5077, 3054, true);
                WriteLiteral(@"',
                type: 'GET',
                dataType: ""json"",
                success: function (response) {

                         var table = $('#tblLocationList').DataTable({
                                data: response.data,
                                 columns: [
                                     { data: ""Name"" },
                                     { data: ""FileNo"" },
                                     { data: ""Addrees1"" },
                                     { data: ""Address2"" },
                                     { data: ""City"" },
                                     { data: ""State"" },
                                     { data: ""ZipCode"" },
                                     { data: ""PointOfContact"" },
                                     { data: ""Telephone"" },
                                     { data: ""ReportYear"" },
                                     { data: ""AccountingYear"" },
                                     { data: ""AquisitionCostCutOff"" },
 ");
                WriteLiteral(@"                                    { data: ""LastUpdated"" },
                                     { data: ""AnnualDepreciationText"" },
                                     { data: ""FirstYearDepreciationText"" },
                                     { data: ""Active"" },
                                     { data: ""ClientId"" }
                                    ],
                                destroy: true,
                                ""aLengthMenu"": [
                                    [5, 10, 15, 20, -1],
                                    [5, 10, 15, 20, ""All""]
                             ],
                                 dom: 'Bfrtip',
                                buttons: [
                                    {
                                        extend: 'csvHtml5',
                                        title: 'Loaction csv',
                                        className:'btn btn-primary',
                                        exportOptions: {
                    ");
                WriteLiteral(@"                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15]
                                        }
                                    },
                                    {
                                        extend: 'excelHtml5',
                                        title: 'Loaction excel',
                                        className: 'btn btn-primary',
                                        exportOptions: {
                                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15]
                                        }
                                    }
                             ],
                             ""aoColumnDefs"": [
                                 {
                                     ""aTargets"": [16],
                                     ""visible"": false
                                 }
                             ],

                             ""iDisplayLength"": ");
                EndContext();
                BeginContext(8132, 20, false);
#line 180 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Location\Index.cshtml"
                                          Write(ViewBag.BasePageSize);

#line default
#line hidden
                EndContext();
                BeginContext(8152, 430, true);
                WriteLiteral(@",
                             ""language"": {
                                 ""emptyTable"": ""No data available in table""
                             },
                             ""drawCallback"": function (settings) {
                                 var api = this.api();
                                 $(""#tblLocationList tbody tr"").dblclick(function () {
                                     window.location.href = '");
                EndContext();
                BeginContext(8583, 30, false);
#line 187 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Location\Index.cshtml"
                                                        Write(Url.Action("Edit", "Location"));

#line default
#line hidden
                EndContext();
                BeginContext(8613, 461, true);
                WriteLiteral(@"' + '?Id=' + table.row(this).data().ClientId;
                                 });
                             }
                         })
                    //}
                },
                error: function (req, status, error) {
                    alert('There is some issue with server. Please try again later.');
                }
        });
        }

        function loadPopup1(Id) {

            $.ajax({
                url: '");
                EndContext();
                BeginContext(9075, 34, false);
#line 202 "E:\Client\Fiverr\Chris Kuntz\Project\IcaHomeValuation\IntegratedAppraisalControl\Views\Location\Index.cshtml"
                 Write(Url.Action("_AddEdit", "Location"));

#line default
#line hidden
                EndContext();
                BeginContext(9109, 2262, true);
                WriteLiteral(@"',
                type: 'GET',
                data: {
                    Id: Id
                },
                success: function (html) {
                    $('#popupAdd').html(html);

                    if ($('#Active').val() == 'True') {
                        $(""input[name=optrole][value='Active']"").prop(""checked"", true);
                    }
                    else {
                        $(""input[name=optrole][value='Inactive']"").prop(""checked"", true);
                    }

                    if (!($('#modalAddLoaction').is(':visible')))
                    {
                        $('#modalAddLoaction').modal('show');
                    }

                    submitForm();
                },
                error: function () {
                    alert('There is some issue, Please try again after some time.')
                }
            });
        }

        function submitForm() {
            fnFormvalidate(""frmLocation"");
            $('#frmLocation'");
                WriteLiteral(@").submit(function (e) {
                e.preventDefault(); // avoid to execute the actual submit of the form.
                if (!$(""#frmLocation"").valid()) {
                    return;
                }


                $(this).ajaxSubmit({
                    success: function (responseText, statusText, xhr, $form) {
                        alert(responseText.Message);
                        console.log(responseText.Data);
                        if (responseText.Status) {
                            $('#frmLocation').resetForm();
                            //$('.imgpreview').attr('src', $('.imgpreview').data('oldimage'));
                            LoadTaleData();
                        }
                    }
                });
                return false;
            });
        }

        $(document).ready(function () {
            
            $('#btnAdd').click(function () {
                loadPopup1(0);
            });

            $(document).on('change', 'inpu");
                WriteLiteral("t[type=radio][name=optrole]\', function () {\r\n                $(\'#Active\').val(false);\r\n                $(\'#\' + $(this).val()).val(true);\r\n            });\r\n\r\n            LoadTaleData();\r\n        });\r\n    </script>\r\n");
                EndContext();
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IntegratedAppraisalControl.Models.DTO.TblClientsDTO> Html { get; private set; }
    }
}
#pragma warning restore 1591
