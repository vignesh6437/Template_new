
//Menu Start

//Hide the All value
$('#MASTER, #TRANSACTION, #USER, #REPORT').hide();  // Hide top-level sections
$('#DAS001, #UAC001, #UAC002, #MAS005, #MAS007, #MAS008, #MAS001, #MAS002, #MAS003, #TRA009, #TRA003, #TRA004, #TRA005, #TRA006, #TRA011, #TRA010, #TRA012, #TRA007, #TRA014, #TRA013, #RPT001, #RPT002, #RPT003, #RPT004, #RPT005, #RPT006, #RPT007, #RPT008').hide(); // Hide all sub-menu items
$.ajax({
    url: '../Login/GetSessionUserValue',
    dataType: 'json',
    type: 'POST',
    success: function (data) {

        if (data.length > 0) {

            var Lsession = data.split('|');
            var Luserdetails = JSON.parse(Lsession[0]);
            var Lscreendetails = JSON.parse(Lsession[1]);
            ScreenDetails(Luserdetails, Lscreendetails);
        }
        else
        {
            var url = '../Login';
            window.location.href = url;
        }
    },
    error: function (XMLHttpRequest, textStatus, errorThrown) {
        bootbox.alert({
            message: '<span class="text-danger"><i class="ace-icon fa fa-exclamation-triangle fa-4x"></i><br>' + textStatus + '</span>',
            size: 'small'
        });

    }
});

function ScreenDetails(Userdetails, Screendetails) {
    //Display Name
    $('#loginuser').text(Userdetails[0]["USERNAME"].toUpperCase());
   // $('#loginuser').text(Userdetails[0]["USERNAME"].toUpperCase());
    //$('#loginuser').text('Welcome ' + Userdetails[0]["USERNAME"].toUpperCase());
    $("#usericonimage").attr('src', "../RearDoorPhoto/" + Userdetails[0].IMAGE);
    //Menu Visiblity
    $.each(Screendetails, function (i, v) {
        //if (v["FUNCTIONNAME"] == "DASHBOARD")
        //{
        //    $('#DAS001').show();
        //}
        if (v["FUNCTIONNAME"] == "MASTER") {
            $('#MASTER').show();
        }
        if (v["FUNCTIONNAME"] == "TRANSACTION") {
            $('#TRANSACTION').show();
        }
        if (v["FUNCTIONNAME"] == "USER") {
            $('#USER').show();
        }
        if (v["FUNCTIONNAME"] == "REPORT") {
            $('#REPORT').show();
        }

        $('#' + v["SCREENID"]).show();
        //console.log($('#' + v["SCREENID"]));
    });
    
}


setInterval(BindTable, 3000);
function BindTable() {
    $.ajax({
        url: '../Home/MessageNotification',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            let result = JSON.parse(response);

            if (Array.isArray(result) && result.length > 0) {
                $("#notificationList").empty();

                result.forEach((message, index) => {
                    let messageDiv = document.createElement("div");
                    messageDiv.classList.add("notification-item");
                    messageDiv.style.animationDelay = `${index * 0.2}s`; // Delay each message slightly

                    messageDiv.innerHTML = `<strong>${message.SENDER}:</strong> ${message.Content}`;
                    document.getElementById("notificationList").appendChild(messageDiv);
                });

                // Show notification panel with fade effect
                let panel = document.getElementById("notificationPanel");
                panel.style.display = "block";
                setTimeout(() => {
                    panel.style.opacity = "1";
                    panel.style.transform = "translateX(-50%) translateY(0)";
                }, 100);
            } else {
                console.log("No new notifications.");
                document.getElementById("notificationPanel").style.display = "none";
            }
        },
        error: function (xhr, status, error) {
            console.error("Error fetching notifications:", error);
        }
    });
}

// Close notification panel smoothly
document.getElementById("closeNotificationPanel").addEventListener("click", function () {
    let panel = document.getElementById("notificationPanel");
    panel.style.opacity = "0";
    panel.style.transform = "translateX(-50%) translateY(-10px)"; // Move slightly up before hiding
    setTimeout(() => {
        panel.style.display = "none";
    }, 500);
});


document.getElementById("closeNotificationPanel").addEventListener("click", function () {
    document.getElementById("notificationPanel").style.display = "none";
});

