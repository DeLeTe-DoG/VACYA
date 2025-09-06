<script>
import { mapState } from 'vuex';

    export default{
        computed: {
        ...mapState({
            visibleMenu: state => state.visibleMenu
            })
        }
    }
</script>

<template class="sideb" >
    <div class="sidebar" v-if="visibleMenu">
        <p>penis</p>
    </div>
</template>

<style lang="scss" scoped>
    .sidebar{
        width: 360px;
        height: 100px;
        background-color:rgb(255, 255, 255);
        position: fixed;
        align-self: start;
    }
</style>