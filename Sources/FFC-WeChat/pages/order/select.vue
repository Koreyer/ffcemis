<template>
	<view class="page">
		<!-- 导航栏 -->
		<view>
			<u-navbar title="订单搜索" fixed placeholder @leftClick="navigateBack"></u-navbar>
		</view>
		<!-- 搜索框 -->
		<view style="width: 92%; display: flex;margin: 20rpx auto;">
			<view style="width: 85%;">
				<u--input v-model="selectName" placeholder="搜索订单" prefixIcon="search" border="surround" clearabl>
				</u--input>
			</view>
			<view style="width: 15%;">
				<u-button style="line-height: 60rpx;" text="搜索" @click="GetOrderByMySelf"></u-button>
			</view>
		</view>
		<!-- 显示订单列表块 -->
		<view class="box_list">
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
				</view>
			</view>
		</view>
		<view v-if="totalCount ==0" style="margin: 50rpx auto;text-align: center;color: gray;">
			此搜索关键词，未搜索到订单
		</view>
	</view>
</template>

<script>
	import api from './api.js'
	export default {
		data() {
			return {
				height: "", // 需要固定swiper的高度
				baseList: [],
				selectName: "",
				scrollTop: 0,
				pageNum: 1,
				pageSize: 5,
				totalCount: 1,
				isType: false,
			};
		},
		async onReachBottom() {
			this.pageSize = this.pageSize + 1
			if (this.isType) {
				this.GetOrderByMySelf()
			} else {
				var result = await api.selectData(this.pageNum, this.pageSize, this.selectName)
				this.totalCount = result.data.totalCount
				this.baseList = result.data.datas
				this.baseList.forEach(item => {
					item.commodityImageUrl = api.FileById + item.commodityImageUrl
				})
			}

		},
		onLoad() {},
		methods: {
			navigateBack() {
				uni.navigateBack()
			},
			async GetOrderByMySelf() {
				this.isType = true
				var result = await api.selectData(this.pageNum, this.pageSize, this.selectName)
				this.totalCount = result.data.totalCount
				this.baseList = result.data.datas
				console.log(this.baseList)
				this.baseList.forEach(item => {
					item.commodityImageUrl = api.FileById + item.commodityImageUrl
				})
			},
			goOorderDetail(id) {
				uni.navigateTo({
					url: '/pages/order/orderDetail?id=' + id
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
		float: right;
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
	}
</style>
