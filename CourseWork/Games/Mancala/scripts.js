//Models representation of the game board
var board = [];

//The current players turn, 1 or 2
var playersTurn;

//Used to reset game
var gameOver = false;

//This starts a game when the window is loaded
window.onload = function(){
	startGame();
};

/** @function pitClicked
  * Called if user clicks on either goal pit and displays a invalid move message
*/
function pitClicked(){
	document.getElementById('message').innerHTML = "You can't select a player's goal";
}

/** @function setUp
  * Fills the board with pebbles. Used for starting and reseting games.
  */
function setUp(){
	gameOver = false;
	board[0] = 0; //This is player1's pit
	
	for(var i=1; i < 14; i++){
		board[i] = 4; //Fill each cup on the board with pebbles
	}
	
	board[7] = 0; //This is player2's pit
	
	document.getElementById('message').innerHTML = "";
	document.getElementById('reset-message').innerHTML = "";
	
	playersTurn = 1; //Player 1 goes first
}


/** @function startGame
  * Calls setUp to start a new game then updates the board
*/
function startGame(){
	setUp(); //Set up board
	updateDisplay(); //Update display so user sees pebbles
}

/** @function updateDisplay
  * Fills the board onscreen with pebbles based on the board model
*/
function updateDisplay(){
	
	var str;
	
	//Update score in UI
	document.getElementById('player1-text').innerHTML = "Player 1's score: " + board[0]; 
	document.getElementById('player2-text').innerHTML = " Player 2's score: " + board[7]; 
	
	//Update turn in UI
	var turnLabel = document.getElementById('turn-label');
	if(playersTurn == 1){
		turnLabel.innerHTML = "Player 1's turn";
		turnLabel.style.color = "Red";
	}
	else if(playersTurn == 2){
		turnLabel.innerHTML = "Player 2's turn";
		turnLabel.style.color = "Blue";
	}
	else{ //For startup
		str = "Mancala";
	}
	
	
	
	//Clear out cups before updating
	for(var i = 0; i < 14; i++){
		var id = "";
		if(i == 0){
			id = "player1-pit";
		}
		else if(i == 7){
			id = "player2-pit";
		}
		else{
			id = "cup" + i;
		}
		document.getElementById(id).innerHTML = "";
	}
	
	//Update board to show pebbles based on model
	for(var i = 0; i < 14; i++){
		var id = "";
		
		//Get id of cup based on i
		if(i == 0){
			id = 'player1-pit';
		}
		else if(i == 7){
			id = 'player2-pit';
		}
		else{
			id = "cup" + i;
		}
		var cup = document.getElementById(id.toString());
		
		//This break is used for spacing
		var br = document.createElement('br');
		//cup.appendChild(br);
		
		var toBig = false;
		
		//Place pebbles in cup
		for(var j=0; j < board[i]; j++){
			if(i == 0 || i == 7){ 
				if(board[i] > 18){
					cup.innerHTML = board[i];
					var pebble = document.createElement('span');
					pebble.classList.add("pebble");
					cup.appendChild(pebble);
					cup.innerHTML += "'s";
					toBig = true;
				}
			}
			else{
				//Stop showing pebbles and start showing number when the number of pebbles gets bigger
				if(board[i] > 9){
					cup.innerHTML = board[i];
					var pebble = document.createElement('span');
					pebble.classList.add("pebble");
					cup.appendChild(pebble);
					cup.innerHTML += "'s";
					toBig = true;
				}
			}
			
			//If the number of pebbles is still small then render pebbles
			if(toBig == false){
				var pebble = document.createElement('div');
				pebble.classList.add("pebble");
				cup.appendChild(pebble);
			}
		}//End inner for loop
	} //End outer for loop
}

/** @function checkWin
  * Used to check if either side of the board is empty
  * @return {Integer} - The winning players number, 1 or 2
*/
function checkWin(){
	var i = 1;
	var check = true;
	var check2 = true;
	
	//Check if top row is empty
	for(i = 1; i < 7; i++){
		if(board[i] != 0){
			check = false;
		}
	}
	//Check if bottom row is empty
	for(i = 8; i < 14; i++){
		if(board[i] != 0){
			check2 = false;
		}
	}
	
	//Total pebbles from opponents side
	var winningAmount = 0;
	
	//Player 1 gets all the pebbles on their side of the board.
	if(check2){
		for(i = 1; i < 7; i++){
			winningAmount += board[i];
			board[i] = 0;
		}
		board[0] += winningAmount;
	}
	
	//Player 2 gets all the pebbles on their side of the board.
	if(check){
		for(i = 8; i < 14; i++){
			winningAmount += board[i];
			board[i] = 0;
		}
		board[7] += winningAmount;
	}
	
	if(check || check2){
		//Determine the games winner based on points.
		if(board[0] > board[7]){
				return 1; 
		}
		else if(board[0] < board[7]){
				return 2;
		}
		else{ //For when the players scores are equal at end
			if(check2) return 3; //player 2's side was empty so player1 wins in the case of a draw.
			if(check) return 4; //player 1's side was empty so player2 wins in the case of a draw.
		}
	}
	return 0; //Game is not over yet so there is no winner.
}

