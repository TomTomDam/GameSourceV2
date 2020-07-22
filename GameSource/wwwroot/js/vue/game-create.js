Vue.component('vue-multiselect', window.VueMultiselect.default);

//let app = new Vue({
//    components: {
//        Multiselect: window.VueMultiselect.default
//    },
//    el: "#app",
//    data: {
//        gameId: document.getElementById('gameId').value,
//        model: null,
//        genresmultiselect: [],
//        isLoadingGenres: false,
//    },
//    mounted() {
//        axios.get(`/Games/${this.gameId}`)
//            .then(response => {
//                this.model = response.data
//            })
//            .catch(error => {
//                console.log("Error: " + error.statusText);
//            });
//    },
//    methods: {
//        genreSearch: function (filter) {
//            axios.get(`/Genre/?filter=${encodeURIComponent(filter)}`)
//                .then(response => {
//                    this.genresmultiselect = response.data;
//                })
//                .catch(error => {
//                    console.log("Error: " + error.statusText);
//                });
//        }
//    }
//});

let app = new Vue({
    components: {
        Multiselect: window.VueMultiselect.default
    },
    el: "#app",
    data() {
        return {
            value: [
                { name: 'Javascript', code: 'js' }
            ],
            options: [
                { name: 'Vue.js', code: 'vu' },
                { name: 'Javascript', code: 'js' },
                { name: 'Open Source', code: 'os' }
            ]
        }
    },
    methods: {
        addTag(newTag) {
            const tag = {
                name: newTag,
                code: newTag.substring(0, 2) + Math.floor((Math.random() * 10000000))
            }
            this.options.push(tag)
            this.value.push(tag)
        }
    }
});