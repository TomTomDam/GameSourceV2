<template>
    <div>
        <div><p class="dropdown-header font-weight-bold">By Genre</p></div>
        <li><div class="dropdown-divider"></div></li>
        <li><div class="dropdown-item" v-for="genre in genres" :key="genre.id" v-on:click="redirectToURL($baseURL + 'genres/details' + genre.id)">{{ genre.name }}</div></li>
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
                this.$api.get("genres")
                    .then(res => {
                        this.genres = res.data.data;
                        console.log("api/genres/GetAll Success: " + res);
                    })
                    .catch(err => {
                        console.log("api/genres/GetAll Error: " + err);
                    });
            },
            redirectToURL(url) {
                console.log(url);
                window.location = url;
            }
        },
        created() {
            this.getAllGenres();
        }
    }
</script>