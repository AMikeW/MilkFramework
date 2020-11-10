---
--- Created by XianBiao
--- DateTime: 2020/11/9
---
---@class UICommonTipForm : BaseUIForm
local UICommonTipForm = class("UICommonTipForm", BaseUIForm)
local base = BaseUIForm

function UICommonTipForm:OnInit(userData)
    base.OnInit(self, userData)
    Log.LogError("UICommonTipForm:OnInit userdata=" .. tostring(userData))
end

function UICommonTipForm:OnRecycle()
    base.OnRecycle(self)
    Log.LogError("UICommonTipForm:OnRecycle")
end

function UICommonTipForm:OnOpen(userData)
    base.OnOpen(self, userData)
    Log.LogError("UICommonTipForm:OnOpen userdata=" .. tostring(userData))
end

function UICommonTipForm:OnClose(isShutdown, userData)
    base.OnClose(self, isShutdown, userData)
    Log.LogError("UICommonTipForm:OnClose")
end

return UICommonTipForm