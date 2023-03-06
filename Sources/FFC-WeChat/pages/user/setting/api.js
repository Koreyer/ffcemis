import request from "@/utils/request.js"
const FileById = request.FileById;

export default{
	async GetIdentityManager(){
		return await request.get('/IdentityManager/AppUser/UserGetSelf')
	},
	async UpdateUserInfo(data){
		return await request.put('/IdentityManager/AppUser/Update',data)
	},
	FileById
}