<template>
    <div>
        <h1>Home page!</h1>
        <main-button></main-button> <!--пример использования компонента, он зарегестрирован в components/UI/index.js-->
    </div>
    <div class="btn">
        <router-link to="/test">Go to test</router-link>
        <router-link to="/begin">Go to test</router-link>
    </div>
</template>

<script>
    export default {}
</script>

<style lang="scss" scoped>
.btn{
    display: flex;
    flex-direction: column;
    
}
</style>