document.getElementById("Notifipanel").addEventListener("click", function () {
    document.getElementById("notificationPanel").style.display = "none";
});







//$(function () {
//    'use strict'   
//});


//$(function () {
//    'use strict'

//    // Toggle Switches
//    $('.az-toggle').on('click', function () {
//        $(this).toggleClass('on');
//    })

//    // Input Masks
//    $('#dateMask').mask('99/99/9999');
//    $('#phoneMask').mask('(999) 999-9999');
//    $('#phoneExtMask').mask('(999) 999-9999? ext 99999');
//    $('#ssnMask').mask('999-99-9999');

//    // Color picker
//    $('#colorpicker').spectrum({
//        color: '#17A2B8'
//    });

//    $('#showAlpha').spectrum({
//        color: 'rgba(23,162,184,0.5)',
//        showAlpha: true
//    });

//    $('#showPaletteOnly').spectrum({
//        showPaletteOnly: true,
//        showPalette: true,
//        color: '#DC3545',
//        palette: [
//            ['#1D2939', '#fff', '#0866C6', '#23BF08', '#F49917'],
//            ['#DC3545', '#17A2B8', '#6610F2', '#fa1e81', '#72e7a6']
//        ]
//    });

//    // Datepicker
//    $('.fc-datepicker').datepicker({
//        showOtherMonths: true,
//        selectOtherMonths: true
//    });
//    var highlightedDays = ['2018-5-10', '2018-5-11', '2018-5-12', '2018-5-13', '2018-5-14', '2018-5-15', '2018-5-16'];
//    var date = new Date();

//    $('.fc-datepicker').datepicker({
//        showOtherMonths: true,
//        selectOtherMonths: true,
//        dateFormat: 'dd/mm/yy',
//        beforeShowDay: function (date) {
//            var m = (d = date.getDate(), date.getMonth(), y = date.getFullYear());
//            for (var i = 0; i < highlightedDays.length; i++) {
//                if ($.inArray(d + '-' + (m + 1) + '-' + y , highlightedDays) != -1) {
//                    return [true, 'ui-date-highlighted', ''];
//                }
//            }
//            return [true];
//        }
//    });
//    $('#datepickerNoOfMonths').datepicker({
//        showOtherMonths: true,
//        selectOtherMonths: true,
//        numberOfMonths: 2
//    });

//    // AmazeUI Datetimepicker
//    $('#datetimepicker').datetimepicker({
//        format: 'yyyy-mm-dd HH:mm',
//        autoclose: true
//    });

//    // jQuery Simple DateTimePicker
//    $('#datetimepicker2').appendDtpicker({
//        closeOnSelected: true,
//        onInit: function (handler) {
//            var picker = handler.getPicker();
//            $(picker).addClass('az-datetimepicker');
//        }
//    });

//    new Picker(document.querySelector('#datetimepicker3'), {
//        headers: true,
//        format: 'MMMM DD, YYYY HH:mm',
//        text: {
//            title: 'Pick a Date and Time',
//            year: 'Year',
//            month: 'Month',
//            day: 'Day',
//            hour: 'Hour',
//            minute: 'Minute'
//        },
//    });




//    $('.rangeslider1').ionRangeSlider();

//    $('.rangeslider2').ionRangeSlider({
//        min: 100,
//        max: 1000,
//        from: 550
//    });

//    $('.rangeslider3').ionRangeSlider({
//        type: 'double',
//        grid: true,
//        min: 0,
//        max: 1000,
//        from: 200,
//        to: 800,
//        prefix: '$'
//    });

//    $('.rangeslider4').ionRangeSlider({
//        type: 'double',
//        grid: true,
//        min: -1000,
//        max: 1000,
//        from: -500,
//        to: 500,
//        step: 250
//    });

//});
//$(document).ready(function () {
//    $('.select2').select2({
//        placeholder: 'Choose one',
//        searchInputPlaceholder: 'Search'
//    });
//    $('.select2-no-search').select2({
//        minimumResultsForSearch: Infinity,
//        placeholder: 'Choose one'
//    });
//    $('select2').select2({ width: "100%" });
//});

//$(function () {
//    'use strict'

//});
//(function ($) {
//    var Defaults = $.fn.select2.amd.require('select2/defaults');

