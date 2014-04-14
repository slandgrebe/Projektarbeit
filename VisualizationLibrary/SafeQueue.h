#pragma once

#include "stdafx.h"

#include <queue>
#include <mutex>

#include <iostream>

using namespace std;


/** Threadsichere Warteschlange
* @author Stefan Landgrebe
*/
template <typename T>
class SafeQueue {
private:
	queue<T> m_queue;
	mutex m_mutex;

public:

	/** Einreihen in die Warteschlange
	* @author Stefan Landgrebe
	* @param entry neu einzureihender Eintrag in die Warteschlange
	*/
	void enqueue(const T entry) {
		m_mutex.lock();
		m_queue.push(entry);
		m_mutex.unlock();
	}

	/** Liefert den vordersten Eintrag der Warteschlange zurück
	* @author Stefan Landgrebe
	* @return vorderster Eintrag der Warteschlange
	*/
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

	/** Prüft ob Elemente in der Warteschlange vorhanden sind
	* @author Stefan Landgrebe
	* @return Prüfung ob noch Elemente in der Warteschlange vorhanden sind
	*/
	bool hasMore() {
		return !m_queue.empty();
	}
};