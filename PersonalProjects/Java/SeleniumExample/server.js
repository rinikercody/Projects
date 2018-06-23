const http = require('http');
const fs = require('fs');

const PORT = 3000;

/** @function handleRequest
  * Handles request from clients and serves request files along 
  * with interacting with the application database.
  * @param {http.ClientRequest} req - The client request object.
  * @param {http.ServerResponse} res - The server response object.
  */
function handleRequest(req,res){	
	if(req.url === '/'){
		serveFile('index.html',res);
	}
	else{
		console.log(req.url);
		serveFile(req.url.substring(1),res);
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