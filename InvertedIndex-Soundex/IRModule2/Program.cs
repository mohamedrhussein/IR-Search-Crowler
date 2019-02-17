using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;
namespace IRModule2
{
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
    class StopWords
    {
        static List<TermDetail> terms = new List<TermDetail>();
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



        public static List<TermDetail> RemoveStopwords(List<TermDetail> t)
        {
            for (int i = 0; i < t.Count(); i++)
            {
                if (!stop_words.ContainsKey(t[i].name))
                {
                    terms.Add(t[i]);
                }
            }
            return terms;
        }

    }

    class TermDetail
    {
        public string name;
        public string doc_id;
        public string frequency;
        public string position;


        public TermDetail()
        {
            position = "";
            doc_id = "";
            frequency = "";
            name = "";
        }
    }

    class GramDetail
    {
        public string Gram;
        public string AllWords;


        public GramDetail()
        {
            Gram = "";
            AllWords = "";
            
          
        }
    }
    class Program
    {
        //Remove any punctuation character from the word (i.e. dot, !, ?...etc)
        static string[] SplitWords(string s)
        {
            return Regex.Matches(s, "\\w+('(s|d|t|ve|m))?")
        .   Cast<Match>().Select(x => x.Value).ToArray();
      
        }

        //Calculate Soundex
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

