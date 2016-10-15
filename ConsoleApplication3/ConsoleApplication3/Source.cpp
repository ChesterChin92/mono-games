
#include <iostream>
using namespace std;
//char numbers[7] = { '*','*','*','*','*','*','*' };
int x, y, value;
int input;

int main() {
	
	while (1)
	{
		cout << "How many numbers you want to input : ";
		cin >> input;
		int numbers[9999] = {}; //Max 9999

		do
		{
			for (x = 0; x<input; x++)
			{

				cout << "\n Please Key In Your Number :";
				cin >> numbers[x];


			}
			cout << "\n";


			system("pause");
			return 0;
		} while ((cin.fail() == false));
	}
	
	
		
}





//#include <iostream>
//using namespace std;
//char numbers[7] = { '*','*','*','*','*','*','*' };
//int x, y, value;
//
//
//int main() {
//	// your code goes here
//	for (y = 0; y<7; y++)
//	{
//		//value = y + 1;
//		//cout << value;
//		//numbers[y] = '1';
//		//cout << numbers[y];
//		switch(y)
//		{
//			case (0):
//			{
//				numbers[y] = '1';
//				break;
//			}
//			case (1) :
//			{
//				numbers[y] = '2';
//				break;
//			}
//			case (2) :
//			{
//				numbers[y] = '3';
//				break;
//			}
//			case (3) :
//			{
//				numbers[y] = '4';
//				break;
//			}
//			case (4) :
//			{
//				numbers[y] = '5';
//				break;
//			}
//			case (5) :
//			{
//				numbers[y] = '6';
//				break;
//			}
//			case (6) :
//			{
//				numbers[y] = '7';
//				break;
//			}
//
//		}
//
//
//		for (x = 0; x<8; x++)
//		{
//
//			cout << numbers[x];
//
//		}
//		cout << "\n";
//	}
//
//	system("pause");
//	return 0;
//}