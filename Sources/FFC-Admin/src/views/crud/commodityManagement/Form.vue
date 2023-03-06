<template>
  <el-dialog
    v-model="dialogVisible"
    :title="dialogTitle"
    width="50vw"
    :close-on-click-modal="false"
    :before-close="closeFormModal"
  >
    <el-form ref="refForm" label-width="150px" :inline="false" :model="subForm" :rules="formRules" class="pr-50px">
      <el-form-item label="商品名称" prop="name" :rules="formRules.isNotNull" label-position="left">
        <el-input v-model="subForm.name" class="w-150px" placeholder="商品名称" />
      </el-form-item>
      <el-form-item label="商品类型" prop="commodityTypeId" :rules="formRules.isNotNull" label-position="left">
        <el-select v-model="subForm.commodityTypeId" placeholder="请选择商品类型">
          <el-option v-for="item in typeList" :key="item.id" :label="item.name" :value="item.id"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="价格" prop="price" :rules="formRules.isNotNull" label-position="left">
        <el-input v-model="subForm.price" class="w-150px" placeholder="价格" />
      </el-form-item>
      <el-form-item label="原价" prop="originalPrice" :rules="formRules.isNotNull" label-position="left">
        <el-input v-model="subForm.originalPrice" class="w-150px" placeholder="原价" />
      </el-form-item>
      <el-form-item label="库存" prop="stock" :rules="formRules.isNotNull" label-position="left">
        <el-input v-model="subForm.stock" class="w-150px" placeholder="库存" />
      </el-form-item>
      <el-form-item label="销量" prop="sellNum" :rules="formRules.isNotNull" label-position="left">
        <el-input v-model="subForm.sellNum" class="w-150px" placeholder="销量" />
      </el-form-item>
      <el-form-item label="浏览量" prop="viewNum" :rules="formRules.isNotNull" label-position="left">
        <el-input v-model="subForm.viewNum" class="w-150px" placeholder="浏览量" />
      </el-form-item>
      <el-form-item label="规格" prop="spec" :rules="formRules.isNotNull" label-position="left">
        <el-input v-model="subForm.spec" class="w-300px" placeholder="规格" />
      </el-form-item>
      <el-form-item label="产地" prop="spec" :rules="formRules.isNotNull" label-position="left">
        <el-input v-model="subForm.producer" class="w-300px" placeholder="产地" />
      </el-form-item>
      <el-form-item label="推荐理由" prop="spec" :rules="formRules.isNotNull" label-position="left">
        <el-input v-model="subForm.introduce" class="w-300px" placeholder="推荐理由" />
      </el-form-item>
      <el-form-item label="保质期" prop="spec" :rules="formRules.isNotNull" label-position="left">
        <el-input v-model="subForm.warrantyTime" class="w-300px" placeholder="保质期" />
      </el-form-item>
      <el-form-item label="储存方式" prop="spec" :rules="formRules.isNotNull" label-position="left">
        <el-input v-model="subForm.storageMode" class="w-300px" placeholder="储存方式" />
      </el-form-item>
      <el-form-item label="详细" prop="description" :rules="formRules.isNotNull" label-position="left">
        <el-input v-model="subForm.description" class="w-300px" placeholder="详细" />
      </el-form-item>
      <el-form-item label="商品图片" prop="imageUrl" label-position="left" :rules="formRules.isNotNull">
        <el-upload
          class="upload-demo"
          action="https://tcnet.club:5656/TCSoft/Files/FileUpload"
          :on-preview="handlePreview"
          :on-remove="handleRemove"
          :before-remove="beforeRemove"
          :on-success="onChang"
          list-type="picture"
          multiple
          :limit="1"
          :on-exceed="handleExceed"
        >
          <el-button size="small" type="primary">点击上传</el-button>
          <div slot="tip" class="el-upload__tip">只能上传jpg/png文件，且不超过500kb</div>
        </el-upload>
      </el-form-item>
      <el-form-item label="商品轮播图" prop="carousels" label-position="left" :rules="formRules.isNotNull">
        <el-upload
          class="upload-demo"
          action="https://tcnet.club:5656/TCSoft/Files/FileUpload"
          :on-preview="handlePreview"
          :on-remove="handleRemove"
          :before-remove="beforeRemove"
          :on-success="onChang2"
          list-type="picture"
          multiple
          :limit="5"
          :on-exceed="handleExceed"
        >
          <el-button size="small" type="primary">点击上传</el-button>
          <div slot="tip" class="el-upload__tip">只能上传jpg/png文件，且不超过500kb</div>
        </el-upload>
      </el-form-item>
      <el-form-item label="商品详情图" prop="descriptionImageUrl" label-position="left" :rules="formRules.isNotNull">
        <el-upload
          class="upload-demo"
          action="https://tcnet.club:5656/TCSoft/Files/FileUpload"
          :on-preview="handlePreview"
          :on-remove="handleRemove"
          :before-remove="beforeRemove"
          :on-success="onChang3"
          list-type="picture"
          multiple
          :limit="10"
          :on-exceed="handleExceed"
        >
          <el-button size="small" type="primary">点击上传</el-button>
          <div slot="tip" class="el-upload__tip">只能上传jpg/png文件，且不超过500kb</div>
        </el-upload>
      </el-form-item>
      <!-- <el-form-item label="测试" prop="descriptionImageUrl" label-position="left" :rules="formRules.isNotNull">
        <el-upload
          class="upload-demo"
          action="https://jsonplaceholder.typicode.com/posts/"
          :on-change="onChang4"
          :file-list="fileList"
          list-type="picture"
        >
          <el-button size="small" type="primary">点击上传</el-button>
          <div slot="tip" class="el-upload__tip">只能上传jpg/png文件，且不超过500kb</div>
        </el-upload>
      </el-form-item> -->
    </el-form>
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="closeFormModal">取 消</el-button>
        <el-button type="primary" @click="confirmBtnClick">确 定</el-button>
      </span>
    </template>
  </el-dialog>
