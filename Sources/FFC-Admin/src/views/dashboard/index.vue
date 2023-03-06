<template>
  <div class="">
   
  </div>
</template>

<script setup>
// import Admin from './Admin'
// import Editor from './Editor'
import { computed } from 'vue'
import { useUserStore } from '@/store/user'
const userStore = useUserStore()
const roles = computed(() => {
  return userStore.roles
})
</script>

<style scoped lang="scss"></style>
