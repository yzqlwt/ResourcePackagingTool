PackageResParser = class("PackageResParser")

function PackageResParser.initRes(packageAssetPath, assetUri, fn)
    uiUtils:unzipSkinAsset(assetUri, packageAssetPath, packageAssetPath, function()
        PackageResParser.packageAssetPath = packageAssetPath
        PackageResParser._initResConfig(packageAssetPath,fn)
    end)
end

function PackageResParser._initResConfig(path,fn)
    local filecontent = getFileContent(path.."/ResConfig.json")
    local config = json.decode(filecontent)
    PackageResParser._config = config
    display.loadSpriteFrames(path.."/default.plist", path.."/default.png")
    fn()
end


function PackageResParser.updateTexture(imageView, name)
    local md5 = PackageResParser._config[name].MD5
    local imagePath = md5..".png"
    imageView:loadTexture(
        imagePath,
        ccui.TextureResType.plistType
    )
end

function PackageResParser.getResPath(name)
    dump(PackageResParser._config)
    local value = PackageResParser._config[name]
    if not value then
        print("资源未找到！！！！")
        return ""
    end
    local extension = value.Extension
    local md5 = value.MD5
    if extension == ".png" then
        return md5..extension
    end
    return PackageResParser.packageAssetPath..md5..extension
end

return PackageResParser

-- USAGE:
-- function Gxxx:loadSkin()
--     local assetPath = self:getSkinProperty('path')
--     local skinUri = findTable(self.activity.skin.attachments, 'name', 'ResConfig').uri
--     PackageResParse.initRes(assetPath,skinUri,function()
--         --资源包初始化完成回调
--         --xxxxx
--     end)
-- end
--
--创建ImageView:
-- local image = ccui.ImageView:create(''):addTo(parent)
-- PackageResParser.updateTexture(image,name)
--
--换图:
--PackageResParser.updateTexture(image,name)
