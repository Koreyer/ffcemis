<template>
  <div class="scroll-y">
    <!--操作-->
    <div class="mr-3 rowSS">
      <el-button type="primary" @click="addBtnClick">
        <el-icon style="vertical-align: middle">
          <FolderAdd />
        </el-icon>
        <span style="vertical-align: middle">增加</span>
      </el-button>
    </div>
    <!--表格和分页-->
    <el-table
      id="resetElementDialog"
      ref="refuserTable"
      :height="`calc(100vh - ${settings.delWindowHeight})`"
      border
      :data="usertableData"
      @selection-change="handleSelectionChange"
    >
      <el-table-column type="selection" align="center" width="50" />
      <el-table-column align="center" prop="name" label="商品名称" width="160" />
      <el-table-column align="center" prop="commodityTypeName" label="商品类型" width="120" />
      <el-table-column align="center" prop="price" label="价格" width="100" />
      <el-table-column align="center" prop="originalPrice" label="原价" width="100" />
      <el-table-column align="center" prop="stock" label="库存" width="100" />
      <el-table-column align="center" prop="sellNum" label="销量" width="100" />
      <el-table-column align="center" prop="viewNum" label="浏览量" width="100" />
      <el-table-column align="center" prop="spec" label="规格" width="100" />
      <el-table-column align="center" prop="producer" label="产地" width="150" />
      <el-table-column align="center" prop="introduce" label="推荐理由" width="150" />
      <el-table-column align="center" prop="warrantyTime" label="保质期" width="150" />
      <el-table-column align="center" prop="storageMode" label="保存方式" width="150" />
      <el-table-column align="center" prop="description" label="详细" width="150" />
      <el-table-column align="center" prop="imageUrl" label="首页商品图" width="300">
        <template #default="{ row }">
          <img :src="imgUrl + row.imageUrl" class="w-80px heightPx-80" style="border-radius: 10px" />
        </template>
      </el-table-column>
      <el-table-column align="center" prop="carousels" label="商品轮播图" width="300">
        <template #default="{ row }">
          <div style="float: left" v-for="item in row.carousels.split(',')">
            <img :src="imgUrl + item" class="w-80px heightPx-80" style="border-radius: 10px" />
          </div>
        </template>
      </el-table-column>
      <el-table-column align="center" prop="descriptionImageUrl" label="商品详情图" width="300">
        <template #default="{ row }">
          <div style="float: left" v-for="item in row.descriptionImageUrl.split(',')">
            <img :src="imgUrl + item" class="w-80px heightPx-80" style="border-radius: 10px" />
          </div>
        </template>
      </el-table-column>
      <el-table-column fixed="right" align="center" label="操作" width="180">
        <template #default="{ row }">
          <el-button text size="small" @click="tableEditClick(row)">编辑</el-button>
          <el-button class="deleteButton" text size="small" @click="tableDelClick(row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    <!--分页-->
    <div class="rowCS mt-20px">
      <el-pagination
        :current-page="pageNum"
        :page-sizes="[20, 30, 50, 100]"
        :page-size="pageSize"
        layout="total, sizes, prev, pager, next, jumper"
        :total="totalPage.page"
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
      />
    </div>
    <!--详情-->
    <el-dialog v-model="detailDialog" :title="dialogTitle" width="500px" :close-on-click-modal="false">
      <div class="detail-container rowBC">
        <div class="detail-container-item">品牌名称：{{ detailData.name }}</div>
      </div>
      <template #footer>
        <span class="dialog-footer">
          <el-button type="primary" @click="detailDialog = false">确 定</el-button>
        </span>
      </template>
    </el-dialog>
    <Form v-if="showFrom" ref="refCRUDForm" @hideComp="hideComp" @selectPageReq="selectPageReq" />
  </div>
</template>
<script>
export default {
  name: 'Brand'
}
</script>
<script setup>
import { Delete, FolderAdd } from '@element-plus/icons-vue'
/*1.初始化引入和实例化*/
import { useCommon } from '@/hooks/global/useCommon'
import settings from '@/settings'
import Form from './Form.vue'
onActivated(() => {
  console.log('onActivated')
})
onDeactivated(() => {
  console.log('onDeactivated')
})
/*2.表格操作和查询*/
let multipleSelection = ref([])
const handleSelectionChange = (val) => {
  multipleSelection.value = val
}
let usertableData = ref([])
//接收商品类型
// let typeList = ref([])
let searchForm = reactive({
  id: '',
  name: '',
  commodityTypeName: '',
  price: '',
  originalPrice: '',
  imageUrl: '',
  stock: '',
  sellNum: '',
  viewNum: '',
  producer:'',
  introduce:'',
  warrantyTime:'',
  storageMode:'',
  description: '',
  carousels: '',
  spec:'',
  descriptionImageUrl: ''
})
onMounted(() => {
  selectPageReq()
  // getType()
})

