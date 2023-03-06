<template>
  <div class="common-layout">
    <el-container>
      <el-aside width="200px">
        <div v-for="item in messList" class="item-message action" @click="getMessage(item.id)">
          {{ item.name }}
        </div>
      </el-aside>
      <el-main>
        <div class="message">
          <ul class="infinite-list" style="overflow: auto">
            <li v-for="message in messages" style="margin-left: 5px">
              <div v-if="message.name == 'FFC_admin'" style="color: #409eff">
                {{ message.name }}
                <div style="display: inline-block; width: 20rpx"></div>
                {{ message.createTime.replace('T', ' ').substring(0, 19) }}
                <div style="color: #303133; margin-bottom: 10px">{{ message.message }}</div>
              </div>
              <div v-else style="color: #67c23a">
                {{ message.name }}
                <div style="display: inline-block; width: 20rpx"></div>
                {{ message.createTime.replace('T', ' ').substring(0, 19) }}
                <div style="color: #303133; margin-bottom: 10px">{{ message.message }}</div>
              </div>
            </li>
          </ul>
        </div>
        <div class="input-bottom" v-if="messages.length != 0">
          <el-input v-model="input.message" placeholder="Please input" />
          <el-button type="primary" :icon="Edit" :text="true" @click="sendMessage">发送</el-button>
        </div>
      </el-main>
    </el-container>
  </div>
</template>
<script setup>
import * as signalR from '@aspnet/signalr'
onMounted(() => {
  loadAll()
})

const messList = ref([])
const messages = ref([])
const input = ref({
  message: '',
  userId: '',
  userName: '',
  businessId: '',
  messageId: ''
})

const signal = new signalR.HubConnectionBuilder().withUrl('https://localhost:3004/chatHub', {}).build()
signal
  .start()
  .then(() => {
    console.log('连接成功')
  })
  .catch((err) => {
    console.log(err)
  })
signal.on('ReceiveMessage', (user, message, time) => {
  getMessage(input.value.businessId)
})

const sendMessage = () => {
  signal.invoke('SendMessage', input.value.userId, input.value.businessId, input.value.message, true).catch((err) => {
    console.log(err)
  })
}

const loadAll = () => {
  input.value.userId = localStorage.getItem('userId')
  input.value.userName = localStorage.getItem('userName')
  console.log(input.value)

  let reqConfig = {
    url: '/MessageManagementBusiness/UserMessage/GetChatList?userRole=false',
    method: 'get'
  }
  axiosReq(reqConfig).then((resData) => {
    messList.value = resData?.datas
    console.log(messList.value)
  })
}
const getMessage = (id) => {
  console.log(id)
  let reqConfig = {
    url:
      '/MessageManagementBusiness/UserMessage/GetHistoryMessage?pageNum=1&pageSize=1000&extendValue1=2&searchValue=' +
      id,
    method: 'get'
  }
  axiosReq(reqConfig).then((resData) => {
    messages.value = resData?.datas
    input.value.businessId = id
    console.log(messages.value)
  })
}
</script>
<style lang="scss">
.message {
  border: 1px solid #409eff;
}
.infinite-list {
  height: 300px;
  padding: 0;
  margin: 0;
  list-style: none;
  .infinite-list-item {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 50px;
    background: var(--el-color-primary-light-9);
    margin: 10px;
    // color: var(--el-color-primary);
  }
}
.item-message {
  text-align: center;
  border-radius: 5px;
}
.action {
  background: rgba($color: skyblue, $alpha: 0.6);
}
.input-bottom {
  margin-top: 10px;
  display: flex;
  justify-content: space-between;
}
</style>
