-- scan character handles
local charas = {
    autopcr.getUnitAddr(104701, 5, 12),
    autopcr.getUnitAddr(104301, 5, 13),
    autopcr.getUnitAddr(102701, 5, 13),
    autopcr.getUnitAddr(107101, 5, 12),
    autopcr.getUnitAddr(170101, 5, 13)
};

-- data for 1600x900
---[[ minitouch test
minitouch.connect("localhost", 1111);
for i = 0, 4 do
    minitouch.setPos(5 - i, 400 + i * 208, 860);
end
minitouch.setPos(6, 1544, 716); --auto
minitouch.setPos(7, 1544, 839); --forward
minitouch.setPos(8, 1512, 43);  --pause
--]]

--[[ mouse calibration
for i = 1, 5 do
    autopcr.calibrate(i);
end
--]]

while (autopcr.getTime() > 1) --when not end
do
    for i = 1, 5 do --judge every chara
        if (autopcr.getTp(charas[i]) == 1000) --ready for tp
        then
            --autopcr.framePress(i);
            minitouch.press(i); --trigger ub press
            break;
        end
    end
end

minitouch.press(8);