//    $.extend(Defaults.defaults, {
//        searchInputPlaceholder: ''
//    }); x

//    var SearchDropdown = $.fn.select2.amd.require('select2/dropdown/search');

//    var _renderSearchDropdown = SearchDropdown.prototype.render;

//    SearchDropdown.prototype.render = function (decorated) {

//        // invoke parent method
//        var $rendered = _renderSearchDropdown.apply(this, Array.prototype.slice.apply(arguments));

//        this.$search.attr('placeholder', this.options.get('searchInputPlaceholder'));

//        return $rendered;
//    };

//})(window.jQuery);
////$('.select2').css('width', '200px').select2({ allowClear: true })
////$('#select2-multiple-style .btn').on('click', function (e) {
////    var target = $(this).find('input[type=radio]');
////    var which = parseInt(target.val());
////    if (which == 2) $('.select2').addClass('tag-input-style');
////    else $('.select2').removeClass('tag-input-style');
////});

//$('#id-disable-check').on('click', function () {
//    var inp = $('#form-input-readonly').get(0);
//    if (inp.hasAttribute('disabled')) {
//        inp.setAttribute('readonly', 'true');
//        inp.removeAttribute('disabled');
//        inp.value = "This text field is readonly!";
//    }
//    else {
//        inp.setAttribute('disabled', 'disabled');
//        inp.removeAttribute('readonly');
//        inp.value = "This text field is disabled!";
//    }
//});

//$('[data-rel=tooltip]').tooltip({ container: 'body' });
//$('[data-rel=popover]').popover({ container: 'body' });
//autosize($('textarea[class*=autosize]'));
//$('textarea.limited').inputlimiter({
//    remText: '%n character%s remaining...',
//    limitText: 'max allowed : %n.'
//});
//$.mask.definitions['~'] = '[+-]';
//$('.input-mask-date').mask('99/99/9999');
//$('.input-mask-phone').mask('(999) 999-9999');
//$('.input-mask-eyescript').mask('~9.99 ~9.99 999');
//$(".input-mask-product").mask("a*-999-a999", { placeholder: " ", completed: function () { alert("You typed the following: " + this.val()); } });



//$("#input-size-slider").css('width', '200px').slider({
//    value: 1,
//    range: "min",
//    min: 1,
//    max: 8,
//    step: 1,
//    slide: function (event, ui) {
//        var sizing = ['', 'input-sm', 'input-lg', 'input-mini', 'input-small', 'input-medium', 'input-large', 'input-xlarge', 'input-xxlarge'];
//        var val = parseInt(ui.value);
//        $('#form-field-4').attr('class', sizing[val]).attr('placeholder', '.' + sizing[val]);
//    }
//});

//$("#input-span-slider").slider({
//    value: 1,
//    range: "min",
//    min: 1,
//    max: 12,
//    step: 1,
//    slide: function (event, ui) {
//        var val = parseInt(ui.value);
//        $('#form-field-5').attr('class', 'col-xs-' + val).val('.col-xs-' + val);
//    }
//});

////"jQuery UI Slider"
////range slider tooltip example
//$("#slider-range").css('height', '200px').slider({
//    orientation: "vertical",
//    range: true,
//    min: 0,
//    max: 100,
//    values: [17, 67],
//    slide: function (event, ui) {
//        var val = ui.values[$(ui.handle).index() - 1] + "";

//        if (!ui.handle.firstChild) {
//            $("<div class='tooltip right in' style='display:none;left:16px;top:-6px;'><div class='tooltip-arrow'></div><div class='tooltip-inner'></div></div>")
//                .prependTo(ui.handle);
//        }
//        $(ui.handle.firstChild).show().children().eq(1).text(val);
//    }
//}).find('span.ui-slider-handle').on('blur', function () {
//    $(this.firstChild).hide();
//});


//$("#slider-range-max").slider({
//    range: "max",
//    min: 1,
//    max: 10,
//    value: 2
//});

//$("#slider-eq > span").css({ width: '90%', 'float': 'left', margin: '15px' }).each(function () {
//    // read initial values from markup and remove that
//    var value = parseInt($(this).text(), 10);
//    $(this).empty().slider({
//        value: value,
//        range: "min",
//        animate: true

//    });
//});

//$("#slider-eq > span.ui-slider-purple").slider('disable');//disable third item


