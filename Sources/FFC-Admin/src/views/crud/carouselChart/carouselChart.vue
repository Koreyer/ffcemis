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
      <el-table-column align="center" prop="url" label="url" min-width="50" />
      <el-table-column align="center" prop="imageId" label="轮播图片" min-width="50">
        <template #default="{ row }">
          <img :src="imgUrl + row.imageId" class="w-80px heightPx-80" style="border-radius: 10px" />
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
    <!-- <div class="rowCS mt-20px">
      <el-pagination
        :current-page="pageNum"
        :page-sizes="[10, 20, 50, 100]"
        :page-size="pageSize"
        layout="total, sizes, prev, pager, next, jumper"
        :total="totalPage"
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
      />
    </div> -->
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
let searchForm = reactive({
  id: '',
  url: '',
  imageId: ''
})
onMounted(() => {
  selectPageReq()
})

let { startEndArr, totalPage } = useCommon()
let selectPageReq = () => {
  const data = Object.assign(searchForm, {
    pageNum: pageNum.value,
    pageSize: pageSize.value
  })
  Object.keys(data).forEach((fItem) => {
    if (data[fItem] === '' || data[fItem] === null || data[fItem] === undefined) delete data[fItem]
  })
  let reqConfig = {
    url: '/SystemtManagementBusiness/HomePageCarousel/Get',
    method: 'get',
    data,
    isParams: true
  }
  axiosReq(reqConfig).then((resData) => {
    console.log(1)
    console.log(resData)
    usertableData.value = resData?.datas
    totalPage = resData?.totalCount
  })
}
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
//单删除
let deleteByIdReq = (id) => {
  return axiosReq({
    url: '/SystemtManagementBusiness/HomePageCarousel/DeleteById',
    data: { id: id },
    isParams: true,
    method: 'delete',
    bfLoading: true
  })
}
let tableDelClick = (row) => {
  elConfirm('确定', `您确定要删除【${row.url}】吗？`)
    .then(() => {
      deleteByIdReq(row.id).then(() => {
        selectPageReq()
        elMessage(`【${row.url}】删除成功`)
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
    url: '/SystemtManagementBusiness/HomePageCarousel/Update',
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
