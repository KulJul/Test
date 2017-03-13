#include <iostream>
#include <vector>
#include <functional>

#include  <boost/function.hpp>
#include  <boost/ptr_container/ptr_vector.hpp>

#include "game_hierarchy.hpp"
#include <typeinfo>
#include <unordered_map>

using namespace std;


template <typename T>


class MyMap
{
	
    public:
	typedef unsigned uint4;
	typedef unsigned long long uint8;


	static uint8 key(const uint4 id1, const uint4 id2)
	{
		return uint8(id1) << 32 | id2;
	}
	
	
	typedef void (T::*CollisionHandler)(T& other);
	typedef std::unordered_map<uint8, CollisionHandler> CollisionHandlerMap;

	static CollisionHandlerMap MyMap::collisionCases;


	static void  addHandler(const uint4 id1, const uint4 id2, const CollisionHandler handler)
	{
		//Meshes[filename].emplace(std::make_pair(meshName, Mesh(v, vn, vt, f)));
		//collisionCases[].emplace(std::make_pair(key(id1, id2), handler));
		collisionCases.insert( {key(id1, id2), handler});

	}
	

};



vector<game_object*>& get_obj_pointers()
{
	static space_ship ship;
	static asteroid ast;
	static space_station station;

	static vector<game_object*> objs;

	if (objs.empty())
	{
		objs.push_back(&ship);
		objs.push_back(&ast);
		objs.push_back(&station);
	}

	return objs;
}


class MyClass
{
public:
	MyClass();
	~MyClass();


	void asteroid_collision(MyClass& other) {  }


	const char* collide_go_go(game_object*, game_object*)
	{
		return "Unsupported colliding!";
	}

	const char* collide_sh_sh(space_ship*, space_ship*)
	{
		return "Space ship collides with space ship";
	}

	const char* collide_sh_as(space_ship*, asteroid*)
	{
		return "Space ship collides with asteroid";
	}

	const char* collide_as_sh(asteroid*, space_ship*)
	{
		return "Asteroid collides with space ship";
	}

	const char* collide_as_as(asteroid*, asteroid*)
	{
		return "Asteroid collides with asteroid";
	}
private:

};

MyClass::MyClass()
{
}

MyClass::~MyClass()
{
}


template < typename Objs>
void collide_tester(Objs& objs)
{
	HashingMap* comp = new HashingMap();
	for (size_t i = 0; i < 2; ++i)
	{
		for (size_t j = 0; j < 2; ++j)
		{
			comp->collideWith(objs[i], objs[j]);
		}
	}
}



int main() {
	MyMap<MyClass>::addHandler(5 , 7,  &MyClass::asteroid_collision);
}

