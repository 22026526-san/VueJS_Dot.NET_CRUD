<template>
    <div class="header_container">
        <div class="header_content">
            <div class="header_content_left">
                <div class="logo_container" @click="$router.push({ name: 'home' })">
                    <div class="icon-logo"></div>
                </div>
                <div class="title">{{ $t('header.appName') }}</div>
                <div class="contact">{{ $t('header.hotline') }}</div>
            </div>
            <div class="header_content_right">
                <div class="label_container">
                    <p>{{ $t('header.menu.insuranceProducts') }}</p>
                </div>
                <div class="label_container">
                    <p>{{ $t('header.menu.profileManagement') }}</p>
                </div>
                <div class="icon-btn" @click="isNoti = true">
                    <div class="icon-24 icon-notification"></div>
                </div>
                <div class="icon-btn">
                    <div class="icon-24 icon-helpmenu"></div>
                </div>
                <div class="icon-btn">
                    <div class="icon-24 icon-setting"></div>
                </div>
                <a-button type="text" @click="toggleLanguage" class="lang-btn">
                    <span v-if="locale === 'vi'">VN</span>
                    <span v-else>EN</span>
                </a-button>
            </div>
        </div>
    </div>
    <NotificationPopup v-if="isNoti" @close="isNoti = false"/>
</template>

<script setup>
import NotificationPopup from '../../views/components/NotificationPopup.vue';
import { ref } from 'vue';
import { useI18n } from 'vue-i18n';

const isNoti = ref(false);

const { locale } = useI18n();

const toggleLanguage = () => {
  const newLang = locale.value === 'vi' ? 'en' : 'vi';
  locale.value = newLang;
  localStorage.setItem('lang', newLang);
};
</script>

<style  scoped>
.header_container{
    width: 100%;
    height: 40px;
    background-color: #f6f6f6;
}
.header_content{
    width: 100%;
    height: 100%;
    display: flex;
    justify-content: space-between;
    align-items: center;
}
.header_content_left, .header_content_right{
    display: flex;
    gap:12px;
    align-items: center;
}
.label_container{
    display: flex;
    align-items: center;
    justify-self: center;
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
.title{
    font-size: 20px;
    color: #212121;
    font-weight: bold;
}
.contact{
    font-size: 17px;
    color: #39c5ab;
    text-decoration: underline;
}
.lang-btn {
  font-weight: 600;
  border: 1px solid #d9d9d9; 
}
</style>