using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

class StopWords
{
    static string[] WordsAfterRemoveStopWords;
    static Dictionary<string, bool> stop_words = new Dictionary<string, bool>
    {
        { "a", true },
        { "b", true },
        { "c", true },
        { "d", true },
        { "e", true },
        { "f", true },
        { "g", true },
        { "h", true },
        { "j", true },
        { "k", true },
        { "l", true },
        { "m", true },
        { "n", true },
        { "o", true },
        { "p", true },
        { "q", true },
        { "r", true },
        { "s", true },
        { "t", true },
        { "u", true },
        { "v", true },
        { "w", true },
        { "x", true },
        { "y", true },
        { "z", true },


        { "about", true },
        { "above", true },
        { "across", true },
        { "after", true },
        { "afterwards", true },
        { "again", true },
        { "against", true },
        { "all", true },
        { "almost", true },
        { "alone", true },
        { "along", true },
        { "already", true },
        { "also", true },
        { "although", true },
        { "always", true },
        { "am", true },
        { "among", true },
        { "amongst", true },
        { "amount", true },
        { "an", true },
        { "and", true },
        { "another", true },
        { "any", true },
        { "anyhow", true },
        { "anyone", true },
        { "anything", true },
        { "anyway", true },
        { "anywhere", true },
        { "are", true },
        { "around", true },
        { "as", true },
        { "at", true },
        { "back", true },
        { "be", true },
        { "became", true },
        { "because", true },
        { "become", true },
        { "becomes", true },
        { "becoming", true },
        { "been", true },
        { "before", true },
        { "beforehand", true },
        { "behind", true },
        { "being", true },
        { "below", true },
        { "beside", true },
        { "besides", true },
        { "between", true },
        { "beyond", true },
        { "bill", true },
        { "both", true },
        { "bottom", true },
        { "but", true },
        { "by", true },
        { "call", true },
        { "can", true },
        { "cannot", true },
        { "cant", true },
        { "co", true },
        { "computer", true },
        { "con", true },
        { "could", true },
        { "couldnt", true },
        { "cry", true },
        { "de", true },
        { "describe", true },
        { "detail", true },
        { "do", true },
        { "done", true },
        { "down", true },
        { "due", true },
        { "during", true },
        { "each", true },
        { "eg", true },
        { "eight", true },
        { "either", true },
        { "eleven", true },
        { "else", true },
        { "elsewhere", true },
        { "empty", true },
        { "enough", true },
        { "etc", true },
        { "even", true },
        { "ever", true },
        { "every", true },
        { "everyone", true },
        { "everything", true },
        { "everywhere", true },
        { "except", true },
        { "few", true },
        { "fifteen", true },
        { "fify", true },
        { "fill", true },
        { "find", true },
        { "fire", true },
        { "first", true },
        { "five", true },
        { "for", true },
        { "former", true },
        { "formerly", true },
        { "forty", true },
        { "found", true },
        { "four", true },
        { "from", true },
        { "front", true },
        { "full", true },
        { "further", true },
        { "get", true },
        { "give", true },
        { "go", true },
        { "had", true },
        { "has", true },
        { "have", true },
        { "he", true },
        { "hence", true },
        { "her", true },
        { "here", true },
        { "hereafter", true },
        { "hereby", true },
        { "herein", true },
        { "hereupon", true },
        { "hers", true },
        { "herself", true },
        { "him", true },
        { "himself", true },
        { "his", true },
        { "how", true },
        { "however", true },
        { "hundred", true },
        { "i", true },
        { "ie", true },
        { "if", true },
        { "in", true },
        { "inc", true },
        { "indeed", true },
        { "interest", true },
        { "into", true },
        { "is", true },
        { "it", true },
        { "its", true },
        { "itself", true },
        { "keep", true },
        { "last", true },
        { "latter", true },
        { "latterly", true },
        { "least", true },
        { "less", true },
        { "ltd", true },
        { "made", true },
        { "many", true },
        { "may", true },
        { "me", true },
        { "meanwhile", true },
        { "might", true },
        { "mill", true },
        { "mine", true },
        { "more", true },
        { "moreover", true },
        { "most", true },
        { "mostly", true },
        { "move", true },
        { "much", true },
        { "must", true },
        { "my", true },
        { "myself", true },
        { "name", true },
        { "namely", true },
        { "neither", true },
        { "never", true },
        { "nevertheless", true },
        { "next", true },
        { "nine", true },
        { "no", true },
        { "nobody", true },
        { "none", true },
        { "nor", true },
        { "not", true },
        { "nothing", true },
        { "now", true },
        { "nowhere", true },
        { "of", true },
        { "off", true },
        { "often", true },
        { "on", true },
        { "once", true },
        { "one", true },
        { "only", true },
        { "onto", true },
        { "or", true },
        { "other", true },
        { "others", true },
        { "otherwise", true },
        { "our", true },
        { "ours", true },
        { "ourselves", true },
        { "out", true },
        { "over", true },
        { "own", true },
        { "part", true },
        { "per", true },
        { "perhaps", true },
        { "please", true },
        { "put", true },
        { "rather", true },
        { "re", true },
        { "same", true },
        { "see", true },
        { "seem", true },
        { "seemed", true },
        { "seeming", true },
        { "seems", true },
        { "serious", true },
        { "several", true },
        { "she", true },
        { "should", true },
        { "show", true },
        { "side", true },
        { "since", true },
        { "sincere", true },
        { "six", true },
        { "sixty", true },
        { "so", true },
        { "some", true },
        { "somehow", true },
        { "someone", true },
        { "something", true },
        { "sometime", true },
        { "sometimes", true },
        { "somewhere", true },
        { "still", true },
        { "such", true },
        { "system", true },
        { "take", true },
        { "ten", true },
        { "than", true },
        { "that", true },
        { "the", true },
        { "their", true },
        { "them", true },
        { "themselves", true },
        { "then", true },
        { "thence", true },
        { "there", true },
        { "thereafter", true },
        { "thereby", true },
        { "therefore", true },
        { "therein", true },
        { "thereupon", true },
        { "these", true },
        { "they", true },
        { "thick", true },
        { "thin", true },
        { "third", true },
        { "this", true },
        { "those", true },
        { "though", true },
        { "three", true },
        { "through", true },
        { "throughout", true },
        { "thru", true },
        { "thus", true },
        { "to", true },
        { "together", true },
        { "too", true },
        { "top", true },
        { "toward", true },
        { "towards", true },
        { "twelve", true },
        { "twenty", true },
        { "two", true },
        { "un", true },
        { "under", true },
        { "until", true },
        { "up", true },
        { "upon", true },
        { "us", true },
        { "very", true },
        { "via", true },
        { "was", true },
        { "we", true },
        { "well", true },
        { "were", true },
        { "what", true },
        { "whatever", true },
        { "when", true },
        { "whence", true },
        { "whenever", true },
        { "where", true },
        { "whereafter", true },
        { "whereas", true },
        { "whereby", true },
        { "wherein", true },
        { "whereupon", true },
        { "wherever", true },
        { "whether", true },
        { "which", true },
        { "while", true },
        { "whither", true },
        { "who", true },
        { "whoever", true },
        { "whole", true },
        { "whom", true },
        { "whose", true },
        { "why", true },
        { "will", true },
        { "with", true },
        { "within", true },
        { "without", true },
        { "would", true },
        { "yet", true },
        { "you", true },
        { "your", true },
        { "yours", true },
        { "yourself", true },
        { "yourselves", true }
    };



