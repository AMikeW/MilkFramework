--[[
@FileName: LuaObject
@Date:     2018-04-11 04/11/18
@Author:   zhangpenghui
@Description:
]]

LuaObject = class("LuaObject")

-- 构造函数
-- ======================================
function LuaObject:Ctor()
	-- 注册Update
	self.m_IsRegisterUpdate = false

	-- 注册FixedUpdate
	self.m_IsRegisterFixedUpdate = false

	-- 事件订阅表
	self.m_SubscribeEvents = {}

	-- 获取引用缓存
	self.m_ReferenceCache = {}
end

-- 停止逻辑
-- ======================================
function LuaObject:StopLogic()
	self:UnregisterUpdate()
	self:UnregisterFixedUpdate()
	self:UnsubscribeAllEvent()
	self:ReleaseAllReference()
end

-- 销毁
-- ======================================
function LuaObject:OnDestroy()
	self:StopLogic()

	self.m_ReferenceCache = nil
	self.m_SubscribeEvents = nil
end

-- 注册Update
-- ======================================
function LuaObject:RegisterUpdate()
	if self.m_IsRegisterUpdate then
		return
	end
	self.m_IsRegisterUpdate = true
	LuaEntry.RegisterUpdate(self)
end

-- 反注册Update
-- ======================================
function LuaObject:UnregisterUpdate()
	if self.m_IsRegisterUpdate then
		LuaEntry.UnregisterUpdate(self)
		self.m_IsRegisterUpdate = false
	end
end

-- 注册FixedUpdate
-- ======================================
function LuaObject:RegisterFixedUpdate()
	if self.m_IsRegisterFixedUpdate then
		return
	end
	LuaEntry.RegisterFixedUpdate(self)
	self.m_IsRegisterFixedUpdate = true
end

-- 反注册FixedUpdate
-- ======================================
function LuaObject:UnregisterFixedUpdate()
	if self.m_IsRegisterFixedUpdate then
		LuaEntry.UnregisterFixedUpdate(self)
		self.m_IsRegisterFixedUpdate = false
	end
end

-- 订阅事件
-- param : id		事件id
-- param : handler	回调函数
-- ======================================
function LuaObject:SubscribeEvent( id, handler )
	if self.m_SubscribeEvents[id] then
		Log.Warning("Event id: {0} already subscribed.", id)
		return
	end

	if handler == nil then
		Log.Warning("Subscribed Event {0} handler is null", id)
		return
	end

	self.m_SubscribeEvents[id] = handler
	GameEntry.Event:Subscribe(id, handler)
end

-- 退订事件
-- param : id		事件id
-- ======================================
function LuaObject:UnsubscribeEvent( id )
	if self.m_SubscribeEvents[id] == nil then
		Log.Warning("Event id: {0} not subscribed.", id)
		return
	end

	GameEntry.Event:Unsubscribe(id, self.m_SubscribeEvents[id])
	self.m_SubscribeEvents[id] = nil
end

-- 退订所有事件
-- ======================================
function LuaObject:UnsubscribeAllEvent()
	for id, handler in pairs(self.m_SubscribeEvents) do
		GameEntry.Event:Unsubscribe(id, handler)
	end
	self.m_SubscribeEvents = {}
end

-- 从引用池获取引用
-- param : t 			引用类型
-- ======================================
function LuaObject:AcquireReference( t )
	local reference = ReferencePool.Acquire(t)
	self.m_ReferenceCache[reference] = t
	return reference
end

-- 将引用归还引用池
-- param : reference 	引用
-- ======================================
function LuaObject:ReleaseReference( reference )
	if self.m_ReferenceCache[reference] == nil then
		Log.Warning("Reference {0} not found.", reference.GetType())
	end

	ReferencePool.Release(self.m_ReferenceCache[reference], reference)
	self.m_ReferenceCache[reference] = nil
end

-- 归还所有引用
-- ======================================
function LuaObject:ReleaseAllReference()
	for reference, t in pairs(self.m_ReferenceCache) do
		ReferencePool.Release(self.m_ReferenceCache[reference], reference)
	end
	self.m_ReferenceCache = {}
end

-- 创建一个通用事件
-- ======================================
function LuaObject:AcquireCommonEvent( type )
	local ref = ReferencePool.Acquire(typeof(PS.CommonEventArgs))
	ref:Fill(type)
	return ref
end

