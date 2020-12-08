<template>
    <div>
        <div><p class="dropdown-header font-weight-bold">By Platform</p></div>
        <li><div class="dropdown-divider"></div></li>
        <li><a class="dropdown-item" v-for="platform in platforms" :key="platform.id">{{ platform.name }}</a></li>
    </div>
</template>
<script>
    export default {
        data: function () {
            return {
                platforms: []
            };
        },
        methods: {
            getAllPlatforms() {
                this.$api.get('platform/GetAllAsync')
                    .then(res => {
                        this.platforms = res.data;
                        console.log("api/platform/GetAllAsync Success: " + res);
                    })
                    .catch(err => {
                        console.log("api/platform/GetAllAsync Error: " + err);
                    });
            }
        },
        created() {
            this.getAllPlatforms();
        }
    }
</script>