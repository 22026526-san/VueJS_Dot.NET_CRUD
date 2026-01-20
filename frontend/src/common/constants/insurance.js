import { 
  INSURANCE_TYPE_ENUM, 
  PROVIDER_ENUM, 
  CAR_BRAND_ENUM, 
  CAR_MODEL_ENUM, 
  USAGE_PURPOSE_ENUM, 
  CAR_TYPE_ENUM 
} from '../enums/enum';

// --- DANH SÁCH TAB BẢO HIỂM ---
export const INSURANCE_TABS = [
  { id: INSURANCE_TYPE_ENUM.CAR, label: 'common.insurance_type.car' },
  { id: INSURANCE_TYPE_ENUM.BIKE, label: 'common.insurance_type.bike' },
  { id: INSURANCE_TYPE_ENUM.HEALTH, label: 'common.insurance_type.health' },
  { id: INSURANCE_TYPE_ENUM.FIRE, label: 'common.insurance_type.fire' },
  { id: INSURANCE_TYPE_ENUM.GOODS, label: 'common.insurance_type.goods' },
  { id: INSURANCE_TYPE_ENUM.TRAVEL, label: 'common.insurance_type.travel' },
  { id: INSURANCE_TYPE_ENUM.ACCIDENT, label: 'common.insurance_type.accident' },
];

// --- DANH SÁCH NHÀ CUNG CẤP ---
export const INSURANCE_PROVIDERS = [
  { value: PROVIDER_ENUM.BSH, label: 'BSH' },
  { value: PROVIDER_ENUM.PVI, label: 'PVI' },
  { value: PROVIDER_ENUM.ABIC, label: 'ABIC' },
  { value: PROVIDER_ENUM.CATHAY, label: 'Cathay' }, 
];

// --- DANH SÁCH HÃNG XE ---
export const CAR_BRANDS = [
  { value: CAR_BRAND_ENUM.ASIA, label: 'ASIA' },
  { value: CAR_BRAND_ENUM.TOYOTA, label: 'Toyota' },
  { value: CAR_BRAND_ENUM.HUYNDAI, label: 'Huyndai' },
  { value: CAR_BRAND_ENUM.BMW, label: 'BMW' },
  { value: CAR_BRAND_ENUM.BENTLEY, label: 'BENTLEY' },
];

// --- DANH SÁCH HIỆU XE ---
export const CAR_MODELS = [
  { value: CAR_MODEL_ENUM.LEGEND_27_V6_AT, label: 'LEGEND 2.7 V6 AT' },
  { value: CAR_MODEL_ENUM.LEGEND_32_V6, label: 'LEGEND 3.2 V6' },
  { value: CAR_MODEL_ENUM.MDX, label: 'MDX' },
  { value: CAR_MODEL_ENUM.NSX_NSX, label: 'NSX NSX' },
];

// --- DANH SÁCH MỤC ĐÍCH SỬ DỤNG XE ---  
export const USAGE_PURPOSES = [
  { value: USAGE_PURPOSE_ENUM.COMMERCIAL, label: 'common.usage_purpose.commercial' },
  { value: USAGE_PURPOSE_ENUM.NON_COMMERCIAL, label: 'common.usage_purpose.non_commercial' },
];

// --- DANH SÁCH LOẠI XE ---
export const CAR_TYPES = [
  { value: CAR_TYPE_ENUM.PASSENGER, label: 'common.car_type.passenger' },
  { value: CAR_TYPE_ENUM.PRACTICE, label: 'common.car_type.practice' },
  { value: CAR_TYPE_ENUM.AMBULANCE, label: 'common.car_type.ambulance' },
  { value: CAR_TYPE_ENUM.MONEY_TRANSPORT, label: 'common.car_type.money_transport' },
  { value: CAR_TYPE_ENUM.SPECIAL_ZONE, label: 'common.car_type.special_zone' },
  { value: CAR_TYPE_ENUM.TRUCK, label: 'common.car_type.truck' },
  { value: CAR_TYPE_ENUM.SPECIAL_OTHER, label: 'common.car_type.special_other' },
  { value: CAR_TYPE_ENUM.PICKUP, label: 'common.car_type.pickup' },
];