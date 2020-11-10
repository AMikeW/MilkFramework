--[[
@FileName: LuaEntry
@Date:     2018-04-09 04/09/18
@Author:   zhangpenghui
@Description:
]]

LuaEntry = {}

function LuaEntry.OnStart()
	LuaEntry.package = {}
	for key, v in pairs(package.loaded) do
		if key ~= "LuaEntry" then
			LuaEntry.package[key] = true
		end
	end
	LuaEntry.Gobal = {}
	for key, v in pairs(_G) do
		LuaEntry.Gobal[key] = true
	end
	
	--require "Utility/InitUtility"
	--require "LuaConfig"
	--require "LuaObject/LuaObject"
	--require "LuaObject/LuaObjectPool"
	--require "LuaObject/LuaNGUIForm"

	LuaEntry.updateForms = {}
	LuaEntry.fixedUpdateForms = {}

end

function LuaEntry.OnUpdate(deltaTime, unscaledDeltaTime)
	for key, form in pairs(LuaEntry.updateForms) do
		form:OnUpdate(deltaTime, unscaledDeltaTime)
	end
end

function LuaEntry.OnFixedUpdate(deltaTime, unscaledDeltaTime)
	for key, form in pairs(LuaEntry.fixedUpdateForms) do
		form:OnFixedUpdate(deltaTime, unscaledDeltaTime)
	end
end

function LuaEntry.OnDestroy()
	LuaEntry.Clear()
	LuaEntry.UnrequireAll()
end

function LuaEntry.RegisterUpdate( form )
	if LuaEntry.updateForms[form] == nil then
		LuaEntry.updateForms[form] = form
	end
end

function LuaEntry.UnregisterUpdate( form )
	if LuaEntry.updateForms[form] then
		LuaEntry.updateForms[form] = nil
	end
end

function LuaEntry.RegisterFixedUpdate( form )
	if LuaEntry.fixedUpdateForms[form] == nil then
		LuaEntry.fixedUpdateForms[form] = form
	end
end

function LuaEntry.UnregisterFixedUpdate( form )
	if LuaEntry.fixedUpdateForms[form] then
		LuaEntry.fixedUpdateForms[form] = nil
	end
end

function LuaEntry.RegisterModule( name, file )
	if LuaEntry[name] ~= nil then
		Log.Warning("module {0} - {1} already exist", name, file)
		return
	end
	local Module = require(file)
	local mod = Module.New()
	LuaEntry[name] = mod
	LuaEntry.modules[name] = mod
	mod:Init()
end

function LuaEntry.UnregisterModule( name )
	if LuaEntry[name] ~= nil then
		LuaEntry[name]:Destroy()
		LuaEntry[name] = nil
		LuaEntry.modules[name] = nil
	end
end

function LuaEntry.UnregisterAllModule()
	-- for name, module in pairs(LuaEntry.modules) do
	-- 	LuaEntry[name] = nil
	-- 	module:Destroy()
	-- end
	-- LuaEntry.modules = nil
end

function LuaEntry.Clear()
	--xlua.clear()
	--Lang.Clear()
	--DataTable.Clear()
	LuaEntry.UnregisterAllModule()
	LuaEntry.updateForms = nil
	LuaEntry.fixedUpdateForms = nil
	LuaEntry.ClearGobal()
end

function LuaEntry.ClearGobal()
	for key, v in pairs(_G) do
		if not LuaEntry.Gobal[key] then
			_G[key] = nil
		end
	end
end

function LuaEntry.UnrequireAll()
	for key, v in pairs(package.loaded) do
		if not LuaEntry.package[key] then
			package.loaded[key] = nil
		end
	end
end