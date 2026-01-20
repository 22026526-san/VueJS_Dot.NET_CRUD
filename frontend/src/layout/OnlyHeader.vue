<template>
    <div class="layout-container">
        <div class="head_nav">
            <Header></Header>
        </div>
        
        <div class="content">
            <slot></slot>
        </div>
    </div>
</template>

<script setup>
import Header from './components/Header.vue';

</script>

<style scoped>
.layout-container {
    width: 100%;
    height: 100%;
    overflow: hidden;
}
.head_nav{
    height: 40px;
    width: 100%;
}
.content{
    width: 100%;
    height: calc(100% - 40px);;
    overflow-y: auto;
    overflow-x: hidden;
}
</style>