<template>
	<view style="height: 100%; display: flex; flex-direction: column">
		<!-- 导航栏 -->
		<view>
			<u-navbar title="商品管理" fixed placeholder @leftClick="navigateBack"></u-navbar>
		</view>
		<!-- 搜索框 -->
		<view style="width: 90%;margin: 10rpx auto;">
			<u--input placeholder="搜索商品" prefixIcon="search" border="surround" clearable></u--input>
		</view>
		<!-- 显示订单列表块 -->
		<view class="box_list">
			<view style="padding: 10rpx;">
				<view style="font-size: 14px;">白菜吧</view>
				<view style="font-size: 14px;">
					<view style="float: left;">价格：99</view>
				</view>
				<view style="padding-top: 10rpx;">
					<view style="float: right;padding-left: 20rpx;">编辑</view>
					<view style="width: 120rpx;float: right;">删除</view>
				</view>
			</view>
		</view>
		<view style="padding-bottom: 20rpx;"></view>
		<view style="width: 100%; flex: none; position: fixed;bottom: 0px;">
			<view style="width: 95%;margin: 0 auto;text-align: center;">
				<view style="width: 95%;background-color: coral;padding: 15rpx;border-radius: 15px;color: #fff;">
					+ 添加商品
				</view>
			</view>
			<view style="padding-top: 20rpx;"></view>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {};
		},

		methods: {
			navigateBack() {
				uni.navigateBack()
			},
			change(index) {
				this.current = index;
				console.log(this.current)
			}
		}
	}
</script>

<style lang="scss" scoped>
	page {}

	.box_list {
		width: 90%;
		margin: 20rpx auto;
		padding: 20rpx;
		background-color: #f0f0f0;
		border-radius: 12px;
	}
</style>
