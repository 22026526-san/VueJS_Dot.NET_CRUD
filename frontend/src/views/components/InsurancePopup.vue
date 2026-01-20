<template>
    <div class="car-item-container">
        <div class="car-header" @click="toggleExpand">
            <div class="header-left">
                <span class="car-title">{{ t('car_item.title', { index: index + 1 }) }}</span>
                <span class="separator">|</span>
                <span class="delete-btn" @click.stop="emitRemove">{{ t('car_item.delete') }}</span>
            </div>
            <div class="header-right">
                <div class="icon-20" :class="isExpanded ? 'icon-dropUp' : 'icon-dropDown'"></div>
            </div>
        </div>

        <div v-show="isExpanded" class="car-body" @keydown.enter="handleEnterKey">
            <a-form :model="formData" :rules="rules" ref="formRef" layout="vertical">
                <div class="form-section">
                    <div class="section-title">
                        <CheckCircleFilled :class="isInsuranceFilled ? 'icon-success' : 'icon-gray'" />
                        <span class="title-span">{{ t('car_item.insurance_info') }}</span>
                    </div>
                    <div class="section-desc">{{ t('car_item.insurance_desc') }}</div>

                    <div class="form-flex">
                        <a-form-item class="form-group" name="usage_purpose">
                            <label>{{ t('car_item.usage_purpose') }} <span class="required">*</span></label>
                            <a-select v-model:value="formData.usage_purpose" 
                                :placeholder="t('car_item.usage_purpose_placeholder')"
                                class="w-full" :options="usagePurposeOptions" allow-clear show-search>
                            </a-select>
                        </a-form-item>
                        <a-form-item class="form-group" name="vehicle_type">
                            <label>{{ t('car_item.vehicle_type') }} <span class="required">*</span></label>
                            <a-select v-model:value="formData.vehicle_type" 
                                :placeholder="t('car_item.vehicle_type_placeholder')" class="w-full"
                                :options="carTypeOptions" allow-clear show-search>  
                            </a-select>
                        </a-form-item>
                    </div>

                    <div class="form-flex">
                        <a-form-item class="form-group" name="seat_count">
                            <label>{{ t('car_item.seat_count') }} <span class="required">*</span></label>
                            <a-input-number v-model:value="formData.seat_count" 
                                :placeholder="t('car_item.seat_count_placeholder')"
                                class="w-full">
                            </a-input-number>
                        </a-form-item>
                        <a-form-item class="form-group" name="payload_capacity">
                            <label>{{ t('car_item.payload') }} <span class="required">*</span></label>
                            <a-input-number v-model:value="formData.payload_capacity" 
                                :placeholder="t('car_item.payload_placeholder')"
                                :addon-after="t('car_item.unit_kg')" class="w-full"/>
                        </a-form-item>
                    </div>

                    <div class="form-flex">
                        <a-form-item class="form-group" name="duration_year">
                            <label>{{ t('car_item.duration') }} <span class="required">*</span></label>
                            <a-select v-model:value="formData.duration_year" class="w-full">
                                <a-select-option :value="1">{{ t('car_item.duration_year', { n: 1 }) }}</a-select-option>
                                <a-select-option :value="2">{{ t('car_item.duration_year', { n: 2 }) }}</a-select-option>
                                <a-select-option :value="3">{{ t('car_item.duration_year', { n: 3 }) }}</a-select-option>
                            </a-select>
                        </a-form-item>
                        <a-form-item class="form-group" name="start_date">
                            <label>{{ t('car_item.start_date') }} <span class="required">*</span></label>
                            <a-date-picker v-model:value="formData.start_date" format="DD/MM/YYYY" :disabled-date="disabledStartDate" class="w-full" :placeholder="t('car_item.date_placeholder')"/>
                        </a-form-item>
                        <a-form-item class="form-group">
                            <label>{{ t('car_item.end_date') }} <span class="required">*</span></label>
                            <a-date-picker v-model:value="formData.end_date" format="DD/MM/YYYY" class="w-full" :placeholder="t('car_item.date_placeholder')"
                                disabled />
                        </a-form-item>
                    </div>


                    <div class="price-estimate">
                        <a href="#" class="benefit-link">{{ t('car_item.view_benefit') }}</a>
                        <span class="title-span">{{ t('car_item.insurance_fee') }}: <strong>{{ formatCurrency(150000) }} VND</strong></span>
                    </div>
                </div>

                <div class="form-section mt-20">
                    <div class="section-title">
                        <CheckCircleFilled :class="isRegistrationFilled ? 'icon-success' : 'icon-gray'" />
                        <span class="title-span">{{ t('car_item.reg_info') }}</span>
                    </div>
                    <div class="section-desc">{{ t('car_item.reg_desc') }}</div>

                    <div :class="isUploadImg ? 'upload-box-img' : 'upload-box'">
                        <div class="upload-info">
                            <strong>{{ t('car_item.upload_guide') }}</strong>
                            <p>{{ t('car_item.upload_format') }}</p>
                            <p>{{ t('car_item.upload_size') }}</p>
                        </div>
                        <div class="upload-actions" v-if="!isUploadImg">
                            <input type="file" ref="fileInput" style="display: none" @change="handleFileChange">

                            <a-button @click="triggerFileUpload">
                                <template #icon>
                                    <UploadOutlined />
                                </template>
                                {{ t('car_item.btn_upload') }}
                            </a-button>
                            <a-button type="primary" ghost class="ml-10">
                                <template #icon>
                                    <CameraOutlined />
                                </template>
                                {{ t('car_item.btn_camera') }}
                            </a-button>
                        </div>
                        <div class="divider" v-if="isUploadImg"></div>
                        <div class="upload-img" v-if="isUploadImg">
                            <div class="content-upload">
                                <div class="icon-24 icon-upload-img"></div>
                                <a :href="formData.registration_image_url" target="_blank" class="img_link">
                                    {{ fileName }}
                                </a>
                            </div>
                            <div class="icon-20 icon-X" @click="handeFileRemove"></div>
                        </div>
                    </div>

                    <div class="form-flex">
                        <a-form-item class="form-group" name="manufacturer">
                            <label>{{ t('car_item.manufacturer') }} <span class="required">*</span></label>
                            <a-select v-model:value="formData.manufacturer" 
                                :placeholder="t('car_item.manufacturer_placeholder')" class="w-full"
                                :options="CAR_MODELS" allow-clear show-search>
                            </a-select>
                        </a-form-item>
                        <a-form-item class="form-group" name="model">
                            <label>{{ t('car_item.model') }} <span class="required">*</span></label>
                            <a-select v-model:value="formData.model" 
                                :placeholder="t('car_item.model_placeholder')" class="w-full" 
                                :options="CAR_BRANDS" allow-clear show-search>
                            </a-select>
                        </a-form-item>
                    </div>

                    <div class="form-flex">
                        <a-form-item class="form-group" name="chassis_number">
                            <label>{{ t('car_item.chassis_num') }} <span class="required">*</span></label>
                            <a-input v-model:value="formData.chassis_number" 
                                :placeholder="t('car_item.chassis_num_placeholder')" :maxlength="20"/>
                        </a-form-item>
                        <a-form-item class="form-group" name="engine_number">
                            <label>{{ t('car_item.engine_num') }} <span class="required">*</span></label>
                            <a-input v-model:value="formData.engine_number" 
                                :placeholder="t('car_item.engine_num_placeholder')" :maxlength="20"/>
                        </a-form-item>
                    </div>

                    <div class="form-flex">
                        <div class="form-group">
                            <div class="label-row">
                                <label>{{ t('car_item.license_plate') }} <span class="required">*</span></label>
                                <a-checkbox :checked="!formData.has_license_plate"
                                    @change="handleToggleLicensePlate">
                                    {{ t('car_item.no_license_plate') }}
                                </a-checkbox>
                            </div>
                            <a-form-item name="license_plate">
                            <a-input v-model:value="formData.license_plate" 
                                :placeholder="t('car_item.license_plate_placeholder')"
                                @input="handleLicenseInput"
                                :disabled="!formData.has_license_plate" :maxlength="10"/>
                            </a-form-item>
                        </div>
                        <div class="form-group"></div>
                    </div>
                </div>
            </a-form>
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
import { ref, watch, reactive, computed } from 'vue';
import { CheckCircleFilled, UploadOutlined, CameraOutlined } from '@ant-design/icons-vue';
import { formatCurrency } from '../../utils';
import { validateSeats, validateWeight, disabledStartDate, formatInputCode} from '../../common/validates/validateForm.js';
import { CAR_TYPES,CAR_BRANDS,CAR_MODELS,USAGE_PURPOSES } from '../../common/constants/insurance.js';
import uploadService from '../../api/services/uploadService.js';
import Loading from '../../components/Loading.vue';
import ToastMessage from '../../components/ToastMessage.vue';
import { useI18n } from 'vue-i18n';

