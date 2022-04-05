# RDTracker
RDTracker for Astonia

To make values show in questlog of your client add the following to your client code
start of `do_display_random`  `questlog.c`
```c
    static unsigned int Base = 0;
    if (!Base)
    {
        Base = (unsigned int)GetModuleHandle(NULL);
    }
```end of `do_display_random`
```c
    y = dd_drawtext_break(10, y, 204, graycolor, 0, "Only shrines in dungeons you have already solved (used the continuity shrine), but not yet used, are shown. The continuity shrine shown is the first one you haven't used yet.");
    y += 12;
    dd_drawtext(10, y, graycolor, 0, "RDTracker:");
    y += 12;
    dd_drawtext_fmt(16, y, graycolor, 0, "Key %d iSprite %d", (unsigned int)&originx - Base, (unsigned int)&map[MAXMN/2] - Base + (unsigned int)&map->isprite - (unsigned int)&map);
    y += 12;
    dd_drawtext_fmt(16, y, graycolor, 0, "flags %d fSprite %d offXY %d %d",
                    (unsigned int)&map->flags - (unsigned int)&map->isprite,
                    (unsigned int)&map->fsprite - (unsigned int)&map->isprite,
                    sizeof(map[0]),
                    MAPDY);
    y += 12;
    return y;
}```
