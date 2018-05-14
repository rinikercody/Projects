/* function executeOperation
 * Description: Performs a simple mathimatical operation on do numbers in textareas.
 * Param: String operation - The mathimatical operation to perform.
 */
function executeOperation(operation){
	var num1 = Number(document.getElementById('box1').value);
	var num2 = Number(document.getElementById('box2').value);
	var answer = 0;
	switch(operation){
		case 'addition':
			answer = num1 + num2;
			break;
		case 'subtraction':
			answer = num1 - num2;
			break;
		case 'multiplication':
			answer = num1 * num2;
			break;
		case 'division':
			answer = num1 / num2;
			break;
		default:
			answer = 0;
	}
	
	var ans = answer.toString();
	document.getElementById('answer').innerHTML = ans;
	if(ans !== 'NaN' && ans !== 'Infinity' && ans !== '-Infinity'){
		document.getElementById('box1').value = ans; //Put new answer in first text box so user can use it.
		document.getElementById('box2').value = ''; //Clear out box2.
	}
	//Else leave numbers in textareas so user can edit them.
}