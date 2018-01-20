template <class T>
class Stack {
public:
	Stack();
	~Stack();
	void Push(T);
	T Pop();
	T Peek();
	//Want to add a count property
private:
	T *arr;
	int max;
	int index;
};

template<class T> 
Stack<T>::Stack() {
	arr = new T[5];
	max = 5;
	index = 0;
}

template<class T> 
Stack<T>::~Stack() {
	delete[] arr;
}

template<class T>
void Stack<T>::Push(T item) {
	if (index == max) {
		T *temp = new T[max * 2];
		for (int i = 0; i < max; i++) {
			temp[i] = arr[i];
		}
		arr = temp;
		max = max * 2;
	}
	arr[index] = item;
	index++;
}

//not correct
template <class T>
T Stack<T>::Pop() {
	if (index - 1 > -1) {
		T temp = arr[index - 1];
		index--;
		return temp; // get last used location since index is incremented after push.
	}
	return -002;
	//Need to throw something here
}

template <class T>
T Stack<T>::Peek() {
	if (index - 1 > -1) {
		return arr[index - 1];
	}
	return -002;
}