    public static string[] RemoveStopwords(string [] Arr)
    {
        
       
      
        List<string> ResList = new List<string>();
       
        for (int i = 0; i < Arr.Length; i++)
        {
            if (!stop_words.ContainsKey(Arr[i]))
            {
                ResList.Add(Arr[i]);
            }
        }
        int ArrLength = ResList.Count();
        WordsAfterRemoveStopWords = new string[ArrLength];
        for (int i = 0; i < ArrLength; i++)
        {
            WordsAfterRemoveStopWords[i] = ResList[i];
        }
        return WordsAfterRemoveStopWords;
    }

}

class Stemmer
{
    string[] doubles = { "bb", "dd", "ff", "gg", "mm", "nn", "pp", "rr", "tt" };
    string[] validLiEndings = { "c", "d", "e", "g", "h", "k", "m", "n", "r", "t" };

    private string[,] step1bReplacements =
        {
            {"eedly","ee"},
            {"ingly",""},
            {"edly",""},
            {"eed","ee"},
            {"ing",""},
            {"ed",""}
        };

    string[,] step2Replacements =
        {
            {"ization","ize"},
            {"iveness","ive"},
            {"fulness","ful"},
            {"ational","ate"},
            {"ousness","ous"},
            {"biliti","ble"},
            {"tional","tion"},
            {"lessli","less"},
            {"fulli","ful"},
            {"entli","ent"},
            {"ation","ate"},
            {"aliti","al"},
            {"iviti","ive"},
            {"ousli","ous"},
            {"alism","al"},
            {"abli","able"},
            {"anci","ance"},
            {"alli","al"},
            {"izer","ize"},
            {"enci","ence"},
            {"ator","ate"},
            {"bli","ble"},
            {"ogi","og"},
            {"li",""}
        };

    string[,] step3Replacements =
        {
            {"ational","ate"},
            {"tional","tion"},
            {"alize","al"},
            {"icate","ic"},
            {"iciti","ic"},
            {"ative",""},
            {"ical","ic"},
            {"ness",""},
            {"ful",""}
        };

    string[] step4Replacements =
            {"ement",
            "ment",
            "able",
            "ible",
            "ance",
            "ence",
            "ate",
            "iti",
            "ion",
            "ize",
            "ive",
            "ous",
            "ant",
            "ism",
            "ent",
            "al",
            "er",
            "ic"
        };

    string[,] exceptions =
        {
        {"skis","ski"},
        {"skies","sky"},
        {"dying","die"},
        {"lying","lie"},
        {"tying","tie"},
        {"idly","idl"},
        {"gently","gentl"},
        {"ugly","ugli"},
        {"early","earli"},
        {"only","onli"},
        {"singly","singl"},
        {"sky","sky"},
        {"news","news"},
        {"howe","howe"},
        {"atlas","atlas"},
        {"cosmos","cosmos"},
        {"bias","bias"},
        {"andes","andes"}
        };

    string[] exceptions2 =
        {"inning","outing","canning","herring","earring","proceed",
            "exceed","succeed"};


    // A helper table lookup code - used for vowel lookup
    private bool arrayContains(string[] arr, string s)
    {
        for (int i = 0; i < arr.Length; ++i)
        {
            if (arr[i] == s) return true;
        }
        return false;
    }

    private bool isVowel(StringBuilder s, int offset)
    {
        switch (s[offset])
        {
            case 'a':
            case 'e':
            case 'i':
            case 'o':
            case 'u':
            case 'y':
                return true;
                break;
            default:
                return false;
        }
    }

    private bool isShortSyllable(StringBuilder s, int offset)
    {
        if ((offset == 0) && (isVowel(s, 0)) && (!isVowel(s, 1)))
            return true;
        else
            if (
                ((offset > 0) && (offset < s.Length - 1)) &&
                isVowel(s, offset) && !isVowel(s, offset + 1) &&
                (s[offset + 1] != 'w' && s[offset + 1] != 'x' && s[offset + 1] != 'Y')
                && !isVowel(s, offset - 1))
                return true;
            else
                return false;
    }

    private bool isShortWord(StringBuilder s, int r1)
    {
        if ((r1 >= s.Length) && (isShortSyllable(s, s.Length - 2))) return true;

        return false;
    }

    private void changeY(StringBuilder sb)
    {
        if (sb[0] == 'y') sb[0] = 'Y';

        for (int i = 1; i < sb.Length; ++i)
        {
            if ((sb[i] == 'y') && (isVowel(sb, i - 1))) sb[i] = 'Y';
        }
    }

    private void computeR1R2(StringBuilder sb, ref int r1, ref int r2)
    {
        r1 = sb.Length;
        r2 = sb.Length;

        if ((sb.Length >= 5) && (sb.ToString(0, 5) == "gener" || sb.ToString(0, 5) == "arsen")) r1 = 5;
        if ((sb.Length >= 6) && (sb.ToString(0, 6) == "commun")) r1 = 6;

        if (r1 == sb.Length) // If R1 has not been changed by exception words
            for (int i = 1; i < sb.Length; ++i) // Compute R1 according to the algorithm
            {
                if ((!isVowel(sb, i)) && (isVowel(sb, i - 1)))
                {
                    r1 = i + 1;
                    break;
                }
            }

        for (int i = r1 + 1; i < sb.Length; ++i)
        {
            if ((!isVowel(sb, i)) && (isVowel(sb, i - 1)))
            {
                r2 = i + 1;
                break;
            }
        }
    }

    private void step0(StringBuilder sb)
    {

        if ((sb.Length >= 3) && (sb.ToString(sb.Length - 3, 3) == "'s'"))
            sb.Remove(sb.Length - 3, 3);
        else
            if ((sb.Length >= 2) && (sb.ToString(sb.Length - 2, 2) == "'s"))
                sb.Remove(sb.Length - 2, 2);
            else
                if (sb[sb.Length - 1] == '\'')
                    sb.Remove(sb.Length - 1, 1);
    }

    private void step1a(StringBuilder sb)
    {

        if ((sb.Length >= 4) && sb.ToString(sb.Length - 4, 4) == "sses")
            sb.Replace("sses", "ss", sb.Length - 4, 4);
        else
            if ((sb.Length >= 3) && (sb.ToString(sb.Length - 3, 3) == "ied" || sb.ToString(sb.Length - 3, 3) == "ies"))
            {
                if (sb.Length > 4)
                    sb.Replace(sb.ToString(sb.Length - 3, 3), "i", sb.Length - 3, 3);
                else
                    sb.Replace(sb.ToString(sb.Length - 3, 3), "ie", sb.Length - 3, 3);
            }
            else
                if ((sb.Length >= 2) && (sb.ToString(sb.Length - 2, 2) == "us" || sb.ToString(sb.Length - 2, 2) == "ss"))
                    return;
                else
                    if ((sb.Length > 0) && (sb.ToString(sb.Length - 1, 1) == "s"))
                    {
                        for (int i = 0; i < sb.Length - 2; ++i)
                            if (isVowel(sb, i))
                            {
                                sb.Remove(sb.Length - 1, 1);
                                break;
                            }
                    }

    }

