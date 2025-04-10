jQuery(function ($) {


    $("#txtemailid").blur(function () {
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        var emailaddress = $("#txtemailid").val();
        if (!emailReg.test(emailaddress)) {
            var msg = 'Please Enter the Correct  Email ID'
            swal("Oops...", msg, "error");
            $('#txtemailid').val("");
            $('#txtemailid').focus();
        }
    });
    $("#contactno").keypress(function (e) {
        var keyCode = e.which;
        if (!((keyCode >= 48 && keyCode <= 57)) &&
            !(keyCode === 43 || keyCode === 45)) // 0-9

        {
            e.preventDefault();
        }
    });
    $("#contactno").change(function () {
        var contact = $("#contactno").val();
        var contactlen = contact.length;
        if (parseInt(contactlen) < 10) {
            var msg = 'Please enter a valid Contact No'
            swal("Oops...", msg, "error");
            $('#contactno').val("");
            $('#contactno').focus();
        }
    });

    BindTable();
    function BindTable() {
        //$("#loadingGIF").show("slow");
        $.ajax({
            url: 'EmployeeMasterPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {
                var pageload = Result.split('|');
                var status = JSON.parse(pageload[0]);
                var department = JSON.parse(pageload[1]);
                var designation = JSON.parse(pageload[2]);
                var resJ = JSON.parse(pageload[3]);

                $("#ddlstatus").empty().append('<option value="">Select an Option</option>');
                $.each(status, function () {
                    $("#ddlstatus").append($("<option></option>").val(this["METASUBCODE"]).html(this["METADATADESCRIPTION"]));
                    $('#ddlstatus').trigger("chosen:updated");
                });
                $("#ddldepartent").empty().append('<option value="">Select an Option</option>');
                $.each(department, function () {
                    $("#ddldepartent").append($("<option></option>").val(this["DEPARTMENTCODE"]).html(this["DEPARTMENTNAME"]));
                    $('#ddldepartent').trigger("chosen:updated");
                });
                $("#ddldesignation").empty().append('<option value="">Select an Option</option>');
                $.each(designation, function () {
                    $("#ddldesignation").append($("<option></option>").val(this["DESIGNATIONCODE"]).html(this["DESIGNATIONNAME"]));
                    $('#ddldesignation').trigger("chosen:updated");
                });
                BindTab(resJ, '1');
            }
        });
        //$(".loadingGIF").hide("slow");
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
                "bSort": false,
                "bPaginate": true,
                "sPaginationType": "full_numbers",
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
                //columns: cols1DATA,

                columns: [

                    { className: "text-capitalize", 'data': 'Employee Code' },
                    { className: "text-capitalize", 'data': 'Employee Name' },
                    { className: "text-capitalize", 'data': 'Department' },
                    { className: "text-capitalize", 'data': 'Designation' },
                    { className: "text-capitalize", 'data': 'Email ID' },
                    { className: "text-capitalize", 'data': 'Contact No' },
                    { className: "text-capitalize", title: 'Status', 'data': 'Status' },
                    {
                        className: "text-capitalize", 'data': 'Edit',
                        'render': function (data, type, full, meta) {

                            return '<button type="button" class="btn-az-secondary editdetails" id="Edit" data-toggle="tooltip" data-placement="top" style="background-color: #5d4fbe; border: #5d4fbe;" data-date=" title="Edit"><i class="fa fa-edit" aria-hidden="true"></i></button><span style = "visibility:hidden; font-size: 1px;    border-style: none;color: #2a3f54;background: none;">' + data + '</span>'

                        }
                    },

                ],

                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'Employee Master Details',
                        text: '<img src="../../Images/excel.png" title="Excel" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" title="Pdf" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'Employee Master Details'


                    }, {
                        extend: 'print',
                        title: 'Employee Master Details',
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
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
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
        $(".loadingGIF").hide("slow");
    }

    //$('#employeemaster-form').parsley().on('field:validated', function () {
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
        if (window.FormData !== undefined) {

            var formData = new FormData();
            formData.append('Empcode', $('#txtemployeecode').val());
            formData.append('Autoid', $('#hdautoid').val());
            formData.append('EmpName', $('#txtemployeename').val());
            formData.append('department', $('#ddldepartent').val());
            formData.append('designation', $('#ddldesignation').val());
            formData.append('emailid', $('#txtemailid').val());
            formData.append('contactno', $('#contactno').val());
            formData.append('status', $('#ddlstatus').val());
            formData.append('actiontype', $('#actiontype').text());
            $("#loadingGIF").show("slow");
            $.ajax({
                url: 'InsertEmployeeMaster',
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
                        $("#actiontype").text("Save");

                        swal("Hey, Good job !!", msg, "success")
                            .then(() => {
                                Clear();
                            });
                    } else {
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
    }
    function Clear() {
        location.reload();
    }

    $("#dynamic-table").on("click", ".editdetails", function () {

        var autoid = $(this).closest('tr').find('td:eq(7) > span').html();
        EmployeeMasterEdit(autoid)
    });

    function EmployeeMasterEdit(autoid) {
        $("#loadingGIF").show("slow");
        $.ajax({
            url: 'EmployeeMasterEdit',
            dataType: 'json',
            type: 'POST',
            data: { "autoid": autoid },

            success: function (data) {
                var Res = data.split('|');
                var resJ = JSON.parse(Res[0]);
                $("#hdautoid").val(resJ[0].AUTOID);
                $('#txtemployeecode').val(resJ[0].EMPLOYEECODE);
                $("#txtemployeename").val(resJ[0].EMPLOYEENAME);
                $("#txtemailid").val(resJ[0].EMAILID);
                $("#contactno").val(resJ[0].MOBILENO);
                $('#ddldepartent').val(resJ[0].DEPARTMENT).trigger('change');
                $('#ddldesignation').val(resJ[0].DESIGNATION).trigger('change');
                $('#ddlstatus').val(resJ[0].RECORDSTATUS).trigger('change');
                $("#actiontype").text("Update");
                 window.scrollTo({ top: 0, behavior: 'smooth' });
                //$("#addtab").addClass("active");
                //$("#detailstab").removeClass("active");
                //$("#adduser").addClass("show active");
                //$("#adduserDetails").removeClass("show active");

                //var inputElement = document.getElementById("txtemployeeid");
                //inputElement.classList.remove("parsley-error");
                //inputElement.classList.add("parsley-success");

                //var inputElement1 = document.getElementById("txtemployeename");
                //inputElement1.classList.remove("parsley-error");
                //inputElement1.classList.add("parsley-success");

                //var inputElement4 = document.getElementById("txtemailid");
                //inputElement4.classList.remove("parsley-error");
                //inputElement4.classList.add("parsley-success");

                //var inputElement5 = document.getElementById("txtcontactno");
                //inputElement5.classList.remove("parsley-error");
                //inputElement5.classList.add("parsley-success");

                //var inputElement6 = document.getElementById("slWrapper1");
                //inputElement6.classList.remove("parsley-error");
                //inputElement6.classList.add("parsley-success");

                //var inputElement6 = document.getElementById("slWrapper2");
                //inputElement6.classList.remove("parsley-error");
                //inputElement6.classList.add("parsley-success");

                //var inputElement6 = document.getElementById("slWrapper3");
                //inputElement6.classList.remove("parsley-error");
                //inputElement6.classList.add("parsley-success");

                //var parsleyRequiredItems = document.querySelectorAll('li.parsley-required');
                //parsleyRequiredItems.forEach(function (item) {
                //    item.style.display = 'none';
                //});
            }
        });

        $(".loadingGIF").hide("slow");
    }

    $('#btnclear').click(function () {
        Clear();
    });

});