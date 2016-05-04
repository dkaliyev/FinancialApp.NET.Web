$(document).ready(function () {
    avg = 0;
    dragula([document.getElementById('formula_input'), document.getElementById('gragabble')], {

        isContainer: function (el) {
            return el.classList.contains('gragabble');
        },

        mirrorContainer: document.body,
        copy: function (el, source) {
            return source.classList.contains("gragabble");
        },
        accepts: function (el, target, source, sibling) {
            return target.id == "formula_input";
        },
        removeOnSpill: function (el, target, source) {
            return source.id == "formula_input";
        }
    });


});

function addRatio() {
    var elements = $("#formula_input").find(".element");
    var formula = "";
    $.each(elements, function (ind, el) {
        var code = $(el).attr("data-code");
        if ($(el).hasClass("ratio")) {
            if (avg == 1) {
                formula += String.format("[{0}], 1)", code);
                avg = 0;
            } else {
                formula += String.format("Get([{0}], 0)", code);
            }

        }
        if ($(el).hasClass("operation")) {

            if (code == "AVG") {
                formula += String.format("Get(", code);
                avg = 1;
            } else {
                formula += code;
            }

        }
        if ($(el).hasClass("input")) {
            formula += $(el).find("input").val();
        }
    });
    console.log(formula);
    $.ajax("/RatiosBuilder/AddRatio", {
        method: "post",
        data: { Formula: formula, Name: $("#ratio_name").val() },
        success: function (resp) {
            console.log(resp);
            var name = resp['_ratio']['Name'];
            var form = resp['_ratio']['Formula'];
            $('.companies_table>tbody').append(String.format("<tr><td>{0}</td><td>{1}</td></tr>", name, form));
        }
    });
    $('#formula_input').empty();
    closeOverlay();
    return false;
}

function getCode(formula) {

    var start = formula.indexOf("Get(");
    var end = formula.indexOf(")");

    var op = "";
    var nextStr = "";
    var flag = formula[end - 1];
    if (end != formula.length - 1) {
        op = formula[end + 1];
        nextStr = formula.substring(end + 2);
    }

    var innerString = formula.substring(start, end);
    var innerStart = innerString.indexOf("[");
    var innerEnd = innerString.indexOf("]");

    var code = innerString.substring(innerStart+1, innerEnd);

    return { code: code, operator: op, flag: flag, nextStr: nextStr };
}

function RemoveRatio(id) {
    
}

function closePopup() {
    closeOverlay();
    $('#formula_input').empty();
    return false;
}