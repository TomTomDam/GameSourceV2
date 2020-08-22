$(document).ready(function () {
    $('#userprofile-create-comment').show();

    $('#userprofile-create-comment-btn').on('click', function (e) {
        userProfileCreateComment();
    });

    function userProfileCreateComment() {
        $.ajax({
            url: '/user/profile/comment/create',
            type: 'POST',
            success: function (result) {

            },
            error: function (xhr) {
                console.log("Error: " + xhr.statusText);
            }
        });
    }
});