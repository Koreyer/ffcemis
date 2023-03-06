<template>
	<view class="fixed-bottom position-sticky">
		<u-tabbar :value="this.$store.state.tabbarIndex" @change="ChangBar" :fixed="false" :placeholder="false"
			:safeAreaInsetBottom="true" >

			<u-tabbar-item v-for="(item,index) in list" :text="item.text" :icon="item.icon" @click="click1">
			</u-tabbar-item>


		</u-tabbar>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				value1: this.$store.state.tabbarIndex,
				list: [{
						pagePath: "/pages/home/home",
						text: "首页",
						icon: "home"
					},
					{
						pagePath: "/pages/chat/chat",
						text: "消息",
						icon: "chat"
					},
					{
						pagePath: "/pages/shoppingCart/shoppingCart",
						text: "购物车",
						icon: "shopping-cart"
					},
					{
						pagePath: "/pages/person/person",
						text: "我的",
						icon: "account"
					}
				],
			}
		},
		onLoad() {},
		methods: {
			click1(e) {
				this.$store.state.tabbarIndex = e
			},
			ChangBar(e) {
				this.$store.state.tabbarIndex = e
				console.log(this.list[e].pagePath)
				uni.switchTab({
					url: this.list[e].pagePath
				})
			}
		}
	}
</script>

<style>
	.position-sticky {
	    position: -webkit-sticky!important;
	    position: sticky!important;
	}
	    
	.fixed-bottom {
	    position: fixed;
	    right: 0;
	    bottom: calc( var(--window-bottom) + 3px);
	    left: 0;
	    z-index: 1030;
	    margin-bottom: 6;
	}
</style>
