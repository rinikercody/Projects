//Screen dimensions
const WIDTH = 1300;
const HEIGHT = 650;

var x = WIDTH/2;
var y = HEIGHT/2;

var playerSpeed = 0.15;
var bulletSpeed = 0.2;
var rotateSpeed = 0.1; //How fast the player changes angle

var lives = 3;
var score = 0;
var wave = 0;
var hasSheild = false;

var gameStarted = false;
var paused = false;
var lightMode = true;
var viewHitBoxes = false;
var checkingRules = false;

//Button below the screen
var mainButton = document.getElementById('main-button');

//Sounds
var fireSFX = document.createElement('audio');
fireSFX.src = 'sounds/playershoot.wav';
var playerHitSFX = document.createElement('audio');
playerHitSFX.src = 'sounds/playerdeath.wav';
var explosionSFX = document.createElement('audio');
explosionSFX.src = 'sounds/explosion.wav';
var powerupSFX = document.createElement('audio');
powerupSFX.src = 'sounds/powerup.wav';
var gameOverSFX = document.createElement('audio');
gameOverSFX.src = 'sounds/gameover.wav';
var alienFireSFX = document.createElement('audio');
alienFireSFX.src = 'sounds/alienfire.wav';

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


/** @function Player
  * The function is a class which contains all the player information
  * @param {Integer} x - player's x position
  * @param {Integer} y - player's y position
*/
function Player(x,y){
	this.x = x;
	this.y = y; //center
	this.width = 10;
	this.height = 10;
	
	//This forms a triangle
	this.vertices = {
		x1: -10,
		y1: 10,
		x2: 0,
		y2: -20,
		x3: 10,
		y3: 10
	};
	
	//Which way the player is pointing
	this.angle = 0;
	this.radius = 20;
}

//The main ship the user controls
var player = new Player(WIDTH/2,HEIGHT/2);

/** @function Bullet
  * This function is a class which contains information about a player's bullet.
  * @param {Integer} x - The bullets x position
  * @param {Integer} y - The bullets y position
  * @param {Integer} xAngle - What the bullets x position will change by every frame.
  * @param {Integer} yAngle - What the bullets y position will change by every frame.
  */
function Bullet(x,y,xAngle, yAngle){
	this.x = x;
	this.y = y;
	this.xAngle = xAngle;
	this.yAngle = yAngle;
}
Bullets = []; //Holds all the players bullets that are on screen

/** @function Alien
  * The function is a class that contains all an aliens information
  * @param {Integer} x - Alien's x position
  * @param {Integer} y - Alien's y position
  */
function Alien(x,y){
	this.x = x;
	this.y = y;
	this.width = 15;
	this.height = 15;
	this.xSlope = 0;
	this.ySlope = 0;
	this.vertices = [];
	this.vertices.push({x:0,y:0});
	this.vertices.push({x:15,y: -5});
	this.vertices.push({x:20,y: -10});
	this.vertices.push({x:10,y: -15});
	this.vertices.push({x:5,y: -20});
	this.vertices.push({x:-5,y: -20});
	this.vertices.push({x:-10,y: -15});
	this.vertices.push({x:-20,y: -10});
	this.vertices.push({x:-15,y: -5});
}

//The alien that will sometimes appear
var alien = null;

/** @function AlienBullet
  * This function is a class that contians all the info for a single bullet fired by the alien
  * @param {Integer} x - bullets x position
  * @param {Integer} y - bullets y position
  * @param {Integer} xSlope - What the bullets x position will change by every frame.
  * @param {Integer} ySlope - What the bullets y position will change by every frame.
  */
function AlienBullet(x,y,xSlope,ySlope){
	this.x = x;
	this.y = y;
	this.xSlope = xSlope;
	this.ySlope = ySlope;
}
AlienBullets = []; //This array holds all the AlienBullets that are on the screen.

/** @function PowerUp
  * This function is a class which contains information about powerups which help the player.
  * @param {Integer} x - The powerups x postion
  * @param {Integer} y - The powerups y postion
  * @param {Integer} width - How wide the powerup is.
  * @param {Integer} height - How tall the powerup is.
  * @param {String} type - What specific powerup it is.
  */
function PowerUp(x,y,width,height,type){
	this.x = x;
	this.y = y;
	this.width = width;
	this.height = height;
	this.type = type;
}
PowerUps = []; //Hold all the power ups that are on screen.

