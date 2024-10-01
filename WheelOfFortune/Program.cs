using System.Text;
using WheelOfFortune.Models;

namespace WheelOfFortune
{
    internal class Program
    {
        static PhraseHandler phraseHandler = new PhraseHandler();
        static Random random = new Random();
        static Wheel wheel = new Wheel();
        static List<string> guessedLetters = new List<string>();
        static List<string> guessedVowels = new List<string>();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            string keepPlaying = "y";
            while (keepPlaying == "y")
            {
                //gameplay begins below this line.
                int playerGameBank = 0;

                //rounds 1 and 2 are the first switch case
                int roundCounter = 0;
                //this while loop handles the first two rounds
                while (roundCounter < 3)
                {
                    roundCounter++;
                    int secretPhraseInt = random.Next(1, PhraseHandler.secretPhrases.Length);//gets random number to select the secret phrase
                    string secretPhraseCamelCase = PhraseHandler.secretPhrases[secretPhraseInt];//gets the secret phrase using the random number
                    Console.WriteLine(secretPhraseCamelCase); // For debugging
                    string secretPhrase = secretPhraseCamelCase.ToLower();
                    string hiddenPhrase = GetHiddenPhrase(secretPhrase);//changes secret phrase to dashes
                    string displayHiddenPhrase = DisplayHiddenPhrase(hiddenPhrase);//adds spaces between the dashes
                    Console.WriteLine(displayHiddenPhrase);
                    int playerRoundBank = 0;
                    bool puzzleComplete = false;
                    //need to do logic of guessing a spin
                    Console.WriteLine($"This Round's Phrase is {displayHiddenPhrase}");
                    StringBuilder displayPhrase = new();
                    char[] displayWordArray = displayHiddenPhrase.ToArray();


                    while (!puzzleComplete)
                    {

                        Console.Write("What would the player like to do (spin/vowel/guess): ");
                        string playerChoice = Console.ReadLine().ToLower();
                        switch (playerChoice)
                        {
                            case "spin":

                                int wheelSpinAmount = SpinTheWheel(playerRoundBank);
                                if (wheelSpinAmount == 0)
                                {
                                    playerRoundBank = 0;
                                    Console.WriteLine("BANKRUPT!");
                                    break;
                                }

                                else if (wheelSpinAmount == 1)
                                {
                                    Console.WriteLine("Lose a turn!");
                                    break;
                                }
                                else
                                {
                                    bool goodGuess = false;
                                    while (goodGuess == false)
                                    {
                                        Console.WriteLine("Letters guessed: " + string.Join(", ", guessedLetters));
                                        Console.Write("Please guess a consonant: ");
                                        string playerGuess = Console.ReadLine().ToLower();
                                        if (guessedLetters.Contains(playerGuess))
                                        {
                                            Console.WriteLine($"You already guessed {playerGuess}. Try Again.");
                                            continue;
                                        }
                                        if (playerGuess != "b" && playerGuess != "c" && playerGuess != "d" && playerGuess != "f" &&
                                            playerGuess != "g" && playerGuess != "h" && playerGuess != "j" && playerGuess != "k" &&
                                            playerGuess != "l" && playerGuess != "m" && playerGuess != "n" && playerGuess != "p" &&
                                            playerGuess != "q" && playerGuess != "r" && playerGuess != "s" && playerGuess != "t" &&
                                            playerGuess != "v" && playerGuess != "w" && playerGuess != "x" &&
                                            playerGuess != "y" && playerGuess != "z")
                                        {
                                            Console.WriteLine("invalid guess. Please guess again.");
                                        }
                                        else
                                        {
                                            //consonant logic
                                            //add letter to the guessedLetters list/bank
                                            Console.WriteLine($"your guess is {playerGuess}");
                                            guessedLetters.Add(playerGuess);
                                            //this is where i need to compare the guess against the phrase
                                           // bool correctGuess = false;
                                            //StringBuilder displayPhrase = new();
                                            int letterInPhrase = 0;
                                            if (secretPhrase.Contains(playerGuess))
                                            {
                                                for (int i = 0; i < secretPhrase.Length; i++)
                                                {
                                                    if (playerGuess == secretPhrase[i].ToString())
                                                    {
                                                        letterInPhrase++;
                                                        displayWordArray[i * 2] = secretPhrase[i];
                                                        //correctGuess = true;
                                                    }
                                                }
                                                if (displayPhrase.Length == 0)
                                                {
                                                    foreach (char c in displayWordArray)
                                                    {
                                                        displayPhrase.Append(c);
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 0; i < displayWordArray.Length; i++)
                                                    {
                                                        displayPhrase[i] = displayWordArray[i];
                                                    }
                                                }
                                            }
                                            Console.WriteLine($" puzzle so far: {displayPhrase}");
                                            playerRoundBank = playerRoundBank + (letterInPhrase * wheelSpinAmount);
                                            Console.WriteLine($"bank: {playerRoundBank}");
                                        }
                                        goodGuess = true;
                                        break;
                                    }
                                }
                                break;
                            case "vowel":
                                if (playerRoundBank < 250)
                                {
                                    Console.WriteLine("Not Enough Money to purchase a vowel. Either Spin Or Guess the Puzzle.");
                                    break;
                                }
                                else
                                {
                                    playerRoundBank -= 250;
                                }
                                bool goodVowel = false;
                                while (goodVowel == false)
                                {
                                    Console.WriteLine("Vowels guessed: " + string.Join(", ", guessedVowels));
                                    Console.Write("Which Vowel would you like to purchase? ");
                                    string buyAVowel = Console.ReadLine().ToLower();
                                    if (guessedVowels.Contains(buyAVowel))
                                    {
                                        Console.WriteLine($"You already guessed {buyAVowel}. Try Again.");
                                        continue;
                                    }
                                    if (buyAVowel != "a" && buyAVowel != "e" && buyAVowel != "i" && buyAVowel != "o" && buyAVowel != "u")
                                    {
                                        Console.WriteLine("Please choose a vowel, not whatever that was.");
                                        continue;
                                    }
                                    Console.WriteLine($"your guess is {buyAVowel}");
                                    guessedVowels.Add(buyAVowel);
                                    if (secretPhrase.Contains(buyAVowel))
                                    {
                                        for (int i = 0; i < secretPhrase.Length; i++)
                                        {
                                            if (buyAVowel == secretPhrase[i].ToString())
                                            {
                                                
                                                displayWordArray[i * 2] = secretPhrase[i];
                                                //correctGuess = true;
                                            }
                                        }
                                        if (displayPhrase.Length == 0)
                                        {
                                            foreach (char c in displayWordArray)
                                            {
                                                displayPhrase.Append(c);
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < displayWordArray.Length; i++)
                                            {
                                                displayPhrase[i] = displayWordArray[i];
                                            }
                                        }
                                    }
                                    Console.WriteLine($" puzzle so far: {displayPhrase}");
                                    Console.WriteLine($"bank: {playerRoundBank}");
                                    goodVowel = true;
                                    break;
                                }
                                break;
                            case "guess":
                            case "solve":
                                Console.Write("Solve by typing the answer to the puzzle:  ");
                                string solveGuess = Console.ReadLine().ToLower();
                                if (secretPhrase.Contains(solveGuess))
                                {
                                    Console.WriteLine("WINNER!");
                                    playerGameBank += playerRoundBank;
                                    Console.WriteLine($"Player Round Total Winnings: {playerRoundBank}");
                                    Console.WriteLine($"Player Game Total Winiings:  {playerGameBank}");
                                    puzzleComplete = true;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Incorrect answer.");
                                    break;
                                }
                                
                            default:
                                Console.WriteLine("Please choose either spin/vowel/solve");
                                break;
                        }
                        //Actual gameplay ends above this line
                    }
                }

                //start round 3 here

            }
            bool goodAnswer = false;
            while (goodAnswer == false)
            {
                Console.Write("Would you like to play again? (y/n): ");
                string kpString = Console.ReadLine().ToLower();
                if (kpString == "y" || kpString == "n")
                {
                    keepPlaying = kpString;
                    goodAnswer = true;
                }
                else
                {
                    Console.WriteLine("please enter Y or N");
                }
            }
            Console.WriteLine("Spin again soon");
        }

