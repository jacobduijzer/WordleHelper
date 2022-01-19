open System
open WordleHelper

let main =
    Console.Clear()
    let allWords = WordleSolver.readWordsFromFile "words"
    let mutable uniqueCharacterWords = WordleSolver.wordsWithUniqueCharacters allWords
    
    for i in [1..6] do
        Console.WriteLine($"Step {i}")
        Console.WriteLine($"Words available {uniqueCharacterWords.Length} words")
        let startWord = WordleSolver.selectRandomWord uniqueCharacterWords
        Console.WriteLine $"Please enter word: {startWord}"
        let firstResult = WordleSolver.enterWordScore startWord
        uniqueCharacterWords <- WordleSolver.filterList uniqueCharacterWords firstResult
        Console.WriteLine()

    0