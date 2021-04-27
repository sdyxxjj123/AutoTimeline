# AutoTimeline

代码基于GPLv3开源，仅供学习交流，禁止用于商业用途

仅适配x64模拟器 x32的pcr

(什么?你不想写代码就想用?请打开auto)

## 用法

### 编写timeline.lua

AutoPcrApi:

- `void autopcr.calibrate(id)` 对站位id进行校准
- `void autopcr.press(id)` 点击站位为id的角色，不占用时间，但可能点不上
- `void autopcr.framePress(id)` 点击站位为id的角色，保证点上，占用两帧，一般用于连点
- `long autopcr.getUnitAddr(unit_id, rarity, rank)` 根据数据获取角色的句柄，请务必保证搜索时该角色tp为0且满血，否则会搜索失败
- `float autopcr.getTp(unit_handle)` 根据获得的句柄返回角色当前tp
- `long autopcr.getHp(unit_handle)` 根据获得的句柄返回角色当前hp
- `long autopcr.getMaxHp(unit_handle)` 根据获得的句柄返回角色最大hp
- `int autopcr.getFrame()` 返回当前时间
- `float autopcr.getTime()` 返回当前帧数
- `void autopcr.waitFrame(frame)` 暂停程序直到帧数达到
- `void autopcr.waitTime(frame)` 暂停程序直到时间达到
- `void autopcr.setOffset(frame_offset, time_offset)` 设定延迟校准参数

MiniTouchApi:

- `void minitouch.getMaxX()` 返回最大X
- `void minitouch.getMaxY()` 返回最大Y
- `void minitouch.connect(host, port)` 链接minitouch server
- `void minitouch.write(text)` 写minitouch指令
- `void minitouch.setPos(id, x, y)` 注册站位id
- `void minitouch.press(id)` 点击站位为id的角色，不占用时间，但可能点不上
- `void minitouch.framePress(id)` 点击站位为id的角色，保证点上，占用两帧，一般用于连点

### 依赖

项目依赖于`.net 5.0 runtime`，请自行百度

### 延迟校准

校准代表着模拟器处理造成的延迟，一般会保持不变，技能释放时，如果打开技能动画，帧数会暂停，你可以根据暂停时候的值和预期值做出帧数的校准

### 运行程序

1. 必须使用管理员模式运行，设置帧率为60，先进入对战，然后在开始时暂停
2. 先进行五个站位的鼠标位置的校准，从1-5，将鼠标移至对应位置然后在窗口按回车即可
3. 输入模拟器主程序的PID(不要输错成前台ui程序)
4. 等待扫描，结束后会显示当前帧数和剩余时间
5. 继续模拟器即可
6. 继续运行后不要乱动鼠标！！！

### 关于Minitouch

Minitouch可以显著减小模拟器层触控延迟，repo内附带bin版minitouch，[使用说明](https://github.com/DeviceFarmer/minitouch)
如果有的菜鸡弄不明白怎么用，也可以使用传统方法。
如果adb在path中可以用minitouch文件夹下的两个bat直接把minitouch开到1111端口(先运行adbshell再运行adbforwarding, adbshell不要关)
