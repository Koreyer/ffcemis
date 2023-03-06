import request from "@/utils/request.js"
const FileById = request.FileById;

export default{
	//获取地址
	async GetMyAdd(id){
		return await request.get('/AddressManageBusiness/AddressManage/GetByid?id='+id)
	},
	async AddAddress(data){
		return await request.post('/AddressManageBusiness/AddressManage/Add',data)
	},
	async DeleteAddress(id) {
		return await request.del('/AddressManageBusiness/AddressManage/SoftDeleteById?id=' + id)
	},
	async UpdateAddress(data){
		return await request.put('/AddressManageBusiness/AddressManage/Update',data)
	},
	
	FileById
}