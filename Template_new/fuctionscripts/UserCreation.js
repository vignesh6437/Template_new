jQuery(function ($) {
    ////-------------------------------------------------------------------------------------------Bind Pageload 
    //Grid Bind Value

    BindTable();

    function BindTable() {
        var RoleDatatable = $('#dynamic-table');
        $("#loadingGIF").show("slow");
        $.ajax({
            url: 'GetUserCreationPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {
                var pageload = Result.split('|');
                var Employee = JSON.parse(pageload[0]);
                var recordstatus = JSON.parse(pageload[1]);
                var resJ = JSON.parse(pageload[2]);

                $("#ddlemployeecode").empty().append('<option value="">Select an Option</option>');
                $.each(Employee, function () {
                    $("#ddlemployeecode").append($("<option></option>").val(this["EMPLOYEECODE"]).html(this["EMPLOYEECODE"]));
                    $('#ddlemployeecode').trigger("chosen:updated");
                });
                $.each(recordstatus, function () {
                    $("#ddlstatus").append($("<option></option>").val(this["METASUBCODE"]).html(this["METADATADESCRIPTION"]));
                    $('#ddlstatus').trigger("chosen:updated");
                });

                BindTab(resJ, '1');

            }
        });
        $(".loadingGIF").hide("slow");
    }
    //-------------------------------------------------------------------------------------------Bind Table
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

                    { className: "text-capitalize", 'data': 'Sl No' },
                    { className: "text-capitalize", 'data': 'Employee Code' },
                    { className: "text-capitalize", 'data': 'Employee Name' },
                    { className: "text-capitalize", 'data': 'User Name' },
                    { className: "text-capitalize", title: 'Status', 'data': 'Record Status' },
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
                        title: 'User Creation',
                        text: '<img src="../../Images/excel.png" title="Excel" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" title="Pdf" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'User Creation'


                    }, {
                        extend: 'print',
                        title: 'User Creation',
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


    $('#ddlemployeecode').change(function () {

        var optionselected = $(this);
        var OValue = optionselected.val();
        $("#loadingGIF").show("slow");
        $.ajax({

            url: 'GetUserCreationByEmployeename',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            data: { 'EmpCode': OValue },
            success: function (data) {

                var Res = data.split('|');
                var EmpName = JSON.parse(Res[0]);

                $.each(EmpName, function (i, item) {
                    $("#txtemployeename").val(EmpName[i].EMPLOYEENAME);

                });

            }
        });
        $(".loadingGIF").hide("slow");
    });

    //$('#usercreation-form').parsley().on('field:validated', function () {
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
        //if (!$('#validation-form').valid()) e.preventDefault();

        //else {

        if (window.FormData !== undefined) {
           
            var formData = new FormData();
            formData.append('Empcode', $('#ddlemployeecode').val());
            formData.append('Autoid', $('#hdautoid').val());
            formData.append('EmpName', $('#txtemployeename').val());
            formData.append('UserName', $('#txtusername').val());
            formData.append('Password', $('#txtpassword').val());
            formData.append('ConfirmPassword', $('#txtconfirmpassword').val());
            formData.append('Status', $('#ddlstatus').val());
            formData.append('actiontype', $('#actiontype').text());
            formData.append('IMAGE', $("#hdemage").val());

            //var photoUpload = document.getElementById("file-input");
            //if (photoUpload.value != null) {
            //    var photofiles1 = $("#file-input").get(0).files;
            //    if (photofiles1.length > 0) {
            //        for (var j = 0; j < photofiles1.length; j++) {
            //            formData.append("photosA" + j, photofiles1[j]);
            //        }
            //    }
            //    formData.append("photocountS", photofiles1.length);
            //} else {
            //    formData.append("photocountS", "0");
            //}
            $("#loadingGIF").show("slow");
            $.ajax({
                url: 'InsertUserCreation',
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
                        $("form").trigger("reset");
                        $("#actiontype").text("Save")
                        swal("Hey, Good job !!", msg, "success")
                            .then(() => {
                                Clear();
                            });
                    } else {
                        swal("Oops...", msg, "error");
                        
                    }

                }
            });
        }
        // }
    }
    function Clear() {
        location.reload();
    }


    //-------------------------------------------------------------------------------------------Table Selected Index Change
    //on Click table Row
    //$(document).ready(function () {
    $("#dynamic-table").on("click", ".editdetails", function () {

        var usercode = $(this).closest('tr').find('td:eq(5) > span').html();
        GetUserCreationDetailsByID(usercode)
    });
    // });
    //-------------------------------------------------------------------------------------------Assign values table into TextBox (Controls)   
    //get the Role Creation Details by ID
    function GetUserCreationDetailsByID(usercode) {
        $("#loadingGIF").show("slow");
        $.ajax({
            url: 'GetUserCreationByID',
            dataType: 'json',
            type: 'POST',
            data: { "usercode": usercode },

            success: function (data) {
                var Res = data.split('|');
                var resJ = JSON.parse(Res[0]);
                $("#hdautoid").val(resJ[0].AUTOID);
                $('#ddlemployeecode').val(resJ[0].EMPLOYEECODE).trigger('change');
                $("#txtemployeename").val(resJ[0].EMPLOYEENAME);
                $("#txtusername").val(resJ[0].USERNAME);
                $("#txtpassword").val(resJ[0].USERPASSWORD);
                $("#txtconfirmpassword").val(resJ[0].CONFIRMPASSWORD);
                $('#ddlstatus').val(resJ[0].RECORDSTATUS).trigger('change');
                $("#actiontype").text("Update");
                $("#addtab").addClass("active");
                $("#detailstab").removeClass("active");
                //$("#hdemage").val((resJ[0].IMAGE));
                //$("#profileImage").attr('src', "../RearDoorPhoto/" + resJ[0].IMAGE);
                $("#adduser").addClass("show active");
                $("#adduserDetails").removeClass("show active");
                window.scrollTo({ top: 0, behavior: 'smooth' });
            }

        });

        $(".loadingGIF").hide("slow");
    }

    //-------------------------------------------------------------------------------------------Clear
    $('#btnclear').click(function () {
        location.reload();
    });

    //-------------------------------------------------------------------------------------------EmployeeCode Text Chaged event
    //$('#ddlemployeeid').focusout(function () {

    //    var empcode = $('#employeecode').val();
    //    var btntext = $("#actiontype").text();

    //    if (empcode && btntext.toUpperCase() != 'UPDATE') {
    //        $("#loadingGIF").show("slow");
    //        $.ajax({
    //            url: 'ExistEmployeeCode',
    //            dataType: 'json',
    //            type: 'POST',
    //            data: { 'employeecode': $('#ddlemployeeid').val() },
    //            success: function (data) {
    //                var resJ = JSON.parse(data);
    //                if (resJ == '0') {
    //                    $("#ddlemployeeid").prop('readonly', true);
    //                }
    //                else {
    //                    $('#ddlemployeeid').val('');
    //                    $("#ddlemployeeid").prop('readonly', false);
    //                    bootbox.alert({
    //                        message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + 'EmployeeCode Already Exist!' + '</span>',
    //                        size: 'small'
    //                    });
    //                    // alert('EmployeeCode Already Exist!')
    //                }
    //            }
    //        });
    //        $(".loadingGIF").hide("slow");
    //    }
    //});

    //-------------------------------------------------------------------------------------------UserName Text Chaged event

    //$('#txtusersname').focusout(function () {
    //    if ($('#txtusersname').val() != '') {
    //        $("#loadingGIF").show("slow");
    //        $.ajax({
    //            url: 'ExistUserName',
    //            dataType: 'json',
    //            type: 'POST',
    //            data: { 'username': $('#txtusersname').val() },
    //            success: function (data) {
    //                var resJ = JSON.parse(data);
    //                if (resJ == '0') {
    //                    $("#txtusersname").prop('readonly', true);
    //                }
    //                else {
    //                    $('#txtusersname').val('');
    //                    $("#txtusersname").prop('readonly', false);
    //                    bootbox.alert({
    //                        message: '<span class="text-danger"><i cclass="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + 'UserName Already Exist!' + '</span>',
    //                        size: 'small'
    //                    });

    //                }
    //            }
    //        });
    //        $(".loadingGIF").hide("slow");
    //    }
    //});
    //-------------------------------------------------------------------------------------------Match password and confirmpassword
    $('#txtconfirmpasswords').change(function () {
        if ($('#txtpassword').val() != '') {
            var pwd = $('#txtpassword').val();
            var conpwd = $("#txtconfirmpasswords").val()
            if (pwd != conpwd) {
                bootbox.alert({
                    message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + 'The password you entered did not match' + '</span>',
                    size: 'small'
                });

                $("#txtconfirmpasswords").val("");
            }
        }
        else {
            bootbox.alert({
                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + 'Please specify a secure password.' + '</span>',
                size: 'small'
            });
            $("#txtconfirmpasswords").val("");

        }
    });

});