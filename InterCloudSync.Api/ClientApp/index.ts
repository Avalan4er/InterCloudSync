import Vue from "vue";
import AppHelloComponent from "./components/AppHello/AppHello.vue";

let v = new Vue({
    el: "#app-root",
    template: '<AppHelloComponent />',
    //render: h => h(AppHelloComponent),
    components: {
        AppHelloComponent
    }
});