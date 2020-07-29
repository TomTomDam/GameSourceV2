<template>
    <div class="col-md-9 float-left" id="account-settings">
        <div class="sub-heading">Account</div>
        <div>
            {{ msg }}
        </div>
    </div>
</template>

<script>
    export default {
        name: 'Account',
        data() {
            return {
                msg: 'Account.vue page'
            }
        }
    }
</script>