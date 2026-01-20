// 1. Loại bảo hiểm
export const INSURANCE_TYPE_ENUM = Object.freeze({
  CAR: 'CAR',
  BIKE: 'BIKE',
  HEALTH: 'HEALTH',
  FIRE: 'FIRE',
  GOODS: 'GOODS',
  TRAVEL: 'TRAVEL',
  ACCIDENT: 'ACCIDENT',
});

// 2. Nhà cung cấp bảo hiểm
export const PROVIDER_ENUM = Object.freeze({
  BSH: 'BSH',
  PVI: 'PVI',
  ABIC: 'ABIC',
  CATHAY: 'Cathay',
});

// 3. Hãng xe
export const CAR_BRAND_ENUM = Object.freeze({
  ASIA: 'ASIA',
  TOYOTA: 'TOYOTA',
  HUYNDAI: 'HUYNDAI',
  BMW: 'BMW',
  BENTLEY: 'BENTLEY',
});

// 4. Hiệu xe (Model)
export const CAR_MODEL_ENUM = Object.freeze({
  LEGEND_27_V6_AT: 'LEGEND_2.7_V6_AT',
  LEGEND_32_V6: 'LEGEND_3.2_V6',
  MDX: 'MDX',
  NSX_NSX: 'NSX_NSX',
});

// 5. Mục đích sử dụng
export const USAGE_PURPOSE_ENUM = Object.freeze({
  COMMERCIAL: 'COMMERCIAL',
  NON_COMMERCIAL: 'NON_COMMERCIAL',
});

// 6. Loại xe
export const CAR_TYPE_ENUM = Object.freeze({
  PASSENGER: 'PASSENGER',
  PRACTICE: 'PRACTICE',
  AMBULANCE: 'AMBULANCE',
  MONEY_TRANSPORT: 'MONEY_TRANSPORT',
  SPECIAL_ZONE: 'SPECIAL_ZONE', 
  TRUCK: 'TRUCK',
  SPECIAL_OTHER: 'SPECIAL_OTHER',
  PICKUP: 'PICKUP',
});

// 7. Trạng thái đơn hàng
export const ORDER_STATUS_ENUM = Object.freeze({
  PENDING: 'PENDING',
  FAILED: 'FAILED',
  COMPLETED : 'COMPLETED'
});