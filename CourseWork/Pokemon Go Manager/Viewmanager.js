/*Viewmanage.js
 *Creates interactive views for the web app.
 */
module.exports ={
	makeTeamForm: makeTeamForm,
	makeEvoForm: makeEvoForm,
	makeTeamSelectForm: makeTeamSelectForm,
	loginErrorForm: loginErrorForm
};

/** @function makePostFrom
  * Makes a genral post request form that is used to handle button presses.
  * @return {String} html - html/javascript that is used to handle a button press.
  */
function makePostForm(){
	var html = "<script>"
    html += "function proceed(info) { "
    html += "  var form = document.createElement('form');" 
    html += "  form.setAttribute('method', 'post');" 
    html += "  form.setAttribute('action', 'AddRequest');" 
    html += "  var field = document.createElement('input');" 
    html += "  field.setAttribute('type', 'hidden');" 
    html += "  field.setAttribute('name', 'info');" 
    html += "  field.setAttribute('value', info);" 
    html += "  form.style.display = 'hidden';" 
    html += "  form.appendChild(field); "
    html += "  document.body.appendChild(form);" 
    html += "  form.submit();" 
    html += "}"
    html += "</script>"
	return html;
}

/** @function makeMenu
  * Generates the main menu strip at the top of every page.
  * @return {String} html - The html that will make the menu strip
  */
function makeMenu(){
	var html =  "<div id='main_menu'>"  
	    html += " <ul>"
		html += "  <li><a href='/index'>Home</a></li>";
	    html += "  <li><a href='/PokemonList'>Pokemon</a></li>";
        html += "  <li><a href='/ManageTeams'>Teams</a></li>";
		html += "  <li><a href='/ManageEvolutions'>Evolutions</a></li>";
		html += " </ul>"
		html += "</div>"
	return html;
}

/** @function makeTeamSelectForm
  * Creates a page that conatains all the users current teams they have created.
  * @param {Array} info - This is string array that contains the name of each of the users teams.
  * @return {String} html - The html page with all the user teams
  */
function makeTeamSelectForm(info){
	var html =  "<!doctype html>"
		html += " <html>"
		html += makePostForm();
        html += "  <head>"
	    html += "   <title>Team Select</title>"
        html += "   <link type='text/css' rel='stylesheet' href='css/TeamStyleSheet.css'/>" // This might need to change latter
        html += "  </head>"
        html += "  <body>"
		html += makeMenu();
		html += "   <div id='mts'>"
		html += "    <ul>"
		
		for(var i = 1; i < info.length; i++){
			var url = info[i].split(':')[1];
			if(url != 'empty'){
			html += "<p><li>" + info[i].split(':')[1] + "</li></a></p>";
			var str = "proceed('ST" + url + "');"
			html += "<button type='button' onclick=" + str + ">View</button>"
			str = "proceed('DT" + i + info[i].split(':')[1] + "');"
			html += "<button type='button' onclick=" + str + ">Remove</button>"
			}
		}
		
		html += "     </ul>"
		html += "   </div>"
		html += "   <form method='POST' id='add-team-form'>"
		html += "    <fieldset>"
		html += "     <label for='new_team'>New Team</label>"
		html += "     <input type='text' name='new_team'/>"
		html += "    </fieldset>"
		html += "    <input type='submit' value='Add'/>"
		html += "   </form>"
		
		html += "  </body>"
		html += " </html>"
		return html;
}

/** @function makeTeamForm
  * Loads all the pokemon on a team and displays them to the user.
  * @param {Array} info - This is an array of strings which contain the id number of each pokemon on a team.
  * @return {String} html - The html page with all the pokemon and information.
  */
