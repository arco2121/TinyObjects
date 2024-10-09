#ifndef TINY_ARRAY_H
#define TINY_ARRAY_H

#include <vector>
#include <iostream>
#include <algorithm>

namespace TinyObjects
{
    class TinyArray
    {
    private:
        std::vector<void*> data;

    public:
        TinyArray();
        TinyArray(std::initializer_list<void*> input);

        int count() const;
        void* operator[](int index);
        void add(void* element);
        void add(void* element, int pos);
        void* get(int index);
        void remove();
        int indexOf(void* element);
        bool contains(void* element);
        void clear();
        void print() const;
    };
}

#endif // TINY_ARRAY_H
