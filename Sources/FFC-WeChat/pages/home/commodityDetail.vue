<template>
	<view class="body">
		<u-navbar title="商品详情" :bgColor="bgColor" @leftClick="leftClick">
		</u-navbar>
		<view style="height: 86rpx;">

		</view>
		<!-- 轮播 -->
		<u-swiper height="800rpx" :list="carousels" circular indicator></u-swiper>
		<!-- 商品信息 -->
		<view class="commodity ">
			<view class="white">
				<u--text style="margin: 20rpx 20rpx;" :text="'￥'+commodityDetail.price" color="red" size="20"></u--text>
				<view style="margin:0 20rpx;">
					<u--text :text="commodityDetail.name+' '+commodityDetail.spec+' '+commodityDetail.description"
						:bold="true">
					</u--text>
					<u--text style="margin-top: 20rpx;"
						:text="'库存：'+commodityDetail.stock+'  销售：'+commodityDetail.sellNum">
					</u--text>
					<!-- 灰色的产品信息 -->
					<view class="gray">
						<view class="graySpec">
							<u--text :text="commodityDetail.introduce" size="14"></u--text>
							<u--text text="推荐理由" size="10" color="#999"></u--text>
						</view>
						<view class="graySpec">
							<u--text :text="commodityDetail.warrantyTime" size="14"></u--text>
							<u--text text="保质期" size="10" color="#999"></u--text>
						</view>
						<view class="graySpec">
							<u--text :text="commodityDetail.producer" size="14"></u--text>
							<u--text text="产地" size="10" color="#999"></u--text>
						</view>
						<view class="graySpec">
							<u--text :text="commodityDetail.spec" size="14"></u--text>
							<u--text text="规格" size="10" color="#999"></u--text>
						</view>
					</view>
					<br>
				</view>
			</view>
			<!-- 保障 -->
			<view class="image666 white">
				<view class="detail">
					<view class="detailItemLeft" style="width: 10%;">
						自提
					</view>
					<view class="detailItemRight" style="width: 90%;">
						柳州职业技术学院竞择楼，明天16:00提货
					</view>
				</view>
				<view class="detail">
					<view class="detailItemLeft" style="width: 10%;">
						保障
					</view>
					<view class="detailItemRight" style="width: 90%;">
						不支持七天无理由
					</view>
				</view>
			</view>
			<!-- 用户评价 -->
			<view class="evaluate white" @click="GotoComment">
				<u--text text="用户评价" :bold="true"></u--text>
				<view style="width: 25%;font-size: 14px;">
					<span style="color: #808080;">好评 </span>&nbsp;<span style="color: red;">{{comment}}% ></span>
				</view>
			</view>
			<!-- 商品详情 -->
			<view class="image666 white" style="padding-top: 20rpx;">
				<u--text text="商品详情" :bold="true"></u--text>
				<view class="detail">
					<view class="detailItemLeft">
						产地
					</view>
					<view class="detailItemRight">
						{{commodityDetail.producer}}
					</view>
				</view>
				<view class="detail">
					<view class="detailItemLeft">
						规格
					</view>
					<view class="detailItemRight">
						{{commodityDetail.spec}}
					</view>
				</view>
				<view class="detail">
					<view class="detailItemLeft">
						保质期
					</view>
					<view class="detailItemRight">
						{{commodityDetail.warrantyTime}}
					</view>
				</view>
				<view class="detail">
					<view class="detailItemLeft">
						储存方式
					</view>
					<view class="detailItemRight">
						{{commodityDetail.storageMode}}
					</view>
				</view>
				<!-- 详情图 -->
				<!-- <u-list>
					<u-list-item sty v-for="(item,index) in descriptionImageUrl" :key="index">
						<u--image :src="item" width="100%"   mode="widthFix"></u--image>
					</u-list-item>
				</u-list> -->
				
				
				
				<!-- <view v-for="(item,index) in descriptionImageUrl" :key="index">
					<u--image :src="item" height="100%" width="100%" mode="widthFix"></u--image>
				</view> -->
				
				<view style="margin-top: 100rpx;">
					<u-list @scrolltolower="scrolltolower" width="100%" height="1150rpx">
						<u-list-item v-for="(item, index) in descriptionImageUrl" :key="index">
							<u--image :src="item" height="100%" width="100%" mode="widthFix"></u--image>
						</u-list-item>
					</u-list>
				</view>
				<view style="height: 100rpx;"></view>
			</view>
			<view class="button-bottom">
				<u-button class="button-item" text="加入购物车" color="linear-gradient(to right, rgb(254, 201, 1), rgb(254, 150, 1))" @click="addShop" ></u-button>
				<u-button class="button-item"  text="立即购买" color="linear-gradient(to right, rgb(254, 120, 1), rgb(255, 74, 1))" @click="addOrder"></u-button>
			</view>
		</view>
		<u-toast ref="uToast"></u-toast>
	</view>