/** @function pickCup
  * Called when user clicks on a cup. Checks if the cup the user picked is valid and if it is makeMove is called.
  * @param {Integer} num - The index of the cup that was clicked.
  */
function pickCup(num){
	if(!gameOver){
		var message = document.getElementById('message');
		if(playersTurn == 1){
			if(num > 6){
				message.innerHTML = "You can't use player2's cups";
				return;
			}
			message.innerHTML = "";
		}
		else{
			if(num < 7){
				message.innerHTML = "You can't use player1's cups";
				return;
			}
			message.innerHTML = "";
		}
		makeMove(num);
	}
}

/** @function makeMove
  * Performs the moving of pebbles in the model.
  * @param {Integer} num - The index of the cup that was clicked.
  */
function makeMove(num){
	//Stop if empty cup was selected
	if(board[num] == 0){
		document.getElementById('message').innerHTML = "The cup you selected has nothing in it.";
		return;
	}
		
	var moves = board[num]; //Pick up pebbles 
	board[num] = 0; //Clear out cup
	
	//While the user has more pebbles to place in cups.
	while(moves > 0){
		
		num--; //place pebbles in cups going counter clockwise
			
		if(num < 0){
			num = 13; //Flip to 13 after 0 because the board is set up that way.
		}
				
		//Place pebble in a cup
		if(num == 0){
			//Player1 can use this cup but player2 can't.
			if(playersTurn == 1){
				board[0]++;
			}
			else{ //Player 2
				num = 13; //Skip player1's pit
				board[13]++;
			}
		}
		else if(num == 7){
			//Player2 can use this cup but player1 can't
			if(playersTurn == 2){
				board[7]++;
			}
			else{ //Player 1
				board[6]++; //Skip player2's pit
			}
		}
		else{
			board[num]++; //Add a pebble to the cup
		}
			
		moves--; //remove pebble from hand after being places in cup
				
		if(moves == 0){ //For when players drops last pebble in cup
			
			//The last cup the player placed a pebble in was empty before they put a pebble in it.
			if(board[num] == 1){
				//If it's player1's turn and the cup was on there side
				if(num < 7 && num > 0 && playersTurn == 1){
					
					//Get the pebbles the player2 has on the opposite side of the board.
					var opponentsPebbles = board[13-(num-1)];
					
					//If there is pebbles in the cup then take them. If there are no pebbles then nothing is done.
					if(opponentsPebbles > 0){
						board[13-(num-1)] = 0;
						board[num] = 0;
						board[0] += opponentsPebbles + 1; //The +1 is for player1's pebbles which is added to the score too.
					}
				}
				//If it's player2's turn and they land on an empty cup on their side.
				if(num > 7 && playersTurn == 2){
					var opponentsPebbles;
					var opponentNum; //The number of the cup on the opposite side of the board.
					
					//This just gets player1's cup on the opposite side of the board. 
					if(num == 13) opponentNum = 1; 
					if(num == 12) opponentNum = 2;
					if(num == 11) opponentNum = 3;
					if(num == 10) opponentNum = 4;
					if(num == 9)  opponentNum = 5;
					if(num == 8)  opponentNum = 6;
					
					//Get the pebbles the player1 has on the opposite side of the board.
					opponentsPebbles = board[opponentNum]
					
					//If there is pebbles in the cup then take them. If there are no pebbles then nothing is done.
					if(opponentsPebbles > 0){
						board[opponentNum] = 0;
						board[num] = 0;
						board[7] += opponentsPebbles + 1;
					}
				}
			}
		}
	}//End while
	
	//See if either side of the board is completly empty.
	var winner = checkWin();
	
	//If either side of the board is empty then there is a winner
	if(winner > 0){
		var message = document.getElementById('message');
		if(winner == 1){
			message.style.color = "Red";
			message.innerHTML = "Player 1 Wins!";
		}
		else if(winner == 2){ //Player2
			message.style.color = "Blue";
			message.innerHTML = "Player 2 Wins!";
		}
		else{ //Same amount of points so its a draw
			message.style.color = "Green";
			message.innerHTML = "Draw! Both players have the same amount of points.";
		}
		
		document.getElementById('reset-message').innerHTML += "Click Reset to play again";
		gameOver = true; //Stops click event from working
		updateDisplay();
		return; //The game is over.
	}
	
	//The games not over
	
	//Switch players turn based on landing postion
	if(playersTurn == 1){
		//If the player lands in their own goal then they get to go again.
		if(num == 0) playersTurn = 1;
		else playersTurn = 2;
	}
	else { //Player2
		if(num == 7) playersTurn = 2
		else playersTurn = 1;
	}
	
	updateDisplay(); //Update pebble on view
}
