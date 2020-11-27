<template>
    <div>
        <div><a class="dropdown-header font-weight-bold">By Platform</a></div>
        <li><div class="dropdown-divider"></div></li>
        <li><a class="dropdown-item" v-for="platform in platforms" :key="platform.id">{{ platform.name }}</a></li>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        data: function () {
            return {
                platforms: []
            };
        },
        methods: {
            getAllPlatforms() {
                axios.get('http://localhost:44375/api/platform/GetAll')
                    .then(res => {
                        this.platforms = res.data;
                        console.log("api/platform/GetAll Success: " + res);
                    })
                    .catch(err => {
                        console.log("api/platform/GetAll Error: " + err);
                    });
            }
        },
        created() {
            this.getAllPlatforms();
        }
    }
</script>