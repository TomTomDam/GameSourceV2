﻿import Vue from '../../lib/vue.js'
import App from './App.vue'
import router from './router'

Vue.config.productionTip = false;

new Vue({
    el: '#app',
    router,
    template: '</App>',
    components: { App },
})