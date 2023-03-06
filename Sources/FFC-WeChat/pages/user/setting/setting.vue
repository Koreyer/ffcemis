<template>
	<view class="page">
		<view class="container">
			<u-navbar
				title="设置"
				@leftClick="navigateBack"
				safeAreaInsetTop
				fixed
				placeholder
			></u-navbar>
			<view class="cell-page">
				<view class="u-page__item">
					<u-cell title="个人信息" value="" size="large" isLink url="/pages/user/setting/personal"></u-cell>
					<u-cell title="我的收货地址" value="" size="large" :border="false" isLink url="/pages/address/address"></u-cell>
				</view>
				<view class="u-page__item">
					<u-cell title="账号与安全" value="" size="large" :isLink="true"></u-cell>
					<u-cell title="帮助与反馈" value="" size="large" :isLink="true"></u-cell>
					<u-cell title="关于平台" value="" size="large" :border="false" :isLink="true"></u-cell>
				</view>
			</view>
			<view style="padding: 20px;">
				
				<u-button text="退出登录" type="primary" @click="gotoReLogin"></u-button>
				
				</view>
		</view>
	</view>

</template>

<script>
	export default {
		data() {
			return {
				
			};
		},
			
		methods:{
			navigateBack() {
				uni.navigateBack()
			},
			
			gotoReLogin(){
				uni.navigateTo({
					url:"/pages/index/sign/sign"
				})
			}
		}
	}
</script>

<style lang="scss">
		
	.page{
		margin: 0 auto;
	}	
	.u-page__item{
		background-color: rgb(249, 249, 249);
		margin-bottom: 30rpx;
		border-radius: 10rpx;
	}
</style>