</template>

<script>
	import {
		callWithErrorHandling
	} from "vue"
	import api from './api.js'
	export default {
		data() {
			return {
				bgColor: '#ffc850',
				carousels: [],
				descriptionImageUrl: [],
				commodityId: "8f667664-4e38-48c0-4b83-08dab72c05f7",
				commodityDetail: {},
				comment: ""
			}
		},
		// 生命周期？？？
		async onLoad(data) {
			this.commodityId = data.id
			this.GetCommodity(data.id)
			api.AddCommodityViewNum(data.id)
		},
		methods: {
			async addShop(){
				var result = await  api.AddShoppingCar(this.commodityId);
				if(result.statusCode == 200){
					this.$refs.uToast.show({
						type:"success",
						message:"加入购物车成功！"
					})
				}
				console.log(result)
			},
			async addOrder(){
				var result = await  api.AddOrder(this.commodityId);
				if(result.statusCode == 200){
					this.$refs.uToast.show({
						type:"success",
						message:"购买成功！"
					})
					uni.navigateTo({
						url:"../order/order?index=0"
					})
				}
			},
			scrolltolower(){},
			GotoComment() {
				console.log(5);
				uni.navigateTo({
					url: '/pages/home/comment?id=' + this.commodityId
				})
			},
			leftClick() {
				uni.navigateBack()
			},
			// 获取商品信息
			async GetCommodity(id) {
				var result = await api.GetCommodity(id)
				console.log(this.commodityId);
				this.commodityDetail = result.data
				// 获取轮播图
				var cars = result.data.carousels.split(",")
				cars.forEach(item => {
					this.carousels.push(api.FileById + item)
				})
				// 获取详情图
				var descriptionImage = result.data.descriptionImageUrl.split(",")
				descriptionImage.forEach(item => {
					this.descriptionImageUrl.push(api.FileById + item)
				})
				//获取评论
				this.comment = (await api.GetComment(id)).data
			}
		}
	}
</script>

<style lang="scss">
	.button-bottom{
		position: fixed;
		bottom: 5px;
		margin-top: 200px;
		width: 97%;
		height: 70rpx;
		display: flex;
		justify-content: space-between;
	}
	
	.body {
		background-color: #f7f7f7;
	}

	.gray {
		background-color: #f7f7f7;
		border-radius: 10px;
		padding: 10rpx;
		margin-top: 10rpx;
		display: flex;
		justify-content: start;

		.graySpec {
			padding: 0 20rpx;
		}
	}

	.white {
		background-color: white;
		border-radius: 10px;
	}

	.commodity {
		padding: 0 10rpx;
	}

	.evaluate {
		display: flex;
		justify-content: space-between;
		margin-top: 10rpx;
	}

	.image666 {
		margin-top: 10rpx;


		.detail {
			display: flex;
			flex-direction: row;
			justify-content: space-between;
			width: 100%;
			margin: 20rpx 0 0 0;

			.detailItemLeft {
				width: 30%;
				color: #808080;
				font-size: 14px;
			}

			.detailItemRight {
				width: 70%;
				font-size: 14px;
			}
		}
	}
</style>
