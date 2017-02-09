function BindCompanies() {

    var responsiveHelper = undefined;
    var breakpointDefinition = {
        tablet: 1024,
        tabletlandscape: 768,
        phone: 480
    };
    var tableElement = $('#jq-datatables-example');
    tableElement.dataTable({
        autoWidth: false,
        preDrawCallback: function () {
            if (!responsiveHelper) {
                responsiveHelper = new ResponsiveDatatablesHelper(tableElement, breakpointDefinition);
            }
        },
        rowCallback: function (nRow) {
            responsiveHelper.createExpandIcon(nRow);
        },
        drawCallback: function (oSettings) {
            responsiveHelper.respond();
        },
       // "order": [[$("#HD_SortValue").val(), "" + $("#HD_SortDirection").val() + ""]],
        "aLengthMenu": [[10, 25, 50], [10, 25, 50]],
        //"iDisplayStart": parseInt($("#HD_PageIndex").val()),
        "iDisplayLength": 10,
        "pagingType": "full_numbers",
        "processing": true,
        "serverSide": true,
        "bFilter": false,
        "sAjaxSource": "../ManageCompany/CompanyList",
        "fnServerParams": function (aoData) {
            aoData.push(
                { "name": "CompanyBranch", "value": "" + $("#ddlBranch").val() + "" },
                { "name": "CompanySalesRepresentative", "value": "" + $("#ddlSalesRepAccount").val() + "" },
                //{ "name": "CompanyFilterString", "value": "" + $("#HD_CompanyFilterString").val() + "" },
                { "name": "CompanyList_Keyword", "value": "" + $("#txtKeyword").val() + "" }
               // { "name": "CompanyList_IsFromBack", "value": "" + $("#HD_IsFromBack").val() + "" },
               // { "name": "CompanyFilterString_ActualQueryCondition", "value": "" + $("#HD_CompanyFilterString_ActualQueryCondition").val() + "" }
                );
        },
        "fnServerData": function (sSource, aoData, fnCallback) {
            $.ajax({
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "type": "GET",
                "url": sSource,
                "data": aoData,
                "success":
                            function (msg) {
                                var json = jQuery.parseJSON(msg.d);
                                fnCallback(json);
                                $("#jq-datatables-example").show();
                            }
            });
        },
        "asStripeClasses": ['odd gradeX', 'even gradeC'],
        "aoColumnDefs": [
          {
              "targets": [0], "visible": true, "bSortable": false,
              "render": function (data, type, row) {
                  return "<input class=\"fa  fa-square-o\" type=\"checkbox\"  id=\"chkRow_" + row["AccountId"] + "\"   />";
              }
          },
            {
                "targets": [1], "visible": true, "bSortable": false,
                "render": function (data, type, row) {
                    return "<span class=\"fa fa-times\" onclick=\"DeleteCompaniesConfirmation('" + row["AccountId"] + "');\"></span>";
                }
            },
            { "targets": [2], "visible": true, "bSortable": true, "mDataProp": "AccountId" },
            {
                "targets": [3], "visible": true, "bSortable": true, "mDataProp": "Account_Name",
                "render": function (data, type, row) {
                    return "<a href=\"#\" onclick=\"return OpenCompanyDetail('" + row["AccountId"] + "')\">" + row["Account_Name"] + "</a>";
                }
            },
            { "targets": [4], "visible": true, "bSortable": true, "mDataProp": "accountSite" },
            { "targets": [5], "visible": true, "bSortable": true, "mDataProp": "Phone" },
            { "targets": [6], "visible": true, "bSortable": true, "mDataProp": "shippingcity" },
            { "targets": [7], "visible": true, "bSortable": true, "mDataProp": "shippingzip" },
            { "targets": [8], "visible": true, "bSortable": true, "mDataProp": "createdDate" },
             { "targets": [9], "visible": true, "bSortable": true, "mDataProp": "Account_type" },
            {
                "targets": [10], "visible": true, "bSortable": false, "mDataProp": "AccountId",
                "render": function (data, type, row) {
                    return "<a href=\"#\" onclick=\"return OpenActivityPopUp('" + row["AccountId"] + "','" + row["Account_Name"] + "')\">Activity</a>";
                }
            }
        ],
        "fnDrawCallback": function (oSettings) {
            //$("#HD_PageIndex").val(oSettings._iDisplayStart);
            //$("#HD_PageSize").val(oSettings._iDisplayLength);
            //if ($("#HD_IsFromBack").val() == 'YES') {
            //    $("#HD_IsFromBack").val('NO');
            //}
        }
    }).api();

    $('#jq-datatables-example_wrapper .table-caption').text('');
    $('#jq-datatables-example_wrapper .dataTables_filter input').attr('placeholder', 'Search...');
    $('#jq-datatables-example').on('click', 'th input:checkbox', function () {
        var that = this;
        $(this).closest('table').find('tr > td:first-child input:checkbox').each(function () {
            this.checked = that.checked;
            $(this).closest('tr').toggleClass('selected');
        });
    });
    $('#jq-datatables-example tbody').on('click', 'tr > td:first-child input:checkbox', function () {
        // CustomBootBoxAlertMessage($(this)[0].checked);
        if ($(this)[0].checked)
        { $(this).parent().parent().addClass('selected'); }
        else { $(this).parent().parent().removeClass('selected'); }
    });
}