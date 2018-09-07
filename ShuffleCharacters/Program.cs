using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Facet.Combinatorics;

namespace ShuffleCharacters
{
   public class Program
   {
      public static void Main(string[] args)
      {
         const string chars = "hackistanbul";
         const string expectedHash = "29f25103ac08b6452697019e2cc2176e";

         var inputSet = chars.ToCharArray();

         var variations = new Variations<char>(inputSet, 12, GenerateOption.WithoutRepetition);
         Console.WriteLine($"Variations of {chars} choose 12: size = {variations.Count}");

         var found = false;
         var temp = "";
         foreach (var v in variations)
         {
            temp = new string(v.ToArray());

            var hash = CreateMd5(temp);
            if (hash != expectedHash) continue;
            found = true;
            break;
         }

         Console.WriteLine(found ? temp : "Not found.");
         Console.ReadLine();
      }

      public static string CreateMd5(string input)
      {
         // Use input string to calculate MD5 hash
         using (var md5 = MD5.Create())
         {
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            var sb = new StringBuilder();
            foreach (var t in hashBytes)
            {
               sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
         }
      }

   }
}
