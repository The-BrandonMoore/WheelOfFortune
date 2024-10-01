using System.Security.Cryptography;

namespace WheelOfFortune.Models
{
    public class PhraseHandler
    {
        public static string[] secretPhrases = new string[]
            {
            "A blessing in disguise",
            "A dime a dozen",
            "Beat around the bush",
            "Better late than never",
            "Bite the bullet",
            "Break the ice",
            "Call it a day",
            "Cutting corners",
            "Easy come easy go",
            "Hit the nail on the head",
            "It takes two to tango",
            "Let the cat out of the bag",
            "Miss the boat",
            "No pain no gain",
            "On the ball",
            "Once in a blue moon",
            "Piece of cake",
            "Spill the beans",
            "Take it with a grain of salt",
            "Under the weather",
            "When pigs fly",
            "Your guess is as good as mine"
            };

        //static Random random = new Random();
        //static private int secretPhraseInt = random.Next(1, secretPhrases.Length);
        //public string secretPhrase = secretPhrases[secretPhraseInt];
    }
}
