<template>
    <div id="browse-games-page">
        <browse-games-by-genre></browse-games-by-genre>
        <browse-games-by-platform></browse-games-by-platform>
    </div>
</template>

<script>
    import BrowseGamesByGenre from './components/BrowseGamesByGenre.vue';
    import BrowseGamesByPlatform from './components/BrowseGamesByPlatform.vue';

    export default {
        components: {
            'browse-games-by-genre': BrowseGamesByGenre,
            'browse-games-by-platform': BrowseGamesByPlatform
        }
    }
</script>

<style>

</style>