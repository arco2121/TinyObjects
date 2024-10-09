cmake_minimum_required(VERSION 3.29)
project(TinyObjects_for_C C)

set(CMAKE_C_STANDARD 23)

add_library(TinyObjects_for_C SHARED library.c)
