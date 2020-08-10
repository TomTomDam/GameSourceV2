import Vue from 'vue';
import VueRouter from 'vue-router';
import AccountSettingsPage from '../AccountSettingsPage.vue';
import AccountSettings from '../components/AccountSettings.vue';
import EmailSettings from '../components/EmailSettings.vue';
import PrivacySettings from '../components/PrivacySettings.vue';

Vue.use(VueRouter);

export default new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes: [
        {
            path: '/account',
            name: 'account-settings',
            component: AccountSettingsPage,
            children: [
                {
                    path: 'account',
                    name: 'account',
                    component: AccountSettings
                },
                {
                    path: 'email',
                    name: 'email',
                    component: EmailSettings
                },
                {
                    path: 'privacy',
                    name: 'privacy',
                    component: PrivacySettings
                }
            ]
        }
    ]
});