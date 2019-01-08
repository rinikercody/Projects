// Screen dimensions
const WIDTH = 1000;
const HEIGHT = 600;

//UI stuff
var score = 0;
var lives = 0;

//Used to start and restart the game
var gameStarted = false;

//Used like an enum for the end game
var gameOver = 0;

//Used to handle deaths
var paused = false;

//Player stuff
var playerX = 500;
var playerY = 580;
var playerWidth = 50;
var playerHeight = 20;
var playerSpeed = 0.15;

var bulletSpeed = 0.15;

//Enemy Stuff
var invaderWidth = 40; //Invaders height is also the same.
var invaderSpeed;
var invaderShotChance = 1000; //This impacts how likely the invader is to shoot.
var wave; //Used to control different sets of invaders

//Get the canvas that the player will see.
var displayCanvas = document.getElementById('display-canvas');
var displayCtx = displayCanvas.getContext('2d');
displayCanvas.height = HEIGHT;
displayCanvas.width = WIDTH;

//Create back buffer canvas
var backBuffer = document.createElement('canvas');
var backCtx = displayCanvas.getContext('2d');
backBuffer.height = HEIGHT;
backBuffer.width = WIDTH;


/** @function Invader
  * This is an Invader class that is used make and track invaders
  * @param {Integer} x - The invaders x position 
  * @param {Integer} y - The invaders y position
  * @param {Integer} direction - The currenct direction the invader is moving in. -1 for left and 1 for right.
*/
function Invader(x,y,direction, yDirection){
	this.x = x;
	this.y = y;
	this.direction = direction;
	this.yDirection = yDirection;
	this.movedDown = false; //Has the invader just hit the edge of the screen and moved down a level
}
Invaders = []; //Holds all the games invaders

/** @function InvaderBullet
  * This class is used to create bullet object which the invaders will fire.
  * @param {Integer} x - The new x cordinate of the bullet.
  * @param {Integer} y - The new y cordinate of the bullet.
 */
function InvaderBullet(x,y){
	this.x = x;
	this.y = y;
}
InvaderBullets = [];//An array to track all the invaders bullets.

/** @function Bullet
  * This class is used to create bullet object which the player will fire.
  * @param {Integer} x - The new x cordinate of the bullet.
  * @param {Integer} y - The new y cordinate of the bullet.
 */
function Bullet(x, y){
	this.x = x;
	this.y = y;
}
Bullets = []; //An array to track all the players bullets.

/** @function startGame
  * Resets all games values to their defualt and starts the game.
  */
function startGame(){
	if(!gameStarted){
		score = 0;
		lives = 3;
		wave = 1;
		gameStarted = true;
		//Reseting things when a new game starts
		Invaders = [];
		Bullets = [];
		InvaderBullets = [];
		invaderSpeed = 0.15;
		playerX = 500;
		document.getElementById('start-button').disabled = true;
		gameOver = 0;
		paused = false;
		
		//Creating 10 invaders
		var x = 900;
		for(var i = 0; i < 10; i++){
			Invaders.push(new Invader(x,15, -1))
			x -= 50;
		}
		
	}
}

//Used to handle input from player
var currentInput = {
	space: false,
	left: false,
	right: false,
	up: false,
	down: false
}

//Used to handle input from player
var priorInput = {
	space: false,
	left: false,
	right: false,
	up: false,
	down: false
}

/** @function handleKeydown
  * Event handler for keydown events
  * @param {KeyEvent} event - the keydown event
  */
function handleKeydown(event) {
  switch(event.key) {
    case ' ':
      currentInput.space = true;
      break;
    case 'ArrowUp':
    case 'w':
      currentInput.up = true;
      break;
    case 'ArrowDown':
    case 's':
      currentInput.down = true;
      break;
	case 'ArrowLeft':
    case 'a':
      currentInput.left = true;
      break;
    case 'ArrowRight':
    case 'd':
      currentInput.right = true;
      break;
  }
}
// Attach keydown event handler to the window
window.addEventListener('keydown', handleKeydown);

