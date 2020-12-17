$(document).ready(function () {
    var userProfileId = $('#userProfileId').val();

    reviewCreateCommentPartialView(userProfileId);

    $('#review-create-comment-btn').click(function () {
        debugger;
        reviewCreateComment();
    });

    function reviewCreateCommentPartialView(userProfileId) {
        $.ajax({
            url: '/review-comment/create/' + userProfileId,
            type: 'GET',
            success: function (result) {
                $('#review-create-comment').html(result);
                $('#review-create-comment').show();
                console.log("GET /review-comment/create/ Success!")
            },
            error: function (xhr) {
                console.log("GET /review-comment/create/" + userProfileId + " Error: " + xhr.statusText);
            }
        });
    }

    function reviewCreateComment() {
        debugger;
        var reviewCommentObject = {
            "body": $('#review-create-comment-input').val(),
            "userProfileID": userProfileId
        };

        $.ajax({
            url: '/review-comment/create',
            data: JSON.stringify(reviewCommentObject),
            type: 'POST',
            success: function (result) {
                console.log("POST /review-comment/create" + " Success!")
            },
            error: function (xhr) {
                console.log("POST /review-comment/create/" + " Error: " + xhr.statusText);
            }
        });
    }
});