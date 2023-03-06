import store from "../store/index.js";


//服务器地址
const baseURL = 'https://tcnet.club:3004';
//调试地址
// const baseURL = 'https://localhost:3004';
const fileUrl = 'https://tcnet.club:5656';
 
const FileById = fileUrl + '/TCSoft/Files/GetFileById?id='



function get(url) {
	return new Promise((resolve) => {
		return uni.request({
			url: baseURL + url,
			data: {},
			header: {
				authorization: "Bearer " + store.state.token
			},
			method: "GET",
			success(res) {
				resolve(res)
			}
		});
	});
};

function post(url, data) {
	return new Promise((resolve) => {
		uni.request({
			url: baseURL + url,
			data: data,
			header: {
				authorization: "Bearer " + store.state.token
			},
			method: "POST",
			success(res) {
				resolve(res)
			}
		});
	});
};

function put(url, data) {
	return new Promise((resolve) => {
		uni.request({
			url: baseURL + url,
			data: data,
			header: {
				authorization: "Bearer " + store.state.token
			},
			method: "PUT",
			success(res) {
				resolve(res)
			}
		});
	});
};


function del(url,data) {
	return new Promise((resolve) => {
		uni.request({
			url: baseURL + url,
			header: {
				authorization: "Bearer " + store.state.token
			},
			data: data,
			method: "DELETE",
			success(res) {
				resolve(res)
			}
		});
	});
};







export default {
	get,
	post,
	put,
	del,
	baseURL,
	FileById
}
