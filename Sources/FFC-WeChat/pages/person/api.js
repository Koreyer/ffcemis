import request from "@/utils/request.js"
const FileById = request.FileById;

export default{
	async GetIdentityManager(){
		return await request.get('/IdentityManager/AppUser/UserGetSelf')
	},
	async GetCommodityList(pageNum,pageSize){
		return await request.get('/CommodityBusiness/Commodity/GetBySelect?pageNum='+pageNum+"&pageSize="+pageSize)
	},
	async GetCommodityType(){
		return await request.get('/CommodityBusiness/CommodityType/get')
	},
	async GetCommodity(id){
		return await request.get('/CommodityBusiness/Commodity/GetById?id='+id)
	},
	FileById
}