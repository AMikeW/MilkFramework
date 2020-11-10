--[[
@FileName: ProcedureMain
@Date:     2018-04-10 04/10/18
@Author:   zhangpenghui
@Description:
]]

-- local _networkCloseEvent = UGF.NetworkClosedEventArgs.EventId
local _openUIFormSuccessEvent = UGF.OpenUIFormSuccessEventArgs.EventId
local _closeUIFormCompleteEvent = UGF.CloseUIFormCompleteEventArgs.EventId
local _readyToChangeSceneEvent = GT.ReadyToChangeSceneEventArgs.EventId

local _hideJoystickUI = {
    Exclusive = true,
    UI = true,
    Mask = true,
    Story = true,
}

local ProcedureMain = class("ProcedureMain")

---Ctor 这方法是在ProcedureMain.New时调用的构造方法..
function ProcedureMain:Ctor()
    -- self.networkCloseInvoker = function(sender, gameEventArgs) 
    --     self:OnNetworkClosed(sender, gameEventArgs) 
    -- end 
    --Log.Info("ProcedureMain-----Ctor")

    ---TODO 打开UI成功回调 C#侧未接入
    self.openUIFormSuccess = function(sender, gameEventArgs) 
        self:OnOpenUIFormSuccess(sender, gameEventArgs) 
    end

    ---TODO 关闭UI完成回调 C#侧未接入
    self.closeUIFormComplete = function(sender, gameEventArgs) 
        self:OnCloseUIFormComplete(sender, gameEventArgs) 
    end

    ---TODO 准备改变Scene C#侧未接入
    self.readyToChangeSceneInvoker = function(sender, gameEventArgs) 
        self:OnWillChangeScene(sender, gameEventArgs) 
    end

    self.hideJosytick = {}
end

function ProcedureMain:OnEnter(procedureOwner)
    self.procedureOwner = procedureOwner
    Log.Info("Lua ProcedureMain:OnEnter")

    self:OpenUIForms()
end

function ProcedureMain:OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds)
    --寻路算法的tick
    -- local activePath = CS.AstarPath.active
    -- if activePath then
    --     activePath:Tick()
    -- end 
end

function ProcedureMain:OnLeave(procedureOwner, isShutdown)

end

function ProcedureMain:OnWillChangeScene(sender, gameEventArgs)
    self.CS_ChangeState(typeof(GT.ProcedureChangeScene))
end

function ProcedureMain:OnOpenUIFormSuccess(sender, gameEventArgs)
    local e = gameEventArgs    

    if e.UserData.UIData and e.UserData.UIData.HideJoystick then
        self.hideJosytick[e.UIForm.SerialId] = true
        self:HandleJoystick()
    end
end

function ProcedureMain:OnCloseUIFormComplete(sender, gameEventArgs)
    if self.hideJosytick[gameEventArgs.SerialId] then
        self.hideJosytick[gameEventArgs.SerialId] = nil
        self:HandleJoystick()
    end
end


function ProcedureMain:OpenUIForms()
    Log.Info("Lua ProcedureMain:OpenUIForms")
    --GameEntry.UI:OpenUIFormById(UIFormEnum.GTWindowForm, self)
end

-- 新对象保留在C#的Procedure的worker中
return ProcedureMain.New()
