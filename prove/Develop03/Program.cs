using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScriptureHideAndSeek
{
    class Reference
    {
        public string Book { get; }
        public int Chapter { get; }
        public int VerseStart { get; }
        public int? VerseEnd { get; }

        public Reference(string book, int chapter, int verseStart, int? verseEnd = null)
        {
            Book = book;
            Chapter = chapter;
            VerseStart = verseStart;
            VerseEnd = verseEnd;
        }

        public override string ToString()
        {
            if (VerseEnd.HasValue)
            {
                return $"{Book} {Chapter}:{VerseStart}-{VerseEnd}";
            }
            else
            {
                return $"{Book} {Chapter}:{VerseStart}";
            }
        }
    }

    class Word
    {
        public string Text { get; }
        public bool Hidden { get; set; }

        public Word(string text)
        {
            Text = text;
            Hidden = false;
        }

        public void Hide()
        {
            Hidden = true;
        }

        public void Show()
        {
            Hidden = false;
        }

        public override string ToString()
        {
            return Hidden ? "*****" : Text;
        }
    }

    class Scripture
    {
        private List<Word> words;

        public Reference Reference { get; }
        public IReadOnlyList<Word> Words => words;

        public Scripture(Reference reference, string text)
        {
            Reference = reference;
            words = text.Split(' ').Select(wordText => new Word(wordText)).ToList();
        }

        public void HideRandomWord()
        {
            var visibleWords = words.Where(word => !word.Hidden).ToList();
            if (visibleWords.Any())
            {
                var wordToHide = visibleWords[new Random().Next(visibleWords.Count)];
                wordToHide.Hide();
            }
        }

        public bool IsCompletelyHidden()
        {
            return words.All(word => word.Hidden);
        }

        public string GetRenderedText()
        {
            return string.Join(" ", words);
        }

        public void Display()
        {
            Console.Clear();
            Console.WriteLine(Reference);
            Console.WriteLine(GetRenderedText());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var scriptureFilePath = "scripture.txt";
            if (!File.Exists(scriptureFilePath))
            {
                Console.WriteLine("Scripture file not found.");
                return;
            }

            var scriptureLines = File.ReadAllLines(scriptureFilePath);
            var reference = new Reference("John", 3, 16);
            var scripture = new Scripture(reference, string.Join(" ", scriptureLines));

            while (!scripture.IsCompletelyHidden())
            {
                scripture.Display();
                Console.WriteLine("Press Enter to hide words or type 'quit' to exit: ");
                var userInput = Console.ReadLine();
                if (userInput.ToLower() == "quit")
                {
                    break;
                }

                scripture.HideRandomWord();
            }

            Console.WriteLine("All words hidden. Program ended.");
        }
    }
}
//KEVIN