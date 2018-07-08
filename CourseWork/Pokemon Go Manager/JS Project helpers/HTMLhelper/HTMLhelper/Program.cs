using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HTMLhelper
{
    class Program
    {
        private static string _readpath = "C:\\Users\\rinik\\Desktop\\JS Project\\PokemonData.txt";
        private static string _writepath = "C:\\Users\\rinik\\Desktop\\JS Project\\views\\PokemonList"; //or 3d
        private static string _gif_path = "C:\\Users\\rinik\\Desktop\\JS Project helpers\\PGifs";

        static void Main(string[] args)
        {
            Update3DPokemon();
            int current_start = 252;
            List<string> arr = new List<string>();
            using (StreamReader sr = new StreamReader(_readpath))
            {
                while (!sr.EndOfStream)
                {
                    arr.Add(sr.ReadLine().Split('.')[1]);
                    current_start++;
                }
            }

            int tvt = 0;
            while (tvt < 2) {

                if (tvt == 0)
                {
                    _writepath = "C:\\Users\\rinik\\Desktop\\JS Project\\views\\PokemonList.html";
                }
                else
                {
                    _writepath = "C:\\Users\\rinik\\Desktop\\JS Project\\views\\PokemonList3D.html"; //or 3d
                }
            
                using (StreamWriter sw = new StreamWriter(_writepath))
                {

                    sw.WriteLine("<!doctype>");
                    sw.WriteLine(" <script>");
                    sw.WriteLine("    function proceed(info) { ");
                    sw.WriteLine("      var form = document.createElement(\"form\"); ");
                    sw.WriteLine("      form.setAttribute(\"method\", \"post\"); ");
                    sw.WriteLine("      form.setAttribute(\"action\", \"AddRequest\"); ");

                    sw.WriteLine("      var field = document.createElement(\"input\"); ");
                    sw.WriteLine("      field.setAttribute(\"type\", \"hidden\"); ");
                    sw.WriteLine("      field.setAttribute(\"name\", \"info\"); ");
                    sw.WriteLine("      field.setAttribute(\"value\", info); ");

                    sw.WriteLine("      form.style.display = \"hidden\"; ");
                    sw.WriteLine("      form.appendChild(field); ");
                    sw.WriteLine("      document.body.appendChild(form); ");
                    sw.WriteLine("      form.submit(); ");
                    sw.WriteLine("    }");
                    sw.WriteLine(" </script>");



                    sw.WriteLine(" <html>");
                    sw.WriteLine("  <head>");
                    sw.WriteLine("   <title>Pokemon List</title>");
                    sw.WriteLine("   <link rel=\"stylesheet\" type=\"text/css\" href=\"css/PokemonListStyleSheet.css\">");
                    sw.WriteLine("  </head>");
                    sw.WriteLine("  <body>");


                    sw.WriteLine("<div id='main_menu'>");
                    sw.WriteLine(" <ul>");
                    sw.WriteLine("  <li><a href='/index'>Home</a></li>");
                    if (tvt == 0)
                    {
                        sw.WriteLine("  <li><a href='/PokemonList3D'>3DPokemon</a></li>");
                    }
                    else
                    {
                        sw.WriteLine("  <li><a href='/PokemonList'>2DPokemon</a></li>");
                    }
                    sw.WriteLine("  <li><a href='/ManageTeams'>Teams</a></li>");
                    sw.WriteLine("  <li><a href='/ManageEvolutions'>Evolutions</a></li>");
                    sw.WriteLine(" </ul>");
                    sw.WriteLine("</div>");


                    sw.WriteLine("   <div id=\"wrapper\">");
                    sw.WriteLine("    <div class=\"heading_text\">");
                    sw.WriteLine("     <h1>Pokemon</h1>");

                    //sw.WriteLine("     <text>Name Type(s) Max CP Candy to Evolve</text>");


                    sw.WriteLine("<div class=\"row\"");

                    sw.WriteLine(" <div class=\"colh\"");
                    sw.WriteLine("  <text>&nbsp&nbsp&nbsp&nbspName</text>"); //fix this in css
                    sw.WriteLine(" </div>");

                    sw.WriteLine(" <div class=\"colh\"");
                    sw.WriteLine("  <text>&nbsp&nbsp&nbsp&nbsp&nbspType(s)</text>");
                    sw.WriteLine(" </div>");

                    sw.WriteLine(" <div class=\"colh\"");
                    sw.WriteLine("  <text>Max CP</text>");
                    sw.WriteLine(" </div>");

                    sw.WriteLine(" <div class=\"colh\"");
                    sw.WriteLine("  <text>Candy</text>");
                    sw.WriteLine(" </div>");

                    sw.WriteLine(" <div class=\"colh\"");
                    sw.WriteLine("  <text>Add Pokemon</text>");
                    sw.WriteLine(" </div>");



                    sw.WriteLine("</div>");




                    sw.WriteLine("    <ul style=\"list-style-type: none;\">");
                    for (int i = 252; i < current_start; i++)
                    {
                        sw.WriteLine("     <li>");
                        if (tvt == 0)
                        {
                            sw.WriteLine("         <img src=\"Pokemon/" + i + ".png\"/>");
                        }
                        if (i < current_start)
                        {
                            string[] list;
                            list = arr[i - 252].Split(','); // get each element
                            if (tvt == 1)
                            {
                                sw.WriteLine("         <img src='PGifs/" + list[0].ToUpper() + ".gif'/>"); //Testing
                            }
                                                            //number.name,type,cp,candy
                            sw.WriteLine("<div class =\"row\">");
                            for (int j = 0; j < list.Length; j++)
                            {
                                sw.WriteLine("<div class=\"col\">");
                                sw.Write("            <text>" + list[j] + "&nbsp&nbsp</text>");
                                sw.WriteLine("</div>");
                            }
                            sw.WriteLine("<button type=\"button\" onclick=\"proceed('AT" + i + "');\">+Team</button>");
                            sw.Write("<text>&nbsp&nbsp</text>");

                            if (Convert.ToInt32(list[3]) != 0)
                            {
                                sw.WriteLine("<button type=\"button\" onclick=\"proceed('AE" + i + "');\">+Evolution</button>");
                            }
                            sw.WriteLine("</div>");
                        }
                        sw.WriteLine("     </li>");
                    }
                    sw.WriteLine("    </ul>");
                    sw.WriteLine("   </div>");
                    sw.WriteLine("  </body>");
                    sw.WriteLine("</html>");
                }
                tvt++;
        }
        }

        static void Update3DPokemon()
        {
        string pokemon_data_path = "C:\\users\\rinik\\desktop\\JS Project\\PokemonData.txt";
        string raw_gif_folder = "C:\\users\\rinik\\desktop\\JS Project helpers\\All_Pokemon_gifs";
        string new_gif_folder = "C:\\users\\rinik\\desktop\\JS Project\\views\\PGifs";

            List<string> pokemon = new List<string>();
            using (StreamReader sr = new StreamReader(pokemon_data_path))
            {
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine().Split('.')[1].Split(',')[0];
                    Console.WriteLine(str);
                    if (!Equals(str, null))
                    {
                        pokemon.Add(str.ToUpper());
                    }
                }
            }

            DirectoryInfo d = new DirectoryInfo(raw_gif_folder);
            FileInfo[] fi = d.GetFiles();
            List<FileInfo> fi2 = new List<FileInfo>();
            foreach (FileInfo file in fi)
            {
                string str = file.Name.Split('.')[0].ToUpper();
                Console.WriteLine(str);
                if (pokemon.Contains(str))
                {
                    if (!File.Exists(new_gif_folder + "\\" + str + ".gif"))
                    {
                        file.CopyTo(new_gif_folder + "\\" + str + ".gif");
                    }
                }
            }
        }
    }
   
}
