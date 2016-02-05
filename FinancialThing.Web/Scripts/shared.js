$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $('.toggle').change(function () {
        var id = $(this).data("id");
        var value = $(this).val();
        var obj = { Id: id, State: value }
        console.log(id);
        $.ajax({
            url: "/Company/Toggle",
            method: "POST",
            data: obj
        });
    });

    $('.data_row').click(function () {
        var toggle = $(this).next().attr("data-toggle");
        if (toggle == "0") {
            $(this).next().attr("data-toggle", "1");
            $(this).next().fadeIn();

        } else {
            $(this).next().attr("data-toggle", "0");
            $(this).next().fadeOut();

        }
    });

    $('.company').each(function () { $(this).css('top', $('#companies_header').height() + 100 + 'px'); });
});

function show(id) {
    $('.link').each(function () {
        $(this).removeClass("show");
        $(this).addClass("hide");
    });

    $('#link-' + id).removeClass('hide');
    $('#link-' + id).addClass('show');

    $('.company').each(function () {
        $(this).removeClass("show");
        $(this).addClass("hide");
    });

    $('#table-' + id).removeClass('hide');
    $('#table-' + id).addClass('show');
}

function toggle(dir) {
    $.ajax({
        url: "/Company/ToggleAll/" + dir,
        method: "GET",
        success: function () {
            $('.toggle').each(function () {
                if (dir == 0) {
                    $(this).removeProp("checked");
                } else {
                    $(this).prop("checked", "true");
                }
            });
        }
    });
}

function addCompany() {
    $('#overlay_blur').removeClass('normal');
    $('#overlay_blur').addClass('blured');
    $('.overlay').slideDown();
    $('.overlay').removeClass('hide');
    $('.overlay').addClass('show');
}

function closeOverlay() {
    $('.overlay').slideUp();
    $('#overlay_blur').removeClass('blured');
    $('#overlay_blur').addClass('normal');
    $('.overlay').removeClass('show');
    $('.overlay').addClass('hide');
}

function add() {
    $(".button").addClass("disabled");
    toastr.info("Adding new companing", "Please wait...", { timeOut: 1500 });
    var comp = $('#company_name').val();
    var exch = $('#exch_name').val();
    var sector = $("#sector").val();
    var industry = $("#industry").val();

    var data = { "StockName": exch, "Code": comp , "Industry": industry, "Sector": sector};

    $.ajax("/Company/AddCompany", {
        data: data,
        method: "POST",
        success: function (res) {
            toastr.success("Company added.", "Success!", { timeOut: 2000 });
            $(".button").removeClass("disabled");
            $('.companies_table>tbody').append(res);
            closeOverlay();
        },
        error: function() {
            toastr.error("There was internal error", "Oops!", { timeOut: 2000 });
            $(".button").removeClass("disabled");
        }
    });
}
function generateData(id) {
    $(".button").addClass("disabled");
    var compName = $("#label-" + id).text();
    toastr.info("Generating data for " + compName, "Please wait...", { timeOut: 2000 });
    $.ajax("/Data/Generate", {
        data: { "id": id },
        method: "POST",
        success: function (res) {
            toastr.success("Finished generating data for " + compName, "Success!", { timeOut: 2000 });
            $(".button").removeClass("disabled");
        }
    });
}

function GenerateAll() {
    $.ajax("/Data/GenerateAll", {
        method: "GET",
        success: function (res) {

        }
    });
}

String.format = function () {
    var s = arguments[0];
    for (var i = 0; i < arguments.length - 1; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "gm");
        s = s.replace(reg, arguments[i + 1]);
    }

    return s;
}