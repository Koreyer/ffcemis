<template>
	<view style="height: 100%; display: flex; flex-direction: column;">
		<view class="up">农场生鲜</view>
		<view class="u-demo-block">
			<!-- 搜索 -->
			<view class="u-demo-block__content" style="margin: 15rpx;">
				<view class="u-page__tag-item">
					<u-search :disabled="true" @click="toSearch()"></u-search>
				</view>
			</view>
			<!-- 轮播 -->
			<u-swiper :list="swiperList" previousMargin="30" nextMargin="30" circular :autoplay="true" radius="5"
				bgColor="#ffffff" @click="goPageUrl"></u-swiper>
			<!-- 种类 -->
			<view>
				<u-grid :border="false" col="5">
					<u-grid-item @click="setTpyeName(baseListItem.name)"
						v-for="(baseListItem,baseListIndex) in baseList" :key="baseListIndex"
						customStyle="padding-top: 20rpx; padding-bottom: 20rpx; ">
						<u-icon :name="baseListItem.imageId" :size="60"></u-icon>
						<text style="font-size: 30rpx;margin:10rpx 0;">{{baseListItem.name}}</text>
					</u-grid-item>
				</u-grid>
			</view>
		</view>
		<!-- 列表 -->
		<!-- <view class="commodityList" :style="{'width':this.$store.screenWidth}"> -->
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
		<!-- 置顶 -->
		<view class="wrap">
			<u-back-top :scroll-top="scrollTop" top="150"></u-back-top>
		</view>
		<!-- 到底了 -->
		<view class="u-demo-block__content">
			<u-loadmore loadmoreText="没有更多了" color="#ffc850" lineColor="#1CD29B" :line="true"></u-loadmore>
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
				list: [],
				// 轮播图
				carouselList: [],
				baseList: [],
				commodityList: [],
				swiperList: [],
				scrollTop: 0,
				pageNum: 1,
				pageSize: 20,
				isType: false,
				typeName: ""

			};
		},
		onPageScroll(e) {
			this.scrollTop = e.scrollTop;
		},
		async onReachBottom() {
			this.pageNum = this.pageNum + 1;
			if (this.isType) {
				await this.searchType()
			} else {
				var result = await api.GetCommodityList(this.pageNum, this.pageSize)
				result.data.datas.forEach(item => {
					item.imageUrl = api.FileById + item.imageUrl
					this.commodityList.push(item)
				})
			}

		},
		onLoad() {
			this.GetCommodityType();
			this.GetCommodityList();
			this.GetHomePage()
		},
		methods: {
			async setTpyeName(name) {
				this.pageNum = 1;
				this.typeName = name
				this.commodityList = []
				await this.searchType()
			},
			async searchType() {
				this.isType = true
				var result = await api.GetCommodityByType(this.pageNum, this.pageSize, this.typeName)
				result.data.datas.forEach(item => {
					item.imageUrl = api.FileById + item.imageUrl
					this.commodityList.push(item)
				})

				// this
				console.log(result);
			},
			async GetCommodityType() {
				var result = await api.GetCommodityType()
				this.baseList = result.data.datas
				this.baseList.forEach(item => {
					item.imageId = api.FileById + item.imageId
				})
			},
			toSearch() {
				uni.navigateTo({
					url: "/pages/search/search"
				})
			},
			async GetCommodityList() {
				var result = await api.GetCommodityList(this.pageNum, this.pageSize)
				this.commodityList = result.data.datas
				this.commodityList.forEach(item => {
					item.imageUrl = api.FileById + item.imageUrl
				})
			},
			async GetHomePage() {
				var result = await api.GetHomePage();
				this.carouselList = result.data.datas
				this.carouselList.forEach(item => {
					this.swiperList.push(api.FileById + item.imageId)
				})
				console.log(55);
				console.log(this.carouselList);
			},
			goPageUrl(index) {
				uni.navigateTo({
					url: this.carouselList[index].url
				})
				console.log(index);
			},
			goDetial(id) {
				uni.navigateTo({
					url: "/pages/home/commodityDetail?id=" + id
				})
			}
		}
	}
</script>

<style lang="scss">
	.up {
		color: #fff;
		text-align: center;
		height: 44px;
		line-height: 80rpx;
		background-color: #ffc850;
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
