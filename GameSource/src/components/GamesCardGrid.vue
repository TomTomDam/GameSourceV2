<template>
    <div class="container">
        <div class="row" v-if="games.length > 0">
            <div class="col-3" v-for="game in games" v-bind:key="game">
                <game-card :game="game"></game-card>
            </div>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        data: function () {
            return {
                games: []
            };
        },
        methods: {
            getAllGames() {
                const { data } = axios.get('games/index');
                this.games = data;
            }
        },
        mounted() {
            this.getAllGames();
        }
    }
</script>