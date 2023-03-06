<template>
	<view class="page">
		<!-- 导航栏 -->
		<view>
			<u-navbar title="我的收货地址" @leftClick="navigateBack" safeAreaInsetTop fixed placeholder></u-navbar>
		</view>
		<!-- 内容 -->
		<view class="content">
			<view class="list">
				<view class="row u-flex" v-for="(addressListItem,addressListIndex) in addressList" :key="addressListIndex" @tap="select(row)">
					
					<view class="left">
						<view class="name-tel u-flex">
							<view class="name">{{addressListItem.name}}</view>
							<view class="tel">{{addressListItem.phone}}</view>
							<view class="default" v-if="addressListItem.isDefault">
								默认
							</view>
						</view>
						<view class="address">
							{{addressListItem.province}}{{addressListItem.city}}{{addressListItem.region}}{{addressListItem.detailedAddress}}
						</view>
					</view>
					
					<view class="right">
						<view class="icon u-flex" @tap.stop="edit(addressListItem)">
							<u-icon name="edit-pen" color="#777" size="28"></u-icon>
						</view>
					</view>
				</view>
			</view>
		</view>
		<!--  添加收货地址-->
		<view style="width: 100%; flex: none; position: fixed;bottom: 0px;">
			<view style="width: 95%;margin: 0 auto;text-align: center;">
				<view style="width: 95%;background-color: coral;padding: 15rpx;border-radius: 15px;color: #fff;" @tap="add">
					+ 添加收货地址
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
				 isSelect:false,
				 addressList: []
			};
		},
		onShow() {
		    uni.getStorage({
		        key:'delAddress',
		        success: (e) => {
		            let len = this.addressList.length;
		            if(e.data.hasOwnProperty('id')){
		                for(let i=0;i<len;i++){
		                    if(this.addressList[i].id==e.data.id){
		                        this.addressList.splice(i,1);
		                        break;
		                    }
		                }
		            }
		            uni.removeStorage({
		                key:'delAddress'
		            })
		        }
		    })
		    uni.getStorage({
		        key:'addAddress',
		        success: (e) => {
		            let len = this.addressList.length;
		            if(e.data.hasOwnProperty('id')){
		                for(let i=0;i<len;i++){
		                    if(this.addressList[i].id==e.data.id){
		                        this.addressList.splice(i,1,e.data);
		                        break;
		                    }
		                }
		            }else{
		                let lastid = this.addressList[len-1];
		                lastid++;
		                e.data.id = lastid;
		                this.addressList.push(e.data);
		            }
		            uni.removeStorage({
		                key:'addAddress'
		            })
		        }
		    })
		},
		onLoad(e) {
		    if(e.type=='select'){
		        this.isSelect = true;
		    }
			console.log(this.$store.state);
			this.GetMyAddress()
		},
		methods: {
			async GetMyAddress() {
				var result = await api.GetMyAddress()
				this.addressList = result.data.datas	
				console.log(result)	
			},
			navigateBack() {
				uni.navigateBack()
			},
			edit(row){
			    uni.setStorage({
			        key:'address',
			        data:row,
			        success() {
			            uni.navigateTo({
			                url:"edit/edit?type=edit&id="+row.id
			            })
			        }
			    });
			    
			},
			add(){
			    uni.navigateTo({
			        url:"edit/edit?type=add"
			    })
			},
			select(row){
			    //是否需要返回地址(从订单确认页跳过来选收货地址)
			    if(!this.isSelect){
			        return ;
			    }
			    uni.setStorage({
			        key:'selectAddress',
			        data:row,
			        success() {
			            uni.navigateBack();
			        }
			    })
			}
		}

	}
</script>

<style lang="scss">
	
	.row {
		padding: 60rpx 20rpx;
	}
	.left {
		float: left;
		display: block;
		width: 80%;
	}
	.name-tel {

	}
	.name {
		float: left;
		font-size: 34rpx;
		padding-right: 30rpx;
	}
	.tel {
		float: left;
		padding-top: 8rpx;
		font-size: 24rpx;
		color: #777;
		margin-right: 20rpx;
	}
	.default {
		float: left;
		margin-top: 8rpx;
		font-size: 22rpx;
		background-color: #f06c7a;
		color: #fff;
		padding: 0 18rpx;
		border-radius: 24rpx;
		margin-left: 20rpx;
		margin-right: 20rpx;
	}

	.address {
		width: 100%;
		padding-top: 8rpx;
		font-size: 24rpx;
		max-width: 540rpx;
		white-space: nowrap;
		text-overflow: ellipsis;
		overflow: hidden;
		color: #777;
	}
	.right {
		display: block;
		float: right;
		margin-left: 20upx;
	}
	.icon {
		margin-top: 15rpx;
		border-left: solid 1upx #aaa;

	}
</style>
