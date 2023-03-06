<template>
	<view class="page">
		<!-- 导航栏 -->
		<view>
			<u-navbar title="订单" fixed placeholder @leftClick="navigateBack"></u-navbar>
		</view>
		<!-- 搜索框 -->
		<view style="width: 92%;margin: 10rpx auto;" @click="goSelect()">
			<u--input placeholder="搜索订单" prefixIcon="search" border="surround" clearabl></u--input>
		</view>
		<!-- Tabs 标签 -->
		<view>
			<u-tabs :id="tabIndex" :list="list" :current="tabIndex" :is-scroll="false" @change="onclick"
				:inactiveStyle="{color: '#606266',}"
				itemStyle="padding-left: 34rpx; padding-right: 34rpx; height: 34px;">
			</u-tabs>
		</view>
		<!-- 显示订单列表块 -->
		<view class="box_list" v-if="tabIndex == 0" @click="GetOrderByMySelf()">
			<view class="list-a">
				<view class="u-page__item" style="height: 300rpx;" v-for="(item,index) in baseList" :key="index">
					<u-icon label="天天有鲜店" name="/static/logo1.png"></u-icon>
					<view class="uni-title uni-common-mt">
						<view class="_u">
							<image :src="item.commodityImageUrl" style="width: 120rpx;height: 120rpx;"></image>
						</view>
						{{item.commodityName}}&nbsp;&nbsp;&nbsp;￥{{item.commodityPrice}}
						<text>\n{{item.commoditySpec}}</text>
					</view>
					<view style="height: 20rpx;"></view>
					<view style="text-align: center;" @click="goOorderDetail(item.id)">
						查看详细
					</view>
					<view
						style="float: right;ont-size: 14px;color: #2eaa00ff;border: 1px solid #2eaa00ff;border-radius: 12px;width: 160rpx;text-align: center;margin-right: 5px;"
						@click="showModalType1(item)">
						确认付款
					</view>
				</view>
			</view>
		</view>

		<view class="box_list" v-if="tabIndex == 1" @click="GetOrderByMySelf()">
			<view class="list-a">
				<view class="u-page__item" style="height: 300rpx;" v-for="(item,index) in baseList" :key="index">
					<u-icon label="天天有鲜店" name="/static/logo1.png"></u-icon>
					<view class="uni-title uni-common-mt">
						<view class="_u">
							<image :src="item.commodityImageUrl" style="width: 120rpx;height: 120rpx;"></image>
						</view>
						{{item.commodityName}}&nbsp;&nbsp;&nbsp;￥{{item.commodityPrice}}
						<text>\n{{item.commoditySpec}}</text>
					</view>
					<view style="height: 20rpx;"></view>
					<view style="text-align: center;" @click="goOorderDetail(item.id)">
						查看详细
					</view>
					<view
						style="float: right;ont-size: 14px;color: #dace1dff;border: 1px solid #dace1dff;border-radius: 12px;width: 180rpx;text-align: center;margin-right: 5px;"
						@click="showModalType2(item)">
						确认已发货
					</view>
				</view>
			</view>
		</view>

		<view class="box_list" v-if="tabIndex == 2" @click="GetOrderByMySelf()">
			<view class="list-a">
				<view class="u-page__item" style="height: 300rpx;" v-for="(item,index) in baseList" :key="index">
					<u-icon label="天天有鲜店" name="/static/logo1.png"></u-icon>
					<view class="uni-title uni-common-mt">
						<view class="_u">
							<image :src="item.commodityImageUrl" style="width: 120rpx;height: 120rpx;"></image>
						</view>
						{{item.commodityName}}&nbsp;&nbsp;&nbsp;￥{{item.commodityPrice}}
						<text>\n{{item.commoditySpec}}</text>
					</view>
					<view style="height: 20rpx;"></view>
					<view style="text-align: center;" @click="goOorderDetail(item.id)">
						查看详细
					</view>
					<view
						style="float: right;ont-size: 14px;color: #aa0000ff;border: 1px solid red;border-radius: 12px;width: 160rpx;text-align: center;margin-right: 5px;"
						@click="showModalType3(item)">
						确认收货
					</view>
				</view>
			</view>
		</view>

		<view class="box_list" v-if="tabIndex == 3" @click="GetOrderByMySelf()">
			<view class="list-b">
				<view class="u-page__item" v-for="(item,index) in baseList" :key="index"
					@click="goOorderDetail(item.id)">
					<u-icon label="天天有鲜店" name="/static/logo1.png"></u-icon>
					<view class="uni-title uni-common-mt">
						<view class="_u">
							<image :src="item.commodityImageUrl" style="width: 120rpx;height: 120rpx;"></image>
						</view>
						{{item.commodityName}}&nbsp;&nbsp;&nbsp;￥{{item.commodityPrice}}
						<text>\n{{item.commoditySpec}}</text>
					</view>
					<view class="b_end">
						查看详情
					</view>
				</view>
			</view>
		</view>

		<view class="box_list" v-if="tabIndex == 4" @click="GetOrderByMySelf()">
			<view class="list-a">
				<view class="u-page__item" v-for="(item,index) in baseList" :key="index"
					@click="goOorderDetail(item.id)">
					<u-icon label="天天有鲜店" name="/static/logo1.png"></u-icon>
					<view class="uni-title uni-common-mt">
						<view class="_u">
							<image :src="item.commodityImageUrl" style="width: 120rpx;height: 120rpx;"></image>
						</view>
						{{item.commodityName}}&nbsp;&nbsp;&nbsp;￥{{item.commodityPrice}}
						<text>\n{{item.commoditySpec}}</text>
					</view>
					<view class="end">
						{{item.orderStatusName}}￥{{item.commodityPrice}}
					</view>
					<view v-if="item.orderStatus == 0">
						<view
							style="float: right;ont-size: 14px;color: #2eaa00ff;border: 1px solid #2eaa00ff;border-radius: 12px;width: 160rpx;text-align: center;margin-right: 5px;"
							@click="showModalType1(item)">
							确认付款
						</view>
					</view>
					<view v-if="item.orderStatus == 1">
						<view
							style="float: right;ont-size: 14px;color: #dace1dff;border: 1px solid #dace1dff;border-radius: 12px;width: 180rpx;text-align: center;margin-right: 5px;"
							@click="showModalType2(item)">
							确认已发货
						</view>
					</view>
					<view v-if="item.orderStatus == 2">
						<view
							style="float: right;ont-size: 14px;color: #aa0000ff;border: 1px solid red;border-radius: 12px;width: 160rpx;text-align: center;margin-right: 5px;"
							@click="showModalType3(item)">
							确认收货
						</view>
					</view>
				</view>
			</view>
		</view>
		<view style="padding-top: 40rpx;"></view>
	</view>
