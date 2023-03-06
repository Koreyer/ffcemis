import request from '@/utils/axiosReq'

export function loginReq(data) {
  return request({
    url: '/IdentityManager/IdentityManager/Login',
    data,
    method: 'post',
    bfLoading: false,
    isAlertErrorMsg: false
  })
}

export function getInfoReq() {
  return request({
    url: '/IdentityManager/AppUser/UserGetSelf',
    bfLoading: false,
    method: 'get',
    isAlertErrorMsg: false
  })
}

export function logoutReq() {
  return request({
    url: '/IdentityManager/IdentityManager/Logout',
    method: 'post'
  })
}
