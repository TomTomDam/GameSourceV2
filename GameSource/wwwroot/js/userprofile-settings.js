$(document).ready(function () {
    var userProfileId = $('#userProfileId').val();
    console.log(userProfileId);

    generalSettings(userProfileId);
    $('#userprofile-general-settings').show();
    $('#userprofile-avatar-settings').hide();
    $('#userprofile-profile-background-settings').hide();

    $('#userprofile-general-settings-btn').on('click', function (e) {
        generalSettings(userProfileId);
    });

    function generalSettings(userProfileId) {
        $('#userprofile-avatar-settings').hide();
        $('#userprofile-profile-background-settings').hide();

        $.ajax({
            url: '/user/profile/' + userProfileId + '/general-settings',
            type: 'GET',
            success: function (result) {
                $('#userprofile-general-settings').html(result);
                $('#userprofile-general-settings').show();
            },
            error: function (xhr) {
                console.log("Error: " + xhr.statusText);
            }
        });
    }

    $('#userprofile-avatar-settings-btn').on('click', function (e) {
        avatarSettings(userProfileId);
    });

    function avatarSettings(userProfileId) {
        $('#userprofile-general-settings').hide();
        $('#userprofile-profile-background-settings').hide();

        $.ajax({
            url: '/user/profile/' + userProfileId + '/avatar-settings',
            type: 'GET',
            success: function (result) {
                $('#avatar-settings').html(result);
                $('#avatar-settings').show();
            },
            error: function (xhr) {
                console.log("Error: " + xhr.statusText);
            }
        });
    }

    $('#userprofile-profile-background-settings-btn').on('click', function (e) {
        profileBackgroundSettings(userProfileId);
    });

    function profileBackgroundSettings(userProfileId) {
        $('#userprofile-general-settings').hide();
        $('#userprofile-avatar-settings').hide();

        $.ajax({
            url: '/user/profile/' + userProfileId + '/privacy-settings',
            type: 'GET',
            success: function (result) {
                $('#userprofile-profile-background-settings').html(result);
                $('#userprofile-profile-background-settings').show();
            },
            error: function (xhr) {
                console.log("Error: " + xhr.statusText);
            }
        });
    }
});