</template>

<script setup>
/*1.初始化引入和实例化*/
const emit = defineEmits(['selectPageReq', 'hideComp'])
/*2.modal新增和修改*/
//新增
let subForm = reactive({
  id: '',
  name: '',
  commodityTypeId: '',
  commodityTypeName: '',
  price: '',
  originalPrice: '',
  imageUrl: '',
  stock: '',
  sellNum: '',
  viewNum: '',
  description: '',
  producer: '',
  introduce: '',
  warrantyTime: '',
  storageMode: '',
  carousels: '',
  spec: '',
  descriptionImageUrl: ''
})
const imgUrl = ref('https://tcnet.club:5656/TCSoft/Files/GetFileById?id=')
const dialogButton = reactive({
  dialogImageUrl: '',
  dialogVisible: false,
  disabled: false
})

onMounted(() => {
  getType()
})
const refForm = ref(null)
let confirmBtnClick = () => {
  refForm.value.validate((valid) => {
    if (valid) {
      if (subForm.id) {
        updateReq()
      } else {
        insertReq()
      }
    } else {
      return false
    }
  })
}
const { formRules, elMessage } = useElement()

let typeList = ref([])
//获取商品类型
let getType = () => {
  let reqConfig = {
    url: '/CommodityBusiness/CommodityType/get',
    method: 'get',
    isParams: true
  }
  axiosReq(reqConfig).then((resData) => {
    typeList.value = resData?.datas
  })
}

const insertReq = () => {
  const data = JSON.parse(JSON.stringify(subForm))
  delete data.id
  axiosReq({
    url: '/CommodityBusiness/Commodity/Add',
    data: data,
    method: 'post', //--c
    bfLoading: true
  }).then(() => {
    elMessage('保存成功')
    emit('selectPageReq')
    emit('hideComp')
  })
}
//修改
const reshowData = (row) => {
  Object.keys(row).forEach((fItem) => {
    Object.keys(subForm).forEach((sItem) => {
      if (fItem === sItem) {
        subForm[sItem] = row[sItem]
      }
    })
  })
}
let updateReq = () => {
  return axiosReq({
    url: '/CommodityBusiness/Commodity/Update',
    data: subForm,
    method: 'put',
    bfLoading: true
  }).then(() => {
    elMessage('更新成功')
    emit('selectPageReq')
    emit('hideComp')
  })
}

/*3.弹框相关*/
//显示弹框
const { dialogTitle, dialogVisible, chooseFileName } = useCommon()
let showModal = (isEdit, detailData) => {
  if (isEdit) {
    dialogTitle.value = `编辑【${detailData.name}】`
    dialogVisible.value = true
    reshowData(detailData)
  } else {
    dialogTitle.value = '添加【商品】'
    dialogVisible.value = true
  }
}
//关闭弹框
let closeFormModal = () => {
  emit('hideComp')
}

/*4.上传文件相关*/
const refSettingFile = ref(null)
const goUploadFile = () => {
  refSettingFile.value.click()
}

const onChang = (response, file, fileList) => {
  subForm.imageUrl = response
}

const onChang2 = (response, file, fileList) => {
  subForm.carousels = ''
  fileList.forEach((item) => {
    subForm.carousels += item.response + ','
  })
}

const onChang3 = (response, file, fileList) => {
  subForm.descriptionImageUrl = ''
  fileList.forEach((item) => {
    subForm.descriptionImageUrl += item.response + ','
  })
}

//图片
const fileList = reactive(
  {
    name: 'food.jpeg',
    url:
      'https://fuss10.elemecdn.com/3/63/4e7f3a15429bfda99bce42a18cdd1jpeg.jpeg?imageMogr2/thumbnail/360x360/format/webp/quality/100'
  },
  {
    name: 'food2.jpeg',
    url:
      'https://fuss10.elemecdn.com/3/63/4e7f3a15429bfda99bce42a18cdd1jpeg.jpeg?imageMogr2/thumbnail/360x360/format/webp/quality/100'
  }
)

const onChang4 = (response, file, fileList) => {
  // fileList = fileList
  subForm.descriptionImageUrl = ''
  fileList.forEach((item) => {
    subForm.descriptionImageUrl += item.response + ','
  })
}

//导出给refs使用
defineExpose({
  showModal
})

//导出属性到页面中使用
// let {levelList} = toRefs(state);
</script>

<style scoped lang="scss"></style>
