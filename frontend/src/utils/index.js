// Các hàm tiện ích (Format date, money...)


/**
 * VD: 1500000 -> 1.500.000
 */
export const formatCurrency = (amount) => {
    if (!amount) return '0';
    return new Intl.NumberFormat('vi-VN').format(amount);
};

/**
 * Format số tiền rút gọn (Triệu, Tỷ)
 * VD: 1500000 -> 1.5 triệu
 */
export const formatShortCurrency = (amount) => {
    if (!amount) return '0 đồng';
    
    // Nếu số lớn hơn 1 Tỷ
    if (amount >= 1000000000) {
        const value = (amount / 1000000000).toFixed(1).replace(/\.0$/, '');
        return `${value} tỷ đồng`;
    }
    
    // Nếu số lớn hơn 1 Triệu
    if (amount >= 1000000) {
        const value = (amount / 1000000).toFixed(1).replace(/\.0$/, '');
        return `${value} triệu đồng`;
    }

    return formatCurrency(amount) + 'đồng';
};

export const formatDate = (dateString) => {
  const date = new Date(dateString);
  
  const day = date.getDate().toString().padStart(2, '0');
  const month = (date.getMonth() + 1).toString().padStart(2, '0'); 
  const year = date.getFullYear();

  return `${day}/${month}/${year}`;
}