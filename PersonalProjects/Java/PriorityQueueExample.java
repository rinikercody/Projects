/* Author: Cody Riniker */
import java.util.*;

/*
 This program ask the user to give a word and a priority then the user can see the words organized by priority.
*/
public class PriorityQueueExample{
	public static void main(String [] args){
		Scanner s = new Scanner(System.in);
		
		//Used to organize words based on lowest integer value
		PriorityQueue<Integer> queue = new PriorityQueue<Integer>();
		
		//Used to lookup words based on integer value given
		Map<Integer,String> dictonary = new HashMap<Integer,String>();
		
		while(true){
			try{
				System.out.print("Enter in a word: ");
				String word = s.nextLine();
				if(word.equals("q")) break; //Exit loop
				
				System.out.print("What is it priority(0 = Highest): "); 
				String priority = s.nextLine();
				int p = Integer.parseInt(priority);
				if(!dictonary.containsKey(p)){ //If priority is not already assigned
					queue.add(p); //Add priority to queue 
					dictonary.put(p, word); //Add (priority,word) to dictonary
				}
				else{
					System.out.println("There is already a word with that priority.");
				}
			}
			catch(NumberFormatException ex){
				System.out.println("Input given was not a number");
			}
		}
		
		//Prints out words based on priority
		while(queue.size() > 0){ 	
			int p = queue.poll();
			System.out.println(p + " " + dictonary.get(p));
		}
	}
}