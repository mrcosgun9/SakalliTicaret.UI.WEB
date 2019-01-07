using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SakalliTicaret.UI.WEB.App_Class
{
    public class Fonctions
    {
        public string CharactertReplace(string character)
        {
            string characterReplace= Regex.Replace(character, "[^0-9a-zA-Z- ]+", "");
            return characterReplace;
        }

    }
}