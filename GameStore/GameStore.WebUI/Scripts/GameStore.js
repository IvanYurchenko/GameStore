﻿$(document).ready(function () {
    var formOriginalData = $("#filter-form").serialize();
    $("input[type=\"submit\"]").click(function () {
        if ($("#filter-form").serialize() != formOriginalData) {
            $("#filter-form").submit();
        } else {
            return false;
        }
    });
});
