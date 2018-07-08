/*server.js
 *Handles all request from client and serves all responses for the Pokemon Go Manager.
 */
//This app uses cookies to manage users and teams for each session
const http = require('http');
const fs = require('fs');
const qs = require('querystring');
var db = require('./DBmanager');
var vm = require('./ViewManager');
var encryption = require('./Encryption');
var ic = require('./InputCleaner');

const PORT = 3000; //The port the application runs on

//Connect to the SQlite database
db.connectToDatabase(function(err,data){
	console.log(data);
});

/**@function loadPokemonData
  *@param {Function} callback - Returns and object contain information on all available pokemon
  */
function loadPokemonData(callback){
	fs.readFile('PokemonData.txt', 'utf8', function(err, data) {
		if (err){ 
			throw err;
		}
		var arr = data.split('\n');
		pokemon_stats = {};
		for(var i = 0; i < arr.length; i++){
			var poke_number = arr[i].split('.')[0]
			pokemon_stats[poke_number] = arr[i].split('.')[1];
		}
		callback(pokemon_stats);
	});
}

/** @function handleRequest
  * Handles request from clients and serves request files along 
  * with interacting with the application database.
  * @param {http.ClientRequest} req - The client request object.
  * @param {http.ServerResponse} res - The server response object.
  */