    private void step1b(StringBuilder sb, int r1)
    {
        for (int i = 0; i < 6; ++i)
        {
            if ((sb.Length > step1bReplacements[i, 0].Length) && (sb.ToString(sb.Length - step1bReplacements[i, 0].Length, step1bReplacements[i, 0].Length) == step1bReplacements[i, 0]))
            {
                switch (step1bReplacements[i, 0])
                {
                    case "eedly":
                    case "eed":
                        if (sb.Length - step1bReplacements[i, 0].Length >= r1)
                            sb.Replace(step1bReplacements[i, 0], step1bReplacements[i, 1], sb.Length - step1bReplacements[i, 0].Length, step1bReplacements[i, 0].Length);
                        break;
                    default:
                        bool found = false;
                        for (int j = 0; j < sb.Length - step1bReplacements[i, 0].Length; ++j)
                        {
                            if (isVowel(sb, j))
                            {
                                sb.Replace(step1bReplacements[i, 0], step1bReplacements[i, 1], sb.Length - step1bReplacements[i, 0].Length, step1bReplacements[i, 0].Length);
                                found = true;
                                break;
                            }
                        }
                        if (!found) return;
                        if (sb.Length >= 2)
                        {
                            switch (sb.ToString(sb.Length - 2, 2))
                            {
                                case "at":
                                case "bl":
                                case "iz":
                                    sb.Append("e");
                                    return;
                            }
                            if (arrayContains(doubles, sb.ToString(sb.Length - 2, 2)))
                            {
                                sb.Remove(sb.Length - 1, 1);
                                return;
                            }
                        }
                        if (isShortWord(sb, r1))
                            sb.Append("e");
                        break;
                }
                return;
            }
        }
    }

    private void step1c(StringBuilder sb)
    {
        if ((sb.Length > 0) &&
            (sb[sb.Length - 1] == 'y' || sb[sb.Length - 1] == 'Y') &&
            (sb.Length > 2) && (!isVowel(sb, sb.Length - 2))
           )
            sb[sb.Length - 1] = 'i';
    }

    private void step2(StringBuilder sb, int r1)
    {
        for (int i = 0; i < 24; ++i)
        {
            if (
                (sb.Length >= step2Replacements[i, 0].Length) &&
                (sb.ToString(sb.Length - step2Replacements[i, 0].Length, step2Replacements[i, 0].Length) == step2Replacements[i, 0])
                )
            {
                if (sb.Length - step2Replacements[i, 0].Length >= r1)
                {
                    switch (step2Replacements[i, 0])
                    {
                        case "ogi":
                            if ((sb.Length > 3) &&
                                (sb[sb.Length - step2Replacements[i, 0].Length - 1] == 'l')
                                )
                                sb.Replace(step2Replacements[i, 0], step2Replacements[i, 1], sb.Length - step2Replacements[i, 0].Length, step2Replacements[i, 0].Length);
                            return;
                        case "li":
                            if ((sb.Length > 1) &&
                                (arrayContains(validLiEndings, sb.ToString(sb.Length - 3, 1)))
                                )
                                sb.Remove(sb.Length - 2, 2);
                            return;
                        default:
                            sb.Replace(step2Replacements[i, 0], step2Replacements[i, 1], sb.Length - step2Replacements[i, 0].Length, step2Replacements[i, 0].Length);
                            return;
                            break;

                    }
                }
                else
                    return;
            }
        }
    }

    private void step3(StringBuilder sb, int r1, int r2)
    {
        for (int i = 0; i < 9; ++i)
        {
            if (
                (sb.Length >= step3Replacements[i, 0].Length) &&
                (sb.ToString(sb.Length - step3Replacements[i, 0].Length, step3Replacements[i, 0].Length) == step3Replacements[i, 0])
                )
            {
                if (sb.Length - step3Replacements[i, 0].Length >= r1)
                {
                    switch (step3Replacements[i, 0])
                    {
                        case "ative":
                            if (sb.Length - step3Replacements[i, 0].Length >= r2)
                                sb.Replace(step3Replacements[i, 0], step3Replacements[i, 1], sb.Length - step3Replacements[i, 0].Length, step3Replacements[i, 0].Length);
                            return;
                        default:
                            sb.Replace(step3Replacements[i, 0], step3Replacements[i, 1], sb.Length - step3Replacements[i, 0].Length, step3Replacements[i, 0].Length);
                            return;
                    }
                }
                else return;
            }
        }
    }

    private void step4(StringBuilder sb, int r2)
    {
        for (int i = 0; i < 18; ++i)
        {
            if (
                (sb.Length >= step4Replacements[i].Length) &&
                (sb.ToString(sb.Length - step4Replacements[i].Length, step4Replacements[i].Length) == step4Replacements[i])                    // >=
                )
            {
                if (sb.Length - step4Replacements[i].Length >= r2)
                {
                    switch (step4Replacements[i])
                    {
                        case "ion":
                            if (
                                (sb.Length > 3) &&
                                (
                                    (sb[sb.Length - step4Replacements[i].Length - 1] == 's') ||
                                    (sb[sb.Length - step4Replacements[i].Length - 1] == 't')
                                )
                               )
                                sb.Remove(sb.Length - step4Replacements[i].Length, step4Replacements[i].Length);
                            return;
                        default:
                            sb.Remove(sb.Length - step4Replacements[i].Length, step4Replacements[i].Length);
                            return;
                    }
                }
                else
                    return;
            }
        }

    }

    private void step5(StringBuilder sb, int r1, int r2)
    {
        if (sb.Length > 0)
            if (
                (sb[sb.Length - 1] == 'e') &&
                (
                    (sb.Length - 1 >= r2) ||
                    ((sb.Length - 1 >= r1) && (!isShortSyllable(sb, sb.Length - 3)))
                )
               )
                sb.Remove(sb.Length - 1, 1);
            else
                if (
                    (sb[sb.Length - 1] == 'l') &&
                        (sb.Length - 1 >= r2) &&
                        (sb[sb.Length - 2] == 'l')
                    )
                    sb.Remove(sb.Length - 1, 1);
    }

    public string stem(string word)
    {
        if (word.Length < 3) return word;

        StringBuilder sb = new StringBuilder(word.ToLower());

        if (sb[0] == '\'') sb.Remove(0, 1);

        for (int i = 0; i < exceptions.Length / 2; ++i)
            if (word == exceptions[i, 0])
                return exceptions[i, 1];

        int r1 = 0, r2 = 0;
        changeY(sb);
        computeR1R2(sb, ref r1, ref r2);

        step0(sb);
        step1a(sb);

        for (int i = 0; i < exceptions2.Length; ++i)
            if (sb.ToString() == exceptions2[i])
                return exceptions2[i];

        step1b(sb, r1);
        step1c(sb);
        step2(sb, r1);
        step3(sb, r1, r2);
        step4(sb, r2);
        step5(sb, r1, r2);


        return sb.ToString().ToLower();
    }


}

