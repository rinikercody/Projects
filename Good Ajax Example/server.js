const http = require('http');
var fs = require('fs');
const PORT = 3000;

CodyPoints = [
    {"month": 0, "shinys": 5}, //JAN is 0
    {"month": 2, "shinys": 12},
    {"month": 3, "shinys": 4},
]

function handleRequest(req,res){
    if(req.url == '/'){
        serveFile('index.html',res);
    }
    else{
        serveFile(req.url.substring(1),res);
    }
}

function serveFile(path,res){
    fs.readFile(path,function(err,data){
        if(err){
            res.end('File not found');
        }
        res.end(data);
    });
}

var server = http.createServer(handleRequest);
server.listen(PORT,function(){
    console.log(PORT);
})