<template>
	<view class="page">
		<!-- 顶部导航 -->
		<u-navbar title="商品评价" :bgColor="bgColor" @leftClick="leftClick">
		</u-navbar>
		<view style="height: 100rpx;">
		</view>
		<!-- 评价 -->
		<view class="comment">
			<view class="comment-item" v-for="comment in commentList">
				<!-- 用户名和头像 -->
				<view class="item-title">
					<u-avatar :src="fileUrl+comment.appUserAvatar"></u-avatar>
					<view style="width: 20rpx;"></view>
					<u--text format="encrypt" mode="name" :lines="1" :text="comment.appUserName"></u--text>
					<u--text size="mini" color="#999"  :text="comment.createTime.replace('T',' ').substring(0,10)"></u--text>
				</view>
				<!-- 评价星星 -->
				<u-rate count="5" :readonly='true' v-model="comment.rate" activeColor="#ffc850"></u-rate>
				<!-- 评价 -->
				<view class="item-message">
					<u--text  :lines="5" :text="comment.message"></u--text>
				</view>
				<!-- 图片 -->
				<view class="item-images" >
					<view v-for="image in comment.imageList" style="display: flex;">
						<u--image  :src="image" width="80px" height="80px"></u--image>
						<!-- 间隔 -->
						<view style="width: 10rpx;height: 10rpx;"></view>
					</view>
				</view>
			</view>
		</view>
		
		
	</view>
</template>

<script>
	import api from './api.js'
	export default {
		data() {
			return {
				bgColor: '#ffc850',
				commodityId:"",
				pageNum: 1,
				pageSize: 600,
				commentList:[],
				commentUrl:[],
				fileUrl:api.FileById
			}
		},
		onLoad(data) {
			this.commodityId=data.id;
			this.searchType()
		},
		methods: {
			leftClick() {
				uni.navigateBack()
			},
			async searchType() {
				this.isType = true
				var result = await api.GetCommentList(this.pageNum, this.pageSize, this.commodityId)
				result.data.datas.forEach(item=>{
					var images = item.imageList.split(",")
					item.imageList = []
					images.forEach(image=>{
						item.imageList.push(api.FileById+image)
					})
				})
				this.commentList = result.data.datas
				console.log(result.data.datas);
			},
		}
	}
</script>

<style lang="scss">
.comment{
	width: 90%;
	margin: auto;
	.comment-item{
		background-color: #fff;
		padding: 10rpx;
		border-radius: 10px;
		.item-title{
			width: 80%;
			display: flex;
			justify-content: space-between;
		}
		.item-message{
			margin: 10rpx 0;
		}
		.item-images{
			width: 80%;
			display: flex;
			
		}
	}
	
}
.page{
	height: 100vh;
	background-color: #f9f9f9;
}
</style>