public partial class SearchPage : System.Web.UI.Page
{
    string ConnectionString = @"Data Source=MohamedRabie\SqlExpress;Initial Catalog=InfoRet;Integrated Security=True";
    SqlConnection conn;
    SqlCommand cmd;
    Object RetrunedObjectFromDb;
    public static int EditDistance(string a, string b)//Edit Distance
    {
        if (String.IsNullOrEmpty(a) || String.IsNullOrEmpty(b)) return 0;

        int lengthA = a.Length;
        int lengthB = b.Length;
        var distances = new int[lengthA + 1, lengthB + 1];
        for (int i = 0; i <= lengthA; distances[i, 0] = i++) ;
        for (int j = 0; j <= lengthB; distances[0, j] = j++) ;

        for (int i = 1; i <= lengthA; i++)
            for (int j = 1; j <= lengthB; j++)
            {
                int cost = b[j - 1] == a[i - 1] ? 0 : 1;
                distances[i, j] = Math.Min
                    (
                    Math.Min(distances[i - 1, j] + 1, distances[i, j - 1] + 1),
                    distances[i - 1, j - 1] + cost
                    );
            }
        return distances[lengthA, lengthB];
    }

    public static string CaseFoldingLower(string x)
    {
        return x.ToLower();
    } //CaseFolding Lower

    public static string[] SplitWords(string s)
    {
        return Regex.Matches(s, "\\w+('(s|d|t|ve|m))?")
        .Cast<Match>().Select(x => x.Value).ToArray();

    }

    public static List<string> SpellCorrection(string s)
    {
        string ConnectionString = @"Data Source=MohamedRabie\SqlExpress;Initial Catalog=InfoRet;Integrated Security=True";
        SqlConnection conn;
        SqlCommand cmd;
        Object RetrunedObjectFromDb;
        conn = new SqlConnection(ConnectionString);
        conn.Open();
        cmd = new SqlCommand("Select P_Content from [InfoRet].[dbo].[IRDPages] where id  = @id ", conn);
        Stemmer Stem = new Stemmer();
        string EnteredQuery = s.ToLower();
        
        

                List<string> BiGramsOfEnteredWord = new List<string>();
                string KGram = s;
                KGram = "$" + KGram + "$";
                string ResultFromDb = "";
                List<string> AllTermsOfGrams = new List<string>();
                for (int j = 0; j < KGram.Length - 1; j++)
                {
                    string CurGram = KGram[j].ToString() + KGram[j + 1].ToString();

                    cmd = new SqlCommand("select terms from [InfoRet].[dbo].[BiGram] where k_gram  = @kgram ", conn);
                    cmd.Parameters.AddWithValue("@kgram", CurGram);
                    RetrunedObjectFromDb = cmd.ExecuteScalar();
                    ResultFromDb = System.Convert.ToString(RetrunedObjectFromDb);


                    BiGramsOfEnteredWord.Add(CurGram);
                    AllTermsOfGrams.Add(ResultFromDb);

                }




                List<string> UniqueAllTerm = new List<string>();
                for (int i = 0; i < AllTermsOfGrams.Count(); i++)
                {
                    string[] SplittedTerms = AllTermsOfGrams[i].Split(',');
                    for (int j = 0; j < SplittedTerms.Length; j++)
                    {
                        if (!UniqueAllTerm.Contains(SplittedTerms[j]))
                        {
                            UniqueAllTerm.Add(SplittedTerms[j]);
                        }
                    }


                }
                List<string> FinalTermsForEditDistance = new List<string>();

                for (int i = 0; i < UniqueAllTerm.Count(); i++)
                {
                    string CurTerm = "$" + UniqueAllTerm[i] + "$";
                    List<string> ListOfCurGrams = new List<string>();
                    for (int j = 0; j < CurTerm.Length - 1; j++)
                    {
                        string KGramTmp = CurTerm[j].ToString() + CurTerm[j + 1].ToString();
                        ListOfCurGrams.Add(KGramTmp);
                    }
                    var IntersectOfGrams = BiGramsOfEnteredWord.Intersect(ListOfCurGrams);
                    int TmpCal = IntersectOfGrams.Count();

                    double FinalResult = 2.0 * TmpCal / (float)(BiGramsOfEnteredWord.Count() + ListOfCurGrams.Count());

                    if (FinalResult >= 0.45)
                    {
                        FinalTermsForEditDistance.Add(UniqueAllTerm[i]);

                    }
                    ListOfCurGrams.Clear();


                }

                


                List<int> Indexes = new List<int>();
                List<int> EditDistanceRes = new List<int>();
                for (int i = 0; i < FinalTermsForEditDistance.Count(); i++)
                {
                    Indexes.Add(i);
                    EditDistanceRes.Add(EditDistance(s.ToLower(), FinalTermsForEditDistance[i]));
                }

                for (int i = 0; i < EditDistanceRes.Count() - 1; i++)
                {
                    for (int j = i + 1; j < EditDistanceRes.Count(); j++)
                    {
                        if (EditDistanceRes[i] > EditDistanceRes[j])
                        {
                            int Tmp = EditDistanceRes[i];
                            EditDistanceRes[i] = EditDistanceRes[j];
                            EditDistanceRes[j] = Tmp;


                            
                            Tmp = Indexes[i];
                            Indexes[i] = Indexes[j];
                            Indexes[j] = Tmp;

                            string Tmp1 = FinalTermsForEditDistance[i];
                            FinalTermsForEditDistance[i] = FinalTermsForEditDistance[j];
                            FinalTermsForEditDistance[j] = Tmp1;
                        }
                    }
                }

                List<string> FinalResultTerms = new List<string>();
                for (int i = 0; i < 3; i++)
                {
                    FinalResultTerms.Add(FinalTermsForEditDistance[Indexes[i]]);
                }
                return FinalResultTerms;
        
    }

    public static string [] SoundexOfWord(string s)
    {
        
        string ConnectionString = @"Data Source=MohamedRabie\SqlExpress;Initial Catalog=InfoRet;Integrated Security=True";
        SqlConnection conn;
        SqlCommand cmd;
        Object RetrunedObjectFromDb;
        conn = new SqlConnection(ConnectionString);
        conn.Open();
        string UserInput = s.ToLower();
        string SoundexOfInput = Soundex(UserInput);
        string ReturnedTermsForSoundex = "";

        cmd = new SqlCommand("select term from [InfoRet].[dbo].[Soundex] where soundex  = @soundex ", conn);
        cmd.Parameters.AddWithValue("@soundex", SoundexOfInput);
        RetrunedObjectFromDb = cmd.ExecuteScalar();
        ReturnedTermsForSoundex = System.Convert.ToString(RetrunedObjectFromDb);

        string[] AllTermsForSoundex = ReturnedTermsForSoundex.Split(',');
        return AllTermsForSoundex;
        
    }