/** @function handleKeyup
  * Event handler for keyup events
  * @param {KeyEvent} event - the keyup event
  */
function handleKeyup(event) {
  switch(event.key) {
    case ' ':
      currentInput.space = false;
      break;
    case 'ArrowUp':
    case 'w':
      currentInput.up = false;
      break;
    case 'ArrowDown':
    case 's':
      currentInput.down = false;
      break;
	case 'ArrowLeft':
    case 'a':
      currentInput.left = false;
      break;
    case 'ArrowRight':
    case 'd':
      currentInput.right = false;
      break;
  }
}
// Attach keyup event handler to the window
window.addEventListener('keyup', handleKeyup);


var start = null; //Used for timing in gameLoop

/** @function gameLoop
  * This method keeps the game moving by using window.requestAnimationFrame.
  * Methods used to update game varibles, get user input, an render sprites to screen are called withen this loop.
  * @param {Double} timestamp - The time which gameLoop was called.
  */  
function gameLoop(timestamp){
	if(gameStarted){
		if(!start) start = timestamp;
		var elapsedTime = timestamp - start;
		start = timestamp;	
		update(elapsedTime);
		processInput();
		render(backCtx);
		displayCtx.drawImage(backBuffer,0,0); //Copy backbuffer to display canvas 
		window.requestAnimationFrame(gameLoop); //Keep loop going
	}
	else{
		window.requestAnimationFrame(gameLoop);
	}
}

/** @function processInput
  * This function changes the priorInput variable to match the currentInput varible.
  * This helps stop things like bullets being constanlty fired.
*/
function processInput(){
  priorInput = JSON.parse(JSON.stringify(currentInput));
}

/** @function update
  * All the games varibles are changed in this function. This manages the game model
  * @param {Double} elapsedTime - Amount of time since last gameLoop iteration.
  */
