<template>
	<view class="u-page">
		<!-- 导航栏 -->
		<view>
			<u-navbar title="编辑地址" @leftClick="navigateBack" safeAreaInsetTop fixed placeholder></u-navbar>
		</view>
		<!-- 表单 -->
		<view class="form">
			<view class="row">
				<view class="nominal">
					收件人
				</view>
				<view class="input">
					<input style="line-height: 80rpx; height: 80rpx;" placeholder="请输入收件人姓名" type="text"
						v-model="data.name" />
				</view>
			</view>
			<view class="row">
				<view class="nominal">
					电话号码
				</view>
				<view class="input">
					<input style="line-height: 80rpx; height: 80rpx;" placeholder="请输入收件人电话号码" type="text"
						v-model="data.phone" />
				</view>
			</view>
			<view class="row ">
				<view class="nominal">
					所在省份
				</view>
				<view class="input">
					<textarea style="line-height: 80rpx; height: 80rpx;" auto-height="true" placeholder="请输入省份/地区"
						v-model="data.province"></textarea>
				</view>
			</view>
			<view class="row ">
				<view class="nominal">
					所在城市
				</view>
				<view class="input">
					<textarea style="line-height: 80rpx; height: 80rpx;" auto-height="true" placeholder="请输入城市名"
						v-model="data.city"></textarea>
				</view>
			</view>
			<view class="row ">
				<view class="nominal">
					所在区县
				</view>
				<view class="input">
					<textarea style="line-height: 80rpx; height: 80rpx;" auto-height="true" placeholder="请输入区/县"
						v-model="data.region"></textarea>
				</view>
			</view>
			<view class="row">
				<view class="nominal">
					详细地址
				</view>
				<view class="input">
					<textarea style="line-height: 80rpx; height: 80rpx;" auto-height="true" placeholder="输入详细地址"
						v-model="data.detailedAddress"></textarea>
				</view>
			</view>
			<view class="row">
				<view class="nominal" style="width: 40%;">
					设置默认地址
				</view>
				<view class="input switch">
					<switch style="line-height: 80rpx; height: 80rpx;margin-left: 60%;" color="#e45656"
						:checked="isDefault" @change="isDefaultChange" />
				</view>
			</view>

		</view>
		<!-- 删除收货地址 -->
		<view v-if="editType=='edit'" @click="DeleteAddress()">
			<view class="del">
				删除收货地址
			</view>
		</view>

		<!--  保存收货地址-->
		<view style="width: 100%; flex: none; position: fixed;bottom: 0px;">
			<view style="width: 95%;margin: 0 auto;text-align: center;">
				<view v-if="editType=='add'"
					style="width: 95%;background-color: coral;padding: 15rpx;border-radius: 15px;color: #fff;"
					@click="AddAddress">
					保存收货地址
				</view>
				<view v-if="editType=='edit'"
					style="width: 95%;background-color: coral;padding: 15rpx;border-radius: 15px;color: #fff;"
					@click="UpdateAddress">
					修改收货地址
				</view>
			</view>
			<view style="padding-top: 20rpx;"></view>
		</view>
		
	</view>
</template>

<script>
	import api from './api.js'
	export default {
		
		data() {
			return {
				editType: 'edit , add',
				addressId: "",
				isDefault: false,
				data: {
					name: "",
					phone: "",
					detailedAddress: "",
					province:"",
					city:"",
					region:"",

					
					
				}

			}
		},

		async onLoad(data) {
			//获取传递过来的参数
			this.editType = data.type;
			this.addressId = data.id
			this.GetMyAdd(data.id)
			console.log(data.id);
			if (data.type == 'edit') {
				uni.getStorage({
					key: 'address',
					success: (data) => {

					}
				})
			}

		},
		methods: {
			navigateBack() {
				uni.navigateBack()
			},
			isDefaultChange(e) {
				this.isDefault = e.detail.value;
			},
			async GetMyAdd() {
				var result = await api.GetMyAdd(this.addressId)
				console.log(this.addressId);
				this.data = result.data
				console.log(result.data);
			},
			async AddAddress() {
				if (this.data.name == null || this.data.name == "" || this.data.name ==
					"undfind") {
					uni.showModal({
						title: "姓名不能为空!"
					})
				}
				else if (this.data.phone == null || this.data.phone == "" || this.data.phone ==
					"undfind") {
					uni.showModal({
						title: "电话不能为空!"
					})
				}
				else if (this.data.province == null || this.data.province == "" || this.data.province ==
					"undfind") {
					uni.showModal({
						title: "省份不能为空!"
					})
				}
				else if (this.data.city == null || this.data.city == "" || this.data.city ==
					"undfind") {
					uni.showModal({
						title: "城市不能为空!"
					})
				}
				
				else if (this.data.region == null || this.data.region == "" || this.data.region ==
					"undfind") {
					uni.showModal({
						title: "区/县不能为空!"
					})
				}
				else if (this.data.detailedAddress == null || this.data.detailedAddress == "" || this.data
					.detailedAddress ==
					"undfind") {
					uni.showModal({
						title: "详细地址不能为空!"
					})
				} else {
					var a = await api.AddAddress(this.data)
					if (a.statusCode == 200) {
						uni.showToast({
							title: "添加成功！"
						})
						setTimeout(() => {
							uni.navigateTo({
								url: "/pages/address/address",
							})
						}, 200)
					}
				}
			},
			async DeleteAddress() {
				var a = await api.DeleteAddress(this.addressId)
				if (a.statusCode == 200) {
					uni.showToast({
						title: "删除成功！"
					})
					setTimeout(() => {
						uni.navigateTo({
							url: "/pages/address/address",
						})
					}, 200)
				}

			},
			async UpdateAddress() {
				if (this.editType == 'edit') {
					var a = await api.UpdateAddress(this.data)
					if (a.statusCode == 200) {
						uni.showToast({
							title: "修改成功！"
						})
						setTimeout(() => {
							uni.navigateTo({
								url: "/pages/address/address",
							})
						}, 200)
					}
				}


			},

		},
	}
</script>

<style lang="scss">
	.form {
		border-top: solid 1upx #eee;
		width: 100%;
		height: 100%;

	}

	.row {
		width: 100%;
		padding: 0 6%;
		height: 80rpx;
		border-bottom: solid 1upx #eee;
		display: flex;
		justify-content: space-between;
	}

	.nominal {
		font-weight: bolder;
		font-size: 35rpx;
		line-height: 80rpx;
		width: 20%;
		margin-right: 28rpx;

	}

	.input {
		line-height: 70rpx;
		width: 75%;
	}

	.del {
		width: 80%;
		margin: 50rpx auto;
		text-align: center;
		height: 60rpx;
		line-height: 60rpx;
		justify-content: center;
		align-items: center;
		font-size: 30rpx;
		color: #fff;
		background-color: #e45656;
		border-radius: 15px;
	}
</style>
