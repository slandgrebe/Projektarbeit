#pragma once

#include "stdafx.h"

#include <queue>
#include <mutex>

#include <iostream>

using namespace std;

template <typename T>
class SafeQueue {
private:
	queue<T> m_queue;
	mutex m_mutex;

public:
	void enqueue(const T entry) {
		m_mutex.lock();
		m_queue.push(entry);
		m_mutex.unlock();
	}
	T dequeue() {
		T returnValue;
		m_mutex.lock();
		if (!m_queue.empty()) {
			returnValue = m_queue.front();
			m_queue.pop();
		}
		m_mutex.unlock();

		return returnValue;
	}
	bool hasMore() {
		return !m_queue.empty();
	}
};