function handleRequest(req,res){	
	var main_user = getCookieInfo(req)[0];
	var main_team = getCookieInfo(req)[1];
	
	if(req.method === 'POST'){
		var body = '';
		
		req.on('data', function(data){
			body += data;
		});
		
		req.on('end', function(){
			var post = qs.parse(body);
            
			if(post.new_team != null){ //If user is adding new team
				var new_team = ic.clean(post.new_team);
				db.updateUserTeams(main_user,new_team,function(err,data){
					//1 is used to represent if the operation was done successfully.
					//-1 means that a team has already reached its max number of members
					//-2/else means the users already has a team with the given name.
					if(data == 1){ //If user has room for another team
						db.addTeam(main_user,new_team); //Create a new team with blank pokemon entries.
						res.writeHeader(200, {"Content-Type": "text/html",
											  'Set-Cookie': 'info=' + main_user + "?" + new_team
										}); //Have to do it this way instead of just using set cookie method.
						serveFile("views/PokemonList.html", res);
					}
					else if(data == -1){
						res.writeHeader(200, {"Content-Type": "text/html"});
						serveFile("views/FullTeamAlertPage.html",res);
					}
					else{
						res.writeHeader(200, {"Content-Type": "text/html"});
						serveFile("views/DuplicateTeamError.html",res);
					}
		        });
				return;
			}
			else if(post.info != null){ // This is for adding/removing pokemon from teams or evolution list
				var command = post.info.substring(0,2); //AT,AE,RT,RE,ST,DT
				if(command === 'AT'){//Add Team Member
					if(main_team === null || main_team === 'NOTEAM')
					{
						res.writeHeader(200, {"Content-Type": "text/html"});
						serveFile("views/NoTeamAlert.html",res);
					}
					var poke_num = post.info.substring(2,5);//3 digit Pokemon number.
					db.updateTeam(main_user,main_team,poke_num,function(err,data){
						if(data == -1){ //Team is full
						    res.writeHeader(200, {"Content-Type": "text/html"});
							serveFile('views/FullTeamAlertPage.html',res);
						}
						else{
							res.writeHeader(200, {"Content-Type": "text/html"});
							serveFile("views/PokemonList.html",res);
						}
					});
					return;
				}
				else if(command === 'AE'){//Add Evolution Member
					if(main_team === null || main_user === 'NOUSER')
					{
						res.writeHeader(200, {"Content-Type": "text/html"});
						serveFile("views/NoUserAlert.html",res);
					}
					var poke_num = post.info.substring(2,5); //3 digit Pokemon number.
					db.updateEvos(main_user,poke_num,function(err,data){
						if(data == -1){
							res.writeHeader(200, {"Content-Type": "text/html"});
							serveFile('views/FullEvoAlertPage.html',res);
						}
						else{
							res.writeHeader(200, {"Content-Type": "text/html"});
							serveFile('views/PokemonList.html',res);
						}
					});
					return;
				}
				else if(command == 'RT'){//Remove Team Member
					var slot = post.info.substring(2);
					db.removeFromTeam(main_user,main_team,slot);
					loadTeams(main_user,main_team,res);
					return;
				}
				else if(command == 'RE'){//Remove Evolution Member
					var slot = post.info.substring(2);
					db.removeEvo(main_user,slot);
					loadEvos(main_user,res);
					return;
				}
				else if(command == 'ST'){//View Team
					var team = post.info.substring(2);
					setCookie(main_user,team,res);
					loadTeams(main_user,team,res);
					return;
				}
				else if(command == 'DT'){//Delete Team
					var slot = post.info.substring(2,3); //Location in table
					var team = post.info.substring(3); //Name of team
					if(main_team === team) setCookie(main_user,"NOTEAM",res);
					db.removeTeam(main_user,team,function(err,data){
						db.removeUserTeams(main_user,slot,function(err2,data2){
							db.getUserTeams(main_user,function(err3,data3){
								res.end(vm.makeTeamSelectForm(data3));
							});
						});
					});
				}
				else{
					//This should be unreachable but, if anything happens it will just go back to main menu.
					serveFile("views/index.html",res);
				}
			} //End (post.info != null)
			else { //This is for if post request is sent from login form
			    if(post.newuser != null){ //If joining
					var newuser = ic.clean(post.newuser);
					var pwd = ic.clean(post.pwd);
					if(newuser == "Bad_Name" || pwd == "Bad_Name" || (/\s/.test(post.newuser)) || (/\s/.test(post.pwd))){
						res.end(vm.loginErrorForm(-3));
						return;
					}
					var encrpyted_password = encryption.encipher(pwd); //Encrypt Password
					db.addUser(newuser,encrpyted_password,function(err,data){
						if(data == 1){
							setCookie(newuser,"NOTEAM",res);
							db.addUserTeams(newuser); //Add to user_teams table for multiple teams.
							db.addEvoUser(newuser); //Create an evolution list for new user.
							console.log("New User Added");
							serveFile('views/index.html',res);
							return;
						}
						res.end(vm.loginErrorForm(-1));
					});
				}
				else{//If loging in
					//Encrypt and check user
					var username = ic.clean(post.username);
					var pwd = ic.clean(post.pwd);
					var encrpyted_password = encryption.encipher(pwd);
					db.checkUser(username, encrpyted_password, function(err,data){
						if(data == 1){
							setCookie(username,"NOTEAM",res);
							console.log("User " + username + " logged in");
							serveFile("views/index.html",res);
							return;
						}
						else{
							res.end(vm.loginErrorForm(-2));
							return;
						}
					});	
				}
			}
		}); // End req.on end
	}//End handle post request
	
	else if(req.url === '/' || req.url === '/index'){//Main Menu
		serveFile("views/index.html",res);
	}
	else if(req.url === "/ManageTeams"){
		if(main_user === "NOUSER" || !main_user){
			res.writeHeader(200, {"Content-Type": "text/html"});
			serveFile("views/NoUserAlert.html",res);
		}
		db.getUserTeams(main_user,function(err,data){
			res.end(vm.makeTeamSelectForm(data)); //Display page with all of the users teams.
			return;
		});
		return;
	}
	else if(req.url === "/ManageEvolutions"){
		if(!main_user || main_user === "NOUSER"){
			res.writeHeader(200, {"Content-Type": "text/html"});
			serveFile("views/NoUserAlert.html",res);
		}
		loadEvos(main_user,res); //Display page with all pokemon user has added to evolution list.
		return;
	}
	else if(req.url === "/LogInPage"){
		res.writeHeader(200, {"Content-Type": "text/html"});
		serveFile("views" + req.url + ".html",res);	
	}
	else if(req.url === "/LogOut"){
		setCookie("NOUSER","NOTEAM",res);
		serveFile("views/index.html",res);
	}
	else if(req.url === "/Credits"){
		res.writeHeader(200, {"Content-Type": "text/html"});
		serveFile("views/Credits.html",res);
	}
	else if(req.url === "/PokemonList" || req.url === "/PokemonList3D"){
		res.writeHeader(200, {"Content-Type": "text/html"});
		serveFile("views" + req.url + ".html", res); //Display page with all avalible pokemon.
	}
	else{
		serveFile("views/" + req.url, res); //For every other file like .css or .js
	}
}

