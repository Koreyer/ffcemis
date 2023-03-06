import request from "@/utils/request.js"
const FileById = request.FileById;

export default{
	async GetMessage(data){
		return await request.get('/MessageManagementBusiness/UserMessage/GetHistoryMessage?pageNum='+data.pageNum+'&pageSize='+data.pageSize+'&searchValue='+data.searchValue+"&extendValue1=1")
	},
	async GetChatList(){
		return await request.get('/MessageManagementBusiness/UserMessage/GetChatList?userRole=true')
	},
	FileById
}