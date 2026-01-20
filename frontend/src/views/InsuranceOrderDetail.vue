<template>
  <div class="checkout-container">
    <header class="header">
      <div class="header-left">
        <div class="icon-btn" @click="$router.back()">
            <div class="icon-24 icon-back"></div>
        </div>
        <h1 class="title">{{ t('order_detail.page_title') }}</h1>
        
        <span :class="getStatusClass(orderDetail.status)" v-if="orderDetail">
            {{ getStatusLabel(orderDetail.status) }}
        </span>
      </div>
      <div class="header-right">
        <span class="provider-label">{{ t('order_detail.provider_label') }}</span>
        <div class="card-header" v-if="orderDetail">
            <img :src="getLogoUrl(orderDetail.productLogo)" alt="Logo" class="brand-logo" />
        </div>
      </div>
    </header>

    <main class="main-content">
      
      <section class="status-section">
        <div class="status-card">
          <div class="img-wrapper">
            <img src="../assets/img/time.png" alt="Time" class="time_img" />
          </div>
          <p v-if="orderDetail" class="status-title">{{ getStatusInfo(orderDetail.status).title }}</p>
          <p v-if="orderDetail" class="status-subtitle">{{ getStatusInfo(orderDetail.status).subtitle }}</p>
        </div>
      </section>

      <aside class="summary-section" v-if="orderDetail">
        <div class="summary-header">
          <h3>{{ t('order_detail.summary_title') }}</h3>
          <span class="order-id">{{ orderDetail.orderCode }}</span>
        </div>

        <div class="order-item">
          <div v-for="(item, index) in orderDetail.cars" :key="index" class="order-item">
            <div class="item-header">
              <div class="item-index">{{ index + 1 }}</div>
              
              <span class="item-name">{{ item.model }} - {{ item.manufacturer }}</span>
            </div>
            
            <div class="item-detail">
              <span class="label">{{ t('order_detail.license_plate') }}</span>
              <span class="value">{{ item.licensePlate }}</span>
            </div>
            
            <div class="item-detail">
              <div class="label-container">
                  <span class="label">{{ t('order_detail.duration') }}</span> 
                  <div class="icon-20 icon-info"></div>
              </div>
              <span class="value">{{ formatDate(item.startDate) }} - {{ formatDate(item.endDate) }}</span>
            </div>
            
            <div class="item-detail">
              <span class="label">{{ t('order_detail.fee') }}</span>
              <span class="value">{{ formatCurrency(item.feeOrder) }} VND</span>
            </div>
          </div>
        </div>

        <div class="divider"></div>

        <div class="total-section">
          <span class="total-label">{{ t('order_detail.total_payment') }}</span>
          <span class="total-value">{{ formatCurrency(orderDetail.totalAmount) }} VND</span>
        </div>
      </aside>
    </main>
  </div>
  <Loading v-if="isLoading" />
</template>

<script setup>
import { ref,onMounted,watch} from 'vue';
import { formatCurrency, formatDate } from '../utils/index.js';
import { useRoute } from 'vue-router';
import orderService from '../api/services/orderService';
import Loading from '../components/Loading.vue';
import { useI18n } from 'vue-i18n';
import { ORDER_STATUS_ENUM } from '../common/enums/enum.js';

const { t } = useI18n();
const route = useRoute();
const orderDetail = ref(null);
const isLoading = ref(false);

const fetchOrderDetail = async () => {
  try {
    isLoading.value = true;
    
    const Id = route.params.orderId; 
    console.log("Fetching details for orderId:", Id);
    const response = await orderService.getById(Id);
    console.log("Response from orderService:");
    orderDetail.value = response;
    console.log("Chi tiết đơn hàng:", orderDetail.value);

  } catch (error) {
    console.error("Lỗi lấy chi tiết đơn hàng:", error);
  } finally {
    isLoading.value = false;
  }
};
onMounted(() => {
    fetchOrderDetail();
});

watch(
    () => [route.params.orderId], 
    () => {
        fetchOrderDetail();
    }
);

const getLogoUrl = (logoName) => {
  return new URL(`../assets/img/logo${logoName}.png`, import.meta.url).href;
};
const getStatusLabel = (status) => {
    if (status === ORDER_STATUS_ENUM.PENDING) return t('notification.status.pending');
    if (status === ORDER_STATUS_ENUM.FAILED) return t('notification.status.failed');
    return t('notification.status.completed'); 
};

