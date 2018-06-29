//Array that holds the paths to the pictures along with their html id.
var pics = [
	['images/Bell.jpg','small-pic-bell'],
	['images/Lilly.jpg','small-pic-lilly'],
	['images/Kirby.jpg','small-pic-kirby'],
	['images/Petals.jpg','small-pic-petals']
];

//The currently displayed picture's address in the array.
var x = 0;

//Controls speed of the chaning display picture.
var interval;

/* function displayNextImage
 * Description: increments or resets x value to zero based on the number of pictures.
 */ 
function changePic() {
	if(x === pics.length -1){ //Reset
		x = 0;
	}
	else{ //Move to next pic
		x++;
	}
	//Update display picture
	updateDisplay(x);
}

/* function startTimer
 * Description: Starts a timer that will change the display picture every few seconds.
 */
function startTimer() {
    interval = setInterval(changePic, 2000);
	setInterval(updateDisplay, interval);
}

/* function updateDisplay
 * Description: Changes text below picture based on current picture
 */
function updateDisplay(index){
	document.getElementById('pic-box').src = pics[index][0];
	document.getElementById('text-box').innerHTML = pics[index][0].split('/')[1].split('.')[0];
	adjustSelected(index);
}

/* function adjustSelected
 * Description: fades out the picture that the user is currently looking at.
 * Param: index - Integer index - The index of the picture being displayed.
 */
function adjustSelected(index){
	for(var i=0; i < pics.length; i++){
		if(index == i){
			document.getElementById(pics[i][1]).style.opacity = '0.7';
		}
		else{
			document.getElementById(pics[i][1]).style.opacity = '1';
		}
	}
}


/* function stop
 * Description: Clears out the interval and stops the picture from changing.
 */
function stop() {
    clearInterval(interval);
}

/* JQuery function
 * Description: Handles the hover event for the main display picture. When hovered over the picture currently
	            displayed will not change. When the mouse is moved then the picture will start to change again.
 */
$(function () {
    startTimer();
    $('#pic-box').hover(function () {
        stop();
    }, function () {
        startTimer();
    })
});

/* JQuery function
 * Description: Handles the hover event for all the small pictures on the bottom by changing the main picture
	            and fading out the small picture. When user moves mouse off small pictures then main picture
				will start to automatically move again.
 */
$(function () {
    $(".small-pics").hover(function () {	
		for(var i=0; i < pics.length; i++){
			if(pics[i][1] == this.id){
				updateDisplay(i);
				break;
			}
		}
		stop();
    }, function () {
        startTimer();
    })
});

