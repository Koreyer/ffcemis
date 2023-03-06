<template>
	<view class="page">
		<!-- 导航栏 -->
		<view>
			<u-navbar title="物流" fixed placeholder @leftClick="navigateBack"></u-navbar>
		</view>
	</view>
</template>

<script>
	import api from './api.js'
	export default {
		data() {
			return {
				dataId: "",
				list: [],
				// url: api.FileById,
			};
		},
		onLoad() {
		
		},
		methods: {
		}
	}
</script>

<style lang="scss" scoped>
	page {
		// background-color: #ebebeb;
	}
</style>