/**@function readCookies
  * Get raw information from session cookie.
  * This code is from https://stackoverflow.com/questions/3393854/get-and-set-a-single-cookie-with-node-js-http-server
  * @param {http.ClientRequest} req - The client request object.
  * @return {Object} list - Cookie data split by ;.
  */
function readCookies(req){
	 var list = {},
     rc = req.headers.cookie;
     rc && rc.split(';').forEach(function( cookie ) {
		 var parts = cookie.split('=');
		 list[parts.shift().trim()] = decodeURI(parts.join('='));
     });
    return list;
}

/**@function getCookieInfo
  * Parses info contained in cookie
  * @param {http.ClientRequest} req - The client request object.
  * @return {Array} - An array with [0] = username and [1] = teamname.
  */
function getCookieInfo(req){
	var list = readCookies(req);
	var str = JSON.stringify(list);
	if(str !== '{}'){
		str = str.replace('}','').replace('{','')
		str = str.split(':')[1];
		var u = str.split('?')[0].replace('"','');
		if(str.split('?')[1]){
			var t = str.split('?')[1].replace('"','');
		}
		return [u,t];
	}
	else{
		return[null,null];
	}
}

/**@function setCookie
  * Sets a session cookie to hold a users name and the team they are managing.
  * @param {String} username - The user who is logged in on the browser.
  * @param {String} team - The name of the team the user is managing.
  */
function setCookie(username, team, res){
	res.writeHead(200,{ 'Set-Cookie': 'info=' + username + "?" + team});
	console.log("Main user is now: " + username);
	console.log("Team is now: " + team);
	return;
}

/** @function loadTeams
  * Gets users team information from the database and sends it to
  * the view manager to create and output page with the users team.
  * @param {String} user - The user who requested to see the team.
  * @param {String} team - The team the user wants to view.
  * @param {http.ServerResponse} res - The server response object.
  */  
function loadTeams(user,team,res){
	console.log("Team " + team + "loaded");
	db.getTeam(user,team,function(err,data){
		if(err){
			console.log("Error");
		}
		else{
			loadPokemonData(function(poke_info){
				res.end(vm.makeTeamForm(data,poke_info));//Display page with users team
			});
		}
	});
}

/** @function loadEvos
  * Gets users evolution list information from the database and sends it to
  * the view manager to create and output page with the users evolution list.
  * @param {String} user - The user who requested to see their evolution list.
  * @param {http.ServerResponse} res - The server response object.
  */
function loadEvos(user,res){
	db.getEvos(user, function(err, data){
		if(err){
			console.log("Error");	
		}
		else{
			loadPokemonData(function(poke_info){
			res.end(vm.makeEvoForm(data,poke_info));//Display Page with users evolution list
			});
		}
	});
}

/** @function serveFile
  * Serves a request file by reading in the information and displaying
  * it to the user.
  * @param {String} path - The path to the file the client requested.
  * @param {http.ServerResponse} res - The server response object.
  */  
function serveFile(path,res){
	fs.readFile(path, function(err,data){
		if(err){
			res.statusCode = 404;
			res.end("File not found");
			return;
		}
		res.end(data);
	});
}

//Start server
var server = http.createServer(handleRequest);
server.listen(PORT, function(){
	console.log("Listening at port " , PORT);
});
