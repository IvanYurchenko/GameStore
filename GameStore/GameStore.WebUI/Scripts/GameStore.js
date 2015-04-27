$(document).ready(function () {
    var formOriginalData = $("#filter-form").serialize();
    $("input[type=\"submit\"]").click(function () {
        if ($("#filter-form").serialize() != formOriginalData) {
            $("#filter-form").submit();
        } else {
            return false;
        }
    });

    $("#paging-drop").change(function () {
        $("#Pagination_PageCapacity").val($(this).val());
        $("#filter-form").submit();
    });

    //$("#paging-drop").val($("#Pagination_PageCapacity").val());
});

