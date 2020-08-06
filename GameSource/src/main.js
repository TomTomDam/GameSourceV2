//Registered modules
import Vue from 'vue';
import axios from 'axios';
import router from './router/router.js';
//Account Settings
import AccountSettingsPage from './AccountSettingsPage.vue'
import AccountSettings from './components/AccountSettings.vue'
import EmailSettings from './components/EmailSettings.vue'
import PrivacySettings from './components/PrivacySettings.vue'
//Browse Games
import BrowseGamesPage from './BrowseGamesPage.vue'
import BrowseGamesByGenre from './components/BrowseGamesByGenre.vue'
import BrowseGamesByPlatform from './components/BrowseGamesByPlatform.vue'
//Game Grid
//import GameCard from './components/GameCard.vue'
//import GamesCardGrid from './components/GamesCardGrid.vue'

Vue.config.productionTip = false;

//Axios
const api = axios.create({
    baseURL: 'http://localhost:52817/api/'
});

const axiosPlugin = {
    install(Vue) {
        Vue.prototype.$api = api;
    }
}

Vue.use(axiosPlugin);

//Vue Instances
new Vue({
    el: '#account-settings-page',
    components: {
        AccountSettings,
        EmailSettings,
        PrivacySettings
    },
    router,
    render: h => h(AccountSettingsPage)
});

new Vue({
    el: '#browse-games-page',
    components: {
        BrowseGamesByGenre,
        BrowseGamesByPlatform
    },
    render: h => h(BrowseGamesPage)
});

window.Vue = Vue;