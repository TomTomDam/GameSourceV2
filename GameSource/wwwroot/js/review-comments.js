$(document).ready(function () {
    var userId = $('#userId').val();

    reviewCreateCommentPartialView(userId);

    $('#review-create-comment-btn').click(function () {
        debugger;
        reviewCreateComment();
    });

    function reviewCreateCommentPartialView(userId) {
        $.ajax({
            url: '/review-comment/create/' + userId,
            type: 'GET',
            success: function (result) {
                $('#review-create-comment').html(result);
                $('#review-create-comment').show();
                console.log("GET /review-comment/create/ Success!")
            },
            error: function (xhr) {
                console.log("GET /review-comment/create/" + userId + " Error: " + xhr.statusText);
            }
        });
    }

    function reviewCreateComment() {
        debugger;
        var reviewCommentObject = {
            "body": $('#review-create-comment-input').val(),
            "userProfileID": userId
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