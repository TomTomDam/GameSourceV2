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
        name: 'AccountSettingsPage',
        data() {
            return {
    
            }
        },
        methods: {

        },
        mounted: {

        },
    }
</script>

<style>

</style>