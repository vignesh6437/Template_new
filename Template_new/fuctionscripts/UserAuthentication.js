jQuery(function ($) {

    //$("#loadingGIF").show("slow");
    //Grid Bind Value
    BindTable();
    var Canc = "";
    //------------------------------------------------------------------------------------------------------- User Name Selected Index change
    $('#username').change(function () {
        $("#txtSelectAllScreens").prop('checked', false);
        var optionselected = $(this);
        var OValue = optionselected.val();
        //var OText = optionselected.find("option:selected").text();

        $("#loadingGIF").show("slow");
        $.ajax({

            url: 'FetchScreenDetailsByUserName',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            data: { 'username': OValue },
            success: function (Result) {

                var pageload = Result.split('|');

                var resJ = JSON.parse(pageload[0]);
                //var Hodis = JSON.parse(pageload[1]);

                BindTab(resJ, '0');
            }

        });
        $(".loadingGIF").hide("slow");
    });

    //-------------------------------------------------------------------------------------------------------Employee Code Selected Index change

    //$('#userauthen-form').parsley().on('field:validated', function () {
    //    var ok = $('.parsley-error').length === 0;
    //    $('.bs-callout-info').toggleClass('hidden', !ok);
    //    $('.bs-callout-warning').toggleClass('hidden', ok);

    //})
    //    .on('form:submit', function () {
    //        save();
    //        return false;
    //    });

    $('#btnsubmit').click(function () {

        save();

    });


    function save() {
        $("#loadingGIF").show("slow");;
        if ($('#username').val() != "") {
            if (window.FormData !== undefined) {

                var formData = new FormData();
                //Split Employee Code Only
                formData.append('username', $('#username').val());
                formData.append('actiontype', $('#actiontype').text());
                //debugger
                var count = 0;

                var sno = "", isselect = "", edit = "", screenid = "", screenname = "", Cancel = "", HOD = "", Debit = "", UH = "", SEH = "";


                jQuery("table.checkupload tbody > tr").each(function () {

                    if (jQuery('td:eq(0)', this).children('input.checkbox').is(':checked')) {
                        screenid += jQuery('td:eq(1)', this).text() + "^";
                        screenname += jQuery('td:eq(2)', this).text() + "^";
                        //   functionname += jQuery('td:eq(3)', this).text() + "^";
                        //  isselect += jQuery('td:eq(0)', this).children('input.checkbox').is(':checked') + "^";
                        //isselect += jQuery('td:eq(0)>div>label', this).children('input.form-check-input').is(':checked') + "^";
                        isselect += jQuery('td:eq(0)', this).children('input.checkbox').is(':checked') + "^";

                    }
                    //debugger
                });

                formData.append('screenid', screenid = screenid.substring(0, screenid.length - 1));
                formData.append('screenname', screenname = screenname.substring(0, screenname.length - 1));
                formData.append('isselect', isselect = isselect.substring(0, isselect.length - 1));
                // formData.append('functionname', functionname = functionname.substring(0, functionname.length - 1));

                if (isselect != "") {
                    $.ajax({
                        url: 'InsertUserAuthenticationMaster',
                        dataType: "json",
                        type: "POST",
                        contentType: false, // Not to set any content header  
                        processData: false, // Not to process data  
                        data: formData,
                        success: function (data) {

                            var Res = data.split('|');
                            var result = Res[0];
                            var msg = Res[1];

                            if (result.toUpperCase() == "TRUE") {
                                $("#actiontype").text("Save")

                                swal("Hey, Good job !!", msg, "success")
                                    .then(() => {
                                        Clear();
                                    });

                            }
                            else {
                                swal("Oops...", msg, "error");
                                //.then(() => {
                                //    // Callback for the error alert closing
                                //    alert("Error alert closed!");
                                //    // Put any other actions for the error case here
                                //});
                            }
                        }
                    });
                }
                else {
                    swal("Oops...", 'Add atleast one Screen Details for Mapped user', "error");           
                }
            }
        } else {
            swal("Oops...", 'Pleae Select User Name.', "error");    
        }
             $(".loadingGIF").hide("slow");
        
    }
    function Clear() {
        location.reload();
    }

    //function Clear() {
    //    $('#username').val("");
    //    $('#username').trigger("chosen:updated");
    //    $('#txtSelectAllScreens').val("");
    //    $(".checkbox").prop('checked', false);
    //};

    $('#btnclear').click(function () {
        location.reload();
        $('#username').val("");
        $('#username').trigger("chosen:updated");
        $('#txtSelectAllScreens').val("");
        $(".checkbox").prop('checked', false);
    });


$('#txtSelectAllScreens').change(function () {
    $(this).attr("value", "true");

});

$('#txtSelectAllScreens').click(function () {
    var value = $("#username").val();
    if (value != "") {
        if ($(this).is(':checked')) {
            $(".checkbox").prop('checked', true);
        }
        else {
            $(".checkbox").prop('checked', false);
        }
    }
    else {
        var isselect1 = "Please Select User Name to Proceed."
        alert(isselect1);

        $("#txtSelectAllScreens").prop('checked', false);
    }
});



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
        $('#dynamic-tableUpload').dataTable().fnDestroy();
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

        var dyTable = $('#dynamic-tableUpload');

        $('#dynamic-tableUpload').dataTable({
            data: ResData,
            "bAutoWidth": false,
            "bSort": false,
            "bPaginate": false,
            "info": false,
            "bFilter": true,
            "ascrollX": "100%",
            "language": { "search": "Search: " },
            "aColumns": [
                finalcolsresult,
                { "bSortable": false }
            ],
            "aSorting": [],

            'columnDefs': [
                { "width": "1%", "targets": 5 }, {
                    'targets': 0,
                    'searchable': true,
                    'orderable': true,
                    'className': 'dt-body-center'
                }
            ],

            columnDefs: cols1,
            columns: [
                { className: "text-capitalize", 'data': 'Select' },
                { className: "text-capitalize", 'data': 'Screen Id' },
                { className: "text-capitalize", 'data': 'Screen Name' },
                { className: "text-capitalize", 'data': 'Function Name' },
            ],


            dom: 'Bfrtip',
            buttons: [

                {
                    extend: 'excelHtml5',
                    title: 'User Authentication',
                    text: '<img src="../../Images/excel.png" title="Excel" style="height: 25px;">',
                    footer: false
                },
                {
                    extend: 'pdfHtml5',

                    text: '<img src="../../Images/pdf.png" title="Pdf" style="height: 25px;">',
                    orientation: 'landscape',
                    pageSize: 'A4',
                    footer: false,
                    title: 'User Authentication'


                }, {
                    extend: 'print',
                    title: 'User Authentication',
                    text: '<img src="../../Images/print.png" title="Print" style="height: 25px;">',
                    footer: false
                }
            ]
        });
    }
    else {
        $('#dynamic-table tbody').remove();
        $('#dynamic-table thead').remove();
        $('#dynamic-table').dataTable({
            "language": {
                "emptyTable": "No records found.."
            },
            'bSort': false,
            'aoColumns': [

                { sWidth: "10%", bSearchable: false, bSortable: false }
            ],
            "scrollCollapse": false,
            "info": true,
            "paging": true,
            "searching": true
        });


    }
    $(".loadingGIF").hide("slow");
}


function BindTable() {
    //var table = document.getElementById("dynamic-tableUpload");
    var cols = [];
    var cols1 = [];
    var cols1DATA = [];
    var nullvalue;
    var colsresult;
    var finalcolsresult;
    $("#loadingGIF").show("slow");
    $.ajax({
        url: 'GetUserAuthenticationPageLoad',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (Result) {

            var pageload = Result.split('|');
            var username = JSON.parse(pageload[0]);

            var resJ = JSON.parse(pageload[1]);
            //  var exampleRecord = resJ[0];

            $("#username").empty().append('<option value="">Select an Option</option>');
            $.each(username, function () {
                $("#username").append($("<option></option>").val(this["USERNAME"]).html(this["USERNAME"]));
                $('#username').trigger("chosen:updated");
            });

            BindTab(resJ, '1');
        }
    });
    $(".loadingGIF").hide("slow");
    }
});