/** @function Asteroid
  * This function is a class that contains information about an asteroid
  * @param {Integer} x - The asteroids x position
  * @param {Integer} y - The asteroids y position
  * @param {Float} velocity - How fast the asteroid is moving.
  * @param {Integer} mass - The size of the asteroid
  * @param {Integer} xSlope - What the asteroid's x position will change by every frame.
  * @param {Integer} ySlope - What the asteroid's y position will change by every frame.
  * @param {Integer} width - Roughly how wide the asteroid is.
  * @param {Integer} height - Roughly how tall the asteroid is.
  */
function Asteroid(x,y, velocity,mass,xSlope,ySlope,width,height){
	this.x = x;
	this.y = y;
	this.vertices = [];
	this.edges = [];
	this.velocity = velocity;
	this.mass = mass;
	this.xSlope = xSlope;
	this.ySlope = ySlope;
	this.width = width;
	this.height = height;
	this.inPlay = false; //Used to allow asteroids to come in from off screen
}
Asteroids = []; //This holds all the asteroids that are in the game.

/** @function startGame
  * Used to start a new game when the button on screen is clicked
  */
function startGame(){
	    if(!gameStarted){
			mainButton.disabled = true;
			//Reset values
			lives = 3;
			score = 0;
			wave = 0;
			player.x = WIDTH/2;
			player.y = HEIGHT/2;
			playerSpeed = 0.15;
			rotateSpeed = 0.10;
			bulletSpeed = 0.20;
			hasSheild = false;	
            alien = null;			
			Asteroids = [];
			Bullets = [];
			PowerUps = [];
			AlienBullets = [];
			
			gameStarted = true;
		}
		if(paused){
			paused = false;
			mainButton.disabled = true;
		}
}

/** @function createAsteroids
  * Creates a new asteroid based on rank
  * @param {Integer} size - How big the asteroid is going to be.
  * @param {Integer} x - The asteroids x position
  * @param {Integer} y - The asteroids y position
  * @param {Integer} xSlope - What the asteroid's x position will change by every frame.
  * @param {Integer} ySlope - What the asteroid's y position will change by every frame.
  */
function createAsteroids(size,x,y,slopeX,slopeY){
	//Smallest asteroid
	if(size == 0){
		var smallerAsteroid = new Asteroid(x,y,0.5,0,slopeX,slopeY,20,20);
		smallerAsteroid.vertices.push({x: 0, y: 0});
		smallerAsteroid.vertices.push({x: 10, y: 5});
		smallerAsteroid.vertices.push({x: 15, y: 15});
		smallerAsteroid.vertices.push({x: 5, y: 20});
		Asteroids.push(smallerAsteroid);
	}
	//Small asteroid
	if(size == 1){
		var smallAsteroid = new Asteroid(x,y,0.1,1,slopeX,slopeY,40,40); //smalled so mass is one
		smallAsteroid.vertices.push({x: 0, y: 0});
		smallAsteroid.vertices.push({x: 25, y: 0});
		smallAsteroid.vertices.push({x: 40, y: 25});
		smallAsteroid.vertices.push({x: 40, y: 35});
		smallAsteroid.vertices.push({x: 20, y: 40});
		smallAsteroid.vertices.push({x: 10, y: 30});
		Asteroids.push(smallAsteroid);
	}
	//Medium asteroid
	if(size == 2){
		var mediumAsteroid = new Asteroid(x, y, 0.1, 2,slopeX,slopeY,50,100); //medium so mass is 2
		mediumAsteroid.vertices.push({x: 0, y: 25});
		mediumAsteroid.vertices.push({x: 50, y: 0});
		mediumAsteroid.vertices.push({x: 50, y: 75});
		mediumAsteroid.vertices.push({x: 50, y: 85});
		mediumAsteroid.vertices.push({x: 30, y: 100});
		mediumAsteroid.vertices.push({x: -10, y: 90});
		Asteroids.push(mediumAsteroid);
	}
	//Big asteroid
	if(size == 3){
		var bigAsteroid = new Asteroid(x, y, 0.2,3,slopeX,slopeY,70,110);
		bigAsteroid.vertices.push({x: 0, y: 0});
		bigAsteroid.vertices.push({x: 50, y: 0});
		bigAsteroid.vertices.push({x: 75, y: 50});
		bigAsteroid.vertices.push({x: 75, y: 100});
		bigAsteroid.vertices.push({x: 50, y: 75});
		bigAsteroid.vertices.push({x: 10, y: 120});
		bigAsteroid.vertices.push({x: -10, y: 100});
		Asteroids.push(bigAsteroid);
	}
	//Bigger asteroid
	if(size == 4){
		var biggerAsteroid = new Asteroid(x, y, 0.4,4,slopeX,slopeY,120,100);
		biggerAsteroid.vertices.push({x: 0, y: 0});
		biggerAsteroid.vertices.push({x: 120, y: 10});
		biggerAsteroid.vertices.push({x: 120, y: 30});
		biggerAsteroid.vertices.push({x: 100, y: 35});
		biggerAsteroid.vertices.push({x: 120, y: 45});
		biggerAsteroid.vertices.push({x: 130, y: 70});
		biggerAsteroid.vertices.push({x: 140, y: 90});
		biggerAsteroid.vertices.push({x: 90, y: 110});
		biggerAsteroid.vertices.push({x: -20, y: 90});
		biggerAsteroid.vertices.push({x: -30, y: 50});
		Asteroids.push(biggerAsteroid);
	}
}


