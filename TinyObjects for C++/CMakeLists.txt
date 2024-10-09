cmake_minimum_required(VERSION 3.29)
project(TinyObjects_for_C__)

set(CMAKE_CXX_STANDARD 26)

add_library(TinyObjects_for_C__ SHARED
        TinyArray.cpp
        TinyArray.h
        TinyList.cpp
        TinyList.h)
