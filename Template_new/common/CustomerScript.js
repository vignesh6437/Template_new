//Hide the All value
debugger;
//function HideAllMenu() {
//    $('.R1,.R2,.R3').hide();
//    $('#S101,#S102,#S103,#S104,#S105,#S106,#S107,#S108,#S109,#S111,#S112,#S121,#S122,#S123').hide();
//    $.ajax({
//        url: '../LoginUser/MenuHidu',
//        dataType: 'json',
//        type: 'POST',
//        async: "false",

//        success: function (data) {
//            if (data != 'true') {
//                $('.' + data).show();
//                $.ajax({
//                    url: '../LoginUser/GetScreenDetailsByParentId',
//                    dataType: 'json',
//                    type: 'POST',
//                    async: "false",
//                    data: { 'ParentId': data },
//                    success: function (data) {
//                        if (data.length > 0) {
//                            var Lscreendetails = JSON.parse(data);
//                            ScreenDetails(Lscreendetails);
//                        }
//                        else {
//                            var url = '../Login';
//                            window.location.href = url;
//                        }
//                    },
//                    error: function (XMLHttpRequest, textStatus, errorThrown) {
//                        alert("Status: " + textStatus); alert("Error: " + errorThrown);
//                    }
//                });

//            }

//        }
//    });
//    //$('#R1,#R2,#R3,#R4').hide();


//}
//HideAllMenu();
function Initialize() {
    debugger;
    $.ajax({
        url: '../LoginUser/GetSessionUserValue',
        dataType: 'json',
        type: 'POST',
        async: "false",

        success: function (data) {
            if (data.length > 0) {
                $('#R1').hide();
                $('#R2').hide();
                $('#R3').hide();

                var Lscreendetails = JSON.parse(data);
                debugger;
                //ScreenDetails(Lscreendetails);
                var a = Lscreendetails.length;
                for (i = 0 ; i < Lscreendetails.length; i++) {

                    switch (Lscreendetails[i].screenid) {
                        case 'R1':
                            $('#R1').show();
                            break;
                        case 'R2':
                            $('#R2').show();
                            break;
                        case 'R3':
                            $('#R3').show();
                            break;
                    }

                }

            }
            else {
                var url = '../LoginUser';
                window.location.href = url;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Status: " + textStatus); alert("Error: " + errorThrown);
        }
    });
}
Initialize();
function ScreenDetails(Screendetails) {
    debugger;
    //Display Name
    // $('#sdisplayname').text(Userdetails[0]["DISPLAYNAME"]);

    //Menu Visiblity
    $.each(Screendetails, function (i, v) {
        $('#' + v["screenid"]).show();
        $('.' + v["screenid"]).show();
    });
}
debugger;
//$(document).ready(function () {
//    debugger;
//    $('a[data-toggle="tab"]').click('shown.bs.tab', function (e) {
//        var target = $(e.target).text(); // get current tab
//        //var LastTab = $(e.relatedTarget).text();
//        debugger;
//        if ((target == 'Rack Name')) {
//            RackNameInitialize();
//        } else {
//            RackMasterInitialize();
//        }

//    });
//});
$('.ParentMenu').click(function () {
    debugger;
    //HideAllMenu();
    $('.R1,.R2,.R3').hide();
    $('#S101,#S102,#S103,#S104,#S105,#S106,#S107,#S108,#S109,#S111,#S112,#S113,#S114,#S121,#S122,#S123,#S124,#S125').hide();
    var pid = $(this).data("parentid");
    $('.' + pid).show();
    $.ajax({
        url: '../LoginUser/GetScreenDetailsByParentId',
        dataType: 'json',
        type: 'POST',
        async: "false",
        data: { 'ParentId': pid },
        success: function (data) {
            if (data.length > 0) {
                var Lscreendetails = JSON.parse(data);
                ScreenDetails(Lscreendetails);
            }
            else {
                var url = '../LoginUser';
                window.location.href = url;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Status: " + textStatus); alert("Error: " + errorThrown);
        }
    });
});

$('.Menulist').click(function () {
    debugger;

    //var pid = $(this).data("parentid");
    //var screenid = $(this).data("screenid");
    //$('#' + screenid).addClass('active');
    // $('#' + screenid).parent().css({ display: 'block' });
    // $('#' + screenid).parent().closest('a').addClass('subdrop');
});

function Initialize2(result) {
    debugger;
    $.ajax({
        url: '../LoginUser/GetSessionUserValue',
        dataType: 'json',
        type: 'POST',
        async: "false",
        data: $.param({ "ID": result }),
        success: function (data) {
            if (data.length > 0) {
                var Lscreendetails = JSON.parse(data);
                ScreenDetails(Lscreendetails);
            }
            else {
                var url = '../LoginUser';
                window.location.href = url;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Status: " + textStatus); alert("Error: " + errorThrown);
        }
    });
}

//$(window).unload(function () {
//    //post - > controller action
//    debugger;
//    $.ajax
//    ({
//        type: 'POST',
//        url: '../LoginUser/EndApplication',
//        //data : { ParameterCollection },
//        dataType: 'html',
//        //error: function (xhr, status, error) 
//        //{
//        //    //show appropriate message
//        //    alert('error');
//        //},
//        success: function () {
//            //show appropriate message
//            //alert('success');
//        }
//    });
//});



//save