function update(elapsedTime){
	if(!paused){
		if(currentInput.left){
			if(playerX > 0){
				playerX -= playerSpeed * elapsedTime;
			}
		}
		
		if(currentInput.right){
			if(playerX + playerWidth < WIDTH){
				playerX += playerSpeed * elapsedTime;
			}
		}
		
		if(currentInput.space && !priorInput.space && Bullets.length < 10){
			Bullets.push(new Bullet(playerX + playerWidth/2,playerY));
		}
		
		
		//Move player bullets
		Bullets.forEach(function(bullet){
			bullet.y -= bulletSpeed * elapsedTime;
			if(bullet.y < 0) Bullets.splice(0,1); //Remove offscreen bullets
		});
		
		var x = 0;
	    
		if(wave == 0){
			x = 900;
			for(var i = 0; i < 10; i++){
				Invaders.push(new Invader(x,15, -1))
				x -= 50;
			}
			wave = 1;
		}
		if(wave == 1 && Invaders.length == 0){
			x = -1000;
			for(var i = 0; i < 20; i++){
				Invaders.push(new Invader(x,15,1));
				x += 50;
			}
			invaderSpeed = 0.20;
			wave = 2;
		}
		if(wave == 2 && Invaders.length == 0){
			x = 2500;
			for(var i = 0; i < 30; i++){
				Invaders.push(new Invader(x,15,1));
				x -= 50;
			}
			invaderSpeed = 0.25;
			wave = 3;
		}	
		if(wave == 3 && Invaders.length == 0){
			x = 50
			
			var y = -50;
		    for(var i = 0; i < 3; i++){
			   Invaders.push(new Invader(WIDTH/2 - 50,y,0,1));
			   Invaders.push(new Invader(WIDTH/2 + 50, y, 0,1));
			   y -= 50
			}
			
			invaderSpeed = 0.15;
			wave = 4;
		}
		
		if(wave == 4 && Invaders.length == 0){
			x = 150
			var y = -50;
			for(var i = 0; i < 8; i++){
				Invaders.push(new Invader(x,y,1,0));
				x += 50;
			}
			invaderSpeed = 0.10;
			wave = 5;
		}
		
		if(wave == 5 && Invaders.length == 0){
			x = WIDTH - 150;
			var y = -50;
			for(var i = 0; i < 8; i++){
				Invaders.push(new Invader(x,y,1,0));
				x -= 50;
			}
			invaderSpeed = 0.10;
			wave = 6;
		}
		if(wave == 6 && Invaders.length == 0){
			x = 50;
			for(var i = 0; i < 3; i++){
				Invaders.push(new Invader(x,0,1,0));
				Invaders.push(new Invader(WIDTH - x,0,-1,0));
				x += 50;
			}
			invaderSpeed = 0.40;
			wave = 7;
		}
		
		if(wave == 7 && Invaders.length == 0){
			    x = 100;
				y = -50;
				for(var i = 0; i < 10; i++){
					Invaders.push(new Invader(x,y,0,1));
					y -= 50;
				}
				invaderSpeed = 0.2;
				wave = 8;
		}
				
		
		if(wave == 8 && Invaders.length == 0){
			x = -50;
			var y = -50;
			for(var i = 0; i < 10; i++){
				Invaders.push(new Invader(x,y,1,0));
				Invaders.push(new Invader(x,y + 50,1,0));
				Invaders.push(new Invader(WIDTH - x, y, -1, 0));
				Invaders.push(new Invader(WIDTH - x, y + 50, -1, 0));
				x -= 50;
				y -= 50;
			}
			wave = 9;
		}
		
		if(wave == 9 && Invaders.length == 0){
			x = WIDTH - 300;
			var y = -50;
			for(var i = 0; i < 10; i++){
				Invaders.push(new Invader(x,y,0,0));
				x -= 50;
			}
			invaderSpeed = 0.12;
			wave = 10;
		}
		
		if(wave == 10 && Invaders.length == 0){
			x = 400;
			var y = -100;
			for(var i = 0; i < 30; i++){
				Invaders.push(new Invader(x,y,0,1));
				x += 50;
				if(x == 650){
					x = 400;
					y -= 50;
				}
			}
			invaderSpeed = 0.04;
			wave = 11;
		}
		
		if(wave == 11 && Invaders.length == 0){
			gameOver = 2;
		}
		
		Invaders.forEach(function(invader,index){
				//Stop invaders from going past the left side of the screen
				
				if(wave == 5 || wave == 6 || wave == 10){
					r = Math.floor(Math.random() * 4);
					if(r == 0) invader.direction = -1;
					else if(r == 1) invader.direction = 1;
					else if(r == 2) invader.yDirection = 1;
					else invader.yDirection = 0;
				}
				
				if(wave == 8){
					if(invader.y > 300){
						invader.direction = 1;
						invader.yDirection = 0;
					}	
					if(invader.x > 800){
						invader.direction = 0;
						invader.yDirection = 1;
					}
				}
				
				if(invader.x <= 0 && invader.movedDown == false){
					invader.direction = 1;
					invader.y += 50;
					invader.movedDown = true;
				}
				if(invader.y > HEIGHT){
					gameOver = 1;
				}
				
				//Stop invaders from going past the right side of the screen
				if((invader.x + invaderWidth) >= WIDTH && invader.movedDown == false){
					invader.direction = -1;
					invader.y += 50;
					invader.movedDown = true;
				}
				
				if(invader.x > 0 && invader.x + invaderWidth < WIDTH){
					invader.movedDown = false;
				}			
				
				var r = Math.floor(Math.random() * invaderShotChance);
				if(r == 0 && wave != 4 && invader.y > 0){
					InvaderBullets.push(new InvaderBullet(invader.x,invader.y));
				}
				checkCollison(invader,index);
				//Basic invader movement.
				if(!invader.yDirection){
					invader.x += invader.direction * invaderSpeed * elapsedTime;
				}
				else{
					invader.y += invader.yDirection * invaderSpeed * elapsedTime;
				}
		});
		
		InvaderBullets.forEach(function(bullet, index){
			bullet.y += 0.3 * elapsedTime;
			if(bullet.x > playerX && bullet.x < playerX + playerWidth && bullet.y >= displayCanvas.height - playerHeight){
				delete InvaderBullets[index];
				lives--;
				paused = true;
			}		
			if(bullet.y > HEIGHT) delete InvaderBullets[index];
		});
		if(lives == 0){
			gameOver = 1;
		}
	}
	else{
		if(currentInput.up){
			Bullets = [];
			Invaders = [];
			InvaderBullets = [];
			wave--;
		    paused = false;
		}
	}
}

