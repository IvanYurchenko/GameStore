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

    // Comments
    $(".comment").find(".btn-reply").bind("click", function () {
        var parentContainer = $(this).parents("div.comment");
        var commentId = parentContainer.find(".comment-id");
        var commentName = parentContainer.find(".comment-name");
        $("#ParentCommentId").val(commentId.val());
        $("#Body").val(commentName.text().trim() + ", " + $("#Body").val());
    });

    $(".comment").find(".btn-quote").bind("click", function () {
        var parentContainer = $(this).parents("div.comment");
        var commentId = parentContainer.find(".comment-id");
        var commentName = parentContainer.find(".comment-name").text();
        var commentBody = parentContainer.find(".comment-body");
        $("#ParentCommentId").val(commentId.val());

        var replacedBody = commentBody.html().toString().trim()
            .replace(/<blockquote>/ig, "[quote]")
            .replace(/<\/blockquote>/ig, "[/quote]")
            .replace(/<footer>/ig, "[author]")
            .replace(/<\/footer>/ig, "[/author]");

        var newBody = "[quote]" + replacedBody + "[author]" + commentName + "[/author]" + "[/quote]\r\n" + $("#Body").val();
        $("#Body").val(newBody);
    });
});

