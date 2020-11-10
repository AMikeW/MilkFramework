--[[
@FileName: LuaConfig
@Date:     2018-04-09 04/09/18
@Author:   zhangpenghui
@Description:
]]

LuaConfig = {}

-- 雷达地图配置
LuaConfig.RadarMap = {
	Scale = 2,	-- 地图缩放比
}

-- 分线状态
LuaConfig.LineState = {
	Busy	= 	150,	-- 繁忙
	Full	=	200,	-- 爆满
}

-- 区域地图配置
LuaConfig.RegionalMap = {
	DrawInterval		=	15,						-- 绘制间隔
	DotSize				=	10,						-- 路径点大小
	ColliderDistance	=	5,						-- 碰撞距离
	SelectedTextColor	=	Color(68/255, 92/255, 114/255),		-- 选中文字颜色
	UnselectedTextColor	=	Color(34/255, 39/255, 53/255),		-- 未选中文字颜色
	FindPathIntervalue	=	3,
}

-- 通知配置
LuaConfig.ToastData = {
	ShowInterval	=	0.5,	-- 显示间隔
    UpSpeed			=	1,		-- 上升速度
    FadeSpeed		=	0.6,	-- 消失速度
    StayTime		=	0.3,	-- 停留时间
    LifeCycle		=	0.8,	-- 生命周期
}

-- 升级界面配置
LuaConfig.LevelUpForm = {
	Life 			=	2,		-- 停留时间
}

-- 装备界面配置
LuaConfig.EquipForm = {
	RotateSpeed		=	1,		-- 旋转速度
}

--对话表示玩家名称的配置
LuaConfig.PlayerName='#{_PLAYERNAME}'


