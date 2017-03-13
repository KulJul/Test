#include "hashingMap.h"

#include <iostream>
#include <vector>
#include <functional>

#include  <boost/function.hpp>
#include  <boost/ptr_container/ptr_vector.hpp>

// multimethod creator for pointer arguments (main header for this example)
#include <mml/generation/make_ref_multimethod.hpp>

// multimethod creator for reference arguments
#include <mml/generation/make_ref_multimethod.hpp>

// utility helper
#include <mml/util/inline_function.hpp>

// header to enable boost::shared_ptr arguments dispatching
#include <mml/casting/sp_dynamic_caster.hpp>
#include <mml/generation/make_multimethod.hpp>

#include <typeinfo>
#include <unordered_map>

using namespace std;
using namespace mml;




