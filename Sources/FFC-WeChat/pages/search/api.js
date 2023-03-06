import request from "@/utils/request.js"
const FileById = request.FileById;

export default{
	async GetCommodityType(){
		return await request.get('/CommodityBusiness/CommodityType/get')
	},
	async GetCommodityList(pageNum,pageSize){
		return await request.get('/CommodityBusiness/Commodity/GetBySelect?pageNum='+pageNum+"&pageSize="+pageSize)
	},
	async GetCommodity(id){
		return await request.get('/CommodityBusiness/Commodity/GetById?id='+id)
	},
	async GetCommodity(searchValue){
		return await request.get('/CommodityBusiness/Commodity/GetBySelect?searchValue='+searchValue)
	},
	FileById
}