//Used to handle input from player
var currentInput = {
	space: false,
	left: false,
	right: false,
	up: false,
	down: false,
	rotateLeft: false,
	rotateRight: false,
	lightmode: false,
	hitboxes: false,
	randomSpawn: false,
	quickContinue: false,
	checkRules: false
}

//Used to handle input from player
var priorInput = {
	space: false,
	left: false,
	right: false,
	up: false,
	down: false,
	rotateLeft: false,
	rotateRight: false,
	lightmode: false,
	hitboxes: false,
	randomSpawn: false,
	quickContinue: false,
	checkRules: false
}

/** @function handleKeydown
  * Event handler for keydown events
  * @param {KeyEvent} event - the keydown event
  */
function handleKeydown(event) {
  switch(event.key) {
    case ' ':
	case 'ArrowUp':
      currentInput.space = true;
      break;
    case 'w':
      currentInput.up = true;
      break;
    case 's':
      currentInput.down = true;
      break;
	case 'a':
      currentInput.left = true;
      break;
    case 'd':
      currentInput.right = true;
      break;
	case 'ArrowLeft':
	  currentInput.rotateLeft = true;
	  break;
	case 'ArrowRight':
	  currentInput.rotateRight = true;
	  break;
	case 'l':
	  currentInput.lightmode = true;
	  break;
	case 'h':
	  currentInput.hitboxes = true;
	  break;
	case 'r':
	  currentInput.randomSpawn = true;
	  break;
	case 'c':
	  currentInput.quickContinue = true;
	  break;
	case 'm':
	case 'Escape':
	  currentInput.checkRules = true;
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
	case 'ArrowUp':
      currentInput.space = false;
      break;
    case 'w':
      currentInput.up = false;
      break;
    case 's':
      currentInput.down = false;
      break;
    case 'a':
      currentInput.left = false;
      break;
    case 'd':
      currentInput.right = false;
      break;
	case 'ArrowLeft':
	  currentInput.rotateLeft = false;
	  break;
	case 'ArrowRight':
	  currentInput.rotateRight = false;
	  break;
	case 'l':
	  currentInput.lightmode = false;
	  break;
	case 'h':
	  currentInput.hitboxes = false;
	  break;
	case 'r':
	  currentInput.randomSpawn = false;
	  break;
	case 'c':
	  currentInput.quickContinue = false;
	  break;
	case 'm':
	case 'Escape':
	  currentInput.checkRules = false;
	  break;
  }
}
// Attach keyup event handler to the window
window.addEventListener('keyup', handleKeyup);

/** @function processInput
  * This function changes the priorInput variable to match the currentInput varible.
  * This helps stop things like bullets being constanlty fired.
*/
function processInput(){
  priorInput = JSON.parse(JSON.stringify(currentInput));
}

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
		if(!paused)update(elapsedTime);
		if(currentInput.checkRules && !priorInput.checkRules){
			checkingRules = !checkingRules;
			paused = !paused;
		}
		if(currentInput.lightmode && !priorInput.lightmode){
			lightMode = !lightMode;
		}
		processInput();
		render(backCtx);
		if(paused && currentInput.quickContinue){
			startGame(); //Allows user to press c after losing a life.
		}
		
		if(lives == 0){
			gameStarted = false;
			gameOverSFX.play();
			mainButton.value = "New Game";
		}

		displayCtx.drawImage(backBuffer,0,0); //Copy backbuffer to display canvas 
		window.requestAnimationFrame(gameLoop); //Keep loop going
	}
	else{
		window.requestAnimationFrame(gameLoop);
		render(backCtx);
	}
}