        static void Main(string[] args)
        {
            //Database Connection String
            string connection_string = @"Data Source=HazemMarawan\SqlExpress;Initial Catalog=InfoRet;Integrated Security=True";

            //Object that has the Returned Value from DB
            Object RetrunedObjectFromDb;

            //Returned Document From DB after Casting it To String
            string ReturnedDocument;

            //Position of Every Term
            int PostionOfWord = 0;

            TermDetail T;
            //Object From Stemmer Class To Apply Stemming
            Stemmer ApplyStemming = new Stemmer();

            //Dictionary To Check If Term Existed Or Not
            Dictionary<string, int> FastIndexOfWords = new Dictionary<string, int>();


            //List Of Terms That Inserted In Inverted Index
            List<TermDetail> AllTerms = new List<TermDetail>();

            //Sql Connection using Connection String
            SqlConnection conn = new SqlConnection(connection_string);
            //Sql Command used in Insertion and Selection
            SqlCommand cmd;
         
            //Open Connection
            conn.Open();

            //Index to Connect Dictionary With All Terms List
            int IndexOfWordInList = 0;

            //Empty String
            string Empty = "";

            //Gram Object
            GramDetail Gram = new GramDetail();

            //List of All Grams To Be Inserted
            List<GramDetail> AllGrams = new List<GramDetail>();

            //List Of Terms Before Stemming
            List<string> AllTermsBeforeStemming = new List<string>();

            //List Of Terms For Soundex
            List<string> TermsForSoundx = new List<string>();

            //List Of Soundex
            List<string> ListOfAllSoundex = new List<string>();


            //List Of All Grams
            List<string> AllGramesFounded = new List<string>();


            //Option
            string Option;
            Console.WriteLine("1-Build Inverted Index" + "\n" + "2-Build BiGram and it's Terms"+"\n"+"3-Build Soundex and it's Terms" );
            Console.Write("Your Choice: ");
            Option = Console.ReadLine();
            Console.Clear();
            //Loop in All Documents
            if (Option.Equals("1"))
            {
                for (int i = 1; i <= 3052; i++)
                {
                    cmd = new SqlCommand("Select P_Content from [InfoRet].[dbo].[IRDPages] where id  = @id ", conn);
                    cmd.Parameters.AddWithValue("@id", i);
                    RetrunedObjectFromDb = cmd.ExecuteScalar();
                    ReturnedDocument = System.Convert.ToString(RetrunedObjectFromDb);
                    PostionOfWord = 1;
                    string[] AllWordsAfterSplit = SplitWords(ReturnedDocument);

                    foreach (string Word in AllWordsAfterSplit)
                    {

                        string LowerCaseOfWord = Word.ToLower();
                        //Check Term if Empty Of Not
                        if (LowerCaseOfWord != Empty)
                        {
                            if (FastIndexOfWords.Keys.Contains(LowerCaseOfWord))
                            {
                                //Same Doc
                                if (AllTerms[FastIndexOfWords[LowerCaseOfWord]].doc_id.Contains(i.ToString()))
                                {
                                    AllTerms[FastIndexOfWords[LowerCaseOfWord]].position += "," + PostionOfWord;

                                }
                                //Diffrent Doc
                                else if (!AllTerms[FastIndexOfWords[LowerCaseOfWord]].doc_id.Contains(i.ToString()))//if It repeated in another Document change docId,Freq,Pos
                                {
                                    AllTerms[FastIndexOfWords[LowerCaseOfWord]].doc_id += "," + i.ToString();
                                    AllTerms[FastIndexOfWords[LowerCaseOfWord]].position += "@" + PostionOfWord;

                                }

                            }
                            //Create New Term
                            else
                            {
                                T = new TermDetail();
                                T.name = LowerCaseOfWord;
                                T.doc_id += i.ToString();
                                T.position += PostionOfWord.ToString();
                                AllTerms.Add(T);
                                FastIndexOfWords.Add(T.name, IndexOfWordInList);
                                IndexOfWordInList = IndexOfWordInList + 1;


                            }

                        }

                        PostionOfWord++;

                    }
                    Console.Clear();
                    Console.WriteLine("Read Documents");
                    Console.WriteLine("Doc Num: " + i);

                }

                Console.WriteLine("Remove Stop Words");
                //Remove Stop Words
                AllTerms = StopWords.RemoveStopwords(AllTerms);

                ////Save a copy of them (term, doc_id) to be used to build the k-gram index and soundex index (as a dictionary)
                for (int j = 1; j <= AllTerms.Count; j++)
                {
                    cmd = new SqlCommand("insert into [InfoRet].[dbo].[AllWordsIR] ([term],[doc_id]) values(@term,@doc_id)", conn);

                    cmd.Parameters.AddWithValue("@term", AllTerms[j - 1].name);
                    cmd.Parameters.AddWithValue("@doc_id", AllTerms[j - 1].doc_id);
                    cmd.ExecuteNonQuery();
                }


                Console.WriteLine("Stemming");
                //Apply Stemmig Using Porter Stemming Class
                for (int j = 0; j < AllTerms.Count(); j++)
                {
                    AllTerms[j].name = ApplyStemming.stem(AllTerms[j].name);

                }
                Console.WriteLine("Remove Repettion");
                for (int i = 0; i < AllTerms.Count() - 1; i++)
                {
                    for (int j = i + 1; j < AllTerms.Count(); j++)
                    {

                        if (AllTerms[i].name == AllTerms[j].name)
                        {
                            if (AllTerms[i].doc_id != AllTerms[j].doc_id)
                            {
                                AllTerms[i].doc_id += "," + AllTerms[j].doc_id;
                                AllTerms[i].position += "@" + AllTerms[j].position;
                            }
                            else
                            {

                                AllTerms[i].position += "," + AllTerms[j].position;
                            }
                            AllTerms.Remove(AllTerms[j]);
                        }
                    }
                    Console.WriteLine(i);
                }

                
                int NumOfComma = 0;
                Console.WriteLine("Save In Inverted Index");
                //Save Words in Inverted Index
                for (int j = 1; j <= AllTerms.Count(); j++)
                {
                    cmd = new SqlCommand("insert into [InfoRet].[dbo].[FinalInvertedIndex] ([id],[term],[doc_id],[frequency],[positions]) values(@id,@term,@doc_id,@frequency,@positions)", conn);
                    cmd.Parameters.AddWithValue("@id", j);
                    cmd.Parameters.AddWithValue("@term", AllTerms[j - 1].name);
                    cmd.Parameters.AddWithValue("@doc_id", AllTerms[j - 1].doc_id);
                    cmd.Parameters.AddWithValue("@positions", AllTerms[j - 1].position);
                    string[] Positions = AllTerms[j - 1].position.Split('@');
                    
                    foreach (var term in Positions)
                    {
                        NumOfComma = 0;
                        for (int i = 0; i < term.Length; i++)
                        {
                            if (term[i] == ',')
                                NumOfComma++;
                        }

                        AllTerms[j - 1].frequency += ',' + (NumOfComma + 1).ToString();
                        
                    }
                    AllTerms[j - 1].frequency.Remove(0, 1);
                    cmd.Parameters.AddWithValue("@frequency", AllTerms[j - 1].frequency);

                    cmd.ExecuteNonQuery();

                }
            }
            else if(Option.Equals("2"))
            {
                string NumOfWords;
                int    NumOfWordsBeforeStem; 
                string SGram;
                cmd = new SqlCommand("select COUNT(*) from InfoRet.dbo.AllWordsIR", conn);
                RetrunedObjectFromDb = cmd.ExecuteScalar();
                NumOfWords = System.Convert.ToString(RetrunedObjectFromDb);
                NumOfWordsBeforeStem = int.Parse(NumOfWords);


                //Get All Words Before Steming
                Console.WriteLine("Get All Words Before Steming");
                for (int i = 1; i <= NumOfWordsBeforeStem; i++)
                {
                    cmd = new SqlCommand("select term from InfoRet.dbo.AllWordsIR where id  = @id ", conn);
                    cmd.Parameters.AddWithValue("@id", i);
                    RetrunedObjectFromDb = cmd.ExecuteScalar();
                    
                    AllTermsBeforeStemming.Add(System.Convert.ToString(RetrunedObjectFromDb));
                }

                //Get Grams
                Console.WriteLine("Get Grams");
                for (int i = 0; i < NumOfWordsBeforeStem; i++)
                {
                    SGram = "$" + AllTermsBeforeStemming[i] + "$";
                    for (int j = 0; j < SGram.Length - 1; j++)
                    {
                        if (!AllGramesFounded.Contains(SGram[j].ToString() + SGram[j + 1].ToString()))
                            AllGramesFounded.Add(SGram[j].ToString() + SGram[j + 1].ToString());

                    }

                }
               
                Dictionary<string, string> GramAndTerms = new Dictionary<string, string>();
                //Get Terms of Grams
                Console.WriteLine("Get Terms of Grams");
                for (int i = 0; i < AllGramesFounded.Count(); i++)
                {
                    string GramSearch;
                    string AllWordsRelatedToGram = "";

                    if (AllGramesFounded[i][0] == '$')
                    {
                        GramSearch = AllGramesFounded[i][1].ToString();
                        for (int j = 0; j < AllTermsBeforeStemming.Count(); j++)
                        {
                            if (AllTermsBeforeStemming[j][0]==GramSearch[0])
                            {
                                AllWordsRelatedToGram += "," + AllTermsBeforeStemming[j];
                            }
                        }

                    }
                    else if (AllGramesFounded[i][1] == '$')
                    {
                        GramSearch = AllGramesFounded[i][0].ToString();
                        for (int j = 0; j < AllTermsBeforeStemming.Count(); j++)
                        {
                            if (AllTermsBeforeStemming[j][AllTermsBeforeStemming[j].Length-1]==GramSearch[0])
                            {
                                AllWordsRelatedToGram += "," + AllTermsBeforeStemming[j];
                            }
                        }
                    }
                    else
                    {
                        GramSearch = AllGramesFounded[i];
                        for (int j = 0; j < AllTermsBeforeStemming.Count(); j++)
                        {
                            if (AllTermsBeforeStemming[j].Contains(GramSearch))
                            {
                                AllWordsRelatedToGram += "," + AllTermsBeforeStemming[j];
                            }
                        }
                    }
                    AllWordsRelatedToGram = AllWordsRelatedToGram.Remove(0, 1);
                    if (!GramAndTerms.ContainsKey(AllGramesFounded[i]))
                    {
                        GramAndTerms.Add(AllGramesFounded[i], AllWordsRelatedToGram);
                    }
                   


                }

                


                //Save in K-Gram Table
                Console.Write("Save in K-Gram Table");
                for (int i = 0; i < GramAndTerms.Count(); i++)
                {
                    string kgram = GramAndTerms.ElementAt(i).Key.ToString();
                    string terms = GramAndTerms.ElementAt(i).Value.ToString();
                    cmd = new SqlCommand("insert into [InfoRet].[dbo].[BiGram] ([k_gram],[terms]) values(@kgram,@terms)", conn);
                    cmd.Parameters.AddWithValue("@kgram", kgram);
                    cmd.Parameters.AddWithValue("@terms", terms);
                    cmd.ExecuteNonQuery();

                }
                Console.WriteLine();
            }
            else if (Option.Equals("3"))
            {
                string NumOfWordsBeforeStemming;
                int NumofAllWords;
                cmd = new SqlCommand("select COUNT(*) from InfoRet.dbo.AllWordsIR", conn);
                RetrunedObjectFromDb = cmd.ExecuteScalar();
                NumOfWordsBeforeStemming = System.Convert.ToString(RetrunedObjectFromDb);
                NumofAllWords = int.Parse(NumOfWordsBeforeStemming);


                //Get All Words Before Steming
                Console.WriteLine("Get All Words Before Steming");
                for (int i = 1; i <= NumofAllWords; i++)
                {
                    cmd = new SqlCommand("select term from InfoRet.dbo.AllWordsIR where id  = @id ", conn);
                    cmd.Parameters.AddWithValue("@id", i);
                    RetrunedObjectFromDb = cmd.ExecuteScalar();
                    TermsForSoundx.Add(System.Convert.ToString(RetrunedObjectFromDb));

                }

                //Calculate Soundex
                Console.WriteLine("Calculate Soundex for each Term");
                for (int i = 0; i < TermsForSoundx.Count(); i++)
                {
                    
                    string CurSoundex=Soundex(TermsForSoundx[i]);
                    if(!ListOfAllSoundex.Contains(CurSoundex))
                    {
                        ListOfAllSoundex.Add(CurSoundex);
         
                    }
                }

              
                Dictionary<string,string> SoudexAndTerms=new Dictionary<string,string>();

                //Get Terms related to Soundex
                Console.WriteLine("Get Terms related to Soundex");
                for (int i = 0; i < ListOfAllSoundex.Count(); i++)
                {
                    string AllWordsRelatedToSoundex = "";
                    for (int j = 0; j < TermsForSoundx.Count(); j++)
                    {
                        if(ListOfAllSoundex[i]==Soundex(TermsForSoundx[j]))
                        {
                            AllWordsRelatedToSoundex+=","+TermsForSoundx[j];
                        }
                    }
                    AllWordsRelatedToSoundex=AllWordsRelatedToSoundex.Remove(0,1);
                    if(!SoudexAndTerms.ContainsKey(ListOfAllSoundex[i]))
                    {
                        SoudexAndTerms.Add(ListOfAllSoundex[i], AllWordsRelatedToSoundex);
                    }
                   
                }

                Console.Write("Save in Soundex Table");
                for (int i = 0; i < SoudexAndTerms.Count(); i++)
                {
                    string soundex = SoudexAndTerms.ElementAt(i).Key.ToString();
                    string terms = SoudexAndTerms.ElementAt(i).Value.ToString();
                    cmd = new SqlCommand("insert into [InfoRet].[dbo].[Soundex] ([soundex],[term]) values(@soundex,@terms)", conn);
                    cmd.Parameters.AddWithValue("@soundex", soundex);
                    cmd.Parameters.AddWithValue("@terms", terms);
                    cmd.ExecuteNonQuery();

                }
                Console.WriteLine();



                




            }





        }
    }
}