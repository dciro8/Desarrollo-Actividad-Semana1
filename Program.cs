using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace visual_Code
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Presiona tecla enter para iniciar prueba");
            var text = Console.ReadLine();

            var currentDate = DateTime.Now;

            string charNormal = "David Fernando Ciro Zapata";

            string charNoLongitudMin = "Davi";

            string charNoLongitudMax = "David Fernando Ciro Zapata El mejor a nivel de Persona y A nivel professional que tendra una super dama al lado";

            string charNoTolower = "david Fernando Ciro Zapata";


            for (int i = 0; i < 1000000; i++)
            {
                var testCharNormal = ValidateRegularExpressionString(charNormal);
                var testCharNoLongitudMin = ValidateRegularExpressionString(charNoLongitudMin);
                var testcharNoLongitudMax = ValidateRegularExpressionString(charNoLongitudMax);
                var testcharNoTolower = ValidateRegularExpressionString(charNoTolower);
            }
            var endDate = DateTime.Now;

            Console.WriteLine($"{Environment.NewLine} tiempo inicio {currentDate},  tiempo final {endDate}");
            Console.Write($"{Environment.NewLine}Press any key to exit...");
            Console.ReadKey(true);
        }
        static List<string> ValidateRegularExpressionString(string input)
        {
            string countCharacter = @"^\w{5,32}\b";

            List<string> constrain = new List<string>();

            var count = Regex.Match(input, countCharacter);

            if (!count.Success)
            {
                constrain.Add($"la cantdad de caracteres es no coincide con la longitud esperada entre  - 32 {input.Trim().Length}");
            }

            string firtsCharacter = @"^[A-Z]+";

            var matches = Regex.Match(input, firtsCharacter);

            if (!matches.Success)
            {
                constrain.Add($"el Primer Caracter es en minuscula");
            }

            Regex specialCharacter = new Regex(@"^[a-zA-Z-Z0-9- ]+$");

            if (!specialCharacter.IsMatch(input))
            {
                constrain.Add($" Contiene caracteres especiales");
            }
            return constrain;
        }

        static List<string> ValidateIfString(string input)
        {

            List<string> constrain = new List<string>();

            if (input.Length < 5 || input.Length > 32)
            {
                constrain.Add($"la cantdad de caracteres es no coincide con la longitud esperada entre 5 - 32 {input.Trim().Length}");
            }


            if (input.Substring(0, 1).Any(c => char.IsLower(c)))
            {
                constrain.Add($"Contiene minusculas");
            }

            var withoutSpecial = new string(input.Where(c => Char.IsLetterOrDigit(c)
                                             || Char.IsWhiteSpace(c)).ToArray());

            if (input != withoutSpecial)
            {
                constrain.Add($" Contiene caracteres especiales");
            }
            return constrain;
        }

        static Exception ValidateExceptionString(string input)
        {
            if (input.Length < 5 || input.Length > 32)
            {
                Exception exception = new Exception($"la cantdad de caracteres es no coincide con la longitud esperada entre 5 - 32 {input.Trim().Length}");
                return exception;
            }
            if (input.Substring(0, 1).Any(c => char.IsLower(c)))
            {
                Exception exception = new Exception($"Contiene minusculas");
                return exception;
            }

            var withoutSpecial = new string(input.Where(c => Char.IsLetterOrDigit(c) || Char.IsWhiteSpace(c)).ToArray());

            if (input != withoutSpecial)
            {
                Exception exception = new Exception($" Contiene caracteres especiales");
                return exception;
            }
            return null;
        }
    }
}