const props = defineProps({
    index: {
        type: Number,
        required: true
    },
    data: {
        type: Object,
        default: () => ({})
    }
});

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

const usagePurposeOptions = computed(() => {
  return USAGE_PURPOSES.map(item => ({
    value: item.value,
    label: t(item.label) 
  }));
});

const carTypeOptions = computed(() => {
  return CAR_TYPES.map(item => ({
    value: item.value,
    label: t(item.label) 
  }));
});

const emit = defineEmits(['remove', 'update:data']);

const { t } = useI18n();
const isExpanded = ref(true);
const fileInput = ref(null);
const isUploadImg = ref(null);
const isLoading = ref(false);
const uploadedImageId = ref(null);
const fileName = ref(null);
const formRef = ref(null);

// Dữ liệu form 
const formData = reactive({
    // --- Thông tin bảo hiểm ---
    usage_purpose: null,                   
    vehicle_type: null,               
    seat_count: null,                  
    payload_capacity: null,            
    duration_year: 1,                 
    start_date: null, 
    end_date: null,              

    // --- Thông tin xe ---
    registration_image_url: null, 
    manufacturer: null,           
    model: null,                
    chassis_number: null,         
    engine_number: null,      
    has_license_plate: true,    
    license_plate: null,          
    ...props.data
});