/** @function checkCollison
  * @param {Invader} invader - The invader object which being check to see if a bullet hit it.
  * @param {Integer} index - The location of the invader in the Invaders array.
*/  
function checkCollison(invader,index){
	Bullets.forEach(function(bullet,i){
		//Need to mess with collison more
		if(bullet.y < invader.y + invaderWidth && bullet.y > invader.y && bullet.x > invader.x && bullet.x < invader.x + invaderWidth) 
			{
				Bullets.splice(i,1); //Delete bullet
				Invaders.splice(index,1); //Delete invader
				score += 10;
			}
	});	
}

/** @function render
  * This functions draws all the games sprites to the screen
  * @param {Canvas context} ctx - The canvas context which the game will be rendered to.
  */
function render(ctx){
	ctx.clearRect(0, 0, WIDTH, HEIGHT);
	ctx.font="12px Arial";
	var playerColor = "";
	if(lives == 3){ 
	playerColor = "DarkBlue";
	}
	else if(lives == 2) {
		playerColor = "Teal";
	}
	else{ 
		playerColor = "FireBrick";
	}
	
	
	if(gameOver == 1){
		gameStarted = false;
		ctx.fillText("You Lose", WIDTH/2, HEIGHT/2);
		ctx.fillText("Score: " + score, WIDTH/2, HEIGHT/2 + 15);
		document.getElementById('start-button').disabled = false;
	}
	else if(gameOver == 2){
		gameStarted = false;
		ctx.fillText("You Win!", WIDTH/2, HEIGHT/2);
		document.getElementById('start-button').disabled = false;
	}
	else{
		ctx.fillStyle = playerColor;
		ctx.fillRect(playerX,playerY,playerWidth,playerHeight);
		
		var invaderColor = "";
		if(wave < 4) invaderColor = "Gray";
		if(wave >= 4 && wave < 7) invaderColor = "Yellow";
		if(wave >= 7 && wave < 9) invaderColor = "Purple";
		if(wave >= 9) invaderColor = "Crimson";
		
		Invaders.forEach(function(invader){
			ctx.fillStyle = invaderColor;
			ctx.fillRect(invader.x,invader.y,invaderWidth,invaderWidth);
		});
		InvaderBullets.forEach(function(bullet){
			ctx.fillStyle = "Yellow";
			ctx.fillRect(bullet.x,bullet.y,5,5);
		});
		Bullets.forEach(function(bullet){
			ctx.fillStyle = playerColor;
			ctx.fillRect(bullet.x,bullet.y,5,5);
		});
		if(paused){
			var str = "You were just hit, press up or w to continue.";
			ctx.fillStyle = "Red";
			ctx.fillText(str, WIDTH/2 - 100, HEIGHT/2);
			ctx.fillText("You have " + lives + " lives left",WIDTH/2 - 50, HEIGHT/2 + 15);
		}
	}
	ctx.fillStyle = "Red";
	ctx.fillText("Score: " + score,5,10);
	if(wave < 11){
		ctx.fillText("Wave: " + wave, WIDTH/2, 10);
	}
	else{
		ctx.fillText("Wave: Final", WIDTH/2, 10);
	}
	ctx.fillText("Lives: " + lives,WIDTH - 50,10);
}

//Starts the gameLoop
window.requestAnimationFrame(gameLoop);