//$('.id-input-file-1 , .id-input-file-2').ace_file_input({
//    no_file: 'No File ...',
//    btn_choose: 'Choose',
//    btn_change: 'Change',
//    droppable: false,
//    onchange: null,
//    thumbnail: false //| true | large
//    //whitelist:'gif|png|jpg|jpeg'
//    //blacklist:'exe|php'
//    //onchange:''
//    //
//});
////pre-show a file name, for example a previously selected file
////$('#id-input-file-1').ace_file_input('show_file_list', ['myfile.txt'])





////$('#id-input-file-3')
////.ace_file_input('show_file_list', [
////{type: 'image', name: 'name of image', path: 'http://path/to/image/for/preview'},
////{type: 'file', name: 'hello.txt'}
////]);




////dynamically change allowed formats by changing allowExt && allowMime function
//$('.id-file-format').removeAttr('checked').on('change', function () {
//    var whitelist_ext, whitelist_mime;
//    var btn_choose
//    var no_icon
//    if (this.checked) {
//        btn_choose = "Drop images here or click to choose";
//        no_icon = "ace-icon fa fa-picture-o";

//        whitelist_ext = ["jpeg", "jpg", "png", "gif", "bmp"];
//        whitelist_mime = ["image/jpg", "image/jpeg", "image/png", "image/gif", "image/bmp"];
//    }
//    else {
//        btn_choose = "Drop files here or click to choose";
//        no_icon = "ace-icon fa fa-cloud-upload";

//        whitelist_ext = null;//all extensions are acceptable
//        whitelist_mime = null;//all mimes are acceptable
//    }
//    var file_input = $('.id-input-file-3');
//    file_input
//        .ace_file_input('update_settings',
//            {
//                'btn_choose': btn_choose,
//                'no_icon': no_icon,
//                'allowExt': whitelist_ext,
//                'allowMime': whitelist_mime
//            })
//    file_input.ace_file_input('reset_input');

//    file_input
//        .off('file.error.ace')
//        .on('file.error.ace', function (e, info) {
//        });




//});

//$('#spinner1').ace_spinner({ value: 0, min: 0, max: 200, step: 10, btn_up_class: 'btn-info', btn_down_class: 'btn-info' })
//    .closest('.ace-spinner')
//    .on('changed.fu.spinbox', function () {
//        //console.log($('#spinner1').val())
//    });
//$('#spinner2').ace_spinner({ value: 0, min: 0, max: 10000, step: 100, touch_spinner: true, icon_up: 'ace-icon fa fa-caret-up bigger-110', icon_down: 'ace-icon fa fa-caret-down bigger-110' });
//$('#spinner3').ace_spinner({ value: 0, min: -100, max: 100, step: 10, on_sides: true, icon_up: 'ace-icon fa fa-plus bigger-110', icon_down: 'ace-icon fa fa-minus bigger-110', btn_up_class: 'btn-success', btn_down_class: 'btn-danger' });
//$('#spinner4').ace_spinner({ value: 0, min: -100, max: 100, step: 10, on_sides: true, icon_up: 'ace-icon fa fa-plus', icon_down: 'ace-icon fa fa-minus', btn_up_class: 'btn-purple', btn_down_class: 'btn-purple' });

////datepicker plugin
////link
//$('.date-picker').datepicker({
//    autoclose: true,
//    todayHighlight: true
//})
//    //show datepicker when clicking on the icon
//    .next().on(ace.click_event, function () {
//        $(this).prev().focus();
//    });

////or change it into a date range picker
//$('.input-daterange').datepicker({ autoclose: true });


////to translate the daterange picker, please copy the "examples/daterange-fr.js" contents here before initialization
//$('input[name=date-range-picker]').daterangepicker({
//    'applyClass': 'btn-sm btn-success',
//    'cancelClass': 'btn-sm btn-default',
//    locale: {
//        applyLabel: 'Apply',
//        cancelLabel: 'Cancel',
//    }
//})
//    .prev().on(ace.click_event, function () {
//        $(this).next().focus();
//    });


//$('.timepicker1').timepicker({
//    minuteStep: 1,
//    showSeconds: false,
//    showMeridian: true,
//    disableFocus: true,
//    icons: {
//        up: 'fa fa-chevron-up',
//        down: 'fa fa-chevron-down'
//    }
//}).on('focus', function () {
//    $('#timepicker1').timepicker('showWidget');
//}).next().on(ace.click_event, function () {
//    $(this).prev().focus();
//});