watch(formData, (newVal) => {
    if (isInsuranceFilled.value && isRegistrationFilled.value) {
        if (newVal.fee_order !== 150000) {
            newVal.fee_order = 150000;
        }
        emit('update:data', { ...newVal, start_date: formData.start_date ? formData.start_date.format('YYYY-MM-DD') : null, end_date: formData.end_date ? formData.end_date.format('YYYY-MM-DD') : null });
    } else {
        newVal.fee_order = null;
        if (props.data.fee_order !== null) {
            emit('update:data', { ...newVal, start_date: formData.start_date ? formData.start_date.format('YYYY-MM-DD') : null, end_date: formData.end_date ? formData.end_date.format('YYYY-MM-DD') : null });
        }
    }
}, { deep: true });

const isInsuranceFilled = computed(() => {
    const { usage_purpose, vehicle_type, seat_count, payload_capacity, duration_year, start_date, end_date } = formData;
    return !!(usage_purpose && vehicle_type && seat_count && payload_capacity && duration_year && start_date && end_date);
});

const isRegistrationFilled = computed(() => {
    const { manufacturer, model, chassis_number, engine_number, has_license_plate, license_plate } = formData;
    const hasBasicInfo = manufacturer && model && chassis_number && engine_number;
    const hasPlateInfo = !has_license_plate || license_plate;
    return !!(hasBasicInfo && hasPlateInfo);
});

