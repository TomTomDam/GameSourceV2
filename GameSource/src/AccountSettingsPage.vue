<template>
    <div id="account-settings-page">
        <div class="page-content container clearfix">
            <div class="col-md-3 float-left pr-4">
                <nav class="menu position-relative" aria-label="Account Settings Sidebar Menu">
                    <router-link to="/account" class="menu-item active" id="account-settings-btn">Account</router-link>
                    <router-link to="/email" class="menu-item" id="email-settings-btn">Email</router-link>
                    <router-link to="/privacy" class="menu-item" id="privacy-settings-btn">Privacy</router-link>
                </nav>
            </div>
        </div>
        <router-view></router-view>
    </div>
</template>

<script>
    export default {
        data() {
            return {
    
            }
        }
    }
</script>

<style>

</style>