<template>
  <div v-if="productInfo" class="container">
    <div class="insurance-card">
      <div class="insurance-logo-box">
        <img :src="getLogoUrl(productInfo.productLogo)" alt="BSH Logo">
      </div>
      <div class="insurance-content">
        <div class="insurance-title">{{ t('order.product_title') }}</div>
        <div class="insurance-subtitle">{{ productInfo.productName }}</div>
      </div>
    </div>

    <div class="left-column">
      <div class="vehicle-list-wrapper">
        <InsurancePopup 
            v-for="(car, index) in cars" 
            :key="car.id" 
            :index="index" 
            :data="car" 
            @remove="removeCar"
            @update:data="(newData) => updateCarData(index, newData)" 
        />
      </div>

      <div class="summary-container">
        <div class="summary-card">
          <h3 class="summary-title">{{ t('order.summary_title') }}</h3>

          <div class="summary-section">
            <div class="summary-label-main">{{ t('order.fee_label') }}</div>

            <div v-for="(item, index) in cars" :key="index" class="summary-row">
              <span class="item-name">
                  {{ t('order.car_item_name', { index: index + 1 }) }}
              </span>
              <span class="item-price">
                  {{ item.fee_order ? formatCurrency(item.fee_order) : '--' }} VND
              </span>
            </div>
          </div>

          <div class="divider"></div>

          <div class="summary-total">
            <div class="total-row">
              <span class="total-label">{{ t('order.total_payment') }}</span>
              <span class="total-price">
                  {{ totalPrice === 0 ? '--' : formatCurrency(totalPrice) }} VND
              </span>
            </div>
            <div class="vat-note">{{ t('order.vat_included') }}</div>
          </div>
        </div>

        <div class="action-buttons">
          <a-button type="primary" class="btn-submit" @click="handleSubmit">
            {{ t('order.btn_submit') }}
          </a-button>
          
          <a-button class="btn-cancel" @click="$router.back()">
            {{ t('order.btn_cancel') }}
          </a-button>
        </div>
      </div>
    </div>

    <div class="button-container">
      <a-button class="btn-add-car" @click="addCar">
        <div class="icon-20 icon-plus"></div>
        {{ t('order.btn_add_car') }}
      </a-button>
    </div>

  </div>
  <Loading v-if="isLoading" />
  <ToastMessage 
    v-model="toastState.show"
    :text="toastState.message"
    :isSuccess="toastState.success"
    :isError="toastState.error"
    :isWarning="toastState.warning"
  />
</template>

<script setup>
import { ref, onMounted, computed , reactive} from 'vue';
import InsurancePopup from './components/InsurancePopup.vue';
import { formatCurrency } from '../utils/index.js'
import ToastMessage from '../components/ToastMessage.vue';
import OrderService from '../api/services/orderService.js';
import { useRouter } from 'vue-router';
import Loading from '../components/Loading.vue';
import { useI18n } from 'vue-i18n';

const { t } = useI18n();
const isLoading = ref(false);
const productInfo = ref(null);
const router = useRouter();

const toastState = reactive({
  show: false,
  message: '',
  success: false,
  error: false,
  warning: false
});


const triggerToast = (msg, type = 'success') => {
  toastState.success = false;
  toastState.error = false;
  toastState.warning = false;


  toastState.message = msg;
  toastState[type] = true; 
  toastState.show = true;
};

onMounted(() => {
  const state = history.state;

  if (state && state.productData) {
    productInfo.value = state.productData;
  }
});

const getLogoUrl = (logoName) => {
  return new URL(`../assets/img/logo${logoName}.png`, import.meta.url).href;
}

const cars = ref([
  { fee_order: null }
]);


const addCar = () => {
  if (cars.value.length < 3) {
    cars.value.push({
      fee_order: null
    });
  } else {
    triggerToast('Thêm xe thất bại. Chỉ tối đa 3 xe cho mỗi đơn bảo hiểm!', 'error');
    return;
  }
};

const removeCar = (index) => {
  cars.value.splice(index, 1);
};

const updateCarData = (index, newData) => {
  cars.value[index] = { ...cars.value[index], ...newData };
  console.log('Dữ liệu', cars.value);
};

