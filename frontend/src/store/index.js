import { createStore } from "vuex";
import { mapState, mapActions } from 'vuex';

import { authModule } from "./authModule";

export default createStore({
    modules: {
        auth: authModule,
    },
    state: {
        visibleMenu: true,
    },
    mutations: {
        toggleMenu(state) {
            state.visibleMenu = !state.visibleMenu
        },
    },
    actions: {},
})




    
    

// export default {
//     data() {
//         return{

//         }
//     }
// }
// }