/** @function generateRandomAsteroids
  * Generates multiple asteroids of varing ranks and amounts.
  * @param {Integer} maxRank - The biggest rank/size the asteroid can be.
  * @param {Integer} amount - The number of asteroids to create.
  * @param {Integer} maxSpeed - The highest slope value the asteroid can have.
  */
function generateRandomAsteroids(maxRank,amount,maxSpeed){
	for(var i = 0; i < amount; i++){
			var rank = Math.floor(Math.random() * (maxRank + 1));
			
			var check = false;
			
			var rx = Math.floor(Math.random() * WIDTH); //adjust these things just floating off screen
			var ry = Math.floor(Math.random() * HEIGHT);
			var invert = Math.random();
			if(invert > 0.5) ry = ry * -1;
			
			Asteroids.forEach(function(asteroid,i){
				if(ry < 0 && asteroid.y < 0 || ry > HEIGHT && asteroid.y > HEIGHT){
					if(rx + rank * 20 < asteroid.x || asteroid.x + asteroid.width < rx){
						//Good
					}
					else{
						Asteroids.splice(i,1); //Just delete overlaping asteroids
					}
				}
			});
		    
			
			var xDirection = 1;
			var yDirection = 1;
			var xSlope = Math.floor(Math.random() * maxSpeed) + 1;
			var ySlope = Math.floor(Math.random() * maxSpeed) + 1;
			
			if(ry < 0 || ry > HEIGHT){
				if(ry < 0) yDirection = 1;
				else yDirection = -1;
			}
			else{
				if(ry <= HEIGHT/2){
					ry = -10; //leftside
					yDirection = 1;
				}
				else{
					ry = HEIGHT + 10; //rightside
					yDirection = -1;
				}
			}
			
			if(rx < 0 || rx > WIDTH){
				if(rx < 0) xDirection = 1; 
				else xDirection = -1;
			}
			else{
				if(rx <= WIDTH/2){
					xDirection = 1;
				}
				else{
					xDirection = -1;
				}
				//y should clamp to a height will x is random 
			}
			createAsteroids(rank,rx,ry,xSlope * xDirection, ySlope * yDirection);
		} //End for loop
}

/** @function playerHit
  * This function is called when the user takes damage in some way.
  */
function playerHit(){
	lives--;
	Bullets = [];
	Asteroids = [];
	alien = null;
	AlienBullets = [];
	PowerUps = [];
	playerSpeed = 0.15;
	bulletSpeed = 0.20;
    player.x = WIDTH/2;
	player.y = HEIGHT/2;
	
	if(wave - 1 > -1) wave = wave -1;
	else wave = 0;
	
	playerHitSFX.play();
	
	paused = true;
	mainButton.disabled = false;
	mainButton.value = "Click to Continue";
}

/** @function update
  * All the games varibles are changed in this function. This manages the game model
  * @param {Float} elapsedTime - Amount of time since last gameLoop iteration.
  */
