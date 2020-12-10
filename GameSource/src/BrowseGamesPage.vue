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
    .dropdown-menu {
        background-color: #c7d5e0;
    }

    .dropdown-header {
        color: #4f94bc;
    }

    .dropdown-item {
        color: black;
    }

    .dropdown-item:hover,
    .dropdown-menu > li:hover:not(.dropdown-header) {
        background-color: #171a21;
        color: white;
    }
</style>