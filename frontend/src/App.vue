<script setup>
import { computed,defineComponent } from 'vue';
import { useRoute } from 'vue-router';
import DefaultLayout from './layout/DefaultLayout.vue';

const route = useRoute();

const EmptyLayout = defineComponent({
  setup(_, { slots }) {
    return () => slots.default && slots.default();
  }
});

const layout = computed(() => {
  if (route.meta.layout) {
    return route.meta.layout;
  }
  
  if (route.meta.layout === null) {
    return EmptyLayout; 
  }

  return DefaultLayout;
});
</script>

<template>
  <component :is="layout">
    <router-view />
  </component>
</template>