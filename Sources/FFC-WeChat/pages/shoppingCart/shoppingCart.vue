<template>
	<view style="height: 100%; display: flex; flex-direction: column">
		<view>
			<view style="background-color: darkorange;color:#f1f1f1;text-align:center; padding: 12px 0; ">购物车</view>
			<view class="flex flex-col page-body-tab relative" style="padding-top: 5rpx;">
				<view class="goods-item ">
					<view>
						<view class="info " v-for="(item,index) in shohppingList" :key="index"
							@click="goDetial(item.id)">
							<view style="height: 20rpx;"></view>
							<view
								style="height: 210rpx;background-color: #f1f1f1;padding-top: 20rpx;border-radius: 12px;">
								<view style=" padding-top: 75rpx;padding-left: 15rpx;">
									<u-checkbox-group v-model="checkboxValue1" placement="column"
										@change="checkboxChange">
										<u-checkbox :customStyle="{marginBottom: '2px'}"
											v-for="(item, index) in checkboxList1" :key="index" :label="item.name"
											:name="item.name">
										</u-checkbox>
									</u-checkbox-group>
								</view>

								<view style="color:dimgray; margin-top: -90rpx;padding-left: 220rpx;">
									{{item.commodityName}}
									{{item.commoditySpec}}
								</view>
								<view style="padding-left: 70rpx;margin-top: -40rpx;">
									<u--image :src="imgUrl + item.commodityImageUrl" width="140rpx" mode="aspectFit"
										height="140rpx">
									</u--image>
								</view>
								<view style="padding-left: 210rpx;margin-top: -50rpx;color: ;color: crimson;">
									￥{{item.commodityPrice}} </view>
								<view style="margin-top: -50rpx;padding-left: 65%">
									<u-number-box v-model="value" @change="valChange"></u-number-box>
								</view>
							</view>
						</view>
						<view class="foot-bar absolute flex j-s "
							style=" position: fixed;margin-bottom: 100rpx;margin-left:-11rpx ;">
							<view class="flex a-c left">
								<view style="margin-top: 16rpx;">
									<u-checkbox-group v-model="checkboxValue1" placement="column"
										@change="checkboxChange">
										<u-checkbox>
										</u-checkbox>
									</u-checkbox-group>
								</view>
								<!-- 	<u-icon class='icon c-m' name="checkmark-circle"></u-icon> -->
								<view class="label">
									全选
								</view>
							</view>
							<view class="flex right a-c">
								<view>总计:</view>
								<view class="c-m total-price">0.00</view>
								<view class="buy flex-c">立即支付</view>

							</view>
						</view>
					</view>
				</view>
			</view>
		</view>



		<!-- 底部导航栏 -->
		<view style="width: 100%; flex: none; position: fixed;bottom: 0px;">
			<c-tabbar></c-tabbar>
		</view>
	</view>
</template>

<script>
	import api from './api.js'
	export default {
		data() {
			return {
				value: 0,
				baseList: [],
				shohppingList: [],
				commodityDetails: [],
				imgUrl: api.FileById,
				// 基本案列数据
				checkboxList1: [{

					name: '',
					disabled: false
				}],
				 allCheck: {
				                    name: '全选',
				                    values: 'all',
				                    checked: true
				                },
								
			}
		},
		onLoad() {
			this.searchType()
		},
		methods: {
			valChange(e) {
				console.log('当前值为: ' + e.value)
			},
			checkboxChange(n) {
				console.log('change', n);
			},
			async searchType() {
				var result = await api.GetCommodityType()
				this.shohppingList = result.data.datas
				console.log(result);
			}
		}
	}

	// async searchType() {
	// 	var result = await api.GetCommodityType()
	// 	this.shohppingList = result.data.datas
	// 	console.log(result);
	// }
</script>

<style lang="scss">
	.goods-item {
		padding: 16rpx;

		.icon {
			font-size: 50rpx;
			padding: 0 20rpx;
		}

		image {
			height: 160rpx;
			width: 180rpx;
			border-radius: 10rpx;
		}

		.info {

			flex: 1;
			padding: 0 16rpx;
		}

		.title {
			line-height: 30rpx;
			max-height: 40rpx;
			display: -webkit-box;
			-webkit-box-orient: vertical;
			-webkit-line-clamp: 2;
			overflow: hidden;
			word-break: break-all;
		}
	}

	.j-c {
		justify-content: center;
	}

	.j-s {
		justify-content: space-between;
	}

	.a-c {
		align-items: center;
	}

	.flex-col {
		flex-direction: column;
	}

	.flex-c {
		height: 100px;
		line-height: 50px;
	}

	.a-s {
		align-content: space-between;
	}

	.c-m {
		color: #ff0000;
	}

	.flex {
		display: flex;
		flex-wrap: wrap;
	}

	.biaoti {
		font-size: 36rpx;
		color: #004ade;
	}

	.foot-bar {
		height: 100rpx;
		width: 100%;
		bottom: 0px;
		background: #fff;
		box-shadow: 0 -1rpx 2rpx #ccc;
		z-index: 9999;
	}

	.left {
		padding: 0 20rpx;
		font-weight: bold;
	}

	.label {
		font-size: 34rpx;
	}

	.icon {
		padding: 0 10rpx;
	}

	.right {
		height: 100%;

		.total-price {
			padding: 0 20rpx;

		}
	}

	.buy {
		color: #fff;
		padding: 0 40rpx;
		background: #fda900;
		height: 100%;


	}




	.total-price {
		font-size: 34rpx;
	}

	.up {
		color: #fff;
		text-align: center;
		height: 44px;
		line-height: 80rpx;
		background-color: #ffc850;
	}
</style>
