<template>
    <div id="browse-games-page">
        <BrowseGamesByGenre></BrowseGamesByGenre>
        <BrowseGamesByPlatform></BrowseGamesByPlatform>
    </div>
</template>

<script>
    import BrowseGamesByGenre from './components/BrowseGamesByGenre.vue';
    import BrowseGamesByPlatform from './components/BrowseGamesByPlatform.vue';

    export default {
        data() {
            return {

            }
        }
    }
</script>

<style>

</style>