<template>
    <div>
        <div>News Categories</div>
        <li><a class="dropdown-item" v-for="category in categories" :key="category.id">{{ category.name }}</a></li>
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
                this.$baseURL.get('api/news-article-category/GetAll')
                    .then(res => {
                        this.categories = res.data;
                        console.log("api/news-article-category/GetAll Success: " + res);
                    })
                    .catch(err => {
                        console.log("api/news-article-category/GetAll Error: " + err);
                    });
            }
        },
        created() {
            this.getAllCategories();
        }
    }
</script>