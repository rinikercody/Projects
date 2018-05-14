/*Source.cpp
 *Author: Cody Riniker
 *Description: Entry point for program. Hanldes most input/output.
*/
#include "sqlitehelper.h"
#include <string>
#include "mystringoperations.h"
#include <iostream>
using namespace std;

int main(int argc, char* argv[]) {
	cout << "What database do you want to open: " << endl;
	string database = "test.db"; //Default to test.db database file
	cin >> database;
	sqlite3 *db = OpenDatabase(database.c_str());


	cout << endl << "SQLITE3 DATABASE MANAGER" << endl;
	cout << "***************************" << endl;

	//Loops through menu until user wants to quit
	while (true) {
		cout << "1. Get all rows of table" << endl;
		cout << "2. Create a table" << endl;
		cout << "3. Insert values into database" << endl;
		cout << "4. Update values in database" << endl;
		cout << "5. Delete value in database" << endl;
		cout << "6. Exit" << endl;
		cout << "Enter(1-5): ";

		int selection = 0;
		cin >> selection;

		string table_name = " ";
		string info = " ";
		int i = 0;
		string* sarr;
		string* types = new string[25];
		string* columns = new string[25];
		int len = 0;

		switch (selection) {
		case 1: //SELECT * FROM tableName query;
			cout << "Enter the table name: " << endl;
			cin >> table_name;
			SelectFromTable(db, table_name);
			break;
		case 2: //CREATE TABLE(col1 INT, ...);
			cout << "Enter name of table: ";
			cin >> table_name;
			cout << "Enter column type and name like col1,INT,col2,DOUBLE" << endl;
			cin >> info;
			sarr = Split(info, ',', &len);
			i = 0;
			while (i < len) {
				int k = i/2;
				columns[k] = sarr[i];
				types[k] = sarr[i + 1];
				i += 2;
			}
			CreateTable(db, table_name, types, columns, len/2);
			break;
		case 3: // INSERT INTO tableName VALUES(?,?,...);
			cout << "Enter name of table: ";
			cin >> table_name;
			cout << "Enter values seperated by commas like 1,cats,6" << endl;
			cin >> info;
			sarr = Split(info, ',', &len);
			InsertIntoTable(db, table_name, sarr, len);
			break;
		case 4: //UPDATE tableName SET col2 = ? WHERE col1 = ? 
			cout << "Enter name of table: ";
			cin >> table_name;
			cout << "Enter column to update and value like col1,6 : ";
			cin >> info;
			sarr = Split(info, ',', &len); //Split string to get values and columns from info.
			cout << "Enter where clause like col1=5 : " << endl;
			cin >> info;
			UpdateTable(db, table_name, sarr[0], sarr[1], info);
			break;
		case 5: //DELETE FROM tableName WHERE col1 = ?
			cout << "Enter name of table: ";
			cin >> table_name;
			cout << "Enter where clause like col1=5 : " << endl;
			cin >> info;
			DeleteFromTable(db, table_name,info);
			break;
		default:
			return 0;
		}
	}

	sqlite3_close(db); //Close database connection
	getchar(); getchar(); //Hold window open
	return 0;
}
