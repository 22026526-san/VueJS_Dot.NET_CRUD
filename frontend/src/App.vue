<script setup>
import { computed } from 'vue';
import { useRoute } from 'vue-router';
import DefaultLayout from './layout/DefaultLayout.vue';

const route = useRoute();

const layout = computed(() => {
  if (route.meta.layout) {
    return route.meta.layout;
  }
  
  if (route.meta.layout === null) {
    return 'div'; 
  }

  return DefaultLayout;
});
</script>

<template>
  <component :is="layout">
    <router-view />
  </component>
</template>