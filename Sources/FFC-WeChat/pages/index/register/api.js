import request from "@/utils/request.js"
export default{
	async Register(account){
		return await request.post('/IdentityManager/IdentityManager/Register',account)
	}
}