import { createStore } from "vuex";
import { mapState, mapActions } from 'vuex';

import { authModule } from "./authModule";

export default createStore({
    modules: {
        auth: authModule,
    },
    state: {
        VisibleMenu: false,
    },
    actions: {
        setVisibleMenu({state}, data){
            state.visibleMenu = data
        }
    },
    methods: {
        ...mapActions({
            setVisibleMenu: 'setVisibleMenu'
        }),
        openMenu(){
            this.setVisibleMenu(true)
        },
        closeMenu(){
            this.setVisibleMenu(false)
        },
        setVisibleMenu(value) {
            this.isVisibleMenu = value
        },
    },

    // computed: {
    //     ...mapState({
    //         visibleMenu: state => state.visibleMenu
    //     })
    // }
    
})




    
    

// export default {
//     data() {
//         return{

//         }
//     }
// }
// }