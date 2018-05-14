/* mystringoperations.h
 * Author: Cody Riniker
 * Description: This file contains non standard string operations.
*/
#include <string.h>
#include <string>
/******************************************************************************
Description: Splits a string into an array of strings based off a deliminator.
Param: string str - Raw string that is going to be split.
Param: char deliminator - Charater to split the string on.
Param: int *len - The number of strings created after being split apart.
Return: string* - An array of substrings of the original string.
*******************************************************************************/
string *Split(string str, char deliminator, int *len) {
	int i = 0;
	int last_index = 0;
	int j = 0;
	static string sarr[50];
	while (str[i] != '\0') {
		if (str[i] == deliminator) {
			sarr[j] = str.substr(last_index, i - last_index);
			last_index = i + 1;
			j++;
		}
		i++;
	}
	sarr[j] = str.substr(last_index, str.length() - last_index);
	*len = j+1;
	return sarr;
}