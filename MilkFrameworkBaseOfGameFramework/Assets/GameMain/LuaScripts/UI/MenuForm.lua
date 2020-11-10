---
--- Created by XianBiao
--- DateTime: 2020/11/8
---

---@class MenuForm : BaseUIForm
local MenuForm = class("MenuForm", BaseUIForm)
local base = BaseUIForm

function MenuForm:OnInit(userData)
    base.OnInit(self, userData)
end

function MenuForm:OnRecycle()
    base.OnRecycle(self)
end

function MenuForm:OnOpen(userData)
    base.OnOpen(self, userData)
end

function MenuForm:OnClose(isShutdown, userData)
    base.OnClose(self, isShutdown, userData)
end

return MenuForm