//if (!ace.vars['old_ie']) $('.date-timepicker1').datetimepicker({
//    //format: 'MM/DD/YYYY h:mm:ss A',//use this option to display seconds
//    showClose: true,
//    icons: {
//        time: 'fa fa-clock-o',
//        date: 'fa fa-calendar',
//        up: 'fa fa-chevron-up',
//        down: 'fa fa-chevron-down',
//        previous: 'fa fa-chevron-left',
//        next: 'fa fa-chevron-right',
//        today: 'fa fa-arrows ',
//        clear: 'fa fa-trash',
//        close: 'fa fa-times-circle'
//    }

//}).next().on(ace.click_event, function () {
//    $(this).prev().focus();
//});
//if (!ace.vars['old_ie']) $('.date-timepickerasn').datetimepicker({
//    //format: 'MM/DD/YYYY h:mm:ss A',//use this option to display seconds
//    showClose: true,
//    defaultDate: 'now',
//    icons: {
//        time: 'fa fa-clock-o',
//        date: 'fa fa-calendar',
//        up: 'fa fa-chevron-up',
//        down: 'fa fa-chevron-down',
//        previous: 'fa fa-chevron-left',
//        next: 'fa fa-chevron-right',
//        today: 'fa fa-arrows ',
//        clear: 'fa fa-trash',
//        close: 'fa fa-times-circle'
//    }

//}).next().on(ace.click_event, function () {
//    $(this).prev().focus();
//});
//if (!ace.vars['old_ie']) $('.date-timepickerevent').datetimepicker({
//    //format: 'MM/DD/YYYY h:mm:ss A',//use this option to display seconds
//    showClose: true,
//    useCurrent: false,
//    defaultDate: 'now',
//    icons: {
//        time: 'fa fa-clock-o',
//        date: 'fa fa-calendar',
//        up: 'fa fa-chevron-up',
//        down: 'fa fa-chevron-down',
//        previous: 'fa fa-chevron-left',
//        next: 'fa fa-chevron-right',
//        today: 'fa fa-arrows ',
//        clear: 'fa fa-trash',
//        close: 'fa fa-times-circle'
//    }

//}).next().on(ace.click_event, function () {
//    $(this).prev().focus();
//});
//$("#eventdatetime").on("dp.show", function (e) {
//    $('#eventdatetime').data("DateTimePicker").minDate(e.date);
//});


////$('#colorpicker1').colorpicker();
//////$('.colorpicker').last().css('z-index', 2000);//if colorpicker is inside a modal, its z-index should be higher than modal'safe

////$('#simple-colorpicker-1').ace_colorpicker();


//$(".knob").knob();


//var tag_input = $('.form-field-tags');
//try {
//    tag_input.tag(
//        {
//            placeholder: tag_input.attr('placeholder'),
//            //enable typeahead by specifying the source array
//            //source: ace.vars['US_STATES'],//defined in ace.js >> ace.enable_search_ahead
//        }
//    )

//    //programmatically add/remove a tag
//    var $tag_obj = $('.form-field-tags').data('tag');
//    //$tag_obj.add('Programmatically Added');

//    var index = $tag_obj.inValues('some tag');
//    $tag_obj.remove(index);
//}
//catch (e) {
//    //display a textarea for old IE, because it doesn't support this plugin or another one I tried!
//    tag_input.after('<textarea id="' + tag_input.attr('id') + '" name="' + tag_input.attr('name') + '" rows="3">' + tag_input.val() + '</textarea>').remove();
//    //autosize($('#form-field-tags'));
//}


///////////
//$('#modal-form input[type=file]').ace_file_input({
//    style: 'well',
//    btn_choose: 'Drop files here or click to choose',
//    btn_change: null,
//    no_icon: 'ace-icon fa fa-cloud-upload',
//    droppable: true,
//    thumbnail: 'large'
//})

