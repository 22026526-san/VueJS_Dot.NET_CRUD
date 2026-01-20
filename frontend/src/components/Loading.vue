<template>
    <Teleport to="body">
        <div class="loading-overlay">
            <div class="loading-box">
                <div class="spinner"></div>
            </div>
        </div>
    </Teleport>
</template>

<script setup>

</script>

<style scoped>

.loading-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.4); 
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 99999; 
    backdrop-filter: blur(2px); 
}


.loading-box {
    padding: 24px 40px;
    display: flex;
    flex-direction: column;
    align-items: center;
    min-width: 200px;
}

/* Hiệu ứng xoay CSS thuần */
.spinner {
    width: 40px;
    height: 40px;
    border: 4px solid #f3f3f3; /* Màu viền mờ */
    border-top: 4px solid #1890ff; /* Màu chính (xanh Ant Design) */
    border-radius: 50%;
    animation: spin 1s linear infinite;
}

@keyframes spin {
    0% { transform: rotate(0deg); }
    100% { transform: rotate(360deg); }
}
</style>