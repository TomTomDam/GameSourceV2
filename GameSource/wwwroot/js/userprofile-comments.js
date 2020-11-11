$(document).ready(function () {
    var userProfileId = $('#userProfileId').val();

    userProfileCreateCommentPartialView(userProfileId);

    $('#userprofile-create-comment-btn').click(function () {
        debugger;
        userProfileCreateComment();
    });

    function userProfileCreateCommentPartialView(userProfileId) {
        $.ajax({
            url: '/user/profile/comment/create/' + userProfileId,
            type: 'GET',
            success: function (result) {
                $('#userprofile-create-comment').html(result);
                $('#userprofile-create-comment').show();
                console.log("GET /user/profile/comment/create/ Success!")
            },
            error: function (xhr) {
                console.log("GET /user/profile/comment/create/" + userProfileId + " Error: " + xhr.statusText);
            }
        });
    }

    function userProfileCreateComment() {
        debugger;
        var userProfileCommentObject = {
            "body": $('#userprofile-create-comment-input').val(),
            "userProfileID": userProfileId
        };

        $.ajax({
            url: '/user/profile/comment/create',
            data: JSON.stringify(userProfileCommentObject),
            type: 'POST',
            success: function (result) {
                console.log("POST /user/profile/comment/create" + " Success!")
            },
            error: function (xhr) {
                console.log("POST /user/profile/comment/create/" + " Error: " + xhr.statusText);
            }
        });
    }
});