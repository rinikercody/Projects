public class UserAccount{
	private String _username;
	private double _balance;
	
	public UserAccount(String username){
		_username = username;
		_balance = 0;
	}
	
	public void depositFunds(double amount){
		_balance += amount;
	}
	
	public boolean withdrawFunds(double amount){
		if(_balance - amount >= 0){
			_balance -= amount;
			return true;
		}
		return false;
	}
	
	public double getBalance(){
		return _balance;
	}
}