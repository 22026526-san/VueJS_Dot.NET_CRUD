import dayjs from 'dayjs';
import { formatCurrency } from '../../utils';


export const validateSeats = (t) => async (_rule, value) => {
  if (!value) return Promise.reject(t('car_item.validation.required'));
  const num = Number(value);
  if (isNaN(num) || num < 2 || num > 45) {
    return Promise.reject(t('car_item.validation.seat_range', { min: 2, max: 45 }));
  }
  return Promise.resolve();
};

export const validateWeight = (t) => async (_rule, value) => {
  if (!value) return Promise.reject(t('car_item.validation.required'));
  const num = Number(value);
  if (isNaN(num) || num < 100 || num > 10000) {
    return Promise.reject(t('car_item.validation.weight_range', { min: 100, max: formatCurrency(10000) }));
  }
  return Promise.resolve();
};

export const disabledStartDate = (current) => {
  return current && current <= dayjs().endOf('day');
};

export const formatInputCode = (value) => {
  if (!value) return value;
  return value.replace(/[^A-Z0-9.-]/g, '');
};

export const removeSpecialChars = (value) => {
  if (!value) return value;
  return value.replace(/[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~`]/g, '');
};