const handleSubmit = async () => {
  const dataLength = cars.value.length;
  for (let i = 0; i < dataLength; i++) {
    if (cars.value[i].fee_order === null) {
      triggerToast(`Vui lòng hoàn thành thông tin xe số ${i + 1} trước khi gửi đơn!`, 'error');
      return;
    }
  }
  try {
    isLoading.value = true;

    const apiPayload = {

      productId: productInfo.value.productId,

      totalAmount: totalPrice.value,

      cars: cars.value.map((car) => ({
        usagePurpose: car.usage_purpose,
        vehicleType: car.vehicle_type,
        seatCount: Number(car.seat_count),
        payloadCapacity: Number(car.payload_capacity),
        durationYear: Number(car.duration_year),
        startDate: car.start_date,
        endDate: car.end_date,
        feeOrder: car.fee_order,
        chassisNumber: car.chassis_number,
        engineNumber: car.engine_number,
        licensePlate: car.license_plate,
        hasLicensePlate: car.has_license_plate,
        registrationImageUrl: car.registration_image_url,
        manufacturer: car.manufacturer,
        model: car.model
      }))
    };

    const response = await OrderService.create(apiPayload);

    router.push({ 
      name: 'insurance_order_detail', 
      params: { 
        orderId: response.orderId
      } 
    });

  } catch (error) {
    console.error("Lỗi khi tạo đơn bảo hiểm:", error);
    triggerToast('Đăng ký bảo hiểm thất bại. Đã có lỗi xảy ra trong quá trình gửi đơn!', 'error');
  } finally {
    isLoading.value = false;
  }
}
const totalPrice = computed(() => {
  return cars.value.reduce((sum, item) => sum + (item.fee_order || 0), 0);
});
</script>

<style scoped>
.container {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
}

.insurance-card {
  display: flex;
  align-items: center;
  background-color: #cee4f58a;
  padding: 12px 16px;
  border-radius: 6px;
  width: 100%;
  margin-bottom: 16px;
}

.insurance-logo-box {
  width: 40px;
  height: 40px;
  background-color: white;
  border-radius: 4px;
  border: 1px solid;
  display: flex;
  align-items: center;
  justify-content: center;
  margin-right: 12px;
  flex-shrink: 0;
}

.insurance-logo-box img {
  max-width: 80%;
  max-height: 80%;
  object-fit: contain;
}

.insurance-content {
  display: flex;
  flex-direction: column;
}

.insurance-title {
  font-size: 16px;
  font-weight: 700;
  color: #2c3e50;
  margin-bottom: 2px;
}

.insurance-subtitle {
  font-size: 13px;
  color: #7f8c8d;
}

.button-container {
  width: 100%;
  display: flex;
  justify-content: flex-start;
  margin-top: 16px;
}

.btn-add-car {
  color: #009c95;
  border: 1px solid #009c95;
  background-color: #ffffff;
  width: 200px;
  font-weight: 500;
  font-size: 14px;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 8px;
  transition: all 0.3s;
}

.btn-add-car:hover {
  color: #218c7e;
  border-color: #218c7e;
  background-color: #e6fffa;
}

.left-column {
  height: 100%;
  overflow-y: auto;
  display: flex;
  gap: 22px;
}

.vehicle-list-wrapper {
  flex: 1;
  overflow-y: auto;
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.summary-card {
  background: #fff;
  border: 1px solid #e8e8e8;
  border-radius: 8px;
  padding: 20px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.05);
  width: 320px;
  min-height: 200px;
}

.summary-title {
  font-size: 16px;
  font-weight: 700;
  color: #1f2937;
  margin-bottom: 16px;
}

.summary-label-main {
  font-weight: 600;
  color: #374151;
  margin-bottom: 8px;
  font-size: 14px;
}

.summary-row {
  display: flex;
  justify-content: space-between;
  margin-bottom: 8px;
  font-size: 14px;
  color: #4b5563;
}

.divider {
  height: 1px;
  background-color: #e5e7eb;
  margin: 16px 0;
}

.total-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.total-label {
  font-size: 14px;
  font-weight: 700;
  color: #1f2937;
}

.total-price {
  font-size: 14px;
  font-weight: 700;
  color: #00b5ad;
}

.vat-note {
  font-size: 12px;
  color: #9ca3af;
  margin-top: 2px;
}

.action-buttons {
  margin-top: 12px;
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.btn-submit {
  background-color: #009c95 !important;
  border-color: #009c95 !important;
  height: 40px;
  font-weight: 500;
  border-radius: 4px;
}

.btn-submit:hover {
  background-color: #00857f !important;
}

.btn-cancel {
  background-color: #e5e7eb;
  border-color: #f3f4f6;
  color: #374151;
  height: 40px;
  font-weight: 500;
  border-radius: 4px;
}

.icon-plus {
  background-color: #009c95;
}

.item-name {
  font-size: 14px;
}

.item-price {
  font-size: 14px;
}
</style>