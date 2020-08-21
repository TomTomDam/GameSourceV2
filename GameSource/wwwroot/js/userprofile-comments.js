$(document).ready(function () {
    userProfileCreateComment();
    $('#userprofile-create-comment').show();

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