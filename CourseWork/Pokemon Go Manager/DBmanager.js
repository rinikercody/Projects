/*DBmanager.js
* This class is used to manage all database actions for the web app.
* Handles all database(SQL) queries,inserts,deletes, and updates.
*/
const sqlite3 = require('sqlite3')
module.exports = {
	connectToDatabase: connectToDatabase,
	addUser: addUser,
	checkUser, checkUser,
	updateTeam: updateTeam,
	updateEvos: updateEvos,
	getTeam: getTeam,
    removeFromTeam: removeFromTeam,
	removeTeam: removeTeam,
	getEvos: getEvos,
	removeEvo: removeEvo,
	addTeam: addTeam,
	getUserTeams: getUserTeams,
	addUserTeams: addUserTeams,
	updateUserTeams: updateUserTeams,
	removeUserTeams: removeUserTeams,
	addEvoUser: addEvoUser
};

//The connection to the database
var db;

/**@function connectToDatabase
  * Opens connection to the SQLite3 database
  * @param {function} callback - A callback function that is used to return message after connecting.
  * @return {function} callback - The function will return a message if the connection has be successfully made.
*/
function connectToDatabase(callback){
	db = new sqlite3.Database('SQLDatabase/PokemonDB.db',function(err){
	if (err) {
		return callback(err,null);
	}
	    return callback(null,'Connected SQlite database.');
	});
}


//////////////////////User Actions///////////////////////////////////
/** @function addUser
  * Adds a new user to the database by inserting their username and password into the database.
  * @param {String} username - The unique name of the user.
  * @param {String} pwd - The users password used to log in.
  * @param {function} callback - Returns -1 if and error occured and 1 if successful.
  */
function addUser(username,pwd,callback){
	db.run("INSERT INTO users VALUES(?,?);",[username,pwd],function(err){
		if(err){
			callback(null,-1);
			return;
		}
		callback(null,1);
		return;
	});
}

/** @function checkUser
  * Used to look up user credentials when loging in.
  * @param {String} username - The unique name of the user.
  * @param {String} pwd - The users password used to log in.
  * @param {function} callback - Returns -1 if and error occured and 1 if successful.
  */
function checkUser(username, pwd, callback){
	var sql = 'SELECT * FROM users WHERE username = ? AND password = ?;';
	db.each(sql,[username,pwd],function(err,row){
		info = cleanInfo(JSON.stringify(row));
		callback(null,1); //Fine
		return;
	},function(err,rows){ //No row was returned
		if(rows == 0){
			console.log(rows);
			callback(null,-1); 
			return;
		}
	});
}

/** @function cleanInfo
  * Takes a given JSON string, removes some characters and converts it into an array.
  * @param {String} row - The raw information from a query on the database.
  * @return {Array} info - An array with each element of the JSON String split by a comma.
  */