    public static string Soundex(string word)
    {
        string value = "";

        int size = word.Length;

        if (size > 1)
        {

            word = word.ToUpper();

            char[] chars = word.ToCharArray();

            StringBuilder buffer = new StringBuilder();
            buffer.Length = 0;

            int prevCode = 0;
            int currCode = 0;

            buffer.Append(chars[0]);

            for (int i = 1; i < size; i++)
            {
                switch (chars[i])
                {
                    case 'A':
                        currCode = 0;
                        break;
                    case 'E':
                        currCode = 0;
                        break;
                    case 'I':
                        currCode = 0;
                        break;
                    case 'O':
                        currCode = 0;
                        break;
                    case 'U':
                        currCode = 0;
                        break;
                    case 'H':
                        currCode = 0;
                        break;
                    case 'W':
                        currCode = 0;
                        break;
                    case 'Y':
                        currCode = 0;
                        break;
                    case 'B':
                        currCode = 1;
                        break;
                    case 'F':
                        currCode = 1;
                        break;
                    case 'P':
                        currCode = 1;
                        break;
                    case 'V':
                        currCode = 1;
                        break;
                    case 'C':
                        currCode = 2;
                        break;
                    case 'G':
                        currCode = 2;
                        break;
                    case 'J':
                        currCode = 2;
                        break;
                    case 'K':
                        currCode = 2;
                        break;
                    case 'Q':
                        currCode = 2;
                        break;
                    case 'S':
                        currCode = 2;
                        break;
                    case 'X':
                        currCode = 2;
                        break;
                    case 'Z':
                        currCode = 2;
                        break;
                    case 'D':
                        currCode = 3;
                        break;
                    case 'T':
                        currCode = 3;
                        break;
                    case 'L':
                        currCode = 4;
                        break;
                    case 'M':
                        currCode = 5;
                        break;
                    case 'N':
                        currCode = 5;
                        break;
                    case 'R':
                        currCode = 6;
                        break;
                }


                if (currCode != prevCode)
                {
                    if (currCode != 0)
                        buffer.Append(currCode);
                }

                prevCode = currCode;

                if (buffer.Length == 4)
                    break;
            }

            size = buffer.Length;
            if (size < 4)
                buffer.Append('0', (4 - size));

            value = buffer.ToString();
        }

        return value.ToLower();
    }

    public static int[] SearchOneWords(string s)
    {
        string ReturnedResultFromDb;
        string ConnectionString = @"Data Source=MohamedRabie\SqlExpress;Initial Catalog=InfoRet;Integrated Security=True";
        SqlConnection conn;
        SqlCommand cmd;
        Object RetrunedObjectFromDb;
        conn = new SqlConnection(ConnectionString);
        conn.Open();
        Stemmer Stem = new Stemmer();
        string EnteredQuery = s;
        string Empty = "";
        string Doc_id;
        string Frequency;

        string CaseFoldingResult = CaseFoldingLower(EnteredQuery);
        string[] AllWordsAfterSplit = SplitWords(CaseFoldingResult);
        string[] ArrWithoutStopWord = StopWords.RemoveStopwords(AllWordsAfterSplit);

        for (int j = 0; j < ArrWithoutStopWord.Length; j++)
        {
            ArrWithoutStopWord[j] = Stem.stem(ArrWithoutStopWord[j]);
        }

        cmd = new SqlCommand("select doc_id from InfoRet.dbo.LastInverted where term  = @terminput ", conn);
        cmd.Parameters.AddWithValue("@terminput", ArrWithoutStopWord[0]);
        RetrunedObjectFromDb = cmd.ExecuteScalar();
        Doc_id = System.Convert.ToString(RetrunedObjectFromDb);


        cmd = new SqlCommand("select frequency from InfoRet.dbo.LastInverted where term  = @terminput ", conn);
        cmd.Parameters.AddWithValue("@terminput", ArrWithoutStopWord[0]);
        RetrunedObjectFromDb = cmd.ExecuteScalar();
        Frequency = System.Convert.ToString(RetrunedObjectFromDb);


        string[] AllidSofWord = Doc_id.Split(',');
        string[] AllFreqSofWord = Frequency.Split(',');
        int[] WordFreq = new int[AllFreqSofWord.Length];
        int[] DocIDs = new int[AllidSofWord.Length];
        int Tmp;
        string DocsIDS = "";
        string FreqStr = "";
        for (int i = 0; i < AllFreqSofWord.Length; i++)
        {
            WordFreq[i] = int.Parse(AllFreqSofWord[i]);
            DocIDs[i] = int.Parse(AllidSofWord[i]);


        }

        for (int i = 0; i < AllFreqSofWord.Length - 1; i++)
        {
            for (int j = i + 1; j < AllFreqSofWord.Length; j++)
            {
                if (WordFreq[i] < WordFreq[j])
                {
                    Tmp = WordFreq[i];
                    WordFreq[i] = WordFreq[j];
                    WordFreq[j] = Tmp;

                    Tmp = DocIDs[i];
                    DocIDs[i] = DocIDs[j];
                    DocIDs[j] = Tmp;

                }
            }
        }
        return DocIDs;
        //for (int i = 0; i < AllidSofWord.Length; i++)
        //{
        //    cmd = new SqlCommand("select Link from InfoRet.dbo.IRDPages where id  = @id ", conn);
        //    cmd.Parameters.AddWithValue("@id", DocIDs[i]);
        //    RetrunedObjectFromDb = cmd.ExecuteScalar();
        //    ReturnedResultFromDb = System.Convert.ToString(RetrunedObjectFromDb);
        //    ListOfResults.Items.Add(ReturnedResultFromDb);
        //}




    }
   
