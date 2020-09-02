<template>
    <div>
        <li><a class="dropdown-header font-weight-bold">By Genre</a></li>
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
                this.$baseURL.get('api/genre/GetAll')
                    .then(res => {
                        this.genres = res.data;
                        console.log("api/genre/GetAll Success: " + res);
                    })
                    .catch(err => {
                        console.log("api/genre/GetAll Error: " + err);
                    });
            }
        },
        created() {
            this.getAllGenres();
        }
    }
</script>