import request from "@/utils/request.js"
const FileById = request.FileById;

export default {
	async GetOrderByMySelf(pageNum, pageSize, payType) {
		return await request.get('/OrderBusiness/Order/GetOrderByMySelf?pageNum=' + pageNum + "&pageSize=" +
			pageSize + "&payType=" + payType)
	},
	async GetIdByData(id) {
		return await request.get('/OrderBusiness/Order/GetById?id=' + id)
	},
	async deleteData(id) {
		return await request.del('/OrderBusiness/Order/DeleteById?id=' + id)
	},
	async selectData(pageNum, pageSize, searchValue) {
		return await request.get('/OrderBusiness/Order/GetBySelect?pageNum=' + pageNum + "&pageSize=" +
			pageSize + "&searchValue=" + searchValue)
	},
	async updateType(data) {
		return await request.put('/OrderBusiness/Order/Update', data)
	},
	FileById
}
