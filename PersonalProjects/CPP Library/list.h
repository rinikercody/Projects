//Author: Cody Riniker
#include <iostream>
using namespace std;

template <class T>
class List {
public:
	List();
	List(int);
	~List();
	void Add(T);
	bool Contains(T);
	int GetCount();
	void print();
	T& operator[](int);
	List<T>* Tail();
	T Head();
private:
	int Max;
	int index;
	T *arr;
};

//Default constructer, Starts with 10 locations.
template <class T>
List<T>::List() {
	index = 0;
	Max = 10;
	arr = new T[10];
}

//A constructer that takes a intial size for the list. List can still grow but may require less reszing this way.
template <class T>
List<T>::List(int lim) {
	index = 0;
	Max = lim;
	arr = new T[lim];
}

//Deletes the main array for the list, called when List is deleted
template <class T>
List<T>::~List() {
	delete[] arr;
}

//Add an element to the list and resizes if the max amount of elements has been reached.
//The size of the array is double when the max numbe of elements have been reached.
template <class T>
void List<T>::Add(T element) {
	if (index == Max) { 
		T *temp = new T[Max * 2]; //Create temp array twice the size of the orginal
		for (int i = 0; i < Max; i++) { //Copying elements from orginal array to temp array.
			temp[i] = arr[i]; 
		}
		delete[] arr; // Delete orginal array because its not needed anymore.
		arr = temp; // The main array for the program is set to temp.
		Max = Max * 2; // Max is doubled
	}
	arr[index] = element; // element added to list.
	index++; //index/count increased by 1
}

//Checks if given element is in the list
template <class T>
bool List<T>::Contains(T element) {
	for (int i = 0; i <= index; i++) {
		if (arr[i] == element) { //Checks if value at location i is equal to the element 
			return true; //Found
		}
	}
	return false;//Not found
}

//Used to return the number of elements in the list
template <class T>
int List<T>::GetCount() {
	return index; //index is the same as number of elements in list
}

//Prints each element of the list
// Use this to print, can edit out stream but want to keep everything in .h file
template <class T>
void List<T>::print() {
	for (int i = 0; i < index; i++) {
		cout << arr[i]; //prints element at location i
	}
}

//Overload the indexing operator so that it can be accessed like an array by other programs
template <class T>
T& List<T>::operator[](int i) {
	return arr[i];
}

//Returns a new list without the first element of the list
template <class T>
List<T>* List<T>::Tail() {
	List<T> *temp = new List<T>(Max);
	for (int i = 1; i < index; i++) {
		temp->Add(i);
	}
	return temp;
}

//Returns the first element of the list
template <class T>
T List<T>::Head() {
	return arr[0];
}

