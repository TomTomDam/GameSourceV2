<template>
    <div id="app">
        <div class="col-md-3 float-left pr-4">
            <nav class="menu position-relative" aria-label="Account Settings">
                <router-link to="/account-settings" class="menu-item active" id="account-settings-btn">
                    Account
                </router-link>
                <router-link to="/email-settings" class="menu-item" id="email-settings-btn">
                    Email
                </router-link>
                <router-link to="/privacy-settings" class="menu-item" id="privacy-settings-btn">
                    Privacy
                </router-link>
            </nav>
        </div>

        <router-view></router-view>
        <div id="account-settings"></div>
        <div id="email-settings"></div>
        <div id="privacy-settings"></div>
    </div>
</template>

<script>
    export default {
        name: 'app'
    }
</script>