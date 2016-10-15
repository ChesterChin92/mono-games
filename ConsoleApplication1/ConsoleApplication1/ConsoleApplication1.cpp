// ConsoleApplication1.cpp : main project file.

#include "stdafx.h"
#include "stdio.h"
#include <conio.h>
#include <time.h>
#include <iostream>
#include <string>


using namespace System;

int main(array<System::String ^> ^args)
{
    Console::WriteLine("Hello World");
	
	Console::WriteLine();
	
	for (int i = 0; i < 99999999999; i++)
	{
		Console::WriteLine("Time : "  + " Current Number : " + i);
		Console::Clear();

	}


	


    return 0;
}

// Get current date/time, format is YYYY-MM-DD.HH:mm:ss
const String currentDateTime() {
	time_t     now = time(0);
	struct tm  tstruct;
	char       buf[80];
	tstruct = *localtime(&now);
	// Visit http://en.cppreference.com/w/cpp/chrono/c/strftime
	// for more information about date/time format
	strftime(buf, sizeof(buf), "%Y-%m-%d.%X", &tstruct);

	return buf;
}


