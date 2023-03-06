<template>
	<view style="height: 100%;width: 95%; margin: auto;">
		<u-navbar bgColor="#ffc850" title="聊天消息" @leftClick="leftClick"></u-navbar>
		<view style="margin-top: 100rpx;">
			<u-list @scrolltolower="scrolltolower" width="100%" height="1150rpx">
				<u-list-item v-for="(item, index) in messageList" :key="index">
					<u--text v-if="item.name == userName" type="primary" :text="item.name+' '+item.createTime.replace('T',' ').substring(0,19)"></u--text>
					<u--text v-else type="success" :text="item.name+' '+item.createTime.replace('T',' ').substring(0,19)"></u--text>
					<u--text type="info" :text="item.message"></u--text>
				</u-list-item>
			</u-list>
		</view>
		<view class="imput-bottom">
			<u--input border="surround" clearable shape="circle" v-model="message" style="height: 10px;"></u--input>
			<span style="width: 3%;"></span>
			<u-button text="发送" style="width: 10%;" size="mini" type="primary" shape="circle" @click="sendMessage">
			</u-button>
		</view>
	</view>
</template>
<!--  -->
<script>
	let signalR = require('signalr-uni')
	import api from './api.js'
	export default {
		data() {
			return {
				messageList: [],
				connection: {},
				selectProp: {
					searchValue: "",
					pageNum: 1,
					pageSize: 1000
				},
				userId: this.$store.state.user.id,
				userName:this.$store.state.user.name,
				businessId: "",
				message: "",
				scrollTop: 0,
			};
		},
		onLoad(data) {
			this.businessId = data.id
			this.selectProp.searchValue = data.id
			this.connect()
			this.getMessage()
		},
		methods: {
			scrolltolower() {

			},
			leftClick() {
				uni.navigateBack()
			},
			connect() {
				this.connection = new signalR.HubConnectionBuilder()
					.withUrl("https://localhost:3004/chatHub")
					.configureLogging(signalR.LogLevel.Information)
					.build();
				this.connection.start().then(() => {
					console.log("连接成功");

				}).catch(err => {
					console.log(err);
				})
				this.connection.on("ReceiveMessage", (user, message, time) => {
					this.getMessage()
				})

			},
			sendMessage() {
				this.connection.invoke("SendMessage", this.userId, this.businessId, this.message,false).catch(err => {
					console.log(err);
				})
			},
			async getMessage() {
				var result = await api.GetMessage(this.selectProp)
				console.log(result);
				result.data.datas.sort((x, y) => {
					return new Date(x.createTime).getTime() - new Date(y.createTime).getTime()
				})
				console.log(result.data.datas);
				this.messageList = result.data.datas
			}
		}
	}
</script>

<style lang="scss">
	.up {
		color: #fff;
		text-align: center;
		height: 80rpx;
		line-height: 80rpx;
		background-color: #ffc850;
	}

	.imput-bottom {
		position: fixed;
		bottom: 10px;
		margin-top: 200px;
		width: 95%;
		display: flex;
		justify-content: space-between;
	}
</style>