        private static int SpinTheWheel(int playerBank)
        {
            int wheelSpin = Wheel.wheelAmounts[random.Next(1, Wheel.wheelAmounts.Length)];
            if (wheelSpin == 0)
            {
                Console.WriteLine("bankrupt");
                return wheelSpin;
                playerBank = 0;
                //player loses turn
            }
            else if (wheelSpin == 1)
            {
                Console.WriteLine("lost a turn");
                return wheelSpin;
                //player loses turn
            }
            else
            {
                Console.WriteLine($"Spin Amount: {wheelSpin}");
                return wheelSpin;
            }
        }
        static int SpinGuess(int spinAmount, int playerRoundBank, string secretPhrase, string displayHiddenPhrase)
        {
            bool goodGuess = false;
            while (goodGuess == false)
            {
                Console.WriteLine(string.Join(", ", guessedLetters));
                Console.Write("Please guess a consonant: ");
                string playerGuess = Console.ReadLine().ToLower();

                if (guessedLetters.Contains(playerGuess))
                {
                    Console.WriteLine($"You already guessed {playerGuess}. Try Again.");
                    continue;
                }


                if (playerGuess != "b" && playerGuess != "c" && playerGuess != "d" && playerGuess != "f" &&
                    playerGuess != "g" && playerGuess != "h" && playerGuess != "j" && playerGuess != "k" &&
                    playerGuess != "l" && playerGuess != "m" && playerGuess != "n" && playerGuess != "p" &&
                    playerGuess != "q" && playerGuess != "r" && playerGuess != "s" && playerGuess != "t" &&
                    playerGuess != "v" && playerGuess != "w" && playerGuess != "x" &&
                    playerGuess != "y" && playerGuess != "z")
                {
                    Console.WriteLine("invalid guess. Please guess again.");
                }
                else
                {
                    //consonant logic
                    //add letter to the guessedLetters list/bank
                    Console.WriteLine($"your guess is {playerGuess}");
                    guessedLetters.Add(playerGuess);
                    //this is where i need to compare the guess against the phrase
                    bool correctGuess = false;
                    char[] displayWordArray = displayHiddenPhrase.ToArray();
                    int letterInPhrase = 0;
                    if (secretPhrase.Contains(playerGuess))
                    {
                        for (int i = 0; i < secretPhrase.Length; i++)
                        {
                            if (playerGuess == secretPhrase[i].ToString())
                            {
                                letterInPhrase++;
                                displayWordArray[i] = secretPhrase[i];
                                correctGuess = true;
                            }
                        }
                    }
                    string displayPhrase = displayWordArray.ToString();
                    Console.WriteLine(displayPhrase);
                    playerRoundBank = playerRoundBank + (letterInPhrase * spinAmount);
                    return playerRoundBank;
                }
                goodGuess = true;
                break;
            }
            return playerRoundBank;
        }


        public static string GetHiddenPhrase(string secretPhrase)
        {
            StringBuilder hiddenPhrase = new StringBuilder();
            foreach (char c in secretPhrase)
            {
                if (c != ' ')
                {
                    hiddenPhrase.Append('_');
                }
                else
                {
                    hiddenPhrase.Append(' ');
                }
            }
            return hiddenPhrase.ToString();
        }
        public static string DisplayHiddenPhrase(string hiddenPhrase)
        {
            string displayPhrase = "";
            foreach (char c in hiddenPhrase)
            {
                displayPhrase += c + " ";
            }
            return displayPhrase;
        }

    }
}
