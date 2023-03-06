import request from "@/utils/request.js"
export default{
	async Login(account){
		return await request.post('/IdentityManager/IdentityManager/Login',account)
	}
}