////chosen plugin inside a modal will have a zero width because the select element is originally hidden
////and its width cannot be determined.
////so we set the width after modal is show
//$('#modal-form').on('shown.bs.modal', function () {
//    if (!ace.vars['touch']) {
//        $(this).find('.chosen-container').each(function () {
//            $(this).find('a:first-child').css('width', '210px');
//            $(this).find('.chosen-drop').css('width', '210px');
//            $(this).find('.chosen-search input').css('width', '200px');
//        });
//    }
//})

//$(document).one('ajaxloadstart.page', function (e) {
//    autosize.destroy('textarea[class*=autosize]')

//    $('.limiterBox,.autosizejs').remove();
//    $('.daterangepicker.dropdown-menu,.colorpicker.dropdown-menu,.bootstrap-datetimepicker-widget.dropdown-menu').remove();
//});

//// Additional code for adding placeholder in search box of select2



////$('.numberonly').keydown(function (e) {
////    if (e.shiftKey || e.ctrlKey || e.altKey) {
////        e.preventDefault();
////    } else {
////        var key = e.keyCode;
////        if (!((key == 8) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
////            e.preventDefault();
////        }
////    }
////});
////$('.characteronly').keydown(function (e) {
////    if (e.shiftKey || e.ctrlKey || e.altKey) {
////        e.preventDefault();
////    } else {
////        var key = e.keyCode;
////        if (!((event.keyCode > 64 && event.keyCode < 91) || (event.keyCode > 96 && event.keyCode < 123) || event.keyCode == 8)) {
////            e.preventDefault();
////        }
////    }
////});

//$("#loadingid").hide();

//$(document).ajaxStart(function () {
//    $("#loadingid").show();
//});

//$(document).ajaxStop(function () {
//    $("#loadingid").hide();
//});

//$('#fromdate').datetimepicker({
//    format: 'DD-MM-YYYY'
//});
//$('#todate').datetimepicker({
//    useCurrent: false, //Important! See issue #1075
//    format: 'DD-MM-YYYY'
//});
//$("#fromdate").on("dp.change", function (e) {
//    $('#todate').data("DateTimePicker").minDate(e.date);
//});
//$("#todate").on("dp.change", function (e) {
//    $('#fromdate').data("DateTimePicker").maxDate(e.date);
//});

//$('#FromDate').datetimepicker({
//    format: 'DD-MM-YYYY'
//});
//$('#ToDate').datetimepicker({
//    useCurrent: false, //Important! See issue #1075
//    format: 'DD-MM-YYYY'
//});
//$("#FromDate").on("dp.change", function (e) {
//    $('#ToDate').data("DateTimePicker").minDate(e.date);
//});
//$("#ToDate").on("dp.change", function (e) {
//    $('#FromDate').data("DateTimePicker").maxDate(e.date);
//});

////$(function () {
////    var f = function () {
////        $(this).next().text($(this).is(':checked') ? ':checked' : ':not(:checked)');
////    };
////    $('input').change(f).trigger('change');
////});
//$(document).one('ajaxloadstart.page', function (e) {
//    autosize.destroy('textarea[class*=autosize]')

//    $('.limiterBox,.autosizejs').remove();
//    $('.daterangepicker.dropdown-menu,.colorpicker.dropdown-menu,.bootstrap-datetimepicker-widget.dropdown-menu').remove();
//});
//var demo1 = $('select[name="duallistbox_demo1[]"]').bootstrapDualListbox({ infoTextFiltered: '<span class="label label-purple label-lg">Filtered</span>' });
//var container1 = demo1.bootstrapDualListbox('getContainer');
//container1.find('.btn').addClass('btn-info btn-bold');
////in ajax mode, remove remaining elements before leaving page
//$(document).one('ajaxloadstart.page', function (e) {
//    $('[class*=select2]').remove();
//    $('select[name="duallistbox_demo1[]"]').bootstrapDualListbox('destroy');
//    $('.rating').raty('destroy');
//    $('.multiselect').multiselect('destroy');
//});

//$('input').change(function () {
//    if ($('input').parents().hasClass('has-error')) {
//        $(this).closest('.form-group').removeClass('has-error');
//        $(this).siblings('div.help-block').addClass('hide');
//    }
//});
//$('.chosen-select').change(function () {
//    if ($('.chosen-select').parents().hasClass('has-error')) {
//        $(this).closest('.form-group').removeClass('has-error');
//        $(this).siblings('div.help-block').addClass('hide');
//    }
//});

    //Menu End

