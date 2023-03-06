import request from "@/utils/request.js"
const FileById = request.FileById;

export default{
	//获取地址
	async GetMyAddress(){
		return await request.get('/AddressManageBusiness/AddressManage/GetMyAddress')
	},
	async DeleteAddress(id){
		return await request.del()('/AddressManageBusiness/AddressManage/SoftDeleteById?id='+id)
	},
	FileById
}