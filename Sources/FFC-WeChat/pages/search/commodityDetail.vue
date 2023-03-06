<template>
	<view class="">
		<u-navbar title="商品详情" @leftClick="leftClick">
		</u-navbar>
		<view style="height: 100rpx;">

		</view>
		<!-- 轮播 -->
		<!-- <u-swiper :list="list3" previousMargin="30" nextMargin="30" circular :autoplay="true" radius="5"
			bgColor="#ffffff">
		</u-swiper> -->
		<u-swiper :list="list3" circular indicator height="300"  @click="click"></u-swiper>
		<u--text style="margin: 20rpx 20rpx;" :text="'￥'+commodityDetail.price" color="red" size="20"></u--text>
		<view style="margin:0 20rpx;">
			<u--text :text="commodityDetail.name+' '+commodityDetail.spec+' '+commodityDetail.description" :bold="true">
			</u--text>
			<u--text style="margin-top: 20rpx;" :text="'库存：'+commodityDetail.stock+'  销售：'+commodityDetail.sellNum">
			</u--text>
		</view>
	</view>
</template>

<script>
	import api from './api.js'
	export default {
		data() {
			return {
				list3: [
					'/static/images/home/jd.jpg',
					'/static/images/home/jd1.jpg',
					'/static/images/home/jd2.jpg',
				],
				commodityId: "8f667664-4e38-48c0-4b83-08dab72c05f7",
				commodityDetail: {}
			}
		},
		async onLoad(data) {
			// this.commodityId = data.id
			this.GetCommodity()
		},
		methods: {
			leftClick() {
				uni.navigateBack()
			},
			async GetCommodity() {
				var result = await api.GetCommodity(this.commodityId)
				console.log(this.commodityId);
				this.commodityDetail = result.data
				console.log(result.data);
			}
		}
	}
</script>

<style>
</style>
