<template>
    <div>
        <li><a class="dropdown-header font-weight-bold">By Genre</a></li>
        <li><div class="dropdown-divider"></div></li>
        <li><a class="dropdown-item" v-for="genre in genres" v-bind:key="genre">{{ genre.name }}</a></li>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        data: function () {
            return {
                genres: []
            };
        },
        methods: {
            getAllGenres() {
                axios.get('/genre/GetAll')
                    .then(res => {
                        this.genres = res;
                        console.log(res);
                    })
                    .catch(err => {
                        console.log("Error:" + err);
                    });
            }
        },
        mounted() {
            this.getAllGenres();
        }
    }
</script>