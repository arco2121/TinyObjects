//
// Created by 10934253 on 09/10/2024.
//

#include "TinyArray.h"

namespace TinyObjects
{
    TinyArray::TinyArray() {
        data.resize(2, nullptr);
    }

    TinyArray::TinyArray(std::initializer_list<void*> input) : TinyArray() {
        for (auto item : input) {
            add(item);
        }
    }

    int TinyArray::count() const {
        return std::count_if(data.begin(), data.end(), [](void* item) { return item != nullptr; });
    }

    void* TinyArray::operator[](int index) {
        return get(index);
    }

    void TinyArray::add(void* element) {
        if (count() >= data.size()) {
            data.resize(data.size() * 2);
        }
        data[count()] = element;
    }

    void TinyArray::add(void* element, int pos) {
        if (pos >= data.size()) {
            data.resize(pos + 1);
        }
        data[pos] = element;
    }

    void* TinyArray::get(int index) {
        if (index < 0 || index >= data.size()) {
            return nullptr;
        }
        return data[index];
    }

    void TinyArray::remove() {
        for (int i = data.size() - 1; i >= 0; --i) {
            if (data[i] != nullptr) {
                data[i] = nullptr;
                break;
            }
        }
    }

    int TinyArray::indexOf(void* element) {
        for (int i = 0; i < data.size(); i++) {
            if (data[i] == element) {
                return i;
            }
        }
        return -1;
    }

    bool TinyArray::contains(void* element) {
        return indexOf(element) != -1;
    }

    void TinyArray::clear() {
        data.clear();
        data.resize(2, nullptr);
    }

    void TinyArray::print() const {
        for (const auto& item : data) {
            if (item != nullptr) {
                std::cout << item << "\n";
            }
        }
    }
}