//Kayshav Problem
#include <cmath>
#include <cstdio>
#include <vector>
#include <iostream>
#include <algorithm>
using namespace std;

int main()
{
	//int n;
	//cin >> n;
	//int arr[n];
	//return 0;
	int row = 0;
	int col = 0;

	cin >> col;
	cin >> row;

	//allocate the array
	int** arr = new int*[row];
	
	for (int i = 0; i < row; i++)
		arr[i] = new int[col];
	
	


	// use the array

	//deallocate the array
	for (int i = 0; i < row; i++)
		delete[] arr[i];
	delete[] arr;
}


//Experimenting with Vector
#include <iostream>
#include <vector>
using namespace std;

int main()
{
	// create a vector to store int
	vector<int> vec;
	int i;

	// display the original size of vec
	cout << "vector size = " << vec.size() << endl;

	// push 5 values into the vector
	for (i = 0; i < 5; i++) {
		vec.push_back(i);
	}

	// display extended size of vec
	cout << "extended vector size = " << vec.size() << endl;

	// access 5 values from the vector
	for (i = 0; i < 5; i++) {
		cout << "value of vec [" << i << "] = " << vec[i] << endl;
	}
	// use iterator to access the values
	vector<int>::iterator v = vec.begin();
	while (v != vec.end()) {
		cout << "value of v = " << *v << endl;
		v++;
	}
	/*int x;
	cin >> x;
	int arr[x];
	return 0;*/
}