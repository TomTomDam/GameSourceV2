<template>
    <div>
        <div><p class="dropdown-header font-weight-bold">By Genre</p></div>
        <li><div class="dropdown-divider"></div></li>
        <li><a class="dropdown-item" v-for="genre in genres" :key="genre.id">{{ genre.name }}</a></li>
    </div>
</template>
<script>
    export default {
        data: function () {
            return {
                genres: []
            };
        },
        methods: {
            getAllGenres() {
                this.$api.get('api/genre/GetAllAsync')
                    .then(res => {
                        this.genres = res.data;
                        console.log("api/genre/GetAllAsync Success: " + res);
                    })
                    .catch(err => {
                        console.log("api/genre/GetAllAsync Error: " + err);
                    });
            }
        },
        created() {
            this.getAllGenres();
        }
    }
</script>