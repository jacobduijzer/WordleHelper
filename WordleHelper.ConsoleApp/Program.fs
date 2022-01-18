open System
open System.IO
open WordleHelper

let allWords = WordleSolver.allWords "words"
let uniqueCharacterWords = WordleSolver.wordsWithUniqueCharacters allWords
let startWord = WordleSolver.randomWord uniqueCharacterWords

Console.WriteLine($"Start with word: {startWord}")

let testResult = WordleSolver.testWord startWord 

Console.WriteLine("Bye")
 
// let startword = WordleSolver.randomWord 
    