const rules = reactive ({
  usage_purpose: [
    { required: true, message: t('car_item.validation.required'), trigger: ['blur', 'change'] }
  ],
  vehicle_type: [
    { required: true, message: t('car_item.validation.required'), trigger: ['blur', 'change'] }
  ],
  seat_count: [
    { required: true, validator: validateSeats(t), trigger: ['blur', 'change'] }
  ],
  payload_capacity: [
    { required: true, validator: validateWeight(t), trigger: ['blur', 'change'] }
  ],
  duration_year: [
    { required: true, message: t('car_item.validation.select_duration'), trigger: 'change' }
  ],
  start_date: [
    { required: true, message: t('car_item.validation.select_start_date'), trigger: ['blur', 'change'] }
  ],
  manufacturer: [
    { required: true, message: t('car_item.validation.required'), trigger: ['blur', 'change'] }
  ],
  model: [
    { required: true, message: t('car_item.validation.required'), trigger: ['blur', 'change'] }
  ],
  chassis_number: [
    { required: true, message: t('car_item.validation.required'), trigger: ['blur', 'change'] }
  ],
  engine_number: [
    { required: true, message: t('car_item.validation.required'), trigger: ['blur', 'change'] }
  ],
  license_plate: [
    { 
      required: formData.has_license_plate, 
      message: t('car_item.validation.required'), 
      trigger: ['blur', 'change'] 
    }
  ]
});

const toggleExpand = () => {
    isExpanded.value = !isExpanded.value;
};

const emitRemove = () => {
    emit('remove', props.index);
};

const triggerFileUpload = () => {
    fileInput.value.click();
};

const handleToggleLicensePlate = (e) => {
  const isChecked = e.target.checked; 
  formData.has_license_plate = !isChecked;
  console.log('Has license plate:', formData.has_license_plate);
  if (isChecked) {
    formData.license_plate = ''; 
    formRef.value?.clearValidate('license_plate');
  }
};

const handleLicenseInput = (e) => {
  const originalValue = e.target.value;
  const cleanValue = formatInputCode(originalValue.toUpperCase());
  formData.license_plate = cleanValue;
  if (originalValue !== cleanValue) {
      e.target.value = cleanValue;
  }
};

const handleFileChange = async (e) => {
    const file = e.target.files[0];
    
    if (!file) return;
    const isImage = file.type.startsWith('image/');
    fileName.value = file.name;
    if (!isImage) {
        triggerToast('Thêm ảnh thất bại. Vui lòng chỉ chọn file ảnh!', 'error');
        return;
    }

    try {
        isLoading.value = true; 
       
        const response = await uploadService.uploadImage(file);
        
        formData.registration_image_url = response.url; 
        
        uploadedImageId.value = response.publicId; 

        isUploadImg.value = true;
        
    } catch (error) {
        console.error("Lỗi upload:", error);
        alert("Upload thất bại, vui lòng thử lại!");
    } finally {
        isLoading.value = false;
    }
};

const handeFileRemove = async () => {
    
    try {
        isLoading.value = true;

        await uploadService.deleteImage(uploadedImageId.value);
        
        resetUploadState();

    } catch (error) {
        console.error("Lỗi xóa ảnh:", error);
        alert("Không thể xóa ảnh này!");
    } finally {
        isLoading.value = false;
    }
};
const calculateEndDate = () => {
    if (formData.start_date && formData.duration_year) {
        const years = parseInt(formData.duration_year);
        formData.end_date = formData.start_date.add(years, 'year');
    }
};

const resetUploadState = () => {
    formData.registration_image_url = null;
    uploadedImageId.value = null;
    isUploadImg.value = false;
}

const handleEnterKey = (e) => {
    
    if (e.target.tagName === 'BUTTON') return;

    e.preventDefault();

    const formContainer = e.currentTarget;
    const focusableElements = Array.from(
        formContainer.querySelectorAll(
            'input:not([disabled]):not([type="hidden"]), select:not([disabled]), textarea:not([disabled]), [tabindex]:not([tabindex="-1"])'
        )
    ).filter(el => {
        return el.offsetWidth > 0 || el.offsetHeight > 0 || el.getClientRects().length > 0;
    });

    const index = focusableElements.indexOf(e.target);

    if (e.shiftKey) {
        // --- TRƯỜNG HỢP: SHIFT + ENTER (QUAY LẠI) ---
        if (index > 0) {
            focusableElements[index - 1].focus();
        }
    } else {
        // --- TRƯỜNG HỢP: ENTER (ĐI TIẾP) ---
        if (index > -1 && index < focusableElements.length - 1) {
            focusableElements[index + 1].focus();
        }
    }
};
watch(
    [() => formData.start_date, () => formData.duration_year],
    () => {
        calculateEndDate();
    },
    { immediate: true }
);
</script>

