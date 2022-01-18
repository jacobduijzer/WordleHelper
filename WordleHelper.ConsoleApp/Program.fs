open System
open WordleHelper

let main =
    Console.Clear()
    let allWords = WordleSolver.allWords "words"
    let mutable uniqueCharacterWords = WordleSolver.wordsWithUniqueCharacters allWords
    
    for i in [0..5] do
        Console.WriteLine($"Step {i}")
        Console.WriteLine($"Words available {uniqueCharacterWords.Length} words")
        let startWord = WordleSolver.randomWord uniqueCharacterWords
        Console.WriteLine $"Please enter word: {startWord}"
        let firstResult = WordleSolver.testWord startWord
        uniqueCharacterWords <- WordleSolver.filterList uniqueCharacterWords firstResult
        Console.WriteLine()

    0