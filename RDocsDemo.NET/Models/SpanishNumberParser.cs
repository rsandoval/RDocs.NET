using System.Collections.Generic;
using System.Globalization;


namespace RDocsDemo.NET.Models
{
    public class SpanishNumberParser : NumberParser
    {
        private List<Number> numbers = new List<Number>(), separators = new List<Number>();

        private static SpanishNumberParser instance = null;
        public static NumberParser getInstance()
        {
            if (instance == null)
                instance = new SpanishNumberParser();
            return instance;
        }

        private SpanishNumberParser()
        {
            separators.Add(new Number(1000, "mil"));
            separators.Add(new Number(1000000, "millones"));
            separators.Add(new Number(1000000, "millón"));

            numbers.Add(new Number(14, "catorce"));
            numbers.Add(new Number(0, "cero"));
            numbers.Add(new Number(100, "cien"));
            numbers.Add(new Number(100, "ciento"));
            numbers.Add(new Number(5, "cinco"));
            numbers.Add(new Number(50, "cincuenta"));
            numbers.Add(new Number(40, "cuarenta"));
            numbers.Add(new Number(4, "cuatro"));
            numbers.Add(new Number(400, "cuatrocientos"));
            numbers.Add(new Number(19, "diecinueve"));
            numbers.Add(new Number(18, "dieciocho"));
            numbers.Add(new Number(16, "dieciséis"));
            numbers.Add(new Number(17, "diecisiete"));
            numbers.Add(new Number(10, "diez"));
            numbers.Add(new Number(12, "doce"));
            numbers.Add(new Number(2, "dos"));
            numbers.Add(new Number(200, "doscientos"));
            numbers.Add(new Number(900, "novecientos"));
            numbers.Add(new Number(90, "noventa"));
            numbers.Add(new Number(9, "nueve"));
            numbers.Add(new Number(80, "ochenta"));
            numbers.Add(new Number(8, "ocho"));
            numbers.Add(new Number(800, "ochocientos"));
            numbers.Add(new Number(11, "once"));
            numbers.Add(new Number(15, "quince"));
            numbers.Add(new Number(500, "quinientos"));
            numbers.Add(new Number(6, "seis"));
            numbers.Add(new Number(600, "seiscientos"));
            numbers.Add(new Number(60, "sesenta"));
            numbers.Add(new Number(700, "setecientos"));
            numbers.Add(new Number(70, "setenta"));
            numbers.Add(new Number(7, "siete"));
            numbers.Add(new Number(13, "trece"));
            numbers.Add(new Number(30, "treinta"));
            numbers.Add(new Number(3, "tres"));
            numbers.Add(new Number(300, "trescientos"));
            numbers.Add(new Number(1, "un"));
            numbers.Add(new Number(1, "uno"));
            numbers.Add(new Number(20, "veinte"));
            numbers.Add(new Number(25, "veinticinco"));
            numbers.Add(new Number(24, "veinticuatro"));
            numbers.Add(new Number(22, "veintidós"));
            numbers.Add(new Number(29, "veintinueve"));
            numbers.Add(new Number(28, "veintiocho"));
            numbers.Add(new Number(26, "veintiséis"));
            numbers.Add(new Number(27, "veintisiete"));
            numbers.Add(new Number(23, "veintitrés"));
            numbers.Add(new Number(21, "veintiuno"));
            numbers.Add(new Number(21, "veintiún"));
        }