function cleanInfo(row){
	var info = row.replace(/"/g,'').replace('{', '').replace('}','').split(',');
	return info;
}

//////////////////User_teams Actions/////////////////////////////
/** @function addUserTeams
  * Add a new team to the user_teams table which is used to track multiple teams.
  * @param {String} username - The user who is adding a new team.
  */
function addUserTeams(username){
	var sql = "INSERT INTO user_teams VALUES(?,'empty','empty','empty','empty','empty','empty','empty','empty','empty');";
	db.run(sql,[username],function(err){
		if(err){
			console.log(err);
		}
		console.log("userteams UPdataed");
	});
}

/** @function removeUserTeams
  * Removes a team from the user_teams table based on the location in table 
  * Removing something from the table is just setting it entry to 'empty'.
  * @param {String} username - The user who is removing a team.
  * @param {int} slot - The number(1-3) of the team the user is removing. Ex remove team1.
  */
function removeUserTeams(username, slot,callback){
		db.serialize(function(){
			var sql =  "UPDATE user_teams";
			    sql += " SET team" + slot + " = 'empty'";
			    sql += " WHERE user = ?;";
			db.run(sql,[username], function(err){
				console.log(err);
			});	
			callback(null,-1);
			return;
		});
}

/** @function getEmptyUserTeamSlot
  * Finds the first empty team slot and returns it through a callback function
  * @param {String} username - The user who teams are being looked at.
  * @param {String} team - The name of the team the user is adding
  * @param {function} callback - Returns the location of the first empty slot in the table or -1 if full.
  */
function getEmptyUserTeamSlot(username,team,callback){
	getUserTeams(username, function(err,data){
		for(var i = 1; i < data.length; i++){
			var team_name = data[i].split(':')[1];
			if(team_name === team){
				callback(null,-2);
				return;
			}
			if(team_name === 'empty'){ // for testing should just be empty
				callback(null,i); //i is the empty slot
				return;
			}
		}
		callback(null,-1);
	});
}

/** @function updateUserTeams
  * Updates user_teams table when a user added a new team.
  * @param {String} username - the user who is adding the team.
  * @param {String} team_name - the name of the team that is being added.
  * @param {function} callback - A callback function that returns 1 if successful and -1 if not.
  */
function updateUserTeams(username,team_name,callback){
	var sql = "SELECT * FROM user_teams WHERE user = ?";
	db.serialize(function(){
			getEmptyUserTeamSlot(username,team_name,function(err,data){
				if(data > 0){
					var sql =  "UPDATE user_teams";
					sql += " SET team" + data + " = ?";
					sql += " WHERE user = ?;";
					db.run(sql,[team_name,username], function(err){
						console.log(err);
					});	
					callback(null,1);
					return;
				}
				else{
					callback(null,data);
					return;
				}
			});
		});
}

/** @function getUserTeams
  * @param {String} username - The name of the user
  * @param {function} callback - returns the users teams if no error occurs.
  */ 
function getUserTeams(username,callback){
	var sql = "SELECT * FROM user_teams WHERE user = ?";
	db.each(sql,[username],function(err,rows){
		if(err){
			callback(err,null);
		}
		else{
			info = cleanInfo(JSON.stringify(rows));
			callback(null,info.slice(0,10));
		}
	});
}




///////////////////////Team Actions/////////////////////////
/** @function getTeam
  * Gets a team of pokemon based on user and team name.
  * @param {String} username - the current user
  * @param {String} team_name - the name of the team the user wants to see.
  * @param {function} callback - A callback function null if team not found or the team if found
*/
function getTeam(username, team_name, callback){
	var info;
	var sql = "SELECT * FROM teams WHERE username = ? AND team_name = ?;";
	db.each(sql,[username, team_name],function(err,rows){
			if(err){
				callback(err,null);
			}
			else{
				console.log(JSON.stringify(rows));
				info = cleanInfo(JSON.stringify(rows));
				callback(null,info.slice(1,8));
			}
	});
}

/** @function addTeam
  * @param {String} username - The user who is creating the team.
  * @param {String} team_name - The name of the team the user is adding.
  */
function addTeam(username, team_name){
	var sql = "INSERT INTO TEAMS VALUES(?,?,0,0,0,0,0,0);"
	console.log(team_name + " was added");
	db.run(sql,[username,team_name],function(err,data){
		console.log("Team added");
	});
}

/** @function getEmptyTeamSlot
  * Gets the first empty pokemon slot in a given team.
  * @param {String} username - the current user
  * @param {String} team_name - the name of the team the user wants to see.
  * @param {function} callback - A function that returns that first available slot for a pokemon to be inserted or -1 if team is full.
*/
function getEmptyTeamSlot(username, team_name, callback){
	getTeam(username,team_name, function(err,data){
		for(var i = 1; i < data.length; i++){
			if(data[i].split(':')[1] === '0'){
				callback(null,i);//i is the empty slot
			    return;
		    }
		}
		callback(null,-1);
	});
}


/** @function updateTeam 
  * Inserts a new pokemon into a user's team.
  * @param {String} username - the current user
  * @param {String} team_name - the name of the team the user wants to use.
  * @param {function} callback - A function that returns 1 if update was successful and -1 if not.
*/
function updateTeam(username,team_name,num,callback){
		db.serialize(function(){
			getEmptyTeamSlot(username,team_name,function(err,data){
				if(data > 0){
					var sql =  "UPDATE teams";
					sql += " SET poke" + data + " = " + num;
					sql += " WHERE username = ? AND team_name = ?;";
					db.run(sql,[username,team_name], function(err){
						console.log(err);
					});	
					callback(null,1);//Fine
					return;
				}
				else{
					callback(null,data);
				}
			});
		});
}

/** @function removeFromTeam
  * Removes a pokemon from a team based on slot in users team.
  * @param {String} username - the current user
  * @param {String} team_name - the name of the team the user wants to remove a pokemon from.
  * @param {int} slot - The number associated with the pokemons postion in the table.
*/
function removeFromTeam(username,team_name,slot){
	var sql =  "UPDATE teams";
		sql += " SET poke" + slot + " = 0";
		sql += " WHERE username = ? AND team_name = ?;";
		db.run(sql,[username,team_name], function(err){
			console.log(team_name + " was removed");
		});	
}

/** @function removeTeam
  * Removes a users team from the teams table
  * @param {String} username - The current user
  * @param {String} team_name - The name of the team that is being removed.
  */
function removeTeam(username,team_name,callback){
	var sql = "DELETE FROM teams WHERE username = ? AND team_name = ?";
	db.run(sql,[username,team_name],function(err){
		if(err){
			console.log(err);
			callback(null,-1);
		}
		callback(null,1);
	});
}


///////////////////Evolution Actions/////////////////////////
/** @function addEvoUser
  * Creates and evolution for a new user.
  * @param {String} username - the current user
*/
function addEvoUser(username){
	var sql = "INSERT INTO evos VALUES(?,0,0,0,0,0,0,0,0,0);"
	db.run(sql,[username],function(err){
		if(err){
			console.log(err);
		}
	});
}

/** @function getEvos
  * Gets evolution list of user from database.
  * @param {String} username - the current user
  * @param {function} callback - A function that returns the users list of pokemon if found.
*/
function getEvos(username, callback){
	var info;
	var sql = "SELECT * FROM evos WHERE username = ?;";
	db.each(sql,[username],function(err,rows){
			if(err){
				callback(err,null);
			}
			else{
				info = cleanInfo(JSON.stringify(rows));
				callback(null,info.slice(1,10)); //This is hardcoded to match database.
			}
	});
}

/** @function getEmptyEvoSlot
  * Get the first empty location in a user evoltuion list.
  * @param {String} username - the current user
  * @param {function} callback - A function that returns the first empty pokemon slot in a users evo list or -1 if list if full.
*/
function getEmptyEvoSlot(username, callback){
	getEvos(username, function(err,data){
		for(var i = 0; i < data.length; i++){
			if(data[i].split(':')[1] === '0'){
				callback(null,i+1); //i+1 because data array dosen't contain username only pokemon so data[0] = poke1
				return;
			}
		}
		callback(null,-1);
	});
}

/** @function updateEvos
  * Inserts a pokemon into a users evolution list
  * @param {String} username - the current user
  * @param {int} poke - the id number of the pokemon being inserted.
  * @param {function} callback - A function that returns 1 if successful and -1 if not.
*/
function updateEvos(username,poke,callback){
	db.serialize(function(){
		getEmptyEvoSlot(username,function(err,data){
			if(data > 0){
				var sql =  "UPDATE evos";					
				sql += " SET poke" + data + " = " + poke;
				sql += " WHERE username = ?";
				db.run(sql,[username], function(err){
					callback(err,-1);
				});
				callback(null,1);
			    return;
			}
			else{
				callback(null,-1);
			}
		});
	});
}

/** @function removeEvo
  * Removes a pokemon from a users evolution list
  * @param {String} username - the current user
  * @param {int} slot - The location in the evo list that the pokemon will be removed from.
*/
function removeEvo(username,slot){
	var sql =  "UPDATE evos";
		sql += " SET poke" + slot + " = 0";
		sql += " WHERE username = ?;";
		db.run(sql,[username], function(err){
			if(err){
				console.log(err);
			}
		});
}