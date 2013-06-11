// Suggest of search
//Todo Componentizar
(function ($) {
    var baseModel =
        {
            query: "",
            results: []
        };

    var viewModel = ko.mapping.fromJS(baseModel);
    viewModel.doSearch = function () {
        var $this = this;
        setTimeout(function () {
            var resultModel = null;
            var q = $this.query();
            if (q == "") {
                resultModel = { results: [] };
                ko.mapping.updateFromJS(viewModel, resultModel);
            } else {
                $.ajax({
                    url: "../Search/SearchPeaple",
                    data: { "query": q },
                    type: "GET",
                    dataType: "json",
                    cache: false,
                    success: function (r) {
                        resultModel = r;
                        ko.mapping.updateFromJS(viewModel, resultModel);
                        $("#advancedSearchLink").attr("onclick", "goAdvancedSearch(\"" + q + "\")");
                    }
                });
            }
        }, 1);

        return true;
    };

    window.vm = viewModel;
    ko.applyBindings(viewModel, $("#search").get(0));
})(jQuery);



// Timeline animation
$(function () {
    $('#collapser').jqcollapse({
        slide: true,
        speed: 1000,
        easing: 'easeOutCubic'
    });

    $('#collapser li').click(
        function () {
            $(this).toggleClass("selected").siblings("li.active").toggleClass("clearfix");
        });
});

// Generate Data menu
// Todo Componentizar
$(document).ready(function () {
    var Menu = $("#_Menu").AjaxFlagMenu({
        Caption: 'Menu',
        CaptionClass: 'CaptionClass',
        onOutClass: 'onOutClass',
        onOverClass: 'onOverClass',
        onClickClass: 'onClickClass',
        hscOutClass: 'hscOutClass',
        hscClickClass: 'hscClickClass',
        Loading_gif: '../../Images/Loading.gif',
        ajaxContent: 'center'
    });

    Menu.add({
        Title: 'Dados Gerais',
        onOutIcon: '../../Images/dados_gerais.png',
        onClickIcon: '../../Images/dados_gerais.png',
        HtmlSatusContent: '',
        url: '/Patient/GeneralData',
        data: ''
    }).sethscCorner();

    //Menu.add({
    //    Title: 'Imagens',
    //    onOutIcon: '../../Images/exames.png',
    //    onClickIcon: '../../Images/exames.png',
    //    HtmlSatusContent: '',
    //    url: '/Patient/Images',
    //    data: ''
    //});

    //Menu.add({
    //    Title: 'Exames',
    //    onOutIcon: '../../Images/exames.png',
    //    onClickIcon: '../../Images/exames.png',
    //    HtmlSatusContent: '',
    //    url: '/Patient/Exams',
    //    data: ''
    //});

    Menu.add({
        Title: 'Procedimentos',
        onOutIcon: '../../Images/procedimentos.png',
        onClickIcon: '../../Images/procedimentos.png',
        HtmlSatusContent: '',
        url: '/Patient/Procedures',
        data: ''
    }).sethscCorner();

    Menu.add({
        Title: 'Hemotransfusão',
        onOutIcon: '../../Images/hemotransfusao.png',
        onClickIcon: '../../Images/hemotransfusao.png',
        HtmlSatusContent: '',
        url: '/Patient/Hemotransfusion',
        data: ''
    }).sethscCorner();

    //Menu.add({
    //    Title: 'Colonização MDR',
    //    onOutIcon: '../../Images/mdr.png',
    //    onClickIcon: '../../Images/mdr.png',
    //    HtmlSatusContent: '',
    //    url: '/Patient/ColonizationbyMdr',
    //    data: ''
    //}).sethscCorner();

    //Menu.add({
    //    Title: 'Receituário',
    //    onOutIcon: '../../Images/receituario.png',
    //    onClickIcon: '../../Images/receituario.png',
    //    HtmlSatusContent: '',
    //    url: '/Patient/Prescriptions',
    //    data: ''
    //}).sethscCorner();

    //Menu.add({
    //    Title: 'Dados da Alta',
    //    onOutIcon: '../../Images/dados_alta.png',
    //    onClickIcon: '../../Images/dados_alta.png',
    //    HtmlSatusContent: '',
    //    url: '/Patient/DataHigh',
    //    data: ''
    //}).sethscCorner();

    //Menu.add({
    //    Title: 'Formulário',
    //    onOutIcon: '../../Images/formulario.png',
    //    onClickIcon: '../../Images/formulario.png',
    //    HtmlSatusContent: '',
    //    url: '/Patient/Form',
    //    data: ''
    //}).sethscCorner();

    $("#_td").corner("4px");
    $("#o_td").corner("4px");

});

