<script>

</script>

<template>
    <header class="header">
    <div class="side">
        <a class="btn2"> </a> 
        <img class="btn" src="/public/icons/поиск.png"/>
    </div>
    <div class="side" >
        <img class="btn" src="/public/icons/уведомления.png"/> 
        <img class="btn" src="/public/icons/настройки.png"/> 
        <div class="btn1">А</div>
    </div>
    </header>
</template>

<style lang="scss" scoped>
    .header{
        height: 108px;
        background-color: rgb(255, 255, 255);
        margin: 0;
        padding: 0;
        box-sizing: border-box;
        filter: drop-shadow(0px 4px 4px rgba(0, 0, 0, 0.25) );
        display: flex;
        flex-direction: row;
        align-items: center;
        justify-content: space-between;
    }
    .rightside{
        display: flex;
        align-items: center;
        margin-right: 25px;
        gap: 25px;
    }
    .side{
        display: flex;
        align-items: center;
        margin-inline: 25px;
        gap: 25px;
    }
    .btn1{
        width: 56px;
        height: 56px;
        cursor: pointer;
        background-color: rgba(65, 140, 255, 0.2);
        border-radius: 100px;
        display: flex;
        justify-content: center;
        align-items: center;
        font-size: 25px;
    }
    .btn{
        width: 36px;
        height: 36px;
        cursor: pointer;
    }
    .btn2{
        width: 36px;
        height: 36px;
        cursor: pointer;
        background-image: url(/public/icons/меню.png)
    }
    
    :hover.btn2{
        background-image: url(/public/icons/актив_меню.png);
        transition:cubic-bezier(1, 0, 0, 1), 300ms;
    }
</style>