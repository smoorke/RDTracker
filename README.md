# RDTracker
[RDTracker](https://github.com/smoorke/RDTracker/releases/download/RDTracker/RDTracker.exe) for Astonia3

To make values show in questlog of your client add the following to your client code

start of `do_display_random`  `questlog.c`
```c
    static unsigned int Base, Key, Isprite, OffX, OffY;
    static int Flags, Fsprite;
    static char Swap;
    if (!Base)
    {
        Base = (unsigned int)GetModuleHandle(NULL);
        if ((unsigned int)&originx < (unsigned int)&originy){
            Key = (unsigned int)&originx - Base;
            Swap = 0;
        }else{
            Key = (unsigned int)&originy - Base;
            Swap = 1;
        }
        Isprite = (unsigned int)&map[MAXMN/2] - Base + (unsigned int)&map->isprite - (unsigned int)&map;
        Flags = (unsigned int)&map->flags - (unsigned int)&map->isprite;
        Fsprite = (unsigned int)&map->fsprite - (unsigned int)&map->isprite;
        OffX = sizeof(map[0]);
        OffY = MAPDY;
    }
```
end of `do_display_random`
```c
    y = dd_drawtext_break(10, y, 204, graycolor, 0, "Only shrines in dungeons you have already solved (used the continuity shrine), but not yet used, are shown. The continuity shrine shown is the first one you haven't used yet.");
    y += 12;
    dd_drawtext(10, y, graycolor, 0, "RDTracker:");
    y += 12;
    dd_drawtext_fmt(15, y, graycolor, 0, "Key %d iSprite %d%s", Key, Isprite, Swap?" SwapXY":"");
    y += 12;
    dd_drawtext_fmt(15, y, graycolor, 0, "flags %d fSprite %d offXY %d %d", Flags, Fsprite, OffX, OffY);
    y += 12;
    return y;
}
```