//Water mark
$(document).ready(function () {

    var watermark = 'Digite o Nome ou CPF do paciente';

    //init, set watermark text and class
    $('.DOMControl_placeholder').val(watermark).addClass('watermark');

    //if blur and no value inside, set watermark text and class again.
    $('.DOMControl_placeholder').blur(function () {
        if ($(this).val().length == 0) {
            $(this).val(watermark).addClass('watermark');
        }
    });

    //if focus and text is watermrk, set it to empty and remove the watermark class
    $('.DOMControl_placeholder').focus(function () {
        if ($(this).val() == watermark) {
            $(this).val('').removeClass('watermark');
        }
    });
});


// Images Gallery
hs.graphicsDir = '../Images/graphics/';
hs.align = 'center';
hs.transitions = ['expand', 'crossfade'];
hs.fadeInOut = true;
hs.dimmingOpacity = 0.8;
hs.wrapperClassName = 'borderless floating-caption';
hs.captionEval = 'this.thumb.alt';
hs.marginLeft = 100; // make room for the thumbstrip
hs.marginBottom = 80; // make room for the controls and the floating caption
hs.numberPosition = 'caption';
hs.lang.number = '%1/%2';

// Add the slideshow providing the controlbar and the thumbstrip
hs.addSlideshow({
    //slideshowGroup: 'group1',
    interval: 5000,
    repeat: false,
    useControls: true,
    overlayOptions: {
        className: 'text-controls',
        position: 'bottom center',
        relativeTo: 'viewport',
        offsetX: 50,
        offsetY: -5

    },
    thumbstrip: {
        position: 'middle left',
        mode: 'vertical',
        relativeTo: 'viewport'
    }
});

// Add the simple close button
hs.registerOverlay({
    html: '<div class="closebutton" onclick="return hs.close(this)" title="Close"></div>',
    position: 'top right',
    fade: 2 // fading the semi-transparent overlay looks bad in IE
});




// Table Component
function newRow(currentNode, url) {
    $.ajax({ type: "GET", url: url, cache: false }).success(function (d) {
        $(currentNode).parent().parent().clone($(currentNode).parent().parent())
            .appendTo($(currentNode).parent().parent().parent());
        var contentDiv = $(currentNode).parent().next();
        $(contentDiv).html(d);
        $(contentDiv).show();
        $(currentNode).parent().hide();
    });
}

function saveRow(element, url) {
    var form = $(element).parent();
    form.submit(function () {
        $.ajax({
            type: "POST",
            url: url,
            cache: false,
            data: form.serialize(),
            success: function (data) {
                var divContent = $(element).parent().parent();
                $(divContent).hide();
                $(divContent).prev().html(data);
                $(divContent).prev().show();
            }
        });
        return false;
    });
}

function editRow(url, data, e) {
    $.ajax({ type: "GET", url: url, cache: false, data: data }).success(function (d) {
        var div = $(e).parent().next();
        $(div).html(d);
        $(div).show();
        $(e).parent().hide();
    });
}

function deleteRow(e, url, data) {
    $.ajax({ type: "POST", url: url, cache: false, data: data }).success(function () {
        var li = $(e).parent();
        $(li).remove();
    });
}

function closeForm(element) {

    var divContent = $(element).parent().parent();
    var spanOfLinkAction = $(divContent).prev().children().children();

    if ($(spanOfLinkAction).text() == "+") {
        var li = $(divContent).parent();
        $(li).remove();
    } else {
        $(divContent).hide();
        $(divContent).prev().show();
    }
}

// Role display or hide table
function Display(element, option) {
    if (option == true) {
        $(element).show();
    } else {
        $(element).hide();
    }
}

// Format datepicker
function formatDatePicker() {
    $('#dob_month, #dob_year').change(function () {
        var year = $('#dob_year').val();
        var month = $('#dob_month').val();
        if ((year != 0) && (month != 0)) {
            var lastday = 32 - new Date(year, month - 1, 32).getDate();
            var selected_day = $('#dob_day').val();

            // Change selected day if it is greater than the number of days in current month
            if (selected_day > lastday) {
                $('#dob_day  > option[value=' + selected_day + ']').attr('selected', false);
                $('#dob_day  > option[value=' + lastday + ']').attr('selected', true);
            }

            // Remove possibly offending days
            for (var i = lastday + 1; i < 32; i++) {
                $('#dob_day  > option[value=' + i + ']').remove();
            }

            // Add possibly missing days
            for (var i = 29; i < lastday + 1; i++) {
                if (!$('#dob_day  > option[value=' + i + ']').length) {
                    $('#dob_day').append($("<option></option>").attr("value", i).text(i));
                }
            }
        }
    });
}

