#ifndef _MML_EXAMPLE_GAME_HIERARCHY_HPP_INCLUDED_
#define _MML_EXAMPLE_GAME_HIERARCHY_HPP_INCLUDED_


struct game_object
{
    virtual ~game_object()
    {
    }
};


struct space_ship
    : game_object
{
};

struct space_station
    : game_object
{
};

struct asteroid
    : game_object
{
};

#endif 
