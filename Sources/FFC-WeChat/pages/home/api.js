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
	async GetHomePage(){
		return await request.get('/SystemtManagementBusiness/HomePageCarousel/Get')
	},
	async GetCommodityByType(pageNum,pageSize,searchValue){
		return await request.get('/CommodityBusiness/Commodity/GetCommodityByType?pageNum='+pageNum+"&&searchValue="+searchValue+"&&pageSize="+pageSize)
	},
	async AddCommodityViewNum(id){
		return await request.get('/CommodityBusiness/Commodity/AddCommodityViewNum?id='+id)
	},
	async GetComment(id){
		return await request.get('/CommodityBusiness/CommodityEvaluation/GetHighOpinion?id='+id)
	},
	async GetCommentList(pageNum,pageSize,searchValue){
		return await request.get('/CommodityBusiness/CommodityEvaluation/GetBySelect?pageNum='+pageNum+"&&searchValue="+searchValue+"&&pageSize="+pageSize)
	},
	async AddShoppingCar(id){
		return await request.post('/ShoppingCarBusiness/ShoppingCarWithItem/Add',{commodityId:id,count:1})
	},
	async AddOrder(id){
		return await request.post('/OrderBusiness/Order/Add',{commodityId:id,count:1,addressId:"33998c82-5507-4440-d85f-08dab5c971ae"})
	},
	FileById
}