function update(elapsedTime){
	//Waves
	if(wave == 0){
		generateRandomAsteroids(1,5,1);
		wave = 1;
		//PowerUps.push(new PowerUp(100,100,20,20,"sheild"));
	}
	if(wave == 1 && Asteroids.length == 0){
		generateRandomAsteroids(2,7,2);
		wave = 2;
	}
	if(wave == 2 && Asteroids.length == 0){
		generateRandomAsteroids(2,5,3);
		alien = new Alien(Math.floor(Math.random() * (WIDTH - 50)) + 50, HEIGHT - 1);
		wave = 3;
	}
	
	if(wave == 3 && Asteroids.length == 0){
		generateRandomAsteroids(3,8,3);
		wave = 4;
	}
	if(wave > 3 && Asteroids.length == 0){
		var speed = 3;
		if(wave % 4 == 0){
			alien = new Alien(WIDTH - 1, HEIGHT - 1);
		}
		if(wave > 7) speed = 4;
		wave++;
		generateRandomAsteroids(4,wave,speed);
	}
	//Player Input
	if(currentInput.up){
		player.y -= playerSpeed  * elapsedTime;
	}
	if(currentInput.down){
		player.y += playerSpeed  * elapsedTime;
	}
	if(currentInput.left){
		player.x -= playerSpeed * elapsedTime;
	}
	if(currentInput.right){
		player.x += playerSpeed  * elapsedTime;
	}
	if(currentInput.rotateLeft){
		player.angle -= rotateSpeed;
		
	}
	if(currentInput.rotateRight){
		player.angle += rotateSpeed;
	}
	
	if(currentInput.hitboxes && !priorInput.hitboxes){
		viewHitBoxes = !viewHitBoxes;
	}
	if(currentInput.space && !priorInput.space){
		var angleX = player.radius * Math.cos(player.angle + 89.52) * -1; //Adding 89.5 to angle puts it in right spot
		var angleY = player.radius * Math.sin(player.angle + 89.52) * -1; //Multiplying be -1 makes it go in right direction.
		Bullets.push(new Bullet(player.x + angleX,player.y + angleY,angleX,angleY));
		fireSFX.play();
	}
	if(currentInput.randomSpawn && !priorInput.randomSpawn){
		var check = false;
		while(check == false){
			var ranX = Math.floor(Math.random() * WIDTH);
			var ranY = Math.floor(Math.random() * HEIGHT);
			check = true;
			Asteroids.forEach(function(asteroid){
				if(ranX > asteroid.x && ranX < asteroid.x + asteroid.width){
					if(ranY > asteroid.y && ranY < asteroid.y + asteroid.height){
						check = false;
					}
				}
			});
		}
		player.x = ranX;
		player.y = ranY;
	}
	checkWrap(player);
	
	
	//Player Bullets
	Bullets.forEach(function(bullet,i){
		bullet.x += bullet.xAngle * bulletSpeed;
        bullet.y += bullet.yAngle * bulletSpeed;
        if(bullet.x < 0 || bullet.x > WIDTH || bullet.y < 0 || bullet.y > HEIGHT){
			Bullets.splice(i,1);
		}		
		checkBulletCollision(bullet,i);
		if(alien != null){
			if(bullet.x > alien.x - alien.width && bullet.x < alien.x + alien.width){
				if(bullet.y > alien.y - alien.height && bullet.y < alien.y){
					Bullets.splice(i,1);
					score += 100;
					alien = null;
				}
			}
		}
	});
	
	
	//Asteriod movement and collison
	Asteroids.forEach(function(asteroid,i){
		if(asteroid.x < -200 || asteroid.x > WIDTH + 200 || asteroid.y < -200 || asteroid.y > HEIGHT + 200){
			Asteroids.splice(i,1); //If a bad spawn happens this will delete the asteroid
		}
		
		//Check collision with other asteroids
		Asteroids.forEach(function(asteroid2,i2){
			if(asteroid.x + asteroid.width > asteroid2.x && asteroid.x + asteroid.width < asteroid2.x + asteroid2.width){
				if(asteroid.y + asteroid.height > asteroid2.y && asteroid.y < asteroid2.y + asteroid2.height){
					if(asteroid.mass > -1){
						if(asteroid.mass > asteroid2.mass){
							var d = asteroid.mass - asteroid2.mass;
							createAsteroids(asteroid2.mass - 1, asteroid2.x + asteroid2.width, asteroid2.y + asteroid2.height, asteroid2.xSlope * -1,asteroid2.ySlope * -1);
							createAsteroids(asteroid2.mass - 1, asteroid2.x - asteroid2.width, asteroid2.y - asteroid2.height, asteroid2.xSlope * -1,asteroid2.ySlope * -1);
							asteroid.xSlope = asteroid2.xSlope;
							asteroid.ySlope = asteroid2.ySlope;
							Asteroids.splice(i2,1);
						}
						if(asteroid.mass < asteroid2.mass){
							var d = asteroid2.mass - asteroid.mass;
							createAsteroids(asteroid.mass - 1, asteroid.x + asteroid.width, asteroid.y + asteroid.height, asteroid.xSlope * -1,asteroid.ySlope * -1);
							createAsteroids(asteroid.mass - 1, asteroid.x - asteroid.width, asteroid.y - asteroid.height, asteroid.xSlope * -1,asteroid.ySlope * -1);
							asteroid2.xSlope = asteroid.xSlope
							asteroid2.ySlope = asteroid.ySlope
							Asteroids.splice(i,1);
						}
					}
					if(asteroid.mass == asteroid2.mass){
						var tempX;
						var tempY;
						tempX = asteroid.xSlope;
						tempY = asteroid.ySlope;
						asteroid.xSlope = asteroid2.xSlope;
						asteroid.ySlope = asteroid2.ySlope;
						asteroid2.xSlope = tempX;
						asteroid2.ySlope = tempY;
					}
				}
			}
		});
		
		//Check asteroid collision with player
		if(alien != null){
			if(player.x > alien.x - alien.width && player.x < alien.x + alien.width){
				if(player.y > alien.y - alien.height && player.y < alien.y){
					if(hasSheild){
						hasSheild = false;
						alien.xSlope *= -1;
						alien.ySlope *= -1;
						alien.x += alien.xSlope * 2;
						alien.y += alien.ySlope * 2;
					}
					else{
						playerHit();
					}
				}
			}
		}
		
		if(player.x > asteroid.x && player.x < asteroid.x + asteroid.width && player.y > asteroid.y && player.y < asteroid.y + asteroid.height){
			if(hasSheild){
				asteroid.xSlope *= -1;
				asteroid.ySlope *= -1;
				asteroid.x += asteroid.xSlope * 2;
				asteroid.y += asteroid.ySlope * 2;
				hasSheild = false;
			}
			else{
				playerHit();
			}
		}
		
		asteroid.x += asteroid.xSlope; //Move asteroid
		asteroid.y += asteroid.ySlope; //Move asteroid
		
		if(asteroid.x > 0 && asteroid.x < WIDTH && asteroid.y > 0 && asteroid.y < HEIGHT){
			asteroid.inPlay = true; //This allows asteroids to spawn offscreen and come in.
		}
		if(asteroid.inPlay){
			checkWrap(asteroid);
		}
	});
	
	//Alien Stuff
	if(alien != null){
		if(alien.xSlope == 0){
		alien.xSlope = Math.floor(Math.random() * 3) + 1;
		alien.ySlope = Math.floor(Math.random() * 3) + 1;
		}
		alien.x += alien.xSlope;
		alien.y += alien.ySlope;
		checkWrap(alien);
		
		var r = Math.floor(Math.random() * 75);
		if(r == 1){
			var xSlope = 0;
			var ySlope = 0;
			if(alien.x > player.x){
				xSlope = -4
			}
			else{
				xSlope = 4;
			}
			if(alien.y > player.y){
				ySlope = -4
			}
			else{
				ySlope = 4;
			}
			AlienBullets.push(new AlienBullet(alien.x,alien.y,xSlope,ySlope));
			alienFireSFX.play();
		}
	}
	AlienBullets.forEach(function(bullet,i){
		bullet.x += bullet.xSlope;
		bullet.y += bullet.ySlope;
		if(bullet.x < 0 || bullet.x > WIDTH || bullet.y < 0 || bullet.y > HEIGHT){
			AlienBullets.splice(i,1);
		}
		if(bullet.x < player.x + player.width && bullet.x > player.x - player.width){
			if(bullet.y < player.y + player.height && bullet.y > player.y - player.height){
				AlienBullets.splice(i,1);
				if(hasSheild){
					hasSheild = false;
				}
				else{
				playerHit();
				}				
			}
		}
	});
	
	//PowerUp stuff
	PowerUps.forEach(function(powerup,index){
		if(player.x < powerup.x + powerup.width && player.x > powerup.x && player.y > powerup.y && player.y < powerup.y + powerup.height){
			powerupSFX.play();
			if(powerup.type == "speed"){
				playerSpeed += 0.05;
			}
			if(powerup.type == "fire"){
				bulletSpeed += 0.1;
			}
			if(powerup.type == "sheild"){
				hasSheild = true;
			}
			PowerUps.splice(index,1);
			score += 100;
		}
	});
}

