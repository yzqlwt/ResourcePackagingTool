# ResourcesPackageTools v1.0.0

## 简介

1. 可对资源打包进行(plist, swf, csb,***)
2. 可把所有png合图,后续版本可提供是否合图选项
3. 在工具内可对资源重命名(Name值用来索引资源，不能重复)
4. 对任意资源添加自定义属性(实用性未知，可参考github上的图片及描述)
5. __导出zip,并可以上传到后台,后台资源以列表形式呈现，可下载后台配置包，导入配置包，使用导出的配置包替换后台资源__
6. __导入后台配置包可以还原到此配置包(可用于多次替换资源，简化操作，点点点就搞定。plist拆分成png。)__
7. __替换配置包会在backup目录下保存一份后台的配置，按时间命名__
8. ResourcePanel支持拖入多个资源
9. 使用后台账户密码登录工具，并且绑定设备码
10. 与配置包一同导出的还有一段lua代码，帮助解析使用配置包

## 下载
https://github.com/yzqlwt/ResourcePackagingTool/releases

## 几个使用场景

- 改需求了，需要增加一张图。

  1. 打开工具选择互动,点击myGod，选择Skin

  2. 点击导入配置包，此时ResourcePanel展示的就是后台的配置

  3. 拖入一张图片，然后点击图片，命名加属性

  4. 点击导出

  5. 点击myGod,选择Skin，替换后台资源

  6. 在backup目录下有一个替换前的资源备份

- 需要换一张图。 

  1. 打开工具选择互动,点击myGod，选择Skin
  2. 点击导入配置包，此时ResourcePanel展示的就是后台的配置
  3. 右键点击删除图片，拖入一张正确的图片，然后点击图片，命名加属性
  4. 点击导出
  5. 点击myGod,选择Skin，替换后台资源
  6. 在backup目录下有一个替换前的资源备份

-  两个skin资源差异很小，可导入Skin1的资源然后导出替换Skin2的资源(更推荐使用Chrome插件)

- 查看配置包内容。导入配置包，在ResourcePanel中显示



## 总之

- 资源打包，后台干净

- png合图，提高效率

- 关联后台，一键上传

- 导入导出，差异修改

- 替换备份，安全可靠

  

  

## 使用提示： ##
图片合图调用的是TexturePackage,首次使用TP工具需要先同意协议。

## 简单使用流程 ##
![image](https://github.com/yzqlwt/ResourcePackagingTool/blob/master/raw/微信图片编辑_20191008151755.jpg)
![image](https://github.com/yzqlwt/ResourcePackagingTool/blob/master/raw/微信截图_20191008152445.png)
![image](https://github.com/yzqlwt/ResourcePackagingTool/blob/master/raw/微信截图_20191008153458.png)
![image](https://github.com/yzqlwt/ResourcePackagingTool/blob/master/raw/微信截图_20191008160432.png)
