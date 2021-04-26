local charas = {
    autopcr.getUnitAddr(104701, 5, 12),
    autopcr.getUnitAddr(104301, 5, 13),
    autopcr.getUnitAddr(102701, 5, 13),
    autopcr.getUnitAddr(107101, 5, 12),
    autopcr.getUnitAddr(170101, 5, 13)
};

while (autopcr.getTime() > 1)
do
    for i = 1, 5 do
        if (autopcr.getTp(charas[i]) == 1000)
        then
            autopcr.framePress(i);
            break;
        end
    end
end