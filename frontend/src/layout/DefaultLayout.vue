<template>
    <div class="layout-container">
        <div class="head_nav">
            <Header></Header>
            <Navbar></Navbar>
        </div>
        <div class="side_content">
            <div class="sidebar">
                <Sidebar></Sidebar>
            </div>
            <div class="content">
                <slot></slot>
            </div>
        </div>
    </div>
</template>

<script setup>
import Header from './components/Header.vue';
import Navbar from './components/Navbar.vue';
import Sidebar from './components/Sidebar.vue';
</script>

<style scoped>
.layout-container {
    width: 100%;
    height: 100%;
    overflow: hidden;
}
.head_nav{
    height: 108px;
    width: 100%;
}
.side_content{
    width: 100%;
    height: calc(100% - 108px);
    display: flex;
    gap: 22px;
}
.sidebar{
    width: 200px;
    height: 100%;
}
.content{
    width: calc(100% - 200px);
    height: 100%;
    overflow-y: auto;
    overflow-x: hidden;
}
</style>