<template>
	<view style="height: 100%; display: flex; flex-direction: column">
		<!-- 用户 -->
		<view class="u-demo-block__content">
			<view class="background-color">
				<view @click="toUpload()">
					<view class="u-avatar-item">
						<u-avatar :src="identityManagerList.avatar" shape="circle" size="100"></u-avatar>
					</view>
					<view class="u-avatar-item">
						<text class="u-avatar-item-text">{{identityManagerList.userName}}</text>
					</view>
				</view>
				<view class="u-avatar-item-icon">
					<u-icon name="chat" color="#fff" size="35" @click="toChat()"></u-icon>
				</view>
				<view class="u-avatar-item-icon">
					<u-icon name="setting" color="#fff" size="35" @click="toSetting()"></u-icon>
				</view>
			</view>
		</view>

		<!-- 商品分类推荐 -->
		<view class="box">
			<!-- 订单 -->
			<view class="u-page">
				<view class="more">
					<view class="u-page__item" @click="goOrder()">
						<u-cell title="我的订单" value="全部订单" size="large" :border="false" :isLink="true"></u-cell>
					</view>
				</view>
				<view class="u-demo-block">
					<u-grid :border="false" col="4">
						<u-grid-item v-for="(baseListItem,baseListIndex) in baseList" :key="baseListIndex" @click="onClick"
							customStyle="padding-top: 20rpx; padding-bottom: 20rpx; ">
							<u-icon :name="baseListItem.src" :size="38"></u-icon>
							<text class="grid-text">{{baseListItem.title}}</text>
						</u-grid-item>
					</u-grid>
				</view>
			</view>
		</view>
		
		<!-- 图片 -->
		<view class="image">
			<u--image :showLoading="true" :src="src1" width="100%" height="120px" @click="click"></u--image>
			<u-row customStyle="margin: 10px" justify="space-between" gutter="10">
				<u-col span="6">
					<u--image radius="10" :showLoading="true" :src="src2" width="100%" height="80px" @click="click">
					</u--image>
			
				</u-col>
				<u-col span="6">
					<u--image radius="10" :showLoading="true" :src="src3" width="100%" height="80px" @click="click">
					</u--image>
			
				</u-col>
			</u-row>
		</view>
		
		<!-- 为你推荐 -->
		<view>
			<view class="u-demo-block__content">
				<u-loadmore loadmoreText="为您推荐" color="#000" lineColor="#1CD29B" :line="true"></u-loadmore>
			</view>
		</view>
		<!-- 商品列表 -->
		<!-- <view class="commodityList" :style="{'width':this.$store.screenWidth}"> -->
		<view class="commodityList">
			<view class="commodityItem" v-for="(item,index) in commodityList" :key="index" @click="toDetail(item.id)">
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
		<!-- 置顶 -->
		<view class="wrap">
			<u-back-top :scroll-top="scrollTop" top="150"></u-back-top>
		</view>
		<!-- 我是有底线的 -->
		<view class="u-demo-block__content">
			<u-loadmore loadmoreText="我是有底线的" color="#1CD29B" lineColor="#1CD29B" :line="true"></u-loadmore>
		</view>
		<view style="padding-top: 100rpx;"></view>
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
				src: 'https://cdn.uviewui.com/uview/album/3.jpg',
				src3: '/static/images/home/lunbo1.jpg',
				src2: '/static/images/home/lunbo4.jpg',
				src1: '/static/images/home/lunbo6.jpg',
				value: 100,
				commodityList: [],
				scrollTop: 0,
				pageNum: 1,
				pageSize: 6,
				identityManagerList: [],
				baseList: [{
						src: '/static/order/Payment.png',
						title: '待付款'
					},
					{
						src: '/static/order/Receipt.png',
						title: '待发货'
					},
					{
						src: '/static/order/Shipment.png',
						title: '待收货'
					},
					{
						src: '/static/order/After-sales.png',
						title: '售后'
					},
				],
			};
		},
		onPageScroll(e) {
			this.scrollTop = e.scrollTop;
		},
		async onReachBottom() {
			this.pageNum = this.pageNum + 1;
			var result = await api.GetCommodityList(this.pageNum, this.pageSize)
			result.data.datas.forEach(item => {
				item.imageUrl = api.FileById + item.imageUrl
				this.commodityList.push(item)
			})

		},
		onLoad() {
			console.log(this.$store.state);
			this.GetCommodityList()
			this.GetIdentityManager()
		},
		methods: {
			async GetIdentityManager() {
				var result = await api.GetIdentityManager()
				this.identityManagerList = result.data.data
				this.identityManagerList.avatar = api.FileById+this.identityManagerList.avatar
				
				console.log(123 + result)
				console.log(this.identityManagerList);
			},
			async GetCommodityList() {
				var result = await api.GetCommodityList(this.pageNum, this.pageSize)
				this.commodityList = result.data.datas
				this.commodityList.forEach(item => {
					item.imageUrl = api.FileById + item.imageUrl
				})
				console.log(1);
				console.log(this.commodityList);
			},
			onClick(index) {
				console.log(index);
				uni.navigateTo({
					url: '/pages/order/order?index=' + index,
				})
			},
				
			toUpload(){
				uni.navigateTo({
					url: '/pages/user/setting/personal'
				})
			},
			toSetting() {
				uni.navigateTo({
					url: '/pages/user/setting/setting'
				})
			},
			toChat() {
				uni.switchTab({
					url:'/pages/chat/chat'
				})
			},
			goOrder() {
				uni.navigateTo({
					url: '/pages/order/order?index=' + 4
				})
			},

			toDetail(id) {
				uni.navigateTo({
					url: "/pages/home/commodityDetail?id="+id
				})
			}
		},
	}
</script>

<style lang="scss">
	.page {
		margin: 0 auto;
	}

	.background-color {
		width: 100%;
		height: 450rpx;
		background-color: #ffc850;
		border-radius: 0 0 15% 15%;
	}

	.u-avatar-item {
		margin-right: 30rpx;
		margin: 80rpx 30rpx;
		float: left;
	}

	.u-avatar-item-text {
		height: 200rpx;
		line-height: 200rpx;
	}

	.u-avatar-item-icon {
		float: right;
		margin-top: 80rpx;
		margin-right: 20rpx;
	}

	.box {
		height: 150Px;
		width: 90%;
		background-color: papayawhip;
		border-radius: 30rpx;
		position: absolute;
		top: 330rpx;
		left: 5%;
	}

	.more {
		margin: 0 auto;
		border-bottom: 1rpx solid #909399;
	}

    .image{
		margin-top: 250rpx;
	}
	.grid-text {
		font-size: 14px;
		color: #909399;
		padding: 10rpx 0 20rpx 0rpx;

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
