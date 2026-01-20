import { createI18n } from 'vue-i18n'
import vi from './locales/vi.json'
import en from './locales/en.json'

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