</template>

<script>
	import api from './api.js'
	export default {
		data() {
			return {
				height: "", // 需要固定swiper的高度
				list: [{
					name: '待付款',
				}, {
					name: '待发货'
				}, {
					name: '待收货'
				}, {
					name: '售后'
				}, {
					name: '全部',
				}],
				baseList: [],
				tabIndex: 0,
				scrollTop: 0,
				pageNum: 1,
				pageSize: 5,
				isType: false,
				show1: false,
				show2: false,
				show3: false,
			};
		},
		async onReachBottom() {
			this.pageSize = this.pageSize + 1
			if (this.isType) {
				this.GetOrderByMySelf()
			} else {
				var result = await api.GetOrderByMySelf(this.pageNum, this.pageSize, this.tabIndex)
				result.data.datas.forEach(item => {
					item.commodityImageUrl = api.FileById + item.commodityImageUrl
					this.baseList.push(item)
				})
			}

		},
		onLoad(options) {
			this.tabIndex = options.index
			this.GetOrderByMySelf()
		},
		methods: {
			navigateBack() {
				uni.navigateBack()
			},
			async GetOrderByMySelf() {
				this.isType = true
				var result = await api.GetOrderByMySelf(this.pageNum, this.pageSize, this.tabIndex)
				this.baseList = result.data.datas
				this.baseList.forEach(item => {
					item.commodityImageUrl = api.FileById + item.commodityImageUrl
				})
			},
			onclick(index) {
				this.tabIndex = index.index
				this.GetOrderByMySelf()
			},
			goOorderDetail(id) {
				uni.navigateTo({
					url: '/pages/order/orderDetail?id=' + id
				})
			},
			goSelect() {
				uni.navigateTo({
					url: '/pages/order/select'
				})
			},
			async showModalType1(data) {
				let that = this
				uni.showModal({
					title: '提示',
					content: '是否确认付款？',
					success: async function(res) {
						if (res.confirm) {
							data.orderStatus = 1
							await api.updateType(data);
							that.GetOrderByMySelf()
						} else {
							return;
						}
					}
				})
			},
			async showModalType2(data) {
				let that = this
				uni.showModal({
					title: '提示',
					content: '是否确认已发货？',
					success: async function(res) {
						if (res.confirm) {
							data.orderStatus = 2
							await api.updateType(data);
							that.GetOrderByMySelf()
						} else {
							return;
						}
					}
				})
			},
			async showModalType3(data) {
				let that = this
				uni.showModal({
					title: '提示',
					content: '是否确认收货？',
					success: async function(res) {
						if (res.confirm) {
							data.orderStatus = 3
							await api.updateType(data);
							that.GetOrderByMySelf()
						} else {
							return;
						}
					}
				})
			},
		}
	}
