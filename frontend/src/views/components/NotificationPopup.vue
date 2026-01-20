<template>
    <Teleport to="body">
        <div class="popup-container">
            <div class="popup">
                <div class="popup-header">
                    <h3 class="title">{{ t('notification.title') }}</h3>
                    <div class="icon-btn" @click="closePopup">
                        <div class="icon-20 icon-X"></div>
                    </div>
                </div>
                <div class="popup-body">

                    <div v-if="!orders || orders.length === 0" class="state-text">
                        {{ t('notification.empty') }}
                    </div>

                    <div v-else class="notification-list">
                        <div v-for="(order, index) in orders" :key="index" class="notif-item">
                            <div class="notif-icon">
                                <div class="bell-circle">🔔</div>
                            </div>
                            <div class="notif-content">
                                <div class="notif-title">
                                    {{ order.orderCode }}
                                </div>
                                <div class="notif-desc">
                                    {{ t('notification.total_amount') }}: {{ formatCurrency(order.totalAmount) }} VND<br>
                                    
                                    {{ t('notification.status_label') }}: 
                                    <span :class="getStatusClass(order.status)">
                                        {{ getStatusLabel(order.status) }}
                                    </span>
                                </div>
                                <div class="notif-time">{{ formatDate(order.orderDate) }}</div>
                            </div>
                            <div class="notif-action">
                                <a @click.prevent="goToDetail(order.orderId)" class="link-detail">
                                    {{ t('notification.view_detail') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </Teleport>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import orderService from '../../api/services/orderService';
import { useRouter } from 'vue-router';
import { formatCurrency, formatDate } from '../../utils/index.js';
import { useI18n } from 'vue-i18n';
import { ORDER_STATUS_ENUM } from '../../common/enums/enum.js';

const { t } = useI18n();
const emit = defineEmits(['close']);
const router = useRouter();
const orders = ref(null);

const fetchOrders = async () => {
  try {
    const response = await orderService.getAll(); 
    orders.value = response; 

  } catch (error) {
    console.error("Lỗi khi lấy danh sách sản phẩm:", error);
  } 
};

onMounted(() => {
  fetchOrders();
});

const closePopup = () => {
    emit('close');
};

const goToDetail = (Id) => {
    closePopup(); 
    router.push({ 
        name: 'insurance_order_detail', 
        params: { orderId: Id } 
    });
};
const getStatusLabel = (status) => {
    if (status === ORDER_STATUS_ENUM.PENDING) return t('notification.status.pending');
    if (status === ORDER_STATUS_ENUM.FAILED) return t('notification.status.failed');
    return t('notification.status.completed'); 
};

// --- LOGIC CLASS MÀU SẮC (Giữ nguyên) ---
const getStatusClass = (status) => {
    if (status === ORDER_STATUS_ENUM.PENDING) return 'status-pending';
    if (status === ORDER_STATUS_ENUM.FAILED) return 'status-failed';
    return 'status-completed';
};
</script>

<style scoped>
.popup-container {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: rgba(0, 0, 0, 0.5);
    z-index: 9999;
    display: flex;
    justify-content: flex-end;
    opacity: 1;
}

.popup {
    background: white;
    border-radius: 4px;
    width: 426px;
    display: flex;
    flex-direction: column;
    box-shadow: 0 4px 20px rgba(0, 0, 0, 0.15);
    font-family: 'Inter', Helvetica, Arial, sans-serif;
}
.popup-header {
    padding: 16px 24px;
    border-bottom: 1px solid #f0f0f0;
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.title{
    color: #212121;
    font-size: 18px;
    font-weight: 600;
    margin: 0;
}

.popup-body {
    padding: 0; 
    overflow-y: auto; 
    flex: 1; 
    background: #f9f9f9;
}

.state-text {
    padding: 40px;
    text-align: center;
    color: #888;
}

.notification-list {
    display: flex;
    flex-direction: column;
}

.notif-item {
    display: flex;
    align-items: flex-start;
    padding: 16px 24px;
    background: white;
    border-bottom: 1px solid #eee;
}

.notif-item:hover {
    background: #f0f7ff; /* Hover màu xanh nhạt */
}

.notif-icon {
    margin-right: 16px;
    padding-top: 4px;
}

.bell-circle {
    width: 36px;
    height: 36px;
    background: #e6f7ff;
    border-radius: 50%;
    display: flex;
    align-items: center;
    justify-content: center;
    font-size: 16px;
}

.notif-content {
    flex: 1;
}

.notif-title {
    font-weight: 600;
    font-size: 15px;
    color: #1f1f1f;
    margin-bottom: 4px;
}

.notif-desc {
    font-size: 13px;
    color: #666;
    line-height: 1.5;
    margin-bottom: 8px;
}

.status-failed {
    color: #ff4d4f;
}
.status-pending {
    color: #faad14;
}
.status-completed {
    color: #18de1f;
}

.notif-time {
    font-size: 12px;
    color: #999;
}

.notif-action {
    margin-left: 12px;
    display: flex;
    align-items: center;
    height: 100%;
}

.link-detail {
    font-size: 13px;
    color: #1890ff;
    cursor: pointer;
    white-space: nowrap;
    font-weight: 500;
}

.link-detail:hover {
    text-decoration: underline;
}

.icon-btn {
    cursor: pointer;
}
</style>