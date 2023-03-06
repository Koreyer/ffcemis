<template>
	<view style="height: 100%; display: flex; flex-direction: column">
		<view class="up">消息列表</view>
		<u-list @scrolltolower="scrolltolower" style="margin-top: 20rpx;">
			<u-list-item v-for="(item, index) in chatList" :key="index" >
				<u-cell :title="item.name" @click="gotoMessage(item)">
					<u-avatar slot="icon" shape="square" size="35" :src="item.avatar"
						customStyle="margin: -3px 5px -3px 0"></u-avatar>
				</u-cell>
			</u-list-item>
		</u-list>

		<!-- 底部导航栏 -->
		<view style="width: 100%; flex: none; position: fixed;bottom: 0px;">
			<c-tabbar></c-tabbar>
		</view>
	</view>
</template>
<!--  -->
<script>
	import api from './api.js'
	export default {
		data() {
			return {
				chatList:[]
			};
		},
		async onLoad() {
			var result = await api.GetChatList();
				
			result.data.datas.forEach(x=>{
				x.avatar = api.FileById+x.avatar
			})
			this.chatList = result.data.datas
		},
		methods: {
			scrolltolower(){
				
			},
			gotoMessage(item){
				uni.navigateTo({
					url:'./message?id='+item.id
				})
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
</style>
