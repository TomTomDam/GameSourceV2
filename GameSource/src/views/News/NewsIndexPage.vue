<template>
    <div id="news-index-menu">
        <news-categories></news-categories>
        <news-calendar></news-calendar>
    </div>
</template>
<script>
    import NewsCategories from '../../components/News/NewsCategories.vue';
    import NewsCalendar from '../../components/News/NewsCalendar.vue';

    export default {
        components: {
            'news-categories': NewsCategories,
            'news-calendar': NewsCalendar
        }
    }
</script>
<style>

</style>