#include <stdio.h> //might try to remove latter if just passing stuff back
#include <stdlib.h>
#include <iostream>
#include <string.h>
#include "sqlite3.h"
#include <string>
#include <tuple>
using namespace std;

sqlite3* OpenDatabase(const char *path) {
	sqlite3 *db;
	char *zErrMsg = 0;
	int rc;

	/* Open database */
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

static int callback(void *data, int argc, char **argv, char **azColName) {
	int i;
	fprintf(stderr, "%s: ", (const char*)data);

	for (i = 0; i<argc; i++) {
		printf("%s = %s\n", azColName[i], argv[i] ? argv[i] : "NULL");
	}

	printf("\n");
	return 0;
}

bool CreateTable(sqlite3 *db,string table_name, string* types, string* columns, int len) {
	char *zErrMsg = 0;
	int rc;

	/* Create SQL statement */
	string ssql = "CREATE TABLE " + table_name + "(";
	for (int i = 0; i < len; i++) {
		ssql += columns[i] + " ";
		ssql += types[i];
		if(i != len-1) ssql += ", ";
	}
	ssql += ");";
	const char *sql = ssql.c_str();
	cout << sql;
	/* Execute SQL statement */
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

bool InsertIntoTable(sqlite3 *db, string table_name, string* values, int len) {
	char *zErrMsg = 0;
	int rc;

	string ssql = "INSERT INTO " + table_name + " VALUES(";
	for (int i = 0; i < len; i++) {
		ssql += values[i];
		if (i != len - 1) ssql += ",";
	}
	ssql += ");";
	cout << ssql;
	const char *sql = ssql.c_str();

	/* Execute SQL statement */
	rc = sqlite3_exec(db, sql, callback, 0, &zErrMsg);

	if (rc != SQLITE_OK) {
		//fprintf(stderr, "SQL error: %s\n", zErrMsg);
		sqlite3_free(zErrMsg);
		return false;
	}
	else {
		fprintf(stdout, "Records created successfully\n");
		return true;
	}
}

void SelectFromTable(sqlite3 *db,string table_name) {
	/* Create SQL statement */
	char *zErrMsg = 0;
	int rc;
	const char* data = "Callback function called";
	string ssql = "SELECT * FROM " + table_name + ";";
	const char *sql = ssql.c_str();

	/* Execute SQL statement */
	rc = sqlite3_exec(db, sql, callback, (void*)data, &zErrMsg);

	if (rc != SQLITE_OK) {
		fprintf(stderr, "SQL error: %s\n", zErrMsg);
		sqlite3_free(zErrMsg);
	}
	else {
		fprintf(stdout, "Operation done successfully\n");
	}
}

bool UpdateTable(sqlite3 *db, string table_name, string column, string value,string wclause) {
	char *zErrMsg = 0;
	int rc;
	const char* data = "Callback function called";

	/* Create merged SQL statement */
	string ssql = "UPDATE " + table_name + " SET " + column + "=" + value + " WHERE " + wclause + ";";
	const char *sql = ssql.c_str();

	/* Execute SQL statement */
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

bool DeleteFromTable(sqlite3 *db, string table_name, string wclause) {
	char *zErrMsg = 0;
	int rc;
	const char* data = "Callback function called";

	string stringsql = "DELETE FROM " + table_name + " WHERE " + wclause + ";";
	const char *sql = stringsql.c_str();

	/* Execute SQL statement */
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
