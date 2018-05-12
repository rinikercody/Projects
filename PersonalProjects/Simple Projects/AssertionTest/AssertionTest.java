/* Author: Cody Riniker */
import java.util.*;
import java.io.*;

/* Assertion Test
 * A simple program that checks whether strings contained in TestCased.txt have matching opening and closing parenthesis
 */
public class AssertionTest{
	public static void main(String [] args){
		
		String filepath = "TestCases.txt";
		String line = null;
		
		try{
			FileReader fr = new FileReader(filepath);
			BufferedReader br = new BufferedReader(fr); //Wrap fileReader in a bufferedReader.
			
			while((line = br.readLine()) != null){
				assert(matchParenthesis(line)); //If parenthesis don't match then this will fail.
				if(matchParenthesis(line)){
					System.out.println("Pass: " + line);
				}
				else{
					System.out.println("Fail: " + line);
				}
			}
			br.close(); //Close bufferedReader
		}
		catch(IOException ex){
			System.out.println("An error occured. File could not be read in.");
		}
	}
	
	/* matchParenthesis
	 * Determines if given String has an opening parenthesis corrisponding to the each closing parenthesis.
	 * Param: line is a String of text from the given text file
	 * Return: True if paraenthesis match and false if not.
	 */
	private static Boolean matchParenthesis(String line){
		char [] arr = line.toCharArray(); //Split line into an array of chars to be iterated through.
		Stack<Character> openParenthesis = new Stack<Character>();//Used to compare opening and closing parenthesis
		int count = 0; //Used to keep track of number of elements in stack, used like openParenthesis.size().
		
		for(int i = 0; i < arr.length; i++){
			if(arr[i] == '(' || arr[i] == '[' || arr[i] == '{'){ //opening parenthesis are pushed onto the stack
				openParenthesis.push(arr[i]);
				count++;
			}
			else if(arr[i] == ')'){ //Checks if last character pushed on the stack was it matching parenthesis
				//Have to check if count > 0 to make sure stack isn't empty before popping.
				if(count > 0 && '(' != openParenthesis.pop()) return false;
				else count--;
			}
			else if(arr[i] == ']'){
				if(count > 0 && '[' != openParenthesis.pop()) return false;
				else count--;
			}
			else if(arr[i] == '}'){
				if(count > 0 && '{' != openParenthesis.pop()) return false;
				else count--;
			}
			else{
					//Do nothing, just to handle characters like numbers and letters.
			}
		}
		
		if(count == 0) return true; //If stack is empty then all parenthesis have been matched
		else return false; //A parenthesis was left unmatched
	}
}