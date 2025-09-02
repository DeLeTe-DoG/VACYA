<template>
    <div>
        <h1>Home page!</h1>
        {{ userData }}
        <main-button></main-button> <!--пример использования компонента, он зарегестрирован в components/UI/index.js-->
    </div>
    <div class="btn">
        <router-link to="/test">Go to test backend</router-link>
        <router-link to="/begin">Go to test HeaderMenu</router-link>
    </div>
    <div class="btn">
        <router-link to="/auth">Go to Log</router-link>
    </div>
</template>

<script>
import { mapState } from 'vuex/dist/vuex.cjs.js';

    export default {
        data() {
            return{
                // userInfo: localStorage.getItem('user')
            }
        },
        computed: {
            ...mapState({
                userData: state => state.auth.userData
            })
        },
    }
</script>

<style lang="scss" scoped>
.btn{
    display: flex;
    flex-direction: column;
    
}
</style>