const getStatusClass = (status) => {
    if (status === ORDER_STATUS_ENUM.PENDING) return 'status-pending';
    if (status === ORDER_STATUS_ENUM.FAILED) return 'status-failed';
    return 'status-completed';
};
const getStatusInfo = (status) => {
    const statusMap = {
        [ORDER_STATUS_ENUM.PENDING]: 'pending',
        [ORDER_STATUS_ENUM.FAILED]: 'failed',
        [ORDER_STATUS_ENUM.COMPLETED]: 'completed', 
    };

    const key = statusMap[status] ; 

    return {
        title: t(`order_detail.order_status.${key}.title`),
        subtitle: t(`order_detail.order_status.${key}.subtitle`)
    };
};
</script>

<style scoped>

.checkout-container {
  min-height: 100%;
  display: flex;
  flex-direction: column;
}

/* Header Styles */
.header {
  padding: 16px 0;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.header-left, .header-right {
  display: flex;
  align-items: center;
  gap: 1rem;
}

.title {
  font-size: 1.125rem;
  font-weight: 700;
  color: #1f2937;
  margin: 0;
}

.status-pending {
    background-color: #ffeeda;
    color: #f97316e6;
    padding: 6px 12px;
    border-radius: 6px;
    font-size: 0.875rem;
    font-weight: 500;
}
.status-failed {
    background-color: #ffdada;
    color: #f91616e6;
    padding: 6px 12px;
    border-radius: 6px;
    font-size: 0.875rem;
    font-weight: 500;
}
.status-completed {
    background-color: #e3ffda;
    color: #40f916e6;
    padding: 6px 12px;
    border-radius: 6px;
    font-size: 0.875rem;
    font-weight: 500;
}

.provider-label {
  color: #212121;
    font-size: 15px;
}


.provider-logo {
  font-weight: bold;
  color: #f97316;
  border: 1px solid #e5e7eb;
  padding: 4px 8px;
  border-radius: 4px;
}

/* Main Layout */
.main-content {
    display: flex;
    padding: 16px 68px;
    gap: 32px;
    width: 100%;
}

.status-section {
  flex: 2; 
}

.summary-section {
  flex: 1; 
  background: white;
  padding: 1.5rem;
  border-radius: 8px;
  box-shadow: 0 1px 3px rgba(0,0,0,0.1);
  height: fit-content;
}

.status-card {
    background: white;
    border-radius: 8px;
    padding: 16px 8px;
    text-align: center;
    box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
}

.img-wrapper {
  margin-bottom: 22px;
}

.time_img {
  height: 100px; 
  object-fit: contain;
}

.card-header {
    text-align: center;
    background: #fff;
    padding: 4px 12px;
}

.brand-logo {
  height: 26px; 
  object-fit: contain;
}

.status-title {
  font-size: 15px;
  color: #212121;
  margin-bottom: 0;
}

.status-subtitle {
  color: #212121;
  font-size: 15px;
}

.summary-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1.5rem;
}

.summary-header h3 {
    font-size: 18px;
    font-weight: 700;
    color: #1f2937;
    margin: 0;
}

.order-id {
  color: #6b7280;
  font-size: 0.875rem;
}

.order-item {
  margin-bottom: 24px;
}

.item-header {
  display: flex;
  gap: 0.75rem;
  margin-bottom: 0.75rem;
  align-items: flex-start;
}

.item-index {
    background-color: white;
    border: 2px solid #10b981;
    color: #10b981;
    width: 24px;
    height: 24px;
    border-radius: 36%;
    display: flex;
    align-items: center;
    justify-content: center;
    font-size: 13px;
    font-weight: bold;
}

.item-name {
  font-weight: 600;
  color: #1f2937;
  font-size: 0.95rem;
}

.item-detail {
  display: flex;
  justify-content: space-between;
  margin-bottom: 0.5rem;
  font-size: 0.875rem;
}
.label-container{
    display: flex;
    align-items: center;
    gap: 4px;
}
.label {
  color: #6b7280;
  display: inline;
}
.icon-info{
    background-color: #868686a8;
}

.value {
  font-weight: 500;
  color: #1f2937;
}

.info-icon {
  font-size: 0.75rem;
  cursor: pointer;
  margin-left: 2px;
}

.divider {
  height: 1px;
  background-color: #e5e7eb;
  margin: 1.5rem 0;
}

.total-section {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.total-label {
  font-weight: 700;
  color: #1f2937;
}

.total-value {
  font-weight: 700;
  color: #10b981; 
  font-size: 1.125rem;
}

@media (max-width: 768px) {
  .main-content {
    flex-direction: column;
    padding: 1rem;
  }
  
  .header {
    flex-direction: column;
    align-items: flex-start;
    gap: 1rem;
  }
  
  .header-right {
    width: 100%;
    justify-content: space-between;
  }
}
</style>