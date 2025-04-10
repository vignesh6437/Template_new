jQuery(function ($) {



    BindTable();
    function BindTable() {
        //$("#loadingGIF").show("slow");
        $.ajax({
            url: 'UpdateProfilePageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {
                var pageload = Result.split('|');
                var DETAILS = JSON.parse(pageload[0]);
                var emailid = JSON.parse(pageload[1]);
                $("#txtusername").text((DETAILS[0].USERNAME));  
                $("#txtemailid").text(emailid[0].EMAILID);
                $("#txtemailidmodel").val(emailid[0].EMAILID);
                $("#hdmailautoid").val(emailid[0].AUTOID);
                $("#txtusernamemodel").val((DETAILS[0].USERNAME));
                $("#hdemage").val((DETAILS[0].IMAGE));
                $("#txtpassword").val((DETAILS[0].USERPASSWORD));
                $("#txtconpassword").val((DETAILS[0].CONFIRMPASSWORD));
                $("#hduserautoid").val((DETAILS[0].AUTOID));
                $("#profileImage").attr('src', "../RearDoorPhoto/" + DETAILS[0].IMAGE);

            }
        });
        //$(".loadingGIF").hide("slow");
    }


    


    $('#btnsubmit').click(function () {
        save();
    });

    function save() {
        if (window.FormData !== undefined) {

            var formData = new FormData();

            formData.append('username', $('#txtusernamemodel').val());
            formData.append('userautoid', $('#hduserautoid').val());
            formData.append('mailautoid', $('#hdmailautoid').val());
            formData.append('password', $('#txtpassword').val());
            formData.append('conformpassword', $('#txtconpassword').val());
            formData.append('emaiid', $('#txtemailidmodel').val());
            formData.append('IMAGE', $("#hdemage").val());
            //formData.append('designation', $('#ddldesignation').val());
            //formData.append('emailid', $('#txtemailid').val());
            //formData.append('contactno', $('#contactno').val());
            //formData.append('status', $('#ddlstatus').val());
            //formData.append('actiontype', $('#actiontype').text());

            var photoUpload = document.getElementById("file-input");
            if (photoUpload.value != null) {
                var photofiles1 = $("#file-input").get(0).files;
                if (photofiles1.length > 0) {
                    for (var j = 0; j < photofiles1.length; j++) {
                        formData.append("photosA" + j, photofiles1[j]);
                    }
                }
                formData.append("photocountS", photofiles1.length);
            } else {
                formData.append("photocountS", "0");
            }

            $("#loadingGIF").show("slow");
            $.ajax({
                url: 'InsertProfile',
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
    }
    function Clear() {
        location.reload();
    }

});