/** @function checkWrap
  * Moves the given object to the opposite side of the screen if it is out of bounds.
  * @param {Object} obj - Whatever object is being looked at to see if its out of bounds.
  */
function checkWrap(obj){
	if(obj.x < -5){
		obj.x = WIDTH - 2;
	}
	if(obj.x > WIDTH + 5){
		obj.x = 2;
	}
	if(obj.y < -5){
		obj.y = HEIGHT - 2;
	}
	if(obj.y > HEIGHT + 5){
		obj.y = 2;
	}
}

/** @function checkBulletCollision
  * Checks if bullet is colliding with asteroid and will break apart or destory asteroid based on mass.
  * Will also randomly generate a powerup when destroying or breaking apart an asteroid. 
  * @param {Bullet} bullet - The bullet that is being looked at.
  * @param {Integer} i - The index of the bullet in the Bullets array.
  */
function checkBulletCollision(bullet,i){
	Asteroids.forEach(function(asteroid,index){
		if(bullet.x < asteroid.x + asteroid.width && bullet.x > asteroid.x && bullet.y < asteroid.y + asteroid.height && bullet.y > asteroid.y){
			if(asteroid.mass > 0){
				createAsteroids(asteroid.mass - 1, asteroid.x + asteroid.width/2, asteroid.y - asteroid.width/2, asteroid.xSlope,asteroid.ySlope);
				createAsteroids(asteroid.mass - 1, asteroid.x - asteroid.height/2, asteroid.y - asteroid.height/2, asteroid.xSlope,asteroid.ySlope);
			}
			Bullets.splice(i,1); //Destroy bullet
			explosionSFX.play();
			var r = Math.floor(Math.random() * 50);
			if(r == 1){
				PowerUps.push(new PowerUp(asteroid.x,asteroid.y,20,20,"speed"));
			}
			if(r == 10){
				PowerUps.push(new PowerUp(asteroid.x,asteroid.y,20,20,"fire"));
			}
			if(r == 25){
				PowerUps.push(new PowerUp(asteroid.x,asteroid.y,20,20,"sheild"));
			}
			Asteroids.splice(index,1); //Destroy asteroid
			score += 10;
		}
	});
}

