$(document).ready(function () {
    userProfileCreateComment();

    $('#userprofile-create-comment-btn').on('click', function (e) {
        userProfileCreateComment();
    });

    function userProfileCreateComment() {
        $.ajax({
            url: '/user/profile/comment/create',
            type: 'GET',
            success: function (result) {
                $('#userprofile-create-comment').html(result);
                $('#userprofile-create-comment').show();
            },
            error: function (xhr) {
                console.log("Error: " + xhr.statusText);
            }
        });
    }
});