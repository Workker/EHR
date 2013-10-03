
// Suggest of search
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
                    url: "http://" + window.location.host + "/Search/SearchPeaple",
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
$(document).ready(function () {
    handleAjaxMessages();
    displayMessages();
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

    Menu.add({
        Title: 'Exames',
        onOutIcon: '../../Images/exames.png',
        onClickIcon: '../../Images/exames.png',
        HtmlSatusContent: '',
        url: '/Patient/Exams',
        data: ''
    });

    Menu.add({
        Title: 'Procedimentos',
        onOutIcon: '../../Images/procedimentos.png',
        onClickIcon: '../../Images/procedimentos.png',
        HtmlSatusContent: '',
        url: '/Patient/Procedures',
        data: ''
    }).sethscCorner();

    Menu.add({
        Title: 'hemotransfus&atilde;o',
        onOutIcon: '../../Images/hemotransfusao.png',
        onClickIcon: '../../Images/hemotransfusao.png',
        HtmlSatusContent: '',
        url: '/Patient/Hemotransfusion',
        data: ''
    }).sethscCorner();

    Menu.add({
        Title: 'MDR',
        onOutIcon: '../../Images/mdr.png',
        onClickIcon: '../../Images/mdr.png',
        HtmlSatusContent: '',
        url: '/Patient/ColonizationbyMdr',
        data: ''
    }).sethscCorner();

    Menu.add({
        Title: 'Prescrição de Alta',
        onOutIcon: '../../Images/receituario.png',
        onClickIcon: '../../Images/receituario.png',
        HtmlSatusContent: '',
        url: '/Patient/Prescriptions',
        data: ''
    }).sethscCorner();

    Menu.add({
        Title: 'Dados da Alta',
        onOutIcon: '../../Images/dados_alta.png',
        onClickIcon: '../../Images/dados_alta.png',
        HtmlSatusContent: '',
        url: '/Patient/DischargeData',
        data: ''
    }).sethscCorner();

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


//// Images Gallery
//hs.graphicsDir = '../Images/graphics/';
//hs.align = 'center';
//hs.transitions = ['expand', 'crossfade'];
//hs.fadeInOut = true;
//hs.dimmingOpacity = 0.8;
//hs.wrapperClassName = 'borderless floating-caption';
//hs.captionEval = 'this.thumb.alt';
//hs.marginLeft = 100; // make room for the thumbstrip
//hs.marginBottom = 80; // make room for the controls and the floating caption
//hs.numberPosition = 'caption';
//hs.lang.number = '%1/%2';

//// Add the slideshow providing the controlbar and the thumbstrip
//hs.addSlideshow({
//    //slideshowGroup: 'group1',
//    interval: 5000,
//    repeat: false,
//    useControls: true,
//    overlayOptions: {
//        className: 'text-controls',
//        position: 'bottom center',
//        relativeTo: 'viewport',
//        offsetX: 50,
//        offsetY: -5

//    },
//    thumbstrip: {
//        position: 'middle left',
//        mode: 'vertical',
//        relativeTo: 'viewport'
//    }
//});

//// Add the simple close button
//hs.registerOverlay({
//    html: '<div class="closebutton" onclick="return hs.close(this)" title="Close"></div>',
//    position: 'top right',
//    fade: 2 // fading the semi-transparent overlay looks bad in IE
//});

// Table Component
function newRow(currentNode, url) {
    $.ajax({
        type: "GET",
        url: "http://" + window.location.host + url,
        cache: false
    }).success(function (d) {
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
            url: "http://" + window.location.host + url,
            cache: false,
            data: form.serialize(),
            success: function (data) {
                var divContent = $(element).parent().parent().parent();
                var data2 = data.toString().replace('<li class="clearfix">', '').replace("</li>", "");
                $(divContent).html(data2);
            }
        });
        return false;
    });
}

function editRow(url, data, e) {
    $.ajax({
        type: "GET",
        url: "http://" + window.location.host + url,
        cache: false,
        data: data
    }).success(function (d) {
        var div = $(e).parent().next();
        $(div).html(d);
        $(div).show();
        $(e).parent().hide();
    });
}

