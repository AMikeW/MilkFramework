---
--- Created by XianBiao
--- DateTime: 2020/11/8
--- 描述：  Init 和 Open的userData是一样的, 其他待测
---
---@class BaseUIForm
local BaseUIForm = class("BaseUIForm")

function BaseUIForm:Init(userData)
    self:OnInit(userData)
end

function BaseUIForm:Recycle()
    self:OnRecycle()
end

function BaseUIForm:Open(userData)
    self:OnOpen(userData)
end

function BaseUIForm:Close(isShutdown, userData)
    self:OnClose(isShutdown, userData)
end

function BaseUIForm:Update(elapseSeconds, realElapseSeconds)
    self:OnUpdate(elapseSeconds, realElapseSeconds)
end

function BaseUIForm:Pause()
    self:OnPause()
end

function BaseUIForm:Resume()
    self:OnResume()
end

function BaseUIForm:Cover()
    self:OnCover()
end

function BaseUIForm:Reveal()
    self:OnReveal()
end

function BaseUIForm:Refocus(userData)
    self:OnRefocus(userData)
end

function BaseUIForm:DepthChanged(uiGroupDepth, depthInUIGroup)
    self:OnDepthChanged(uiGroupDepth, depthInUIGroup)
end

function BaseUIForm:OnInit(userData) end
function BaseUIForm:OnRecycle() end
function BaseUIForm:OnOpen(userData) end
function BaseUIForm:OnClose(isShutdown, userData) end
function BaseUIForm:OnPause() end
function BaseUIForm:OnResume() end
function BaseUIForm:OnCover() end
function BaseUIForm:OnReveal() end
function BaseUIForm:OnUpdate(elapseSeconds, realElapseSeconds) end
function BaseUIForm:OnRefocus(userData) end
function BaseUIForm:OnDepthChanged(uiGroupDepth, depthInUIGroup) end

return BaseUIForm