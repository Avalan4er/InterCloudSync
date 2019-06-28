import Vue from "vue";
import Router from "vue-router";

import Vuetify from "vuetify";
import 'vuetify/dist/vuetify.min.css';
import AppHelloComponent from "./components/AppHello/AppHello.vue";

Vue.use(Vuetify);
Vue.use(Router);

let v = new Vue({
    el: "#app-root",
    template: '<AppHelloComponent />',
    //render: h => h(AppHelloComponent),
    components: {
        AppHelloComponent
    }
});