        /// <summary>
        /// Transform a Spanish phrase to the int number it represents.
        /// </summary>
        /// <param name="words">The Spanish phrase.</param>
        /// <returns>The numeric value of the phrase.</returns>
        public int transformToDigits(string words)
        {
            int value = 0, temp = 0;
            bool previousIsThousand = false;
            string[] t = words.Split(' ');
            foreach (string s in t)
            {
                foreach (Number n in numbers)
                {
                    if (string.Compare(s, n.Text, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0)
                    {
                        temp += n.Value;
                        break;
                    }
                }
                foreach (Number n in separators)
                {
                    if (string.Compare(s, n.Text, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0)
                    {
                        if (temp != 0)
                            value += n.Value * temp;
                        else if (previousIsThousand)
                        {
                            value *= n.Value;
                            previousIsThousand = false;
                        }
                        else
                            value += n.Value;
                        temp = 0;
                        if (n.Value == 1000)
                            previousIsThousand = true;
                        break;
                    }
                }
            }
            value += temp;
            return value;
        }

        /// <summary>
        /// Transform an int value to the Spanish phrase that represents it.
        /// </summary>
        /// <param name="number">The number to convert.</param>
        /// <returns>A Spanish string with the text conversion.</returns>
        public string transfromToWords(int number)
        {
            string words = "";
            if (number == 0)
                return "cero";

            if (number >= 2000000000)
                words += transformThreeDigits((int)(number / 1000000000)) + " mil ";
            else if (number >= 1000000000)
                words += "mil ";
            number %= 1000000000;

            if (number >= 2000000 || words.Contains("mil"))
                words += transformThreeDigits((int)(number / 1000000)) + " millones ";
            else if (number >= 1000000)
                words += "un millón ";
            number %= 1000000;

            if (number >= 2000)
                words += transformThreeDigits((int)(number / 1000)) + " mil ";
            else if (number >= 1000)
                words += "mil ";
            number %= 1000;

            words += transformThreeDigits((int)(number));
            if (number % 100 != 11 && number % 10 == 1)
                words += "o ";
            if (words.Length > 4 && words[words.Length - 4] == 'ú')
                words = words.Substring(0, words.Length - 4) + "uno ";

            return words;
        }

        private string transformThreeDigits(int n)
        {
            string result = "";
            switch (n / 100)
            {
                case 1:
                    result += "cien" + (n > 100 ? "to " : "");
                    break;
                case 2:
                    result += "doscientos" + (n > 200 ? " " : "");
                    break;
                case 3:
                    result += "trescientos" + (n > 300 ? " " : "");
                    break;
                case 4:
                    result += "cuatrocientos" + (n > 400 ? " " : "");
                    break;
                case 5:
                    result += "quinientos" + (n > 500 ? " " : "");
                    break;
                case 6:
                    result += "seiscientos" + (n > 600 ? " " : "");
                    break;
                case 7:
                    result += "setecientos" + (n > 700 ? " " : "");
                    break;
                case 8:
                    result += "ochocientos" + (n > 800 ? " " : "");
                    break;
                case 9:
                    result += "novecientos" + (n > 900 ? " " : "");
                    break;
            }

            n %= 100;
            switch (n / 10)
            {
                case 1:
                    if (n == 10)
                        result += "diez";
                    else if (n == 11)
                        result += "once";
                    else if (n == 12)
                        result += "doce";
                    else if (n == 13)
                        result += "trece";
                    else if (n == 14)
                        result += "catorce";
                    else if (n == 15)
                        result += "quince";
                    else if (n == 16)
                        result += "dieciséis";
                    else if (n == 17)
                        result += "diecisiete";
                    else if (n == 18)
                        result += "dieciocho";
                    else if (n == 19)
                        result += "diecinueve";
                    return result;
                case 2:
                    if (n == 20)
                        result += "veinte";
                    else if (n == 21)
                        result += "veintiún";
                    else if (n == 22)
                        result += "veintidós";
                    else if (n == 23)
                        result += "veintitrés";
                    else if (n == 24)
                        result += "veinticuatro";
                    else if (n == 25)
                        result += "veinticinco";
                    else if (n == 26)
                        result += "veintiséis";
                    else if (n == 27)
                        result += "veintisiete";
                    else if (n == 28)
                        result += "veintiocho";
                    else if (n == 29)
                        result += "veintinueve";
                    return result;
                case 3:
                    result += "treinta" + (n > 30 ? " y " : "");
                    break;
                case 4:
                    result += "cuarenta" + (n > 40 ? " y " : "");
                    break;
                case 5:
                    result += "cincuenta" + (n > 50 ? " y " : "");
                    break;
                case 6:
                    result += "sesenta" + (n > 60 ? " y " : "");
                    break;
                case 7:
                    result += "setenta" + (n > 70 ? " y " : "");
                    break;
                case 8:
                    result += "ochenta" + (n > 80 ? " y " : "");
                    break;
                case 9:
                    result += "noventa" + (n > 90 ? " y " : "");
                    break;
            }

            switch (n % 10)
            {
                case 1:
                    result += "un";
                    break;
                case 2:
                    result += "dos";
                    break;
                case 3:
                    result += "tres";
                    break;
                case 4:
                    result += "cuatro";
                    break;
                case 5:
                    result += "cinco";
                    break;
                case 6:
                    result += "seis";
                    break;
                case 7:
                    result += "siete";
                    break;
                case 8:
                    result += "ocho";
                    break;
                case 9:
                    result += "nueve";
                    break;
            }
            return result;
        }
    }
}
