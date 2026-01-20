<template>
  <div class="sidebar-filter">
    <div class="fillter-container">
        <h3 class="filter-title">{{ t('sidebar.title') }}</h3>
    </div>

    <a-checkbox-group v-model:value="selectedInsurers" class="filter-group">
      <a-row :gutter="[0, 12]"> <a-col :span="24" v-for="item in insuranceOptions" :key="item.value">
          <a-checkbox :value="item.value">
            {{ item.label }}
          </a-checkbox>
        </a-col>

      </a-row>
    </a-checkbox-group>

    </div>
</template>

<script setup>
import { ref } from 'vue';
import { INSURANCE_PROVIDERS } from '../../common/constants/insurance.js';
import { useI18n } from 'vue-i18n';

const { t } = useI18n();

const insuranceOptions = INSURANCE_PROVIDERS;

const selectedInsurers = ref([]);

</script>

<style scoped>
.sidebar-filter {
    background-color: white;
    padding: 16px;
    border-radius: 8px; 
    width: 200px; 
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.05); 
}

.fillter-container{
    display: flex;
    align-items: center;
    justify-content: center;
}
.filter-title {
    font-size: 16px;
    font-weight: 600;
    color: #212121; 
    margin-bottom: 16px;
}

:deep(.ant-checkbox-wrapper) {
    font-size: 16px;
    color: #212121; 
}

</style>