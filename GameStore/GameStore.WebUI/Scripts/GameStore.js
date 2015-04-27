$(document).ready(function () {
    var formOriginalData = $("#filter-form").serialize();
    $("input[type=\"submit\"]").click(function () {
        if ($("#filter-form").serialize() != formOriginalData) {
            $("#filter-form").submit();
        } else {
            return false;
        }
    });

    $("#PageCapacity").change(function () {
        var capacity = $("#PageCapacity option:selected").val();

        var pageCapacityStr = "Pagination.PageCapacity";

        var oldUrl = window.location.toString();
        var newUrl;
        if (oldUrl.indexOf(pageCapacityStr) > 0) {
            var oldUrlPart = pageCapacityStr + "=[0-9]*";
            var regExp = new RegExp(oldUrlPart, "");
            var newUrlPart = pageCapacityStr + "=" + capacity;
            newUrl = oldUrl.replace(regExp, newUrlPart);
        }
        else {
            newUrl = oldUrl + ((oldUrl.indexOf("?") > 0) ? ("&" + pageCapacityStr + "=") : ("?" + pageCapacityStr + "=")) + capacity;
        }

        window.location.assign(newUrl);
    });
});

