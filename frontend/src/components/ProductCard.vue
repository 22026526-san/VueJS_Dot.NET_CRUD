<template>
  <div class="product-card">
    <div class="icon-badge-new badge-new"></div>

    <div class="card-header">
      <img :src="getLogoUrl(props.product.productLogo)" alt="Logo" class="brand-logo" />
      
      <h3 class="product-name">{{ t('productCard.name_tnds') }}</h3>
    </div>

    <div class="divider"></div>

    <div class="card-body">
      <div class="info-row">
        <span class="label">{{ t('productCard.price_from') }}</span>
        <span class="value price">
            {{ formatCurrency(props.product.minPrice) }} / {{ t('productCard.month') }}
        </span>
      </div>

      <div class="divider"></div>
      
      <div class="info-row">
        <span class="label">{{ t('productCard.coverage_upto') }}</span>
        <span class="value">{{ formatShortCurrency(props.product.maxCoverage) }}</span>
      </div>

      <div class="divider"></div>

      <div class="info-row last-row">
        <span class="label">{{ t('productCard.duration_upto') }}</span>
        <span class="value">{{ props.product.maxDuration }} {{ t('productCard.year') }}</span>
      </div>

      <div class="divider"></div>
    </div>

    <div class="card-footer">
      <div class="rating-area">
        <div class="stars">
          <span v-for="n in 5" :key="n" class="star" :class="{ filled: n <= 5 }">★</span>
        </div>
        <div class="icon-20 icon-dropDown"></div>
        <span class="rating-arrow">|</span>
        <span class="review-count">72 {{ t('productCard.reviews') }}</span>
      </div>
      <div class="sold-count">
        138 {{ t('productCard.sold_success') }}
      </div>
    </div>

    <div class="action-area">
      <a-button 
        type="primary" 
        class="btn-register" 
        @click="handleRegister"
      >
        {{ t('productCard.register_btn') }}
      </a-button>

      <a-button 
        type="link" 
        class="link-detail" 
      >
        {{ t('productCard.view_detail') }}
      </a-button>
    </div>
  </div>
</template>

<script setup>
import { useRouter } from 'vue-router';
import { formatCurrency, formatShortCurrency } from '../utils';
import { useI18n } from 'vue-i18n';
const { t } = useI18n();

const props = defineProps({
  product: {
    type: Object,
    required: false, 
    default: () => ({
      productId: '',
      productLogo: '',
      productName: '',
      minPrice: '',
      maxCoverage: '',
      maxDuration: ''
    })
  }
});

const router = useRouter();

const handleRegister = () => {
  const rawData = JSON.parse(JSON.stringify(props.product));

  router.push({ 
    name: 'insurance_order', 
    state: { 
      productData: rawData 
    } 
  });
};

const getLogoUrl = (logoName) => {
  return new URL(`../assets/img/logo${logoName}.png`, import.meta.url).href;
}
</script>

<style scoped>
.product-card {
  position: relative;
  background: white;
  border-radius: 8px;
  width: 255px; 
  padding: 50px 16px 16px 16px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.05);
  border: 1px solid;
}

.product-card:hover {
  transform: translateY(-2px);
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.1);
}

.badge-new {
  position: absolute;
  top: -7px;
  right: -11px; 
  z-index: 10;
}


.card-header {
  text-align: center;
  margin-bottom: 26px;
}
.brand-logo {
  height: 40px; 
  object-fit: contain;
}
.product-name {
  font-size: 16px;
  font-weight: 700;
  color: #333;
  margin: 0;
}

.card-body {
  padding: 0 10px;
}
.info-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  font-size: 13px;
}
.info-row.last-row {
  margin-bottom: 0;
}
.label {
  color: #666;
}
.value {
  font-weight: 700;
  color: #333;
}
.value.price {
  color: #00a68d; 
  font-size: 14px;
}

.card-footer {
  text-align: center;
  margin-bottom: 16px;
}
.rating-area {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 4px;
  margin-bottom: 4px;
  font-size: 13px;
}
.star {
  color: #ddd;
  font-size: 20px;
}
.stars{
  display: flex;
  align-items: center;
}
.star.filled {
  color: #fadb14; 
}
.rating-arrow {
  font-size: 10px;
  color: #999;
  cursor: pointer;
}
.review-count {
  color: #007bff; 
  cursor: pointer;
}
.sold-count {
  font-size: 12px;
  color: #999;
}

.action-area {
  text-align: center;
  display: flex;
  flex-direction: column;
  gap: 5px;
}
.btn-register {
  background-color: #39c5ab; 
  color: white;
  border: none;
  border-radius: 4px;
  padding: 10px 0;
  width: 100%;
  font-weight: 600;
  cursor: pointer;
  transition:  0.3s;
}
.btn-register:hover {
  background-color: #008f7a;
}
.link-detail {
  color: #39c5ab;
  font-size: 14px;
  text-decoration: none;
}
.link-detail:hover {
  text-decoration: underline;
}
</style>