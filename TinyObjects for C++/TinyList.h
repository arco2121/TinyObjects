#ifndef TINY_LIST_H
#define TINY_LIST_H

#include <vector>
#include <iostream>
#include <algorithm>
#include <stdexcept>

namespace  TinyObjects
{
    template <typename Tiny>
    class TinyList
    {
        private:
            std::vector<Tiny> data;
            int _count;

        public:
            TinyList();
            TinyList(std::initializer_list<Tiny> input);

            int count() const;
            int length() const;
            Tiny& operator[](int index);
            void add(const Tiny& ele);
            void add(const Tiny& ele, int pos);
            void remove();
            void remove(int i);
            int indexOf(const Tiny& ele) const;
            bool contains(const Tiny& ele) const;
            void sort();
            void reverseSort();
            void swap(int index, int index2);
            void insert(const Tiny& ele, int pos);
            void clear();
            void forEach(void (*action)(const Tiny&, int)) const;
            std::string toString() const;
            std::vector<Tiny> toArray() const;
    };
}

#endif // TINY_LIST_H