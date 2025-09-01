<template>
    <div>
        <h1>Home page!</h1>
        {{ userInfo }}
        <main-button></main-button> <!--пример использования компонента, он зарегестрирован в components/UI/index.js-->
    </div>
    <div class="btn">
        <router-link to="/test">Go to test backend</router-link>
        <router-link to="/begin">Go to test HeaderMenu</router-link>
    </div>
    <div class="btn">
        <router-link to="/register">Go to Reg</router-link>
        <router-link to="/login">Go to Log</router-link>
    </div>
</template>

<script>
    export default {
        data() {
            return{
                userInfo: localStorage.getItem('user')
            }
        }
    }
</script>

<style lang="scss" scoped>
.btn{
    display: flex;
    flex-direction: column;
    
}
</style>