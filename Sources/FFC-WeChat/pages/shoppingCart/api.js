import request from "@/utils/request.js"
const FileById = request.FileById;

export default{
	async GetCommodityType(){
		return await request.get('/ShoppingCarBusiness/ShoppingCarWithItem/GetShoppingCarMySelf')
	},
	async commodityId(id){
		return await request.get('/ShoppingCarBusiness/ShoppingCarWithItem/GetShoppingCarMySelf'+id)
	},
	FileById
	}