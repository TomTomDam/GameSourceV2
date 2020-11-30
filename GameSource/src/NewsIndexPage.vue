<template>
    <div id="news-index-menu">
        <news-categories></news-categories>
        <news-calendar></news-calendar>
    </div>
</template>
<script>
    import NewsCategories from './components/NewsCategories.vue';
    import NewsCalendar from './components/NewsCalendar.vue';

    export default {
        components: {
            'news-categories': NewsCategories,
            'news-calendar': NewsCalendar
        }
    }
</script>
<style>

</style>