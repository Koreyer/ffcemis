<template>
	<view style="height: 100%; display: flex; flex-direction: column">
		
		<div class="u-page">
			<!-- 按钮，通知和设置 -->
			<view class="u-demo-block1">
				<view class="u-demo-block__content">
					<u-row customStyle="margin: 10px 15px">
						<u-col span="9">
							<view class="demo-layout ">
							</view>
						</u-col>
						<u-col span="3">
							<view class="demo-layout">
								<view class="u-nav-slot">
									<u-icon name="bell" size="24"></u-icon>
									<u-line direction="column" :hairline="false" length="16" margin="0 8px"></u-line>
									<u-icon name="setting" size="22"></u-icon>
								</view>
							</view>
						</u-col>
					</u-row>
				</view>
			</view>
			
			<!-- 农场用户登录信息 -->
			<view class="u-demo-block1">
				<view class="u-demo-block__content">
					<u-row customStyle="margin: 0px 15px">
						<u-col span="6">
							<view class="demo-layout ">
								<u-icon label="天天有鲜店" size="65" space="5" labelSize="20" name="/static/images/fpage/logo.png"></u-icon>
							</view>
						</u-col>
						<u-col span="3">
							<view class="demo-layout ">
							</view>
						</u-col>
					</u-row>
				</view>
			</view>
			
			<!-- 功能栏 查询订单，管理商品，管理配送 -->
			<u-gap bgColor="transparent" height="30px" marginTop="30px" marginBottom="10px"></u-gap>
			<u-gap bgColor="transparent" height="10px" marginTop="0px" marginBottom="0px"></u-gap>
			<view class="u-demo-block">
				<u-scroll-list :indicator="false">
					<view class="scroll-list">
						<view class="scroll-list__line" v-for="(item, index) in menuArr" :key="index">
							<view class="scroll-list__line__item" v-for="(item1, index1) in item" :key="index1"
								:class="[(index1 === item.length - 1) && 'scroll-list__line__item--no-margin-right']">
								<image class="scroll-list__line__item__image" :src="menuBaseUrl + item1.icon" mode="">
								</image>
								<text class="scroll-list__line__item__text">{{ item1.name }}</text>
							</view>
						</view>
					</view>
				</u-scroll-list>
			</view>
			
			<!-- 农场用户已上传的商品信息 -->
			<u-gap bgColor="transparent" height="5px" marginTop="5px" marginBottom="5px"></u-gap>
			<view class="u-demo-block">
				<text class="u-demo-block__title">我的商品</text>
				<view class="grid1">
					<u-grid :border="false" col="3" :margin="15">
						<u-grid-item v-for="(listItem,listIndex) in list1" :key="listIndex"
							customStyle="padding-left: 0px; padding-right: 0px">
							<u-icon :customStyle="{paddingTop:2+'rpx'}" :name="listItem.name" :size="100"height="100"></u-icon>
							<text class="grid-text">{{listItem.title}}</text>
						</u-grid-item>
					</u-grid>
				</view>
			</view>
			
			<!-- 广告内容区 -->
			<u-gap bgColor="transparent" height="5px" marginTop="5px" marginBottom="5px"></u-gap>
			<view class="cell1">
				<u-cell-group >
					<u-cell title="福利乐园" value="尽享好礼" isLink="":border="false"></u-cell>
				</u-cell-group>
			</view>
			<view class="u-demo-block">
				<view class="grid1">
					<u-grid :border="false" col="2"align="center">
						<u-grid-item v-for="(listItem,listIndex) in list2" :key="listIndex"
							customStyle="padding-top: 0px; padding-bottom: 0px">
							<u-icon :customStyle="{paddingTop:5+'rpx'}" :name="listItem.name" :size="150"height="70"></u-icon>
							<text class="grid-text">{{listItem.title}}</text>
						</u-grid-item>
					</u-grid>
				</view>
			</view>
			<view class="u-demo-block">
				<view class="grid1">
					<u-grid :border="false" col="1"align="center">
						<u-grid-item v-for="(listItem,listIndex) in list3" :key="listIndex"
							customStyle="padding-top: 0px; padding-bottom: 0px">
							<u-icon :customStyle="{paddingTop:5+'rpx'}" :name="listItem.name" :size="310" height="90"></u-icon>
							<text class="grid-text">{{listItem.title}}</text>
						</u-grid-item>
					</u-grid>
				</view>
			</view>
			
			<!-- 版权声明 -->
			<u-gap bgColor="transparent" height="50px" marginTop="5px" marginBottom="5px"></u-gap>
			<view>
				<u--text margin="0px" type="primary" align="center" text="农场生鲜"></u--text>
				<u--text margin="0px" type="primary" align="center" text="名称：软件2班-第一小组-1.0.0"></u--text>
				<u--text margin="0px" type="primary" align="center" text="到底啦!"></u--text>
			</view>
		</div>

		<!-- 底部导航栏 -->
		<view style="width: 100%; flex: none; position: fixed;bottom: 0px;">
			<c-tabbar></c-tabbar>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				albumWidth: 0,
				src1: '/static/images/fpage/logo.png',
				urls2: [{
					src: '/static/images/fpage/23.png',
				}],
				
				menuBaseUrl: 'https://cdn.uviewui.com/uview/menu/',
				menuArr: [
					[{
							name: '查询订单',
							icon: '7.png'
						},
						{
							name: '管理商品',
							icon: '2.png'
						}, {
							name: '管理配送',
							icon: '17.png'
						},
					],
				],
				list1: [{
						name: '/static/images/fpage/11.jpg',
						title: '鲜活九孔鲍鱼'
					},
					{
						name: '/static/images/fpage/12.jpg',
						title: '帝王蟹'
					},
					{
						name: '/static/images/fpage/13.jpg',
						title: '冷冻南极冰鱼'
					},
					{
						name: '/static/images/fpage/14.jpg',
						title: '白虾'
					},
					{
						name: '/static/images/fpage/15.jpg',
						title: '咸无头小黄鱼干'
					},
					{
						name: '/static/images/fpage/16.jpg',
						title: '加拿大大龙虾'
					},
				],
				list2: [
					{
					name:'/static/images/fpage/21.png',
				
					},
					{
					name:'/static/images/fpage/22.png',
					
					}
				],
				list3: [{
						name: '/static/images/fpage/23.png',
					
					}
				],
				
			}
		},
		methods: {
			rightClick() {
				console.log('rightClick');
			},
			leftClick() {
				console.log('leftClick');
			},
			click(name) {
				this.$refs.uToast.success(`点击了第${name}个`)
			}
		}
	}
