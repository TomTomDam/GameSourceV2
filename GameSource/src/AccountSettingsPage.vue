<template>
    <div id="account-settings-page">
        <div class="page-content container clearfix">
            <div class="col-md-3 float-left pr-4">
                <nav class="menu position-relative" aria-label="Account Settings Sidebar Menu">
                    <router-link to="/account/account" class="menu-item active" id="account-settings-btn">Account</router-link>
                    <router-link to="/account/email" class="menu-item" id="email-settings-btn">Email</router-link>
                    <router-link to="/account/privacy" class="menu-item" id="privacy-settings-btn">Privacy</router-link>
                </nav>
            </div>
            <div class="col-md-9 float-left">
                <router-view></router-view>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
    
            }
        },
        methods: {
            accountSettings() {
                $('#email-settings').hide();
                $('#privacy-settings').hide();

                $.ajax({
                    url: '/account/account-settings',
                    type: 'GET',
                    success: function (result) {
                        $('#account-settings').html(result);
                        $('#account-settings').show();
                    },
                    error: function (xhr) {
                        console.log("Error: " + xhr.statusText);
                    }
                });
            },
            emailSettings() {
                $('#account-settings').hide();
                $('#privacy-settings').hide();

                $.ajax({
                    url: '/account/email-settings',
                    type: 'GET',
                    success: function (result) {
                        $('#email-settings').html(result);
                        $('#email-settings').show();
                    },
                    error: function (xhr) {
                        console.log("Error: " + xhr.statusText);
                    }
                });
            },
            privacySettings() {
                $('#email-settings').hide();
                $('#account-settings').hide();

                $.ajax({
                    url: '/account/privacy-settings',
                    type: 'GET',
                    success: function (result) {
                        $('#privacy-settings').html(result);
                        $('#privacy-settings').show();
                    },
                    error: function (xhr) {
                        console.log("Error: " + xhr.statusText);
                    }
                });
            }
        },
        mounted: {
            accountSettings();
            $('#account-settings').show();
            $('#email-settings').hide();
            $('#privacy-settings').hide();

            $('#account-settings-btn').on('click', function (e) {
                accountSettings();
            });

            $('#email-settings-btn').on('click', function (e) {
                emailSettings();
            });

            $('#privacy-settings-btn').on('click', function (e) {
                privacySettings();
            });
        },
    }
</script>

<style>

</style>