    protected void Page_Load(object sender, EventArgs e)
    { 

    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            ListOfResults.Items.Clear();
            conn = new SqlConnection(ConnectionString);
            conn.Open();
            cmd = new SqlCommand("Select P_Content from [InfoRet].[dbo].[IRDPages] where id  = @id ", conn);
            Stemmer Stem = new Stemmer();
            string EnteredQuery = SearchQuery.Text;
            string Empty = "";
            string ReturnedResultFromDb;

            if (RadioButtonList1.Text == "Spell Correction")
            {
                string[] EnteredWords = SearchQuery.Text.Split(' ');
                List<List<string>> NestedList = new List<List<string>>();
                for (int i = 0; i < EnteredWords.Count(); i++)
                {
                    List<string> TList = SpellCorrection(EnteredWords[i]);
                    NestedList.Add(TList);

                }
                string Con = "";
                List<string> DisplayedWords = new List<string>();
                for (int i = 0; i < 3; i++)
                {
                    Con = "";
                    for (int j = 0; j < NestedList.Count(); j++)
                    {
                        Con = Con + NestedList[j][i] + " ";
                    }
                    Con.Remove(Con.Length - 1, 1);
                    DisplayedWords.Add(Con);

                }
                for (int i = 0; i < DisplayedWords.Count(); i++)
                {
                    ListOfWords.Items.Add(DisplayedWords[i]);
                }

            }
            else if (RadioButtonList1.Text == "Soundex")
            {
                ListOfWords.Items.Clear();
                List<string> AllSoundex = new List<string>();
                string[] AllWords = SearchQuery.Text.Split(' ');
                for (int i = 0; i < AllWords.Count(); i++)
                {
                    string[] AllTermsForSoundex = SoundexOfWord(AllWords[i]);
                    foreach (string term in AllTermsForSoundex)
                    {
                        if (!AllSoundex.Contains(term))
                            AllSoundex.Add(term);

                    }
                }
                for (int i = 0; i < AllSoundex.Count(); i++)
                {
                    ListOfWords.Items.Add(AllSoundex[i]);
                }
            }
            else
            {
                if (EnteredQuery == Empty)
                {
                    Response.Write("<script>alert('Please Enter Seach Query')</script>");
                }
                else if (EnteredQuery.Split(' ').Length == 1)
                {
                    string Doc_id;
                    string Frequency;

                    string CaseFoldingResult = CaseFoldingLower(EnteredQuery);
                    string[] AllWordsAfterSplit = SplitWords(CaseFoldingResult);
                    string[] ArrWithoutStopWord = StopWords.RemoveStopwords(AllWordsAfterSplit);

                    for (int j = 0; j < ArrWithoutStopWord.Length; j++)
                    {
                        ArrWithoutStopWord[j] = Stem.stem(ArrWithoutStopWord[j]);
                    }

                    cmd = new SqlCommand("select doc_id from InfoRet.dbo.FinalInvertedIndex where term  = @terminput ", conn);
                    cmd.Parameters.AddWithValue("@terminput", ArrWithoutStopWord[0]);
                    RetrunedObjectFromDb = cmd.ExecuteScalar();
                    Doc_id = System.Convert.ToString(RetrunedObjectFromDb);


                    cmd = new SqlCommand("select frequency from InfoRet.dbo.FinalInvertedIndex where term  = @terminput ", conn);
                    cmd.Parameters.AddWithValue("@terminput", ArrWithoutStopWord[0]);
                    RetrunedObjectFromDb = cmd.ExecuteScalar();
                    Frequency = System.Convert.ToString(RetrunedObjectFromDb);


                    string[] AllidSofWord = Doc_id.Split(',');
                    string[] AllFreqSofWord = Frequency.Split(',');
                    int[] WordFreq = new int[AllFreqSofWord.Length];
                    int[] DocIDs = new int[AllidSofWord.Length];
                    int Tmp;
                    string DocsIDS = "";
                    string FreqStr = "";
                    for (int i = 0; i < AllFreqSofWord.Length; i++)
                    {
                        WordFreq[i] = int.Parse(AllFreqSofWord[i]);
                        DocIDs[i] = int.Parse(AllidSofWord[i]);


                    }

                    for (int i = 0; i < AllFreqSofWord.Length - 1; i++)
                    {
                        for (int j = i + 1; j < AllFreqSofWord.Length; j++)
                        {
                            if (WordFreq[i] < WordFreq[j])
                            {
                                Tmp = WordFreq[i];
                                WordFreq[i] = WordFreq[j];
                                WordFreq[j] = Tmp;

                                Tmp = DocIDs[i];
                                DocIDs[i] = DocIDs[j];
                                DocIDs[j] = Tmp;

                            }
                        }
                    }

                    for (int i = 0; i < AllidSofWord.Length; i++)
                    {
                        cmd = new SqlCommand("select Link from InfoRet.dbo.IRDPages where id  = @id ", conn);
                        cmd.Parameters.AddWithValue("@id", DocIDs[i]);
                        RetrunedObjectFromDb = cmd.ExecuteScalar();
                        ReturnedResultFromDb = System.Convert.ToString(RetrunedObjectFromDb);
                        ListOfResults.Items.Add(ReturnedResultFromDb);
                    }






                }
                else if (EnteredQuery[0] == '"' && EnteredQuery[EnteredQuery.Length - 1] == '"')
                {
                    string UserText = SearchQuery.Text;
                    string TmpTxt = SearchQuery.Text;
                    TmpTxt = TmpTxt.Remove(0, 1);
                    TmpTxt = TmpTxt.Remove(TmpTxt.Length - 1, 1);
                    TmpTxt = CaseFoldingLower(TmpTxt);

                    //orignal words
                    string[] OriginalWords = TmpTxt.Split(' ');


                    string CaseFoldingResult = CaseFoldingLower(UserText);

                    string[] AllWordsAfterSplit = SplitWords(CaseFoldingResult);
                    string[] ArrWithoutStopWord = StopWords.RemoveStopwords(AllWordsAfterSplit);

                    //apply steaming
                    for (int j = 0; j < ArrWithoutStopWord.Length; j++)
                    {
                        ArrWithoutStopWord[j] = Stem.stem(ArrWithoutStopWord[j]);
                    }

                    //apply steaming
                    for (int j = 0; j < OriginalWords.Length; j++)
                    {
                        OriginalWords[j] = Stem.stem(OriginalWords[j]);
                    }


                    int[] PositionsOfWords = new int[ArrWithoutStopWord.Length];

                    for (int i = 0; i < ArrWithoutStopWord.Length; i++)
                    {
                        for (int j = 0; j < OriginalWords.Length; j++)
                        {
                            if (ArrWithoutStopWord[i] == OriginalWords[j])
                            {
                                PositionsOfWords[i] = j;
                            }
                        }

                    }

                    //diff of positions
                    int[] DiffOfPositions = new int[ArrWithoutStopWord.Length - 1];

                    //get diff of all words
                    for (int i = 0; i < ArrWithoutStopWord.Length - 1; i++)
                    {
                        DiffOfPositions[i] = PositionsOfWords[i + 1] - PositionsOfWords[i];

                    }


                    string[] Doc_IDsOfEachWord = new string[ArrWithoutStopWord.Length];
                    string[] AllPostionOfEachWord = new string[ArrWithoutStopWord.Length];


                    //add doc id's and positions as strings
                    for (int i = 0; i < ArrWithoutStopWord.Length; i++)
                    {
                        cmd = new SqlCommand("select doc_id from InfoRet.dbo.FinalInvertedIndex where term  = @terminput ", conn);
                        cmd.Parameters.AddWithValue("@terminput", ArrWithoutStopWord[i]);
                        RetrunedObjectFromDb = cmd.ExecuteScalar();
                        Doc_IDsOfEachWord[i] = System.Convert.ToString(RetrunedObjectFromDb);


                        cmd = new SqlCommand("select positions from InfoRet.dbo.FinalInvertedIndex where term  = @terminput ", conn);
                        cmd.Parameters.AddWithValue("@terminput", ArrWithoutStopWord[i]);
                        RetrunedObjectFromDb = cmd.ExecuteScalar();
                        AllPostionOfEachWord[i] = System.Convert.ToString(RetrunedObjectFromDb);


                    }

                    Dictionary<int, int> DetectExact = new Dictionary<int, int>();
                    List<int> ReturnedDocID = new List<int>();
                    List<int> ReturnedFreq = new List<int>();

                    for (int i = 0; i < ArrWithoutStopWord.Length - 1; i++)
                    {

                        string W1IDs = Doc_IDsOfEachWord[i];
                        string W2IDs = Doc_IDsOfEachWord[i + 1];

                        string[] ArrIDsW1 = W1IDs.Split(',');
                        string[] ArrIDsW2 = W2IDs.Split(',');

                        string W1Ps = AllPostionOfEachWord[i];
                        string W2Ps = AllPostionOfEachWord[i + 1];

                        string[] ArrPW1 = W1Ps.Split('@');
                        string[] ArrPW2 = W2Ps.Split('@');

                        var CommonIDs = ArrIDsW1.Intersect(ArrIDsW2);
                        List<string> CommonIDsList = new List<string>();
                        foreach (var ID in CommonIDs)
                        {
                            CommonIDsList.Add(ID.ToString());

                        }

                        int INdexOfID1 = 0, INdexOfID2 = 0;
                        //get index in first doc
                        for (int j = 0; j < CommonIDsList.Count(); j++)
                        {
                            for (int g = 0; g < ArrIDsW1.Length; g++)
                            {
                                if (CommonIDsList[j] == ArrIDsW1[g])
                                {
                                    INdexOfID1 = g;
                                    break;
                                }

                            }
                            //get index in second doc
                            for (int k = 0; k < ArrIDsW2.Length; k++)
                            {
                                if (CommonIDsList[j] == ArrIDsW2[k])
                                {
                                    INdexOfID2 = k;
                                    break;
                                }

                            }
                            int X = ArrPW2.Length;
                            string P1 = ArrPW1[INdexOfID1];
                            string P2 = ArrPW2[INdexOfID2];

                            string[] ArrPo1 = P1.Split(',');
                            string[] ArrPo2 = P2.Split(',');

                            int[] ArrPo1Int = new int[ArrPo1.Length];
                            int[] ArrPo2Int = new int[ArrPo2.Length];

                            for (int d = 0; d < ArrPo1.Length; d++)
                            {
                                ArrPo1Int[d] = int.Parse(ArrPo1[d]);
                            }

                            for (int d = 0; d < ArrPo2.Length; d++)
                            {
                                ArrPo2Int[d] = int.Parse(ArrPo2[d]);
                            }

                            for (int d = 0; d < ArrPo2Int.Length; d++)
                            {
                                for (int b = 0; b < ArrPo1Int.Length; b++)
                                {
                                    if (ArrPo2Int[d] - ArrPo1Int[b] == DiffOfPositions[i])
                                    {
                                        int TmpDId = int.Parse(CommonIDsList[j]);
                                        if (DetectExact.ContainsKey(TmpDId))
                                        {
                                            DetectExact[TmpDId]++;
                                            break;
                                        }
                                        else
                                        {
                                            DetectExact.Add(TmpDId, 1);
                                            break;
                                        }

                                    }
                                }
                            }


                        }
                    }

                    for (int i = 0; i < DetectExact.Count; i++)
                    {
                        ReturnedDocID.Add(int.Parse(DetectExact.ElementAt(i).Key.ToString()));
                        ReturnedFreq.Add(int.Parse(DetectExact.ElementAt(i).Value.ToString()));
                    }


                    List<int> LastDoc = new List<int>();
                    List<int> LastFreq = new List<int>();

                    for (int i = 0; i < ReturnedFreq.Count(); i++)
                    {
                        if (ReturnedFreq[i] == ArrWithoutStopWord.Length - 1)
                        {
                            LastFreq.Add(ReturnedFreq[i]);
                            LastDoc.Add(ReturnedDocID[i]);


                        }
                    }


                    for (int i = 0; i < LastFreq.Count() - 1; i++)
                    {
                        for (int j = i + 1; j < LastFreq.Count(); j++)
                        {
                            if (LastFreq[i] < LastFreq[j])
                            {
                                int Tmp = LastFreq[i];
                                LastFreq[i] = LastFreq[j];
                                LastFreq[j] = Tmp;

                                Tmp = LastDoc[i];
                                LastDoc[i] = LastDoc[j];
                                LastDoc[j] = Tmp;
                            }
                        }
                    }

                    string ReturnedLink = "";
                    for (int i = 0; i < LastDoc.Count(); i++)
                    {
                        cmd = new SqlCommand("select Link from InfoRet.dbo.IRDPages where id  = @id ", conn);
                        cmd.Parameters.AddWithValue("@id", LastDoc[i]);
                        RetrunedObjectFromDb = cmd.ExecuteScalar();
                        ReturnedLink = System.Convert.ToString(RetrunedObjectFromDb);
                        ListOfResults.Items.Add(ReturnedLink);
                    }

                }
                else
                {
                    string UserText = SearchQuery.Text;
                    string TmpTxt = SearchQuery.Text;

                    TmpTxt = CaseFoldingLower(TmpTxt);

                    string[] OriginalWords = TmpTxt.Split(' ');


                    string CaseFoldingResult = CaseFoldingLower(UserText);

                    string[] AllWordsAfterSplit = SplitWords(CaseFoldingResult);
                    string[] ArrWithoutStopWord = StopWords.RemoveStopwords(AllWordsAfterSplit);

                    for (int j = 0; j < ArrWithoutStopWord.Length; j++)
                    {
                        ArrWithoutStopWord[j] = Stem.stem(ArrWithoutStopWord[j]);
                    }

                    for (int j = 0; j < OriginalWords.Length; j++)
                    {
                        OriginalWords[j] = Stem.stem(OriginalWords[j]);
                    }


                    int[] PositionsOfWords = new int[ArrWithoutStopWord.Length];
                    for (int i = 0; i < ArrWithoutStopWord.Length; i++)
                    {
                        for (int j = 0; j < OriginalWords.Length; j++)
                        {
                            if (ArrWithoutStopWord[i] == OriginalWords[j])
                            {
                                PositionsOfWords[i] = j;
                            }
                        }

                    }
                    int[] DiffOfPositions = new int[ArrWithoutStopWord.Length - 1];
                    for (int i = 0; i < ArrWithoutStopWord.Length - 1; i++)
                    {
                        DiffOfPositions[i] = PositionsOfWords[i + 1] - PositionsOfWords[i];

                    }
                    string[] Doc_IDsOfEachWord = new string[ArrWithoutStopWord.Length];
                    string[] AllPostionOfEachWord = new string[ArrWithoutStopWord.Length];

                    for (int i = 0; i < ArrWithoutStopWord.Length; i++)
                    {
                        cmd = new SqlCommand("select doc_id from InfoRet.dbo.FinalInvertedIndex where term  = @terminput ", conn);
                        cmd.Parameters.AddWithValue("@terminput", ArrWithoutStopWord[i]);
                        RetrunedObjectFromDb = cmd.ExecuteScalar();
                        Doc_IDsOfEachWord[i] = System.Convert.ToString(RetrunedObjectFromDb);


                        cmd = new SqlCommand("select positions from InfoRet.dbo.FinalInvertedIndex where term  = @terminput ", conn);
                        cmd.Parameters.AddWithValue("@terminput", ArrWithoutStopWord[i]);
                        RetrunedObjectFromDb = cmd.ExecuteScalar();
                        AllPostionOfEachWord[i] = System.Convert.ToString(RetrunedObjectFromDb);


                    }

                    Dictionary<int, int> DetectExact = new Dictionary<int, int>();

                    Dictionary<int, int> DetectFreq = new Dictionary<int, int>();
                    Dictionary<int, int> DetectSumPosition = new Dictionary<int, int>();

                    List<int> ReturnedDocID = new List<int>();
                    List<int> ReturnedPosSum = new List<int>();
                    for (int i = 0; i < ArrWithoutStopWord.Length - 1; i++)
                    {

                        string W1IDs = Doc_IDsOfEachWord[i];
                        string W2IDs = Doc_IDsOfEachWord[i + 1];

                        string[] ArrIDsW1 = W1IDs.Split(',');
                        string[] ArrIDsW2 = W2IDs.Split(',');

                        string W1Ps = AllPostionOfEachWord[i];
                        string W2Ps = AllPostionOfEachWord[i + 1];

                        string[] ArrPW1 = W1Ps.Split('@');
                        string[] ArrPW2 = W2Ps.Split('@');

                        var CommonIDs = ArrIDsW1.Intersect(ArrIDsW2);
                        List<string> CommonIDsList = new List<string>();
                        foreach (var ID in CommonIDs)
                        {
                            CommonIDsList.Add(ID.ToString());

                        }

                        int INdexOfID1 = 0, INdexOfID2 = 0;
                        for (int j = 0; j < CommonIDsList.Count(); j++)
                        {
                            for (int g = 0; g < ArrIDsW1.Length; g++)
                            {
                                if (CommonIDsList[j] == ArrIDsW1[g])
                                {
                                    INdexOfID1 = g;
                                    break;
                                }

                            }

                            for (int k = 0; k < ArrIDsW2.Length; k++)
                            {
                                if (CommonIDsList[j] == ArrIDsW2[k])
                                {
                                    INdexOfID2 = k;
                                    break;
                                }

                            }
                            int X = ArrPW2.Length;
                            string P1 = ArrPW1[INdexOfID1];
                            string P2 = ArrPW2[INdexOfID2];

                            string[] ArrPo1 = P1.Split(',');
                            string[] ArrPo2 = P2.Split(',');

                            int[] ArrPo1Int = new int[ArrPo1.Length];
                            int[] ArrPo2Int = new int[ArrPo2.Length];

                            for (int d = 0; d < ArrPo1.Length; d++)
                            {
                                ArrPo1Int[d] = int.Parse(ArrPo1[d]);
                            }

                            for (int d = 0; d < ArrPo2.Length; d++)
                            {
                                ArrPo2Int[d] = int.Parse(ArrPo2[d]);
                            }
                            int TmpPos = 0;
                            for (int d = 0; d < ArrPo2Int.Length; d++)
                            {
                                for (int b = 0; b < ArrPo1Int.Length; b++)
                                {
                                    if (ArrPo2Int[d] - ArrPo1Int[b] > 0)
                                    {
                                        if (TmpPos == 0)
                                            TmpPos = ArrPo2Int[d] - ArrPo1Int[b];
                                        else if ((ArrPo2Int[d] - ArrPo1Int[b]) < TmpPos)
                                            TmpPos = ArrPo2Int[d] - ArrPo1Int[b];
                                    }
                                }
                            }
                            if (DetectSumPosition.ContainsKey(int.Parse(CommonIDsList[j])))
                            {
                                DetectSumPosition[int.Parse(CommonIDsList[j])] = DetectSumPosition[int.Parse(CommonIDsList[j])] + TmpPos;
                            }
                            else
                            {
                                DetectSumPosition.Add(int.Parse(CommonIDsList[j]), TmpPos);
                            }

                        }
                    }

                    for (int i = 0; i < DetectSumPosition.Count; i++)
                    {
                        ReturnedDocID.Add(int.Parse(DetectSumPosition.ElementAt(i).Key.ToString()));
                        ReturnedPosSum.Add(int.Parse(DetectSumPosition.ElementAt(i).Value.ToString()));
                    }






                    for (int i = 0; i < ReturnedPosSum.Count() - 1; i++)
                    {
                        for (int j = i + 1; j < ReturnedPosSum.Count(); j++)
                        {
                            if (ReturnedPosSum[i] > ReturnedPosSum[j])
                            {
                                int Tmp = ReturnedPosSum[i];
                                ReturnedPosSum[i] = ReturnedPosSum[j];
                                ReturnedPosSum[j] = Tmp;

                                Tmp = ReturnedDocID[i];
                                ReturnedDocID[i] = ReturnedDocID[j];
                                ReturnedDocID[j] = Tmp;
                            }
                        }
                    }



                    string ReturnedLink = "";
                    for (int i = 0; i < ReturnedDocID.Count(); i++)
                    {
                        cmd = new SqlCommand("select Link from InfoRet.dbo.IRDPages where id  = @id ", conn);
                        cmd.Parameters.AddWithValue("@id", ReturnedDocID[i]);
                        RetrunedObjectFromDb = cmd.ExecuteScalar();
                        ReturnedLink = System.Convert.ToString(RetrunedObjectFromDb);
                        ListOfResults.Items.Add(ReturnedLink);
                    }


                }
            }
        }
        catch
        {

        }
       
    }
    protected void ListOfResults_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect(ListOfResults.SelectedItem.ToString());
    }
    protected void SPC_CheckedChanged(object sender, EventArgs e)
    {
       


    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        SearchQuery.Text = "";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect(ListOfResults.SelectedItem.ToString());
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        SearchQuery.Text = ListOfWords.SelectedItem.ToString();
        conn = new SqlConnection(ConnectionString);
        conn.Open();
        string CorrectWord = ListOfWords.SelectedItem.ToString();


        try
        {
            CorrectWord = CorrectWord.Remove(CorrectWord.Length - 1, 1);
            string [] arr = CorrectWord.Split(' ');

            int LengthOfArr=arr.Length;

            if (LengthOfArr == 1)
            {
                int[] ListOfDocIds = SearchOneWords(CorrectWord);
                for (int i = 0; i < ListOfDocIds.Length; i++)
                {
                    cmd = new SqlCommand("select Link from InfoRet.dbo.IRDPages where id  = @id ", conn);
                    cmd.Parameters.AddWithValue("@id", ListOfDocIds[i]);
                    RetrunedObjectFromDb = cmd.ExecuteScalar();

                    ListOfResults.Items.Add(System.Convert.ToString(RetrunedObjectFromDb));
                }
            }
            else
            {
                List<int> RetDoc = SearchMultibleWords(CorrectWord);

                string ReturnedLink = "";
                for (int i = 0; i < RetDoc.Count(); i++)
                {
                    cmd = new SqlCommand("select Link from InfoRet.dbo.IRDPages where id  = @id ", conn);
                    cmd.Parameters.AddWithValue("@id", RetDoc[i]);
                    RetrunedObjectFromDb = cmd.ExecuteScalar();
                    ReturnedLink = System.Convert.ToString(RetrunedObjectFromDb);
                    ListOfResults.Items.Add(ReturnedLink);
                }
            }
            
        }
        catch
        {
        }

        

        
    }
}