function makeTeamForm(info,poke_info){
  var html = " <!doctype html>"
      html += makePostForm();
	  html += " <html>"
      html += "  <head>"
	  html += "   <title>Team</title>"
      html += "   <link type='text/css' rel='stylesheet' href='css/TeamStyleSheet.css'/>" // This might need to change latter
      html += "  </head>"
      html += "  <body>"
	  html += makeMenu();
	  console.log(info[1] + " " + info[2]);
	  html += "   <h1>" + info[0].split(':')[1] + "</h1>" 
	  html += "    <div id='team-list'>"
	  html += "     <ul>"
	  var total_cp = 0;
      for(var i = 1; i < 7; i++){
		var poke = info[i].toString().split(':')[1]
		if(poke != 0){
			total_cp += Number(poke_info[poke].split(',')[2]);
			html += "       <li><img src='Pokemon/" + poke + ".png'/>"
			html += "       <text>" + poke_info[poke].split(',')[0] + "," + poke_info[poke].split(',')[1] + "," 
			+ poke_info[poke].split(',')[2] + "</text>"
			var str = "proceed('RT" + i + "');"
			html += "        <button type='button' onclick=" + str + ">Remove</button>"
			html += "       </li>"
		}
      }
	  html += "     </ul>"
	  html += "     <div id='team-totals'>"
	  html += "      <text>Teams total CP is: " + total_cp + "</text>"
	  html += "     </div>"
	  html += "   </div>"
      html += "  </body>"
      html += " </html>"
      return html;
}

/** @function makeEvoForm
  * Creates a page with all the pokemon in a users evolution list
  * @param {Array} info - An array of strings that contain the id numbers of pokemon in the evolution list.
  * @return {String} html - The html form with all the pokemon in a users evolution list.
  */
function makeEvoForm(info,poke_info){
	  var html = "<!doctype html>"
	  html += "  <html>"
	  html += makePostForm();
      html += "   <head>"
	  html += "    <title>Evolutions</title>"
      html += "    <link type='text/css' rel='stylesheet' href='css/TeamStyleSheet.css'/>"
      html += "   </head>"
      html += "   <body>"
	  html += makeMenu();
	  html += "    <h1>Pokemon to Evolve</h1>"
	  html += "     <div id='evo-list'>"
	  html += "      <ul>"
      for(var i = 0; i < info.length; i++){
		var poke = info[i].toString().split(':')[1];
		if(poke != 0){
			var str = "proceed('RE" + (i+1) + "');"
			html += "       <button type='button' onclick=" + str + ">Remove</button>"
			html += "      <li><img src='Pokemon/" + poke + ".png'/>"
			var evolution = Number(poke)+1;
			html += "       <text>"+ poke_info[poke].split(',')[0];
			html += " evolves into " + poke_info[evolution].split(',')[0] + "</text>";
			html += "      <li><img src='Pokemon/" + evolution + ".png'/>"
			html += "<text> with " + poke_info[poke].split(',')[3] + "candies</text>";
			html += "      </li>"
			html += "<br></br>"
		}
      }
	  html += "     </ul>"
	  html += "    </div>"
      html += "    </body>"
      html += "   </html>"
      return html;
}

/** @function loginErrorForm
  * Displays appropriate error message to user based on login attempt
  * @param {int} type - 1 if bad login attempt and -1 is new user error. 
  * @return {String} html - An html page with the appropraite error message.
  */
function loginErrorForm(type){
	var html = "<!doctype html>"
	  html += "  <html>"
	  html += makePostForm();
      html += "   <head>"
	  html += "    <title>Login Error</title>"
      html += "    <link type='text/css' rel='stylesheet' href='css/TeamStyleSheet.css'/>"
      html += "   </head>"
      html += "   <body>"
	  html += makeMenu();
	  html += "<div id=alert>"
	  if(type == -1){
		  html += "    <h1>Error, Username is already being used.</h1>"
	  }
	  else if(type == -2){
		  html += "    <h1>Error, Username and password did not match.</h1>"
	  }
	  else{
		  html += "    <h1>Please avoid using spaces in username or password</h1>"
	  }
	  html += "<ul>"
	  html += "<li><a href='index'>Home</a></li>"
	  html += "<li><a href='LogInPage'>Retry</a></li>"
	  html += "</ul>"
	  html += "</div>"
      html += "   </body>"
      html += "   </html>"
      return html;
}