</script>

<style lang="scss">
	/* #ifndef APP-NVUE */
	page {
		background-color: $u-bg-color;
	}

	/* #endif */

	.u-page {
		padding: 0;
		padding-bottom: 100px;

		&__item {

			&__title {
				color: $u-tips-color;
				background-color: $u-bg-color;
				padding: 15px;
				font-size: 15px;

				&__slot-title {
					color: $u-primary;
					font-size: 14px;
				}
			}
		}
	}

    // 按钮css
	.u-nav-slot {
		@include flex;
		align-items: center;
		justify-content: center;
		border: false;
		padding: 3px 7px;
		opacity: 0.8;
	}

	.wrap {
		padding: 0px;
	}

	.demo-layout {
		height: 5px;
		border-radius: 4px;
	}

	.bg-purple {
		background: #CED7E1;
	}

	.bg-purple-light {
		background: #e5e9f2;
	}

	.bg-purple-dark {
		background: #99a9bf;
	}

    //我的商品
    .grid1{
		margin: 0px 15px;
	}
	.grid-text {
		font-size: 14px;
		color: #909399;
		padding: 0rpx 0 20rpx 0rpx;
		/* #ifndef APP-PLUS */
		box-sizing: border-box;
		/* #endif */
	}

	.scroll-list {
		margin: 0px 15px;
		background: #ffffff;
		@include flex(column);

		&__show-more {
			background-color: #fff0f0;
			border-radius: 3px;
			padding: 3px 6px;
			@include flex(column);
			align-items: center;

			&__text {
				font-size: 12px;
				width: 12px;
				color: #f56c6c;
				line-height: 0px;
			}
		}

        //功能栏
		&__line {
			@include flex;
			margin-top: 0px;

			&__item {
				// margin-right: 16px;
				height: 80px;

				&__image {
					width: 56px;
					height: 48px;
					display: block;
					padding-left: 35px;
				}

				&__text {
					margin-top: 0px;
					color: $u-content-color;
					font-size: 14px;
					//text-align: center;
					padding-left: 40px;
				}

			}
		}
	}

	.album {
		@include flex;
		align-items: flex-start;
		background-color: "#ffffff";

		&__avatar {
			background-color: $u-bg-color;
			padding: 5px;
			border-radius: 3px;
		}

		&__content {
			margin-left: 15px;
			flex: 1;
		}
	}

	.u-demo-block {
		background-color: #FFFFFF;
		margin: 0px 15px;
	}


	.u-demo-block__title {
		text-align: left;
		margin: 15px;
		bold: true;

	}

	.cell-page {
		padding-bottom: 20px;
	}

	.cell-box {
		&__title {
			font-size: 16px;
			color: rgb(143, 156, 162);
			margin: 20px 0px 0px 15px;
		}

		&__block {
			// background-color: #fff;
			margin-top: 20px;
		}
	}

	.u-slot-title {
		@include flex;
		flex-direction: row;
		align-items: center;
	}

	.u-cell-text {
		font-size: 25px;
		line-height: 22px;
		color: #303133;
		margin-right: 5px;

	}

	.u-slot-value {
		line-height: 17px;
		text-align: center;
		font-size: 10px;
		padding: 0 5px;
		height: 17px;
		color: #FFFFFF;
		border-radius: 100px;
		background-color: #f56c6c;
	}
	
	.cell1{
		background-color: #FFFFFF;
		margin: 0px 15px;
		
	}
</style>