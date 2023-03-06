<template>
	<view class="page">
		<view>
			<u-navbar title="搜索" fixed placeholder @leftClick="navigateBack"></u-navbar>
		</view>
		<view style="margin: 0 auto;text-align: center;">
			<view style="width: 80%;float: left;padding-left: 5%;">
				<u--input v-model="searchValue" placeholder="搜索商品" prefixIcon="search" border="surround" clearable></u--input>
			</view>
			<view style="float: left;width: 12%;line-height: 60rpx;" @click="GetCommodityList">搜索</view>
		</view>
		<view class="commodityList">
			<view class="commodityItem" v-for="(item,index) in commodityList" :key="index" @click="goDetial(item.id)">
				<u--image :src="item.imageUrl" width="100%" mode="aspectFit" height="250rpx"></u--image>
				<u--text style="margin-left: 15rpx;" :text="item.name+' '+item.spec"></u--text>
				<view style="display: flex;">
					<u--text style="margin-left: 15rpx;" :text="'￥'+item.price" color="red"></u--text>
					<u--text style="margin-right: 25rpx;" :text="'￥'+item.originalPrice" size="12" color="#999"
						decoration="line-through"></u--text>
				</view>
				<u--text style="margin-left: 15rpx;" :text="' 销量：'+item.sellNum" size="12"></u--text>
			</view>
		</view>
		
	</view>
</template>

<script>
	import api from './api.js'
	export default {
		data() {
			return {
				commodityList: [],
				searchValue:""
			};
		},

		methods: {
			navigateBack() {
				uni.navigateBack()
			},
			async GetCommodityList(){
				
				var result = await api.GetCommodity(this.searchValue)
				this.commodityList = result.data.datas
				this.commodityList.forEach(item => {
					item.imageUrl = api.FileById + item.imageUrl
				})
			},
			goDetial(id) {
				uni.navigateTo({
					url: "/pages/home/commodityDetail?id=" + id
				})
			}
		}
	}
</script>

<style lang="scss" scoped>
	page {}

	.box_list {
		width: 90%;
		margin: 10rpx auto;
		padding: 20rpx;
		background-color: #f0f0f0;
		border-radius: 12px;
	}
	.commodityList {
		background-color: #f9f9f9;
		display: flex;
		justify-content: space-around;
		width: 100%;
		height: 100%;
		flex-wrap: wrap;
	
		.commodityItem {
			width: 45%;
			height: 400rpx;
			background-color: #fff;
			margin: 2% 0;
			// border: 1px solid #ffc850;
			// background-color: skyblue;
		}
	}
</style>