function deleteRow(e, url, data) {
    $.ajax({
        type: "POST",
        url: "http://" + window.location.host + url,
        cache: false,
        data: data
    }).success(function () {
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

function SaveComplementaryExam(element) {

    var period = $(element).prev().prev().prev().val();
    var description = $(element).prev().prev().prev().prev().prev().prev().prev().val();

    $.ajax({
        type: "POST",
        url: "http://" + window.location.host + "/Patient/SaveComplementaryExam",
        cache: false,
        data: { description: description, period: period },
        success: function (data) {
            var divContent = $(element).parent().parent();
            $(divContent).hide();
            $(divContent).prev().html(data);
            $(divContent).prev().show();
        }
    });
}

function SaveMedicalReview(element) {

    var termMedicalReviewAt = $(element).prev().prev().prev().prev().prev().prev().prev().prev().val();
    var specialtyId = $(element).prev().prev().prev().val();
    var specialtyDescription = $(element).prev().prev().prev().prev().val();

    $.ajax({
        type: "POST",
        url: "http://" + window.location.host + "/Patient/SaveMedicalReview",
        cache: false,
        data: { TermMedicalReviewAt: termMedicalReviewAt, "Specialty.Id": specialtyId, "Specialty.Description": specialtyDescription },
        success: function (data) {
            var divContent = $(element).parent().parent();
            $(divContent).hide();
            $(divContent).prev().html(data);
            $(divContent).prev().show();
        }
    });
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
            var selectedDay = $('#dob_day').val();

            // Change selected day if it is greater than the number of days in current month
            if (selectedDay > lastday) {
                $('#dob_day  > option[value=' + selectedDay + ']').attr('selected', false);
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
        url: "http://" + window.location.host + "/Search/Index/?query=" + q,
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

function GetMore(element, url) {
    $.ajax({
        url: "http://" + window.location.host + url,
        type: "GET",
        cache: false,
        success: function (r) {
            if (r.length > 10) {
                $(element).parent().prev().append(r);
            } else {
                $(element).hide();
                $(element).parent().append('<br/><b style="font-size:12px;">Sem mais resultados</b><br/><br/>');
            }
        }
    });
}

function approveAccount(element) {
    var form = $(element).parent().parent().parent().parent();

    form.submit(function () {
        $.ajax({
            type: "POST",
            url: "http://" + window.location.host + "/Home/ApproveAccount",
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

function refuseAccount(element) {
    var form = $(element).parent().parent().parent().parent();
    form.submit(function () {
        $.ajax({
            type: "POST",
            url: "http://" + window.location.host + "/Home/RefuseAccount",
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
            url: "http://" + window.location.host + "/Home/AlterPasswordOfAccount",
            cache: false,
            data: form.serialize(),
            success: function () {
                HideFormChangePassword(element);
            }
        });
        return false;
    });
}

function ShowFormAddprofessionalResgistration(element) {
    $(element).addClass("grayButtonClicked");
    $(element).next().show();
}

function HideFormAddprofessionalResgistration(element) {
    $(element).parent().parent().parent().prev().removeClass("grayButtonClicked");
    $(element).parent().parent().parent().hide();
}

function AddprofessionalResgistration(element) {
    var form = $(element).parent().parent();
    form.submit(function () {
        $.ajax({
            type: "POST",
            url: "http://" + window.location.host + "/Home/AddprofessionalResgistration",
            cache: false,
            data: form.serialize(),
            success: function () {
                HideFormAddprofessionalResgistration(element);
            }
        });
        return false;
    });
}

function ShowTopMenu(element) {
    if ($(element).next().hasClass("Display")) {
        $(element).next().removeClass("Display");
        $(element).next().addClass("noDisplay");
        $(element).parent().removeClass("activeMenu");
    } else {
        $(element).next().removeClass("noDisplay");
        $(element).next().addClass("Display");
        $(element).parent().addClass("activeMenu");
    }
}

function GenericAutoComplete(autoCompleteElement, url, codeElement) {
    $(autoCompleteElement).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "http://" + window.location.host + url,
                type: "GET",
                dataType: "JSON",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Description, value: item.Description, code: item.Code };
                    }));
                }
            });
        },
        messages: {
            noResults: "",
            results: ""
        }, select: function (event, ui) {
            $(codeElement).val(ui.item.code);
        }
    });
}

function displayMessage(message, messageType) {
    $("#messagewrapper").html('<div class="messagebox ' + messageType.toLowerCase() + '"></div>');
    $("#messagewrapper .messagebox").text(message);
    displayMessages();
}

function displayMessages() {
    if ($("#messagewrapper").children().length > 0) {
        $("#messagewrapper").show();
        $(document).click(function () {
            clearMessages();
        });
    }
    else {
        $("#messagewrapper").hide();
    }
}

function clearMessages() {
    $("#messagewrapper").fadeOut(500, function () {
        $("#messagewrapper").empty();
    });
}

function handleAjaxMessages() {
    $(document).ajaxSuccess(function (event, request) {
        checkAndHandleMessageFromHeader(request);
    }).ajaxError(function (event, request) {
        displayMessage(request.responseText, "error");
    });
}

function checkAndHandleMessageFromHeader(request) {
    var msg = request.getResponseHeader('X-Message');
    if (msg) {
        displayMessage(msg, request.getResponseHeader('X-Message-Type'));
    }
}

$(document).on('submit', '#ColonizationByMDR', function (e) {
    e.preventDefault();
    data = $(this).serialize();
    $.ajax({
        type: "POST",
        url: "http://" + window.location.host + '/Patient/SaveMdr',
        cache: false,
        data: data,
        success: function () {
        }
    });
});

$(document).on('submit', '#dischargeData', function (e) {
    e.preventDefault();
    data = $(this).serialize();
    $.ajax({
        type: "POST",
        url: "http://" + window.location.host + '/Patient/SaveHighData',
        cache: false,
        data: data,
        success: function () {
        }
    });
});

function FinalizeSummary(url) {
    $.ajax({
        type: "POST",
        url: "http://" + window.location.host + "/Patient/FinalizeSummary",
        cache: false,
        success: function () {
            RemoveEdition();
        }
    });
}

function RemoveEdition() {
    $(".contentPage input[type=radio], .contentPage textarea, .contentPage select, .contentPage input[type=text], .contentPage input[type=time]").attr("disabled", true);
    $(".action, .contentPage input[type=submit], .contentPage input[type=reset], .contentPage input[type=button]").remove();
}

function SaveObsevation(element) {
    var form = $(element).parent();
    form.submit(function () {
        $.ajax({
            type: "POST",
            url: "http://" + window.location.host + "/Patient/SaveObservation",
            cache: false,
            data: form.serialize(),
            success: function () {
            }
        });
        return false;
    });
}