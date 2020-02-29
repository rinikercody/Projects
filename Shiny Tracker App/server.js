const http = require('http');
const fs = require('fs');

const PORT = 3000;

function createHTML(){
	var codyInfo = fs.readFileSync('data/codyInfo.txt','utf8');
	var teresaInfo = fs.readFileSync('data/teresaInfo.txt','utf8');
	var codyData = codyInfo.split('\n');
	var teresaData = teresaInfo.split('\n');
	
	
	var html = '';
	html += '<!DOCTYPE html>'
	html += '<html>';
	html += '	<head>';
	html += '		<title>shiny</title>';
	html += '		<link rel=stylesheet type=text/css href=css/stylesheet.css>'
	html += '	</head>';
	html += '	<body>';
	html += '		<div id=wrapper>'
	html += '           <div id=container>';
	html += '       	<div id=codyPokemon>'
	html += '           <h1>Cody</h1>'
	for(var i = 0; i < codyData.length; i++){
		html += '<div class=pokemonBox>';
		if(!teresaData.includes(codyData[i])){
			html += '               <text class=unique>' + codyData[i] + '</text>';
		}
		else{
			html += '				<text>' + codyData[i] + '</text>'
		}
		html += '<img src="ShinySprites/' + codyData[i].trim().toLowerCase() + '.png" alt=c' + codyData[i] + '>'
		html += '</div>'
	}
	
	html += '           </div>';
	
	html += '			<div id=teresaPokemon>'
	html += '           <h1>Teresa</h1>'
	for(var i = 0; i < teresaData.length; i++){
		html += '<div class=pokemonBox>';
		if(!codyData.includes(teresaData[i])){
			html += '               <text class=unique>' + teresaData[i] + '</text>';
		}
		else{
			html += '				<text>' + teresaData[i] + '</text>'
		}
		html += '<img src="ShinySprites/' + teresaData[i].trim().toLowerCase() + '.png" alt=c' + teresaData[i] + '>'
		html += '</div>';
	}
	html += ' </div>';
	html += '           </div>';
	
	html += '	</body>';
	html += '</html>';
	
	return html;
}

/** @function handleRequest
  * Handles request from clients and serves request files along 
  * with interacting with the application database.
  * @param {http.ClientRequest} req - The client request object.
  * @param {http.ServerResponse} res - The server response object.
  */
function handleRequest(req,res){	
	if(req.method === 'GET'){
		if(req.url === '/'){
			//serveFile('index.html',res);
			var html = createHTML();
			res.end(html);
		}
		else{
			//console.log(req.url);
			serveFile(req.url.substring(1),res); //cut of / on request
		}
	}
	
	if(req.method === 'POST'){
		var body = '';
		req.on('data', function(data){
			body += data;
		});
		
		req.on('end', function(){
			var post = qs.parse(body);
			//Do Stuff Here
		});
	}
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