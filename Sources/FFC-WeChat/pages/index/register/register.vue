<template>
	<view class="box">
		<view style="padding-top: 200rpx;">
			<image class="imgStyle" src="../../../static/images/logo.png"></image>
		</view>
		<view class="box_list">
			<view style="font-size: 20px;">欢迎注册</view>
			<view style="padding-top: 25rpx;">
				<u-input placeholder="请输入账号" prefixIcon="android-fill" v-model="account.userName">
				</u-input>
			</view>
			<view style="padding-top: 15rpx;">
				<u-input placeholder="请输入密码" prefixIcon="lock-fill" v-model="account.password" password>
				</u-input>
			</view>
			<view style="padding-top: 15rpx;">
				<u-input placeholder="再次输入密码" prefixIcon="lock-fill" v-model="checkPWD" password>
				</u-input>
			</view>
			<view
				style="width: 22%;padding-top: 10rpx;font-size: 14px;color: #a8a8a8;border-bottom: 1px  solid #00aaff;text-align: center;"
				@click="goSign">
				返回登录
			</view>
			<view style="margin: 0 auto;padding-top: 25rpx;width: 200rpx;text-align: center;">
				<u-button type="primary" size="120rpx" @click="register">
					注册
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
					userName: "",
					password: ""
				},
				checkPWD: ""

			}
		},
		methods: {
			async register() {
				if (this.account.userName == null || this.account.userName == "" || this.account.userName ==
					"undfind") {
					uni.showModal({
						title: "账号不能为空!"
					})
				} else if (this.account.password == null || this.account.password == "" || this.account
					.password == "undfind") {
					uni.showModal({
						title: "密码不能为空!"
					})
				} else if (this.checkPWD == null || this.checkPWD == "" || this.checkPWD == "undfind") {
					uni.showModal({
						title: "请再次输入密码!"
					})
				} else if (this.checkPWD != this.account.password) {
					uni.showModal({
						title: "两次密码不符!"
					})
				} else {
					var a = await api.Register(this.account)
					if (a.data.message == "账号已存在") {
						uni.showModal({
							title: "该用户已注册!"
						})
					}
					if (a.statusCode == 200) {
						uni.showToast({
							title: "注册成功！"
						})
						setTimeout(() => {
							uni.navigateTo({
								url: "/pages/index/sign/sign",
							})
						}, 2000)
					}
				}
			},
			goSign() {
				uni.navigateTo({
					url: "/pages/index/sign/sign"
				})
			},
		}
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
