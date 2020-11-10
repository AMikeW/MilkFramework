--
-- 枚举
-- 描述：将固定的枚举重写
-- ======================================

-- 关闭游戏框架类型。
-- ======================================
ShutdownType = {
	None	=	0,	-- 仅关闭游戏框架。
	Restart	=	1,	-- 关闭游戏框架并重启游戏。
	Quit	=	2,	-- 关闭游戏框架并退出游戏。
}

-- 界面类型
-- ======================================
UIFormEnum = {
	GTWindowForm		=	1,
}

-- EasyTouch Button 状态
-- ======================================
EasyTouchButtonState = {
	Normal		=	CS.UIButtonColor.State.Normal,
	Hover		=	CS.UIButtonColor.State.Hover,
	Pressed		=	CS.UIButtonColor.State.Pressed,
	Disabled	=	CS.UIButtonColor.State.Disabled,
}

-- DataTable 获取
-- ======================================
DataTableGetRow = {
	All		=	-9999,
}

-- 摄像机Layer
-- ======================================
CameraLayer = {
	UI		=		5,
}

-- 区域地图NpcType
-- ======================================
MapNpcType = {
	Function	=	1,
	Story		=	2,
	Monster		=	3,
}

-- 操作返回结果
-- ======================================
OperateResult = {
	OK		=			0,		-- 成功
}


-- 请求查看装备模式
-- ======================================
AskEquipMode = {
	All		=		0,		-- 所有
	Set 	=		1,		-- 查询集合
}


-- Tips类型
-- ======================================
EnumTipsType =
{
    None    =   -1,  --无
    Item    =   0,   --道具
    Equip   =   1,   --装备
    Gem     =   2,   --宝石
}