// AdvancedSearch
function goAdvancedSearch(q) {
    $.ajax({
        url: "../Search/Index/?query=" + q,
        type: "GET",
        cache: false,
        success: function (r) {
            $("#content").html(r);
            $("ul.results").hide();
        }
    });
}

function goFilter(form) {
    $.ajax({
        url: "../Search/FilterPeople",
        data: form,
        type: "POST",
        cache: false,
        success: function (r) {
            $("div#DivResult").html(r);
            $("ul.results").hide();
        }
    });
}

function GetMore(url, element) {

    $.ajax({
        url: url,
        type: "GET",
        cache: false,
        success: function (r) {
            if (r.length > 0) {
                $("#DivResult").append(r);
            } else {
                $(element).hide();
                $(element).parent().append('<br/><b style="font-size:12px;">Sem mais resultados</b><br/><br/>');
            }
        }
    });
}

function approveAccount(element, url) {
    var form = $(element).parent().parent().parent().parent();
    form.submit(function () {
        $.ajax({
            type: "POST",
            url: url,
            cache: false,
            data: form.serialize(),
            success: function (data) {
                var divContent = $(element).parent().parent().parent().parent().parent();
                $(divContent).hide();
            }
        });
        return false;
    });
}

function refuseAccount(element, url) {
    var form = $(element).parent().parent().parent().parent();
    form.submit(function () {
        $.ajax({
            type: "POST",
            url: url,
            cache: false,
            data: form.serialize(),
            success: function (data) {
                var divContent = $(element).parent().parent().parent().parent().parent();
                $(divContent).hide();
            }
        });
        return false;
    });
}

// Toggle of class of Advanced Search
$(".unitsSideBar li input[type='checkbox']").live(
    "click", function () {
        if ($(this).is(':checked')) {
            $(this).parent().addClass("itemChecked");
        } else {
            $(this).parent().removeAttr("class");
        }
    }
);

function ToggleAllergy() {
    var liCount = $("#ulAllergy > li").length;
    if (liCount > 1) {
        $('input[value="false"]').removeAttr("checked");
        $('input[value="true"]').attr("checked", "");
        $('#ulAllergy').removeAttr("style");
    }
}

function ToggleHemotransfusion() {
    var liCount = $("#ulHemotransfusion > li").length;
    if (liCount > 1) {
        $('input[value="false"]').removeAttr("checked");
        $('input[value="true"]').attr("checked", "");
        $('#ulHemotransfusion').removeAttr("style");
    }
}


function ShowFormChangePassword(element) {
    $(element).addClass("grayButtonClicked");
    $(element).next().show();
}

function HideFormChangePassword(element) {
    $(element).parent().parent().parent().prev().removeClass("grayButtonClicked");
    $(element).parent().parent().parent().hide();
}

function ChangePassword(element) {
    var form = $(element).parent().parent();
    form.submit(function () {
        $.ajax({
            type: "POST",
            url: "../Home/AlterPasswordOfAccount",
            cache: false,
            data: form.serialize(),
            success: function () {
                HideFormChangePassword(element);
            }
        });
        return false;
    });
}

function ShowListOfHospital(element) {
    if ($(element).hasClass("grayButtonClicked")) {
        $(element).removeClass("grayButtonClicked");
        $("#ChangeHospitalButton span").removeClass("arrowActive");
        $("#ChangeHospitalButton span").addClass("arrow");
        $(element).next().hide();
    } else {
        $(element).addClass("grayButtonClicked");
        $(element).next().show();
        $("#ChangeHospitalButton span").removeClass("arrow");
        $("#ChangeHospitalButton span").addClass("arrowActive");
    }
}

function ChangeCurrentHospital(q) {
    $.ajax({
        url: "../Home/ChangeCurrentHospital/?q=" + q,
        type: "GET",
        cache: false,
        success: function () {
            location.reload(true);
        }
    });
}

function ShowTopMenu(element) {
    if ($(element).hasClass("grayButtonClicked")) {
        $(element).removeClass("grayButtonClicked");
        $("#ChangeHospitalButton span").removeClass("arrowActive");
        $("#ChangeHospitalButton span").addClass("arrow");
        $(element).next().hide();
    } else {
        $(element).addClass("grayButtonClicked");
        $(element).next().show();
        $("#ChangeHospitalButton span").removeClass("arrow");
        $("#ChangeHospitalButton span").addClass("arrowActive");
    }
}