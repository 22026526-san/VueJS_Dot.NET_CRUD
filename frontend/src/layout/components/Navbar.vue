<template>
  <div class="navbar-wrapper">
    <div class="nav-container">
      <div 
        v-for="tab in tabs" 
        :key="tab.id"
        class="nav-item"
        :class="{ 'active': currentTab === tab.id }"
        @click="selectTab(tab.id)"
      >
        {{ tab.label }}
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue';
import { INSURANCE_TABS } from '../../common/constants/insurance.js';
import { INSURANCE_TYPE_ENUM } from '../../common/enums/enum.js';
import { useI18n } from 'vue-i18n';

const currentTab = ref(INSURANCE_TYPE_ENUM.CAR);
const { t } = useI18n();

const tabs = computed(() => {
  return INSURANCE_TABS.map(tab => ({
    id: tab.id,
    label: t(tab.label)
  }));
});

const selectTab = (id) => {
  currentTab.value = id;
}
</script>

<style scoped>

.navbar-wrapper {
  display: flex;
  margin:16px 0;
}

.nav-container {
  background-color: white;
  display: flex; 
  padding: 4px;
  border-radius: 99px; 
  box-shadow: 0 2px 8px rgba(0,0,0,0.05); 
}

.nav-item {
  padding: 3px 16px;
  border-radius: 99px; 
  cursor: pointer;
  
  font-size: 15px;
  font-weight: 500;
  color: #999999; 
  
  transition: all 0.3s ease; 
  user-select: none; 
}

.nav-item:hover:not(.active) {
  color: #666;
  background-color: #fFF;
}

.nav-item.active {
  background-color: #39c5ab; 
  color: white;
  font-weight: 600;
  box-shadow: 0 2px 4px rgba(0, 166, 141, 0.3); 
}
</style>