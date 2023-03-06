<template>
	<view class="page">
		<!-- 导航栏 -->
		<view>
			<u-navbar title="详情" fixed placeholder @leftClick="navigateBack"></u-navbar>
		</view>

		<!-- 详情内容 -->
		<view style="background-color: #ffffff;margin: 10rpx;border-radius: 10px;">
			<view style="padding: 20rpx;">
				<view class="_t" style="padding: 20rpx;">
					<u-icon label="天天有鲜店" name="/static/logo1.png"></u-icon>
				</view>
				<view>
					<view>
						<image :src="list.commodityImageUrl" style="width: 200rpx;height: 200rpx;"></image>
					</view>
					<view style="font-weight: bold;font-size: 18px;">
						{{list.commodityName}}
					</view>
				</view>
				<view style="height: 50rpx;line-height: 50rpx;">
					<view style="float: left;">
						<text>{{list.commoditySpec}}</text>
					</view>
					<view style="color: green;float: right;">
						{{list.orderStatusName}}
					</view>
				</view>
			</view>
			<view style="margin: 20rpx;">
				<view style="height: 60rpx;">
					<view style="width:25%;float: left;text-align: right;">单价：</view>
					<view style="width:70%;float: right;text-align: right;">{{list.commodityPrice}}</view>
				</view>
				<view style="height: 60rpx;">
					<view style="width:25%;float: left;text-align: right;">商品数量：</view>
					<view style="width:70%;float: right;text-align: right;">{{list.commodityCount}}</view>
				</view>
				<view style="height: 60rpx;">
					<view style="width:25%;float: left;text-align: right;">商品总价：</view>
					<view style="width:70%;float: right;text-align: right;">
						{{list.commodityPrice * list.commodityCount}}
					</view>
				</view>
				<view style="height: 60rpx;">
					<view style="width:25%;float: left;text-align: right;">实付款：</view>
					<view style="width:70%;float: right;text-align: right;">{{list.totalPrice}}</view>
				</view>
				<view style="height: 60rpx;">
					<view style="width:25%;float: left;text-align: right;">订单编号：</view>
					<view style="width:70%;float: right;text-align: right;">{{list.orderNumber}}</view>
				</view>
				<view style="height: 60rpx;">
					<view style="width:25%;float: left;text-align: right;">手机号：</view>
					<view style="width:70%;float: right;text-align: right;">{{list.addressPhone}}</view>
				</view>
				<view style="height: 60rpx;">
					<view style="width:25%;float: left;text-align: right;">收货地址：</view>
					<view style="width:70%;float: right;text-align: right;">{{list.addressLocation}}</view>
				</view>
			</view>
			<view style="padding-top: 30rpx;"></view>
		</view>
		<view style="width: 100%; flex: none; position: fixed;bottom: 0px;" v-if="list.orderStatusName == '已收货'">
			<view style="background-color: #ffaa00;padding: 30rpx;text-align: center;" @click="deleteData()">
				删除订单
			</view>
		</view>
	</view>
</template>

<script>
	import api from './api.js'
	export default {
		data() {
			return {
				dataId: "",
				list: [],
				url: api.FileById,
			};
		},
		onLoad(options) {
			this.dataId = options.id
			this.getData()
		},
		methods: {
			navigateBack() {
				uni.navigateBack()
			},
			async getData() {
				var result = await api.GetIdByData(this.dataId)
				this.list = result.data
				this.list.commodityImageUrl = api.FileById + this.list.commodityImageUrl
			},
			async deleteData() {
				let that = this
				uni.showModal({
					title: '提示',
					content: '是否确认删除？',
					success: async function(res) {
						if (res.confirm) {
							api.deleteData(that.dataId)
							uni.navigateBack()
						} else {
							return;
						}
					}
				})
			}
		}
	}
</script>

<style lang="scss" scoped>
	page {
		background-color: #ebebeb;
	}
</style>
