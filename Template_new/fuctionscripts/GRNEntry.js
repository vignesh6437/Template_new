jQuery(function ($) {
    BindTable();

    function BindTable() {
        $.ajax({
            url: 'GRNEntryPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result.split('|');
                var Resj1 = JSON.parse(pageload[0]);



                BindTab(Resj1, '1');
            }
        });
       
    }


    function BindTab(ResData, type) {

        var currentDate = new Date()
        var day = currentDate.getDate()
        var month = currentDate.getMonth() + 1
        var year = currentDate.getFullYear()
        var currentdate = day + "/" + month + "/" + year;

        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var finalcolsresult;
        var elements = Array();
        var exampleRecord = ResData[0];
        // TABLE BIND     
        if (type == '0') {
            $('#dynamic-table').dataTable().fnDestroy();
            cols.length = 0;
            cols1.length = 0;
        }
        if (exampleRecord) {
            //get keys in object. This will only work if your statement remains true that all objects have identical keys
            var keys = Object.keys(exampleRecord);

            //for each key, add a column definition
            keys.forEach(function (k) {
                cols.push({
                    title: k,
                    data: k,
                    targets: k

                    //optionally do some type detection here for render function
                });
            });

            $.each(cols, function (index, item) {

                item.title, item.targets

                cols1.push({
                    title: item.title,
                    targets: index

                });

                cols1DATA.push({
                    data: item.title,
                });
                finalcolsresult += 'null' + ',';

            });

            var dyTable = $('#dynamic-table');

            $('#dynamic-table').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "ascrollX": "100%",
                "bSort": false,
                "aColumns": [
                    finalcolsresult,
                    { "bSortable": false }
                ],
                "aSorting": [],
                'columnDefs': [{
                    'targets': 0,
                    'searchable': true,
                    'orderable': true,
                    'className': 'dt-body-center'
                }],

                columnDefs: cols1,
                // columns: cols1DATA,
                columns: [

                    { 'data': 'GRNNO' },
                    { 'data': 'DATE' },
                    {

                        'data': 'EDIT',
                        'render': function (data, type, full, meta) {


                            return '<button type="button" class="btn btn-primary shadow btn-xs sharp me-1 editdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit"><i class="fas fa-pencil-alt" aria-hidden="true"></i></button><span style="visibility: hidden;">' + data + '</span>'
                        }
                    },
                ],
                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'BOM Details',
                        text: '<img src="../../Images/excel.png" title="Excel" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" title="PDF" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'BOM Details'


                    }, {
                        extend: 'print',
                        title: 'BOM Details',
                        text: '<img src="../../Images/print.png" title="Print" style="height: 25px;">',
                        footer: false
                    }
                ]
            });

            //$("#dynamic-table").on("click", ".searchdetails", function () {
            //    var $row = $(this).closest("tr");    // Find the row
            //    var $text = $row.find(".sorting_1").text(); // Find the text
            //    $('#modalview').modal('show');
            //    callpopup($text);
            //});





        }
        else {
            $('#dynamic-table tbody').remove();
            $('#dynamic-table thead').remove();
            $('#dynamic-table').dataTable({
                "language": {
                    "emptyTable": "BOM  No records found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false }

                ],
                "scrollCollapse": false,
                "info": true,
                "paging": true,
                "searching": true
            });


        }
        $(".loader").hide("slow");
    }

    $('#btnadd').click(function (event) {

        var action = $('#actiontypeaddd').html();

        var err="";
        if ($('#txtitemcode').val() == "")
        {
            err+="Please enter Itemcode.<br>"
        }
        if ($('#txtitemname').val() == "") {
            err += "Please enter Itemname.<br>"
        }
        if ($('#txtquantity').val() == "") {
            err += "Please enter Quantity.<br>"
        }

        if (err.length == 0) {
            if (window.FormData !== undefined) {
                var formData = new FormData();
                formData.append('MATERIALCODEdts', $('#txtitemcode').val());
                formData.append('MATERIALNAMEdts', $('#txtitemname').val());
                formData.append('QTY', $('#txtquantity').val());
                formData.append('action', action);
                $.ajax({
                    url: 'AddBOMCreationItemDetails',
                    type: "POST",
                    contentType: false, // Not to set any content header  
                    processData: false, // Not to process data  
                    data: formData,
                    success: function (result) {
                        var results = result.split('|');
                        if (results[0] == 'true') {
                            if ($('#actiontypeaddd').html() == "Update")
                                $('#actiontypeaddd').html("Add");

                            BindChildTab(JSON.parse(results[1]), '1');
                            reset();
                        }
                        else {
                            bootbox.alert({
                                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + results[1] + '</span>',
                                size: 'small'
                            });
                        }
                    },
                    error: function (err) {
                        //alert(err.statusText);
                    }
                });


                // $(".loader").hide("slow");
            }
        }
        else
        {
            swal("Oops...", err, "error");
        }

    });
    function BindChildTab(ResData, type) {

        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var finalcolsresult;
        var elements = Array();
        var exampleRecord = ResData[0];
        var rcount = $("#dynamic-tableChild > tbody > tr").length
        // TABLE BIND     
        if (rcount > 0) {
            $('#dynamic-tableChild').dataTable().fnDestroy();
            cols.length = 0;
            cols1.length = 0;
        }
        if (exampleRecord) {
            //get keys in object. This will only work if your statement remains true that all objects have identical keys
            var keys = Object.keys(exampleRecord);

            //for each key, add a column definition
            keys.forEach(function (k) {
                cols.push({


                    title: k,
                    data: k,
                    targets: k

                    //optionally do some type detection here for render function
                });
            });

            $.each(cols, function (index, item) {

                item.title, item.targets

                cols1.push({

                    title: item.title,
                    targets: index

                });

                cols1DATA.push({
                    data: item.title,

                });
                finalcolsresult += 'null' + ',';

            });

            $('#dynamic-tableChild').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "ascrollX": "100%",
                "bPaginate": false,
                "bFilter": false,
                "aColumns": [
                    finalcolsresult,
                    { "bSortable": false }
                ],
                "aSorting": [],
                'columnDefs': [{
                    'targets': 0,
                    'searchable': true,
                    'orderable': true,
                    'className': 'dt-body-center'
                }],

                columnDefs: cols1,

                columns: [

                    { 'data': 'MaterialCode', title: 'Material Code' },
                    { 'data': 'MaterialName', title:'Material Name' },
                    { 'data': 'Quantity', title: 'Quantity' },

                    {

                        'data': 'Remove',
                        'render': function (data, type, full, meta) {

                            return '<button type="button" class="btn btn-danger shadow btn-xs sharp remove" data-toggle="tooltip" data-placement="top" title="Remove"><i class="fa fa-trash"></i></button>'

                        }
                    },
                ],

            });
        }
        else {
            $('#dynamic-tableChild tbody').remove();
            $('#dynamic-tableChild thead').remove();
            $('#dynamic-tableChild').dataTable({
                "language": {
                    "emptyTable": "BOM Details No Records Found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false }


                ],
                "scrollCollapse": false,
                "info": false,
                "paging": false,
                "searching": true
            });

        }


    }

    $("#dynamic-tableChild").on("click", ".remove", function () {
        var Code = $(this).closest('tr').find('td:eq(0)').html();

        $.ajax({
            url: 'BomCreationDeleteRow',
            dataType: "json",
            type: "POST",
            data: $.param({ 'Code': Code }),

            success: function (result) {

                BindChildTab(JSON.parse(result), '1');

            }
        });

    });
    function reset() {
        $('#txtitemcode').val("");
        $('#txtitemname').val("");
        $('#txtquantity').val("");

    }
    $('#btnsubmit').click(function () {


        if ($('#txtgrnno').val() != "") {


            if (window.FormData !== undefined) {
                var formData = new FormData();
                formData.append('GRNNO', $('#txtgrnno').val());
                formData.append('DATE', $('#txtgrndate').val());
                formData.append('AUTOID', $('#hdautoid').val());
                formData.append('actiontype', $('#actiontype').text());
                $(".loadingGIF").show("slow");
                $.ajax({
                    url: 'InsertBomDetails',
                    dataType: "json",
                    type: "POST",
                    contentType: false, // Not to set any content header
                    processData: false, // Not to process data

                    data: formData,
                    success: function (data) {
                        var Res = data.split('|');
                        if (Res[0].length > 0) {
                            var result = Res[0];
                            var msg = Res[1];
                            if (result.toUpperCase() == "TRUE") {
                                $(".loadingGIF").hide("slow");
                                $("form").trigger("reset");
                                $("#actiontype").text("Save")

                                swal("Hey, Good job !!", msg, "success")
                                    .then(() => {
                                        Clear();
                                    });

                            }
                            else {
                                swal("Oops...", msg, "error");
                                $('.loadingGIF').hide();
                            }
                        }
                        else {
                            Clear();
                        }

                    }
                });
            }
           /* $('.bd-example-modal-sm').modal('show');*/
        }
        else
        {
                                swal("Oops...", "please Enter Grn No.", "error");
                                $('.loadingGIF').hide();
         }

    });
    function Clear() {
        //$.ajax({
        //    url: 'ClearBomMasterDetail',
        //    type: "POST",
        //    contentType: false, // Not to set any content header  
        //    processData: false, // Not to process data  
        //    success: function (result) {
        location.reload();

        //    },
        //    error: function (err) {
        //        //alert(err.statusText);
        //    }
        //});
    };
    $('#btnclear').click(Clear);
    $("#dynamic-table").on("click", ".searchdetails", function () {
        var AUTOID = $(this).closest('tr').find('td:eq(5)>span').text();
        $('#modalview').modal('show');
        callpopup(AUTOID)
    });
    function callpopup(AUTOID) {
        $(".loader").show("slow");
        $.ajax({
            url: 'BOMCreationView',
            dataType: 'json',
            type: 'POST',
            data: { "AUTOID": AUTOID },

            success: function (data) {
                var Tables = data.split('|');
                var ORDERItemDetails = JSON.parse(Tables[0]);
              
                BindPopupTab(ORDERItemDetails, '1');
                

            }
        });
        $(".loader").hide("slow");
    }
    function BindPopupTab(ResData, type) {

        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var finalcolsresult;
        var elements = Array();
        var exampleRecordUpload = ResData[0];
        // TABLE BIND
        var len = $('#dynamic-tableDetails tbody').children().length;
        if (type == '0' || len != '0') {
            $('#dynamic-tableDetails').dataTable().fnDestroy();
            cols.length = 0;
            cols1.length = 0;
        }
        if (exampleRecordUpload) {
            //get keys in object. This will only work if your statement remains true that all objects have identical keys
            var keys = Object.keys(exampleRecordUpload);

            //for each key, add a column definition
            keys.forEach(function (k) {

                cols.push({
                    title: k,
                    data: k,
                    targets: k

                    //optionally do some type detection here for render function
                });
            });

            $.each(cols, function (index, item) {

                item.title, item.targets

                cols1.push({
                    title: item.title,
                    targets: index

                });

                cols1DATA.push({
                    data: item.title,
                });
                finalcolsresult += 'null' + ',';

            });

            //  var dyTable1 = $('#dynamic-tableUpload');

            $('#dynamic-tableDetails').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "bSort": false,
                "ascrollX": "100%",
                "bFilter": false,
                "lengthChange": false,
                "aColumns": [
                    finalcolsresult,
                    { "bSortable": false }
                ],
                "aSorting": [],
                'columnDefs': [{
                    'targets': 0,
                    'searchable': false,
                    'orderable': false,
                    'className': 'dt-body-center'
                }],

                columnDefs: cols1,
                columns: cols1DATA

            });
        }
        else {
            $('#dynamic-tableDetails tbody').remove();
            $('#dynamic-tableDetails thead').remove();
            $('#dynamic-tableDetails').dataTable({
                "language": {
                    "emptyTable": "No records found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false }
                ],
                "scrollCollapse": false,
                //"info": false,
                //"paging": false,
                //"searching": false
            });


        }

    }
    $("#dynamic-table").on("click", ".editdetails", function () {

        var AUTOID = $(this).closest('tr').find('td:eq(2)> span').text();

        GetBranchDetailsByID(AUTOID)
    });
    function GetBranchDetailsByID(AUTOID) {
        $(".loader").show("slow");
        $.ajax({
            url: 'BomMasterEdit',
            dataType: 'json',
            type: 'POST',
            data: { "AUTOID": AUTOID },
            success: function (result) {
                var Tables = result.split('|');
                var BomHeader = JSON.parse(Tables[0]);
                var adddetails = JSON.parse(Tables[1]);
                $('#hdautoid').val(BomHeader[0].AUTOID);
                $('#txtgrnno').val(BomHeader[0].GRNNO);
                $('#txtgrndate').val(BomHeader[0].DATE);
                $('#ddlmaterialcode').prop('disabled', true);
                $('#actiontype').html("Update");
                window.scrollTo({ top: 0, behavior: 'smooth' });
                BindChildTab(adddetails, '1');
            }
        });
        $(".loader").hide("slow");
    }

    $('#btnremove').click(function () {
        reset();
    });
});