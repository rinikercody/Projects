/*InputCleaner.js
* This class sanitizes user input
*/ 
module.exports = {
	cleanHTML : cleanHTML,
	clean: clean
};

/**@function cleanHTML
  *This function removes < and > and replaces them with X.
  *@param {String} str - The raw input that is going to be edited.
  *@return {String} str - The new input with possible malicious input removed.
  */
function cleanHTML(str){
	str = str.replace(/</g,'X').replace(/>/g, 'X');
	return str;
}

/**@function cleanSQL
  *If input contians sql like syntax a dummy string is returned. 
  *This is probble useless because of how queries are sent in the DBmanager class.
  *@param {String} str - The raw string that is being checked
  *@return {Int} ok - If any SQL syntax is found then -1 is returned.
  */  
function cleanSQL(str){
	str = str.toUpperCase();
	var ok = 1;
	if(str.indexOf("INSERT") !== -1 && str.indexOf("VALUES") !== -1) ok = -1;
	if(str.indexOf("DROP") !== -1 && str.indexOf("TABLE") !== -1) ok = -1;
	if(str.indexOf("SELECT") !== -1 && str.indexOf("FROM") !== -1) ok = -1;
	return ok;
}

/**@function clean
  *This function will call all the other cleaning function to completly sanatize input.
  *@param {String} str - The raw input that is going to be edited.
  *@return {String} str - The new input with possible malicious input removed.
  */
function clean(str){
	str = cleanHTML(str).replace(/ /g,"_");
	if(cleanSQL(str) === -1){
		return "Bad_Name";
	}
	return str;
}