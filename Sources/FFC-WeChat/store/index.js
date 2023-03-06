import Vue from 'vue'
import Vuex from 'vuex'
Vue.use(Vuex)

const store = new Vuex.Store({
	screenHeight:"",
	screenWidth:"",
	state: {
		tabbarIndex:0,
		token:"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicnVhbmppYW4iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjlmMzgyNDE0LWUwNmItNGMwNy0zZGNiLTA4ZGFiN2I4YjMwYSIsIkpXVFZlcnNpb24iOiIwIiwiZXhwIjoxNjY5Nzc2NDA5LCJpc3MiOiJSdWFuSmlhbkppU2h1MiIsImF1ZCI6IlJ1YW5KaWFuSmlTaHUyIn0.y9mEyFPmDD5ltcT7Zs9h43bAzuS6oZNXLCFTrX4ZL00",
		user:{
			id:"9f382414-e06b-4c07-3dcb-08dab7b8b30a",
			name:"FFC_ruanjian"
		},
		role:2
	},
})
//获取屏幕高度
 uni.getSystemInfo({
		success: (res) => {
			 console.log(res.screenHeight); //屏幕高度  注意这里获得的高度宽度都是px 需要转换rpx
						        console.log(res.windowWidth);  //可使用窗口宽度
						        console.log(res.windowHeight); //可使用窗口高度
						        console.log(res.screenWidth); //屏幕宽度
			var rpx=(res.screenHeight * (750 / res.windowWidth))
			store.screenHeight = rpx  +"rpx"
			store.screenWidth = res.screenWidth+"px"
			console.log(rpx);
		}
	})


export default store