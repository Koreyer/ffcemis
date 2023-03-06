<template>
	<view class="box">
		<view style="padding-top: 200rpx;">
			<image class="imgStyle" src="../../../static/images/logo.png"></image>
		</view>
		<view class="box_list">
			<view style="font-size: 20px;">欢迎登录</view>
			<view style="padding-top: 25rpx;">
				<u-input placeholder="请输入账号" prefixIcon="android-fill" v-model="account.userName">
				</u-input>
			</view>
			<view style="padding-top: 15rpx;">
				<u-input placeholder="请输入密码" prefixIcon="lock-fill" v-model="account.password" password>
				</u-input>
			</view>
			<view
				style="width: 22%;padding-top: 10rpx;font-size: 14px;color: #a8a8a8;border-bottom: 1px  solid #00aaff;text-align: center;"
				@click="goRegister">
				用户注册
			</view>
			<view style="margin: 0 auto;padding-top: 25rpx;width: 200rpx;text-align: center;">
				<u-button type="primary" size="120rpx" @click="Login">
					登录
				</u-button>
			</view>
		</view>
	</view>
</template>

<script>
	import api from './api.js'
	export default {
		data() {
			return {
				account: {
					userName: "ruanjian",
					password: "123456"
				},
			};
		},
		onLoad() {

		},
		methods: {
			goRegister() {
				uni.navigateTo({
					url: "/pages/index/register/register"
				})
			},
			async Login() {
				var result = await api.Login(this.account)
				console.log(result.statusCode);
				if (result.statusCode == 200) {
					this.$store.state.token=result.data.token
					this.$store.state.user.id=result.data.id
					this.$store.state.user.name=result.data.name
					uni.switchTab({
						url: "/pages/home/home",
						fail(res) {
							console.log(res);
						}
					})
				}

			}
		},
	}
</script>

<style lang="scss" scoped>
	.box {
		width: 100%;
		height: 100vh;
		overflow: hidden;
	}

	.imgStyle {
		margin: 0 auto;
		display: block;
		width: 220rpx;
		height: 220rpx;
	}

	.box_list {
		margin: 40rpx auto;
		width: 80%;
	}
</style>