const totalPage  = reactive({
  page:0,
})
let selectPageReq = () => {
  const data = Object.assign(searchForm, {
    pageNum: pageNum.value,
    pageSize: pageSize.value
  })
  Object.keys(data).forEach((fItem) => {
    if (data[fItem] === '' || data[fItem] === null || data[fItem] === undefined) delete data[fItem]
  })
  let reqConfig = {
    url: '/CommodityBusiness/Commodity/GetBySelect',
    method: 'get',
    data,
    isParams: true
  }
  axiosReq(reqConfig).then((resData) => {
    console.log(resData)
    usertableData.value = resData?.datas
    totalPage.page = resData.totalCount
    console.log(totalPage)
  })
}

// //获取商品类型
// let getType = () => {
//   let reqConfig = {
//     url: '/CommodityBusiness/CommodityType/get',
//     method: 'get',
//     isParams: true
//   }
//   axiosReq(reqConfig).then((resData) => {
//     typeList.value = resData?.datas
//   })
// }

import tablePageHook from '@/hooks/useTablePage'

let { pageNum, pageSize, handleCurrentChange, handleSizeChange } = tablePageHook(selectPageReq)
const dateTimePacking = (timeArr) => {
  if (timeArr && timeArr.length === 2) {
    searchForm.startTime = timeArr[0]
    searchForm.endTime = timeArr[1]
  } else {
    searchForm.startTime = ''
    searchForm.endTime = ''
  }
}
const searchBtnClick = () => {
  //此处要重置页数，也是常出的bug
  pageNum.value = 1
  selectPageReq()
}
//删除相关
let { elMessage, elConfirm } = useElement()
const refuserTable = ref(null)
const multiDelBtnClick = () => {
  let rowDeleteIdArr = []
  let deleteNameTitle = ''
  rowDeleteIdArr = multipleSelection.value.map((mItem) => {
    deleteNameTitle = deleteNameTitle + mItem.name + ','
    return mItem.id
  })
  if (rowDeleteIdArr.length === 0) {
    elMessage('表格选项不能为空', 'warning')
    return
  }
  let stringLength = deleteNameTitle.length - 1
  elConfirm('删除', `您确定要删除【${deleteNameTitle.slice(0, stringLength)}】吗`)
    .then(() => {
      const data = rowDeleteIdArr
      axiosReq({
        url: `/integration-front/brand/deleteBatchIds`,
        data,
        method: 'DELETE',
        bfLoading: true
      }).then((res) => {
        elMessage('删除成功')
        selectPageReq()
      })
    })
    .catch(() => {})
}
let deleteByIdReq = (id) => {
  return axiosReq({
    url: '/CommodityBusiness/Commodity/SoftDeleteById',
    data: { id: id },
    isParams: true,
    method: 'delete',
    bfLoading: true
  })
}
let tableDelClick = (row) => {
  elConfirm('确定', `您确定要删除【${row.name}】吗？`)
    .then(() => {
      deleteByIdReq(row.id).then(() => {
        selectPageReq()
        elMessage(`【${row.name}】删除成功`)
      })
    })
    .catch(() => {})
}
//添加和修改
const imgUrl = ref('https://tcnet.club:5656/TCSoft/Files/GetFileById?id=')
let showFrom = ref(false)
const refCRUDForm = ref(null)
let addBtnClick = () => {
  showFrom.value = true
  nextTick(() => {
    refCRUDForm.value.showModal()
  })
}
onMounted(() => {
  console.log('import', import.meta.env.VITE_APP_IMAGE_URL)
})
const hideComp = () => {
  showFrom.value = false
}
let tableEditClick = (row) => {
  console.log(row)
  showFrom.value = true
  nextTick(() => {
    refCRUDForm.value.showModal(true, row)
  })
}
/*3.详情modal*/
let detailData = ref({})
let { dialogTitle, detailDialog } = useElement()
let tableDetailClick = (row) => {
  dialogTitle.value = `详情【${row.name}】`
  getDetailByIdReq(row.id).then((resData) => {
    detailData.value = resData.data
    detailDialog.value = true
  })
}
let getDetailByIdReq = (id) => {
  return axiosReq({
    url: '/CommodityBusiness/Commodity/Update',
    data: { id: id }, //--c
    isParams: true,
    method: 'put'
  })
}
</script>

<style scoped lang="scss">
/*详情*/
.detail-container {
  flex-wrap: wrap;
}

.detail-container-item {
  min-width: 40%;
  margin-bottom: 20px;
}

.detailDialog-title {
  margin-bottom: 14px;
  font-weight: bold;
  font-size: 16px;
}

.deleteButton {
  color: rgb(241, 53, 53);
}
</style>