<style scoped>
.car-item-container {
    border: 1px solid #e0e0e0;
    border-radius: 8px;
    background: #f2f2f2;
}

/* Header Styles */
.car-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 12px 16px;
    background-color: #f2f2f2;
    cursor: pointer;
    transition: all 0.3s;
    border-radius: 8px;
}

.header-left {
    display: flex;
    align-items: center;
    gap: 10px;
}

.car-title {
    font-weight: 700;
    color: #333;
}

.separator {
    color: #ccc;
}

.delete-btn {
    color: #ff4d4f;
    cursor: pointer;
    font-size: 13px;
}

.delete-btn:hover {
    text-decoration: underline;
}

/* Body Styles */
.car-body {
    padding: 12px;
    border-top: 1px solid #e0e0e0;
    border-bottom-left-radius: 8px;
    border-bottom-right-radius: 8px;
}

.form-section {
    background-color: #fff;
    padding: 16px 24px;
    border-radius: 8px;
}

.section-title {
    display: flex;
    align-items: center;
    gap: 8px;
    font-weight: 600;
    font-size: 15px;
    margin-bottom: 4px;
}

.title-span {
    color: #212121;
    font-size: 13px;
}

.icon-success {
    color: #87d068;
}

.icon-gray {
    color: #bfbfbf;
}

.section-desc {
    font-size: 13px;
    color: #666;
    margin-bottom: 16px;
    margin-left: 24px;
}


.form-flex {
    display: flex;
    gap: 22px;
}

.form-group {
    display: flex;
    flex-direction: column;
    flex: 1
}

label {
    font-size: 13px;
    font-weight: 500;
    color: #333;
    margin-bottom: 3px;
}

.required {
    color: #ff4d4f;
    margin-left: 2px;
}

.date-group {
    display: flex;
    flex-direction: row;
    gap: 16px;
}

.price-estimate {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-top: 10px;
}

.benefit-link {
    color: #1890ff;
    font-size: 13px;
}

.price-text {
    font-size: 14px;
}

.label-row {
    display: flex;
    align-items: center;
    gap: 12px
}

/* Upload Box Styling */
.upload-box {
    border: 2px dashed #40a9ff;
    background-color: #f0faff;
    border-radius: 6px;
    padding: 16px;
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin-bottom: 20px;
}
.upload-box-img {
    border: 2px dashed #40a9ff;
    background-color: #f0faff;
    border-radius: 6px;
    padding: 16px;
    display: flex;
    flex-direction: column;
}

.icon-upload-img, .icon-X{
    background-color: red;
}

.upload-info strong {
    display: block;
    font-size: 13px;
    color: #333;
    margin-bottom: 4px;
}

.upload-info p {
    margin: 0;
    font-size: 11px;
    color: #888;
}

.upload-actions {
    display: flex;
    align-items: center;
}
.upload-img{
    display: flex;
    align-items: center;
    justify-content: space-between;
}
.content-upload{
    display: flex;
    align-items: center;
    gap: 5px;
}

.content-upload p{
    margin: 0;
    color: #1890ff;
}
.ml-10 {
    margin-left: 10px;
}

.mt-20 {
    margin-top: 20px;
}

/* Helpers */
.w-full {
    width: 100%;
}

.img_link {
    color: #1890ff;
    text-decoration: underline;
}

:where(.css-dev-only-do-not-override-1p3hq3p).ant-form-item {
    margin-bottom: 12px;
}
</style>