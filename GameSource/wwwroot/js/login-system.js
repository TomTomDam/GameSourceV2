function logoutUser() {
    $.ajax({
        url: 'User/Logout',
        success: function (data) {
            if (data.success) {

            }
            else {

            }
        }
    });
}