<template>
  <Teleport to="body">
    <div name="toast-anim">
      <div v-if="modelValue" class="toast-container" :class="statusClass">
        <div class="toast-icon">
          <div class="icon-btn" v-if="isSuccess">
              <div class="icon-20 icon-success"></div>
          </div>
          <div class="icon-btn" v-else-if="isError">
              <div class="icon-20 icon-error"></div>
          </div>
          <div class="icon-btn" v-else-if="isWarning">
              <div class="icon-20 icon-warning"></div>
          </div>
        </div>
        <div class="toast-content">{{ text }}</div>
      </div>
    </div>
  </Teleport>
</template>

<script setup>
import { computed, watch } from 'vue';

const props = defineProps({
  modelValue: Boolean,
  text: String,
  isSuccess: Boolean,
  isError: Boolean,
  isWarning: Boolean,
  duration: { type: Number, default: 3000 }
});

const emit = defineEmits(['update:modelValue']);

// Tự động đóng toast
watch(() => props.modelValue, (val) => {
  if (val) {
    setTimeout(() => emit('update:modelValue', false), props.duration);
  }
});

const statusClass = computed(() => ({
  'is-success': props.isSuccess,
  'is-error': props.isError,
  'is-warning': props.isWarning
}));
</script>

<style scoped>
.toast-container {
  position: fixed;
  top: 24px;
  right: 24px;
  z-index: 9999;
  display: flex;
  align-items: center;
  padding: 12px 20px;
  background: #fff;
  border-radius: 4px;
  box-shadow: 0 4px 12px rgba(0,0,0,0.1);
  min-width: 280px;
}

.toast-icon { 
  width: 20px; 
  height: 20px; 
  margin-right: 12px; 
  display: flex; 
  align-items: center;
  justify-content: center;}
.toast-content { font-size: 14px; color: #333; }

.icon-success { 
    background-color: #52c41a; 
}

.icon-error { 
    background-color: #ff4d4f; 
}

.icon-warning { 
    background-color: #faad14; 
}


.toast-anim-enter-active, .toast-anim-leave-active { 
    transition: all 0.4s ease; 
}
.toast-anim-enter-from {
     opacity: 0; 
     transform: translateX(50px); 
}
.toast-anim-leave-to { 
    opacity: 0; 
    transform: scale(0.9); 
}
</style>