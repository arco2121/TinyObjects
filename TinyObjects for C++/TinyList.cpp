//
// Created by 10934253 on 09/10/2024.
//

#include "TinyList.h"

namespace TinyObjects
{
    template <typename Tiny>
    TinyList<Tiny>::TinyList() : _count(0) {
        data.reserve(2);
    }

    template <typename Tiny>
    TinyList<Tiny>::TinyList(std::initializer_list<Tiny> input) : TinyList() {
        for (const auto& i : input) {
            add(i);
        }
    }

    template <typename Tiny>
    int TinyList<Tiny>::count() const {
        return _count;
    }

    template <typename Tiny>
    int TinyList<Tiny>::length() const {
        return data.size();
    }

    template <typename Tiny>
    Tiny& TinyList<Tiny>::operator[](int index) {
        if (index < 0 || index >= _count) {
            throw std::out_of_range("Index out of range");
        }
        return data[index];
    }

    template <typename Tiny>
    void TinyList<Tiny>::add(const Tiny& ele) {
        data.push_back(ele);
        _count++;
    }

    template <typename Tiny>
    void TinyList<Tiny>::add(const Tiny& ele, int pos) {
        if (pos < 0 || pos > _count) {
            throw std::out_of_range("Index out of range");
        }
        data.insert(data.begin() + pos, ele);
        _count++;
    }

    template <typename Tiny>
    void TinyList<Tiny>::remove() {
        if (_count > 0) {
            data.pop_back();
            _count--;
        }
    }

    template <typename Tiny>
    void TinyList<Tiny>::remove(int i) {
        if (i < 0 || i >= _count) {
            throw std::out_of_range("Index out of range");
        }
        data.erase(data.begin() + i);
        _count--;
    }

    template <typename Tiny>
    int TinyList<Tiny>::indexOf(const Tiny& ele) const {
        auto it = std::find(data.begin(), data.end(), ele);
        return (it != data.end()) ? std::distance(data.begin(), it) : -1;
    }

    template <typename Tiny>
    bool TinyList<Tiny>::contains(const Tiny& ele) const {
        return indexOf(ele) != -1;
    }

    template <typename Tiny>
    void TinyList<Tiny>::sort() {
        std::sort(data.begin(), data.end());
    }

    template <typename Tiny>
    void TinyList<Tiny>::reverseSort() {
        std::sort(data.rbegin(), data.rend());
    }

    template <typename Tiny>
    void TinyList<Tiny>::swap(int index, int index2) {
        if (index < 0 || index >= _count || index2 < 0 || index2 >= _count) {
            throw std::out_of_range("Index not found");
        }
        std::swap(data[index], data[index2]);
    }

    template <typename Tiny>
    void TinyList<Tiny>::insert(const Tiny& ele, int pos) {
        if (pos < 0 || pos > _count) {
            throw std::out_of_range("Index out of range");
        }
        data.insert(data.begin() + pos, ele);
        _count++;
    }

    template <typename Tiny>
    void TinyList<Tiny>::clear() {
        data.clear();
        _count = 0;
    }

    template <typename Tiny>
    void TinyList<Tiny>::forEach(void (*action)(const Tiny&, int)) const {
        for (int i = 0; i < _count; i++) {
            action(data[i], i);
        }
    }

    template <typename Tiny>
    std::string TinyList<Tiny>::toString() const {
        std::string result;
        for (const auto& u : data) {
            result += std::to_string(u) + "\n"; // Cambia in base al tipo Tiny
        }
        return result;
    }

    template <typename Tiny>
    std::vector<Tiny> TinyList<Tiny>::toArray() const {
        return data;
    }
}