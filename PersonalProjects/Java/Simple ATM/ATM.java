import java.util.*;
import java.io.*;

public class ATM{
	private static UserAccount _user = new UserAccount("DefaultUser");
	
	public static void main(String [] args){
		Scanner s = new Scanner(System.in);
		int choice = 0;
		String str = "";
		while(true){
			System.out.println("Select an option");
			System.out.println("1. Deposit");
			System.out.println("2. Withdraw");
			System.out.println("3. View Balance");
			System.out.println("4. Exit");
			System.out.print("Select(1-4): ");
			
			str = s.nextLine();
			
			try{
				choice = Integer.parseInt(str);
			}
			catch(NumberFormatException ex){
				System.out.println("Invalid selection please enter a number between 1 and 6");
			}
			
			double amount = 0;
			switch(choice){
				case 1:
					System.out.print("Enter in deposit amount: ");
					str = s.nextLine();
					try{
						amount = Double.parseDouble(str);
						_user.depositFunds(amount);
					}
					catch(NumberFormatException ex){
						System.out.println("Please Enter a dollar amount");
					}
					break;
				case 2:
					System.out.print("Enter in withdrawl amount: ");
					str = s.nextLine();
					try{
						amount = Double.parseDouble(str);
						if(_user.withdrawFunds(amount)){
							System.out.println("Withdrawl successful");
						}
						else{
							System.out.println("Insufficient funds");
						}
					}
					catch(NumberFormatException ex){
						System.out.println("Please Enter a dollar amount");
					}
					break;
				case 3:
					System.out.print("Current balance: $");
					System.out.printf("%.2f\n", _user.getBalance());
					break;
				case 4:
					System.exit(0);
				default:
					break;
			}
		}
	}
}//End class