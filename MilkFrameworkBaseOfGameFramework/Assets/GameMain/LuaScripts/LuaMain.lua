--[[
@FileName: LuaMain
@Date:     2018-04-09 04/09/18
@Author:   zhangpenghui
@Description:
]]

require "Init"

function OnStart()
	LuaEntry.OnStart();
	CS.UnityEngine.Debug.Log("LuaEntry Start")
end

function OnUpdate(deltaTime, unscaledDeltaTime)
	LuaEntry.OnUpdate(deltaTime, unscaledDeltaTime);
end

function OnFixedUpdate(deltaTime, unscaledDeltaTime)
	LuaEntry.OnFixedUpdate(deltaTime, unscaledDeltaTime);
end

function OnDestroy()
	LuaEntry.OnDestroy();
end

