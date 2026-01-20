<template>
  <h3 class="title">
    {{ t('home_page.title') }}
  </h3>
  
  <div class="nav-option">
    <div class="label_container active">
      <div class="icon-TNDS"></div>
      <p>{{ t('home_page.type_tnds') }}</p>
    </div>
    
    <div class="label_container">
      <div class="icon-VCX"></div>
      <p>{{ t('home_page.type_vcx') }}</p>
    </div>
  </div>

  <div class="product_list">
    <ProductCard 
      v-for="item in productList" 
      :key="item.id" 
      :product="item" 
    />
  </div>
</template>

<script setup>
import ProductCard from '../components/ProductCard.vue';
import productService from '../api/services/productService';
import { ref ,onMounted} from 'vue';
import { useI18n } from 'vue-i18n';

const { t } = useI18n();
const productList = ref(null);

const fetchProducts = async () => {
  try {
    const response = await productService.getAll(); 
    productList.value = response; 

  } catch (error) {
    console.error("Lỗi khi lấy danh sách sản phẩm:", error);
  } 
};

onMounted(() => {
  fetchProducts();
});
</script>

<style scoped>
.product_list{
    display: flex;
    gap: 22px;
    flex-wrap: wrap; 
    justify-content: flex-start;
}
.title{
    font-size: 16px;
    font-weight: 600;
    color: #212121; 
}
.nav-option{
    display: flex;
    gap: 22px;
    align-items: center;
    margin-bottom: 16px;
}
.label_container{
    display: flex;
    align-items: center;
    gap: 5px;
    border-radius:20px ;
    background-color: #fff;
    padding: 20px 16px;
    height: 36px;
}

.label_container p{
    font-size: 15px;
    color: #212121;
    margin-bottom: 0;
}

.label_container.active{
    background-color: #39c5ab;
}
.label_container.active p{
    color: #fff;
}
.label_container.active .icon-TNDS, 
.label_container.active .icon-VCX{
    filter: brightness(0) invert(1);
}
</style>