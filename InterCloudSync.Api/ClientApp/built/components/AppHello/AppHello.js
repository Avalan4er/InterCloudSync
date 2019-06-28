import Vue from "vue";
import HelloComponent from "../Hello/Hello.vue";
export default Vue.extend({
    data: function () {
        return {
            name: "World"
        };
    },
    components: {
        HelloComponent: HelloComponent
    }
});
//# sourceMappingURL=AppHello.js.map