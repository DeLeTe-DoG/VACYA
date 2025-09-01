<script>
import PageHeader from '../components/layouts/PageHeader.vue';
import Sidebar from '../components/layouts/Sidebar.vue';

export default {}
</script>

<template class="Begin_body">
    <PageHeader />
    <main>
        <!-- <Sidebar class="sidebar" /> -->
    </main>
</template>

<style lang="scss" scoped>
    .Begin_body{
        display: flex;
        flex-direction: row;
    }
        .sidebar{
        width: 360px;
        height: 100%;
        background-color:rgb(255, 255, 255);
        position: fixed;
        align-self: start;
    }
</style>