</script>

<style lang="scss" scoped>
	page {}

	.box_list {
		width: 100%;
		padding-bottom: 1px;
		//background-color: #f0f0f0ff;
	}

	.list-a {
		width: 95%;
		margin: 10px 0 10px 10px;
		padding-top: 10px;
	}

	.list-a .u-page__item {
		background-color: #ffffffff;
		border-radius: 12px;
		margin-bottom: 10px;
		padding: 10px 10px 0 10px;
		height: 120px;
		box-shadow: 0 0 10px 1px #d9d9d9;
	}

	.list-a .uni-title {
		padding: 10px 0 20px 0;
	}

	.list-a .uni-title text {
		font-size: 10px;
		color: #686868;
	}

	.list-a .uni-title ._u {
		float: left;
		padding-right: 20px;
	}

	.list-a .end {
		float: left;
		color: red;
		font-size: 14px;
	}

	.list-b {
		width: 95%;
		margin: 10px 0 10px 10px;
		padding-top: 10px;
	}

	.list-b .u-page__item {
		background-color: #ffffffff;
		border-radius: 12px;
		margin-bottom: 10px;
		padding: 10px 10px 0 10px;
		height: 300rpx;
		box-shadow: 0 0 10px 1px #d9d9d9;
	}

	.list-b .uni-title {
		padding: 10px 0 20px 0;
	}

	.list-b .uni-title text {
		font-size: 10px;
		color: #686868;
	}

	.list-b .uni-title ._u {
		float: left;
		padding-right: 20px;
	}

	.list-b ._b {
		background-color: #f8f8f8ff;
		width: 85%;
		height: 40rpx;
		line-height: 40rpx;
		text-align: center;
		margin: 15px 0 20px 25px;
	}

	.list-b .b_end {
		float: right;
		font-size: 14px;
		color: #aa0000ff;
		border: 1px solid red;
		border-radius: 12px;
		width: 160rpx;
		text-align: center;
		margin-right: 5px;
	}

	.list-b .b_dle {
		float: right;
		font-size: 14px;
		color: #aa0000ff;
		border: 1px solid red;
		border-radius: 12px;
		width: 160rpx;
		text-align: center;
	}
</style>
