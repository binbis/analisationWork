# Documentation

## _fastCopDelta_
    виправляє координати MGRS, відкриває дельту та шукає
- Дельта повинна бути відкритою окремим вікном`(окремою вкладкою)` або перед використанням перейти на вкладку дельти.
- Виділяєш текст`(буддь де)`де є кори mgrs.
- жмеш `Alt+X`, магія почне працювати

## _createMarkInJbd_
    заповнює або оновлює з жбд обрану мітку
- `дивись гіфку`
гіфка
- в дельті обераєш відповідну мітку для заповлення`(оновлення)`
- в жбд обираєш будь яку клітинку`(аби вона була на необхідному рядку)`
- жмеш `Alt+Z`, магія почне працювати

## _ecipashCreateFast_
    створює папку на пробочому столі в якій будуть створені папки з екіпажами
- відкрий ексель табличку `Планування ...`
- перейди на сторінку `Звіт`
- обери необхідну дату
- побери 8 рядок(клітинку)
- жмеш `Ctrl+Alt+T`, магія почне працювати

HotKeys:

	hk["Alt+Z"] = o => script.run(@"\analisationWork-main\main\fastCopDelta.cs");
	hk["Alt+X"] = o => script.run(@"\analisationWork-main\main\createMarkInJbd.cs");
	hk["Ctrl+Alt+T"] = o => script.run(@"\analisationWork-main\main\ecipashCreateFast.cs");
	hk["Alt+Shift+F"] = o => script.run(@"\analisationWork-main\main\fastZonaHiro.cs");