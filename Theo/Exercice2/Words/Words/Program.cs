using System;
using System.Linq;
using System.Text.RegularExpressions;

//string[] words = { "bonjour", "hello", "monde", "vert", "rouge", "bleu", "jaune", "Tot" };

//Func<string, bool> Filtred = wordFiltred => !wordFiltred.Contains("x");
//Func<string, bool> Filtred2 = wordFiltred => wordFiltred.Length >= 4;
//Func<string, bool> Filtred3 = wordFiltred => wordFiltred.Length == words.Average(word2 => word2.Length);

//var filters = new List<Func<string,bool>>();
//filters.Add(Filtred);
//filters.Add(Filtred2);
//filters.Add(Filtred3);

//Console.Write($"Voici la liste : {String.Join(", ", words)}");

//int choice = Convert.ToInt32(Console.ReadLine()) - 1;

//var filtered = words.Where(filters[choice]);
//Console.WriteLine($"Résultat: {String.Join(", ",filtered)}");

//int choiceAfterFilter = Convert.ToInt32(Console.ReadLine());

//var reverse = filtered.Reverse();
//var aToZ = filtered.OrderBy(w => w).ToList();
//var zToA = filtered.OrderByDescending(w => w).ToList();
//switch (choiceAfterFilter)
//{
//    case 1:
//        Console.Write($"Voici la liste : {String.Join(", ", reverse)}");
//        break;
//    case 2:
//        Console.Write($"Voici la liste : {String.Join(", ", aToZ)}");
//        break;
//    case 3:
//        Console.Write($"Voici la liste : {String.Join(", ", zToA)}");
//        break;
//}
//string[] words = { "whatThe!!!", "bonjour", "hello", "monde", "vert", "rouge", "bleu", "jaune", "My kingdom for a horse !", "Ooops I did it again" };

//var sansLast2 = words.Skip(1).Take(words.Length - 3);

//Console.Write($"Voici la liste : {String.Join(", ", sansLast2)}");

//string[] words = { "+++++", "<<<<<", ">>>>>", "bonjour", "hello", "@@@@", "vert", "rouge", "bleu", "jaune", "#####", "%%%%%%%" };

//var letters = words.Where(x => Regex.IsMatch(x,"^[a-zA-Z]")).ToList();

//Console.Write($"Voici la liste : {String.Join(", ", letters)}");

//string[] words = { "i am the winner", "hello", "monde", "vert", "rouge", "bleu", "i am the looser" };

//var famous = words.First();
//var famousLast = words.Last();

//Console.WriteLine($"The winner is : {famous}");
//Console.Write($"The loser is : {famousLast}");

var frequencies = new Dictionary<char, double>
        {
            {'A', 8.15}, {'B', 0.97}, {'C', 3.15}, {'D', 3.73}, {'E', 17.39},
            {'F', 1.12}, {'G', 0.97}, {'H', 0.85}, {'I', 7.31}, {'J', 0.45},
            {'K', 0.02}, {'L', 5.69}, {'M', 2.87}, {'N', 7.12}, {'O', 5.28},
            {'P', 2.80}, {'Q', 1.21}, {'R', 6.64}, {'S', 8.14}, {'T', 7.22},
            {'U', 6.38}, {'V', 1.64}, {'W', 0.03}, {'X', 0.41}, {'Y', 0.28},
            {'Z', 0.15}
        };

string[] words = {
    "bonjour",
    "maison",
    "ordinateur",
    "arbre",
    "fenêtre",
    "voiture",
    "pluie",
    "chat",
    "étoile",
    "livre",
    "soleil",
    "montagne",
    "rivière",
    "oiseau",
    "école"
};

var charWords = words.Select(x => x.ToCharArray()).ToArray();

foreach (var chars in charWords)
{
    double epsilon = CalculateEpsilon(chars, frequencies);
    if (epsilon >= 0.5 && epsilon <= 0.95)
    {
        Console.WriteLine($"Mot : {new string(chars)}, Epsilon : {epsilon:F4}%");
    }
}
    

    static double CalculateEpsilon(char[] wordChars, Dictionary<char, double> frequencies)
{
    var letterCounts = wordChars.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
    double epsilon = 0;

    foreach (var letter in letterCounts)
    {
        if (frequencies.ContainsKey(letter.Key))
        {
            epsilon += frequencies[letter.Key] / letter.Value;
        }
    }

    return epsilon;
}


