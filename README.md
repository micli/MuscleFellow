# MuscleFellow
---
## MuscleFellow 是什么？
为了配合我个人书籍《微软开源跨平台移动开发实践》这本书籍的写作和演示，我创建了MuscleFellow 这个开源项目来作为本书的样例代码。
MuscleFellow 一个用Visual Studio 2015 Update 3 创建的，用来演示.NET Core 和 ASP.NET Core 1.0 的Demo。
整个项目的解决方案叫做MuscleFellow, 同时也是这个网站的名字:做肌肉伙伴(MuscleFellow)。这个基于ASP.NET Core 开发的网站可以实现简单的商品购买、购物车、地址管理、订单查看等功能。
同时，这个MuscleFellow.API 项目还提供了对外的Web API 功能。用来支持Cordova 和Xamarin 技术开发的移动程序的访问。

## 涉及到的技术主要有：
### Azure 部署
目前这个网站部署在微软云Azure 的云服务(Cloud Service)上面，以经典模式进行部署。Web 服务器采用Ubuntu 14.04 LTS，Web 服务通过Nginx + Supervisor + Kestrel 进行发布。
发布的Web 服务包括肌肉伙伴网站: http://musclefellow.chinacloudapp.cn 和肌肉伙伴Web API http://musclefellow.chinacloudapp.cn:8081
### ASP.NET Core
MuscleFellow 的网站项目MuscleFellow.Web 和MuscleFellow的Web API 项目MuscleFellow.API 都是通过ASP.NET Core 技术进行开发。在项目中演示了Tag Helper、Middleware、Routing、依赖注入等ASP.NET Core 的主要特性。
### ASP.NET Core Web API
在MuscleFellow.API 项目中着重演示了如何实现对HTTP GET、POST、PUT 等谓词的处理。
### Entity Framework Core
在MuscleFellow.Data 项目中主要实现了通过Resposity 模式，用Entity Framework Core 去访问数据库。
### Apache Cordova 开发
演示了使用Ionic 框架配合JavaScript 脚本实现一款基于Cordova 技术的移动应用，项目可以直接编译成Android 使用的apk 应用程序包。通过配置，也可将项目编译成iOS 使用的ipa。
### Xamarin.Forms 开发
演示了使用Xamarin.Forms 实现一个自定义的ListView 来展示MuscleFellow 的产品，并支持使用Xamarin 提供的iOS 模拟器在Windows 上进行应用程序调试。

## 如何购买《微软开源跨平台移动开发实践》这本书？
### 天猫购书: [请点击这里](https://list.tmall.com/search_product.htm?q=%E5%BE%AE%E8%BD%AF%E5%BC%80%E6%BA%90%E8%B7%A8%E5%B9%B3%E5%8F%B0%E7%A7%BB%E5%8A%A8%E5%BC%80%E5%8F%91%E5%AE%9E%E8%B7%B5&type=p&vmarket=&spm=875.7931836%2FA.a2227oh.d100&from=mallfp..pc_1_searchbutton)
### 京东购书: [请点击这里](http://search.jd.com/Search?keyword=%E5%BE%AE%E8%BD%AF%E5%BC%80%E6%BA%90%E8%B7%A8%E5%B9%B3%E5%8F%B0%E7%A7%BB%E5%8A%A8%E5%BC%80%E5%8F%91%E5%AE%9E%E8%B7%B5&enc=utf-8&wq=%E5%BE%AE%E8%BD%AF%E5%BC%80%E6%BA%90%E8%B7%A8%E5%B9%B3%E5%8F%B0%E7%A7%BB%E5%8A%A8%E5%BC%80%E5%8F%91%E5%AE%9E%E8%B7%B5&pvid=3039ekwi.ihvhms)

### 书籍目录如下:
1. 微软“云+端”站略
2. 开源跨平台的ASP.NET Core
3. 开源跨平台的设备开发
4. 项目设计和需求分析
5. 构建开发环境
6. 构建实体模型
7. 创建Web API 网站
8. 创建Web API 服务
9. 在Azure 上部署服务
10. AngularJS 和Ionic
11. 使用Cordova开发跨平台移动应用
12. 使用Xamarin开发跨平台移动应用

