<template>
    <div class="card text-center">
        <img src="" alt="Game Cover Image"/>
        <div>
            <h5>{{ game.name }}</h5>
            <p>
                {{ game.genre }}
                <br />
                {{ game.developer }}
                <br />
                {{ game.publisher }}
                <br />
                {{ game.platform }}
            </p>
            <a class="btn btn-primary" onclick="{ this.gameDetails() }">View</a>
            <a class="btn btn-primary" onclick="{ this.gameEdit() }">Edit</a>
            <a class="btn btn-primary" onclick="{ this.gameDelete() }">Delete</a>
        </div>
    </div>
</template>

<script>
    export default {
        props: {
            user: Object
        },
        methods: {
            gameDetails() {
                $.get('/games/details/' + this.$route.params.id);
            },
            gameEdit() {
                $.get('/games/edit/' + this.$route.params.id);
            },
            gameDelete() {
                $.get('/games/delete/' + this.$route.params.id);
            }
        }
    }
</script>

<style>
    .card {
        box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
        transition: 0.3s;
        margin: 10px;
        padding-bottom: 5px
    }

    .card:hover {
        box-shadow: 0 8px 16px 0 rgba(0,0,0,0.2);
    }

    .card > img {
        width: 100%;
    }
</style>