import { createRouter, createWebHashHistory } from 'vue-router'
import Layout from '@/layout'

/* Router Modules */
import chartsRouter from './modules/charts'
import useExample from './modules/useExample'
import writingDemo from './modules/writingDemo'

export const constantRoutes = [
  {
    path: '/redirect',
    component: Layout,
    hidden: true,
    children: [
      {
        path: '/redirect/:path(.*)',
        component: () => import('@/views/redirect')
      }
    ]
  },
  {
    path: '/login',
    component: () => import('@/views/login/Login.vue'),
    hidden: true
  },
  {
    path: '/404',
    component: () => import('@/views/error-page/404.vue'),
    hidden: true
  },
  {
    path: '/401',
    component: () => import('@/views/error-page/401.vue'),
    hidden: true
  },
  {
    path: '/',
    component: Layout,
    redirect: '/dashboard',
    children: [
      {
        path: 'dashboard',
        name: '首页',
        component: () => import('@/views/dashboard/index.vue'),
        //using el svg icon, the elSvgIcon first when at the same time using elSvgIcon and icon
        meta: { title: '首页', elSvgIcon: 'Fold' }
      }
    ]
  },
  {
    path: '/crud',
    component: Layout,
    meta: { title: '系统管理', icon: 'guide' },
    alwaysShow: true,
    children: [
      {
        path: 'crud',
        component: () => import('@/views/crud/crud'),
        name: 'crud',
        meta: { title: '商品类型' }
      },
      {
        path: 'commodityManagement',
        component: () => import('@/views/crud/commodityManagement'),
        name: 'commodityManagement',
        meta: { title: '管理商品' }
      },
      {
        path: 'carouselChart',
        component: () => import('@/views/crud/carouselChart'),
        name: 'carouselChart',
        meta: { title: '管理轮播图' }
      }
    ]
  },
  {
    path: '/',
    component: Layout,
    redirect: '/message',
    children: [
      {
        path: 'message',
        name: '聊天消息',
        component: () => import('@/views/crud/crud/message.vue'),
        //using el svg icon, the elSvgIcon first when at the same time using elSvgIcon and icon
        meta: { title: '聊天消息', elSvgIcon: 'Comment' }
      }
    ]
  }
]

/**
 * asyncRoutes
 * the routes that need to be dynamically loaded based on user roles
 */
export const asyncRoutes = [
  // 404 page must be placed at the end !!!
  // using pathMatch install of "*" in vue-router 4.0
  { path: '/:pathMatch(.*)', redirect: '/404', hidden: true }
]

const router = createRouter({
  history: createWebHashHistory(),
  scrollBehavior: () => ({ top: 0 }),
  routes: constantRoutes
})

export default router
