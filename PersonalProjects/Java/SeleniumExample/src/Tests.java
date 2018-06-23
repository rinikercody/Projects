/* Author: Cody Riniker */
/* This program uses a Selenium webdriver and Google Chrome to perform a comparison on two strings
   after a few commands have been executed. 
 */
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.By;

public class Tests{
	public static void main(String[] args){
		System.setProperty("webdriver.chrome.driver","chromedriver.exe");
		WebDriver driver = new ChromeDriver();
		
		driver.get("localhost:3000"); //Port can changed in server.js file
		System.out.println(driver.getTitle());
		
		driver.findElement(By.linkText("Page2")).click();
		driver.findElement(By.id("text1")).sendKeys("World!");
		
		Boolean check = driver.findElement(By.id("text1")).getAttribute("value").equals("HelloWorld!");
		if(check) System.out.println("Passed");
		else System.out.println("Failed");
		assert(check); //in case assertion checking is enabled.
		driver.close(); //close window
	}
}
