<template>
	<view class="u-page">
		<u-navbar title="个人信息" @leftClick="navigateBack" safeAreaInsetTop fixed placeholder></u-navbar>
		<view class="u-demo-block">
			<view class="u-demo-block__content">
				<!-- 注意，如果需要兼容微信小程序，最好通过setRules方法设置rules规则 -->
				<view class="u-avatar-item">
					<u-avatar :src="avatar" shape="circle" size="100"></u-avatar>
				</view>
				<u--form labelPosition="left" :model="model" ref="form">
					<u-form-item label="姓名" prop="userInfo.name" borderBottom ref="item">
						<u--input v-model="model.userInfo.name" border="none" placeholder="姓名不能为空"></u--input>
					</u-form-item>
					<u-form-item label="性别" prop="userInfo.sexName" borderBottom @click="showSex = true; hideKeyboard()"
						ref="item">
						<u--input v-model="model.userInfo.sexName" disabled disabledColor="#ffffff" placeholder="请选择性别"
							border="none"></u--input>
						<u-icon slot="right" name="arrow-right"></u-icon>
					</u-form-item>
					<u-form-item label="电话" prop="userInfo.phoneNumber" borderBottom ref="item">
						<u--input maxlength="11" v-model="model.userInfo.phoneNumber" border="none"
							placeholder="电话号码不能为空"></u--input>
					</u-form-item>
					<u-form-item label="邮箱" prop="userInfo.email" borderBottom ref="item">
						<u--input v-model="model.userInfo.email" border="none" placeholder="email不能为空"></u--input>
					</u-form-item>
					<!-- <u-form-item label="生日" prop="userInfo.birthday" borderBottom
						@click="showBirthday = true; hideKeyboard()" ref="item">
						<u--input v-model="model.userInfo.birthday" disabled disabledColor="#ffffff" placeholder="请选择生日"
							border="none"></u--input>
						<u-icon slot="right" name="arrow-right"></u-icon>
					</u-form-item> -->
				</u--form>
				<u-button type="primary" text="提交" customStyle="margin-top: 50px" @click="submit"></u-button>
				<u-button type="error" text="重置" customStyle="margin-top: 10px" @click="reset"></u-button>
				<u-action-sheet :show="showSex" :actions="actions" title="请选择性别" description="" @close="showSex = false"
					@select="sexSelect">
				</u-action-sheet>

				<u-datetime-picker :show="showBirthday" :value="birthday" mode="date" closeOnClickOverlay
					@confirm="birthdayConfirm" @cancel="birthdayClose" @close="birthdayClose">
				</u-datetime-picker>
			</view>
		</view>
		<u-toast ref="uToast"></u-toast>
	</view>
</template>

<script>
	import api from './api.js'
	export default {
		data() {
			return {
				avatar: "",
				identityManagerList: [],
				fileList: [],
				disabled: false,
				tips: '',
				value: '',
				showCalendar: false,
				showBirthday: false,
				model: {
					userInfo: {

					},

				},
				showSex: false,
				birthday: Number(new Date()),
				actions: [{
					name: '女',
				}, {
					name: '男',
				}],
				rules: {
					'userInfo.name': [{
						type: 'string',
						required: true,
						message: '请填写姓名',
						trigger: ['blur', 'change']
					}, {
						// 此为同步验证，可以直接返回true或者false，如果是异步验证，稍微不同，见下方说明
						validator: (rule, value, callback) => {
							// 调用uView自带的js验证规则，详见：https://www.uviewui.com/js/test.html
							return uni.$u.test.Number(value);
						},
						message: "姓名不能为空",
						// 触发器可以同时用blur和change，二者之间用英文逗号隔开
						trigger: ["change", "blur"],
					}],
					'userInfo.phoneNumber': {
						type: 'string',
						required: true,
						message: '请输入手机号',
						trigger: ['blur', 'change'],
						validator: (rule, value, callback) => {
							return uni.$u.test.mobile(value);
						}
					},
					'userInfo.email': {
						type: 'string',
						required: true,
						message: '请填写email',
						trigger: ['blur', 'change']
					},

					'userInfo.sexName': {
						type: 'string',
						max: 1,
						required: true,
						message: '请选择男或女',
						trigger: ['blur', 'change']
					},


					'userInfo.birthday': {
						type: 'string',
						required: true,
						message: '请选择生日',
						trigger: ['change']
					},
				},


			}
		},
		onReady() {
			// 如果需要兼容微信小程序，并且校验规则中含有方法等，只能通过setRules方法设置规则
			this.$refs.form.setRules(this.rules)
		},
		onLoad() {
			console.log(this.$store.state);
			this.GetIdentityManager()
		},
		methods: {
			async GetIdentityManager() {
				var result = await api.GetIdentityManager()
				this.model.userInfo = result.data.data
				this.avatar = api.FileById + this.model.userInfo.avatar
				console.log(result.data.data);
			},
			afterRead(event) {
				this.fileList.push({
					url: event.file,
					status: 'uploading',
					message: '上传中'
				})
			},
			groupChange(n) {
				// console.log('groupChange', n);
			},
			radioChange(n) {
				// console.log('radioChange', n);
			},
			navigateBack() {
				uni.navigateBack()
			},
			sexSelect(e) {
				this.model.userInfo.sex = this.actions.indexOf(e)
				this.model.userInfo.sexName = e.name
				// this.model.userInfo.sex = 
				this.$refs.form.validateField('userInfo.sexName')
			},
			change(e) {
				// console.log(e);
			},


			codeChange(text) {
				this.tips = text;
			},


			birthdayClose() {
				this.showBirthday = false
				this.$refs.form.validateField('userInfo.birthday')
			},
			birthdayConfirm(e) {
				this.showBirthday = false
				this.model.userInfo.birthday = uni.$u.timeFormat(e.value, 'yyyy-mm-dd')
				this.$refs.form.validateField('userInfo.birthday')
			},
			async submit() {
				// 如果有错误，会在catch中返回报错信息数组，校验通过则在then中返回true
				var ref = await this.$refs.form.validate().catch(error => {
					this.$refs.uToast.show({
							type:"error",
							message:"校验失败"
						})
				})
				if (ref) {
					var result = await api.UpdateUserInfo(this.model.userInfo)
					if(result.data.code == 200){
						this.$refs.uToast.show({
							type:"success",
							message:result.data.message
						})
					}
					console.log(result);
				}

			},
			reset() {
				this.GetIdentityManager()
				// const validateList = ['userInfo.name', 'userInfo.sexName', 'radiovalue1', 'checkboxValue1', 'intro',
				// 	'hotel', 'code', 'userInfo.birthday'
				// ]
				// this.$refs.form.resetFields()
				// this.$refs.form.clearValidate()
				// setTimeout(() => {
				// 	this.$refs.form.clearValidate(validateList)
				// 	// 或者使用 this.$refs.form.clearValidate()
				// }, 10)
			},
			hideKeyboard() {
				uni.hideKeyboard()
			}
		},
	}
</script>

<style lang="scss">
	.u-avatar-item {
		padding: 0 37%;
	}

	.u-demo-block {
		margin: 0 20rpx;
	}
</style>