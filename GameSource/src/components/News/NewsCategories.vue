<template>
    <div>
        <div>News Categories</div>
        <div v-for="category in categories" :key="category.id">{{ category.name }}</div>
    </div>
</template>
<script>
    export default {
        data: function () {
            return {
                categories: []
            };
        },
        methods: {
            getAllCategories() {
                this.$api.get('news-article-category/GetAllAsync')
                    .then(res => {
                        this.categories = res.data.data;
                        console.log("api/news-article-category/GetAllAsync Success: " + res);
                    })
                    .catch(err => {
                        console.log("api/news-article-category/GetAllAsync Error: " + err);
                    });
            }
        },
        created() {
            this.getAllCategories();
        }
    }
</script>