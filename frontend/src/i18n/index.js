import { createI18n } from 'vue-i18n'
import vi from './locales/vi.json'
import en from './locales/en.json'

// 1. Kiểm tra xem người dùng đã từng chọn ngôn ngữ chưa khi người dùng f5

const savedLocale = localStorage.getItem('lang') || 'vi'

const i18n = createI18n({
  legacy: false,          
  locale: savedLocale,    
  fallbackLocale: 'en',   
  globalInjection: true,  
  messages: {
    vi,
    en
  }
})

export default i18n