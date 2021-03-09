<template>
    <div>
        <div><p class="dropdown-header font-weight-bold">By Platform</p></div>
        <li><div class="dropdown-divider"></div></li>
        <li><div class="dropdown-item" v-for="platform in platforms" :key="platform.id" v-on:click="redirectToURL($baseURL + 'platform/details/' + platform.id)">{{ platform.name }}</div></li>
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
                this.$api.get("platforms")
                    .then(res => {
                        this.platforms = res.data.data;
                        console.log("api/platforms/GetAll Success: " + res.data.data);
                    })
                    .catch(err => {
                        console.log("api/platforms/GetAll Error: " + err);
                    });
            },
            redirectToURL(url) {
                console.log(url);
                window.location = url;
            }
        },
        created() {
            this.getAllPlatforms();
        }
    }
</script>