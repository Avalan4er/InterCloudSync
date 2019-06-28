import Vue from "vue";
import HelloComponent from "../Hello/Hello.vue";

export default Vue.extend({
    data() {
        return {
            name: "World"
        }
    },
    components: {
        HelloComponent
    }
});