/** @function render
  * This functions draws all the games sprites to the screen
  * @param {Canvas context} ctx - The canvas context which the game will be rendered to.
  */
function render(ctx){
	ctx.clearRect(0, 0, WIDTH, HEIGHT); //Clear screen before rendering new frame.
    if(lightMode == true){
		ctx.fillStyle = "White"
		ctx.fillRect(0,0,WIDTH,HEIGHT);
		ctx.strokeStyle = "Black";
	}
	else{
		ctx.fillStyle = "Black"
		ctx.fillRect(0,0,WIDTH,HEIGHT);
		ctx.strokeStyle = "White";
	}
	if(lives == 0){
		ctx.font = "24px Arial";
		ctx.fillStyle = "Red";
		ctx.fillText("Game Over", WIDTH/2 - 30, HEIGHT/2);
		ctx.fillText("Score:" + score, WIDTH/2 - 30, HEIGHT/2 + 30);
	}
	else if(!gameStarted || checkingRules){
	//How to play
	ctx.font = "16px Arial";
	var y = 150;
	if(lightMode)ctx.fillStyle = "Black";
	else ctx.fillStyle = "White";
	ctx.fillText("Use wasd to move ship, w up, a down, s left, d right",WIDTH/3,10 + y);
	ctx.fillText("The left arrow key rotates the ship left",WIDTH/3,30 + y);
	ctx.fillText("The right arrow key rotates that ship right",WIDTH/3,50 + y);
	ctx.fillText("Press the space bar or up arrow key to fire a bullet",WIDTH/3,70 + y);
	ctx.fillText("Press the L key to flip the color scheme", WIDTH/3,90 + y);
	ctx.fillText("Press the R key to teleport the ship to a random position",WIDTH/3,110 + y);
	ctx.fillText("Green powerups increase your movement speed",WIDTH/3,130 + y);
	ctx.fillText("Orange powerups increase how fast your bullets move", WIDTH/3,150 + y);
	ctx.fillText("Blue powerups give you a sheild which will protect you from damage",WIDTH/3,170 + y);
	ctx.fillText("Press the M or esc key during gameplay to see these instructions at any time", WIDTH/3, 190 + y);
	if(!gameStarted) ctx.fillText("Click button below to start game!", WIDTH/3, 230 + y);
	}
	else if(lives > 0 && paused){
		ctx.fillStyle = "Blue";
		ctx.font = "24px Arial";
		ctx.fillText("You got hit",WIDTH/2 - 60, HEIGHT/2 - 30);
		ctx.fillText("Lives left: " + lives,WIDTH/2 - 60, HEIGHT/2);
		ctx.fillText("Press C or click continue at bottom of screen", WIDTH/2 - 200,HEIGHT/2 + 60);
	}
	else if(lives > 0){
		ctx.font = "16px Arial";
		ctx.save();
		ctx.translate(player.x,player.y);
		ctx.rotate(player.angle);
		if(hasSheild) ctx.strokeStyle = "Blue";
		ctx.beginPath();
		ctx.moveTo(player.vertices.x1, player.vertices.y1);
		ctx.lineTo(player.vertices.x2, player.vertices.y2);
		ctx.lineTo(player.vertices.x3, player.vertices.y3);
		ctx.closePath();
		ctx.stroke();
		ctx.restore();

		Asteroids.forEach(function(asteroid){
			if(lightMode)ctx.strokeStyle = "Black";
			else ctx.strokeStyle = "White";
			ctx.beginPath();
			ctx.moveTo(asteroid.vertices[0].x + asteroid.x,asteroid.vertices[0].y + asteroid.y);
			asteroid.vertices.forEach(function(a,index){
				ctx.lineTo(asteroid.vertices[index].x + asteroid.x,asteroid.vertices[index].y + asteroid.y);
			});
			ctx.closePath();
			ctx.stroke();
			if(viewHitBoxes == true){
				ctx.strokeStyle = "Red";
				ctx.beginPath();
				ctx.moveTo(asteroid.x,asteroid.y);
				ctx.lineTo(asteroid.x + asteroid.width, asteroid.y);
				ctx.lineTo(asteroid.x + asteroid.width, asteroid.y + asteroid.height);
				ctx.lineTo(asteroid.x, asteroid.y + asteroid.height);
				ctx.closePath();
				ctx.stroke();
				ctx.beginPath();
				ctx.moveTo(player.x - player.width, player.y);
				ctx.lineTo(player.x + player.width, player.y);
				ctx.lineTo(player.x + player.width, player.y + player.height);
				ctx.lineTo(player.x - player.width, player.y + player.height);
				ctx.closePath();
				ctx.stroke();
				
				if(alien != null){
					ctx.beginPath();
					ctx.moveTo(alien.x + alien.width, alien.y);
					ctx.lineTo(alien.x - alien.width, alien.y);
					ctx.lineTo(alien.x - alien.width, alien.y - alien.height);
					ctx.lineTo(alien.x + alien.width, alien.y - alien.height);
					ctx.closePath();
					ctx.stroke();
				}
			}
		});
		
		
		Bullets.forEach(function(bullet,index){
			if(lightMode == true) ctx.fillStyle = "Black";
			else ctx.fillStyle = "White"; //dark mode
			ctx.fillRect(bullet.x,bullet.y,4,4);
		});
		ctx.font = "16px Arial";
		if(lightMode == true){
			ctx.strokeStyle = "Black";
			ctx.fillStyle = "Black";
		}
		else{
			ctx.strokeStyle = "White";
			ctx.fillStyle = "White";
		}
		
		PowerUps.forEach(function(powerup){
			if(powerup.type == "speed"){
			ctx.fillStyle = "Green";
			}
			if(powerup.type == "fire"){
			ctx.fillStyle = "Orange";
			}
			if(powerup.type == "sheild"){
			ctx.fillStyle = "Blue";
			}
			ctx.fillRect(powerup.x,powerup.y,powerup.width,powerup.height);
		});
		
		if(lightMode) {
			ctx.strokeStyle = "Black";
			ctx.fillStyle = "Black";
		}
		else{
			ctx.strokeStyle = "White";
			ctx.fillStyle = "White";
		}
		if(alien != null){
			ctx.beginPath();
			ctx.moveTo(alien.vertices[0].x + alien.x,alien.vertices[0].y + alien.y);
			for(var i = 1; i < 9; i++){
				ctx.lineTo(alien.vertices[i].x + alien.x, alien.vertices[i].y + alien.y);
			}
			
			AlienBullets.forEach(function(bullet,i){
				ctx.fillStyle = "Purple";
				ctx.fillRect(bullet.x,bullet.y,5,5);
			});
			
			ctx.closePath();
			ctx.stroke();
		}
		
		if(lightMode) {
			ctx.strokeStyle = "Black";
			ctx.fillStyle = "Black";
		}
		else{
			ctx.strokeStyle = "White";
			ctx.fillStyle = "White";
		}
		
		ctx.fillText("Lives: ",10,15);
		var x = 60;
		for(var i = 0; i < lives; i++){
			ctx.beginPath()
			ctx.moveTo(x,15);
			ctx.lineTo(x+7,15);
			ctx.lineTo(x+4,5);
			ctx.closePath();
			ctx.stroke();
			x += 10;
		}
		ctx.fillText("Score: " + score, WIDTH-120, 15);
		ctx.fillText("Level: " + wave, WIDTH/2 - 20, 15);
	}
	else{ 
	
	}
}
//Starts the gameLoop
window.requestAnimationFrame(gameLoop);