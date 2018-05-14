/* sqlitehelper.h
 * Author: Cody Riniker
 * This file is used to help perform simple database actions on an sqlite3 database.
 * Most of the code in this file is based off code from this website
 * https://www.tutorialspoint.com/sqlite/sqlite_c_cpp.htm
*/
#include <stdio.h>
#include <stdlib.h>
#include <iostream>
#include "sqlite3.h"
#include <string>
using namespace std;


/************************************************************************************************
 * Description: Connects to an sqlite3 database file.
 * Param: const char *path - The full path to the database file.
 * Return: If there are no problems opeing the database then the sqlite3 connection is returned.
************************************************************************************************/
sqlite3* OpenDatabase(const char *path) {
	sqlite3 *db;
	char *zErrMsg = 0;
	int rc;

	rc = sqlite3_open(path, &db);

	if (rc) {
		fprintf(stderr, "Can't open database: %s\n", sqlite3_errmsg(db));
		return(0);
	}
	else {
		fprintf(stdout, "Opened database successfully\n");
		return db;
	}
}

/**************************************************************************************
Description: A callback function used to get information back from database functions.
Param: void *data - Information recived from database call.
Param: int argc - Argument count
Param: char **argv - An array containing each argument.
Param: char **azColName - Column name
Return: 0
***************************************************************************************/
static int callback(void *data, int argc, char **argv, char **azColName) {
	int i;
	fprintf(stderr, "%s: ", (const char*)data);

	for (i = 0; i<argc; i++) {
		printf("%s = %s\n", azColName[i], argv[i] ? argv[i] : "NULL");
	}

	printf("\n");
	return 0;
}

/*******************************************************************************************
 * Description: Creates a table in the database.
 * Param: sqlite3 *db - Database connection.
 * Param: string tableName - What the user wants to call the table.
 * Param: string* types - An array of strings that contain the datatype of for each column.
 * Param: string* columns - An array of strings that contain the column names.
 * Param: int len - The number of columns.
 * Return: True if opereation done successfully and false if not.
 *******************************************************************************************/
bool CreateTable(sqlite3 *db,string tableName, string* types, string* columns, int len) {
	char *zErrMsg = 0;
	int rc;

	string ssql = "CREATE TABLE " + tableName + "(";
	for (int i = 0; i < len; i++) {
		ssql += columns[i] + " ";
		ssql += types[i];
		if(i != len-1) ssql += ", ";
	}
	ssql += ");";
	const char *sql = ssql.c_str();
	cout << sql;

	rc = sqlite3_exec(db, sql, callback, 0, &zErrMsg);

	if (rc != SQLITE_OK) {
		fprintf(stderr, "SQL error: %s\n", zErrMsg);
		sqlite3_free(zErrMsg);
		return false;
	}
	else {
		fprintf(stdout, "Table created successfully\n");
		return true;
	}

}

/*********************************************************************************
 * Description: Inserts values into a table in the database.
 * Param: sqlite3 *db - Database connection.
 * Param: string tableName - Table that is having values added.
 * Param: string *values - An array with the values to be added to each column.
 * Param: int len - The number of values being inserted. 
 * Return: True if opereation done successfully and false if not.
 ********************************************************************************/
bool InsertIntoTable(sqlite3 *db, string tableName, string* values, int len) {
	char *zErrMsg = 0;
	int rc;

	string ssql = "INSERT INTO " + tableName + " VALUES(";
	for (int i = 0; i < len; i++) {
		ssql += values[i];
		if (i != len - 1) ssql += ",";
	}
	ssql += ");";
	cout << ssql;
	const char *sql = ssql.c_str();

	rc = sqlite3_exec(db, sql, callback, 0, &zErrMsg);

	if (rc != SQLITE_OK) {
		sqlite3_free(zErrMsg);
		return false;
	}
	else {
		fprintf(stdout, "Insert done successfully\n");
		return true;
	}
}

/*******************************************************
 * Description: Runs a simple query on the database.
 * Param: sqlite3 *db: Database Connection.
 * Param: string tableName: Table to run the query on.
*******************************************************/
void SelectFromTable(sqlite3 *db,string tableName) {
	char *zErrMsg = 0;
	int rc;
	const char* data = "";
	string ssql = "SELECT * FROM " + tableName + ";";
	const char *sql = ssql.c_str();

	rc = sqlite3_exec(db, sql, callback, (void*)data, &zErrMsg);

	if (rc != SQLITE_OK) {
		fprintf(stderr, "SQL error: %s\n", zErrMsg);
		sqlite3_free(zErrMsg);
	}
	else {
		fprintf(stdout, "Operation done successfully\n");
	}
}

/**********************************************************************************
 * Description: Updates a value in a table
 * Param: sqlite3 *db - Database Connection.
 * Param: string tableName - Table to update.
 * Param: string column - Column in table to update.
 * Param: string value - New value of column
 * Param: string wClause - Statment to find location in table to update like key=1.
 * Return: True if opereation done successfully and false if not.
***********************************************************************************/
bool UpdateTable(sqlite3 *db, string tableName, string column, string value,string wClause) {
	char *zErrMsg = 0;
	int rc;
	const char* data = "Callback function called";

	string ssql = "UPDATE " + tableName + " SET " + column + "=" + value + " WHERE " + wClause + ";";
	const char *sql = ssql.c_str();

	rc = sqlite3_exec(db, sql, callback, (void*)data, &zErrMsg);

	if (rc != SQLITE_OK) {
		fprintf(stderr, "SQL error: %s\n", zErrMsg);
		sqlite3_free(zErrMsg);
		return false;
	}
	else {
		fprintf(stdout, "Operation done successfully\n");
		return true;
	}
}

/*************************************************************************************
Description: Deletes a row from a table.
Param: sqlite3 *db - Database Connection.
Param: string tableName - Name of table to delete row from.
Param: string wClause - Statement to find location in table to delete from like key=1.
**************************************************************************************/
bool DeleteFromTable(sqlite3 *db, string tableName, string wClause) {
	char *zErrMsg = 0;
	int rc;
	const char* data = "Callback function called";

	string stringsql = "DELETE FROM " + tableName + " WHERE " + wClause + ";";
	const char *sql = stringsql.c_str();

	rc = sqlite3_exec(db, sql, callback, (void*)data, &zErrMsg);

	if (rc != SQLITE_OK) {
		fprintf(stderr, "SQL error: %s\n", zErrMsg);
		sqlite3_free(zErrMsg);
		return false;
	}
	else {
		fprintf(stdout, "Operation done successfully\n");
		return true;
	}
}
