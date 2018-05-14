#include <iostream>
#include <fstream>
#include <string>
using namespace std;

/************************************************************************************************************************************
* Description: Writes all information to a given file.
* Parameters: path is the full path to the file that needs to be written to. info is the stuff that is going to be written to the file.
* Return: Returns true if file was written to succesfully and false if an error occurred. 
*************************************************************************************************************************************/
bool FileWrite(string path, string info) {
	if (path.length() > 2) { // Helps prevent bad/nonreal paths
		ofstream myfile(path); // Open an out file stream to the given file.
		if (myfile.is_open())
		{
			myfile << info << endl; // Write infomation to the file.
			myfile.close();
			return true; //Success
		}
		else return false; // Failure
	}
	else return false; //path was to short
}

/*******************************************************************************************************************************
* Description: Reads in all information from a given file
* Parameters: path is the full path to the file that needs to be read.
* Return: Returns all the information in the file if read in succesfully. If an error occur than an error message is returned.
*******************************************************************************************************************************/
string FileRead(string path) {
	string info; // All the data that is read in.
	string line; // Used to read data in line by line.
	ifstream myfile(path); // Open up file for reading.
	if (myfile.is_open())
	{
		while (getline(myfile, line)) //Writes information from file to line.
		{
			info += line; //Add line to info 
			info += '\n'; // Add newline for spacing
		}
		myfile.close();
		return info; // Return information from file.
	}
	else return "File could not be read"; // An error occurered.
}
