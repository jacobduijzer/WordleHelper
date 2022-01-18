module WordleHelper.UnitTests.WordleSolverShould

open WordleHelper
open WordleHelper.WordleSolver
open Xunit

[<Theory>]
[<InlineData("Test", false)>]
[<InlineData("Tesa", true)>]
[<InlineData("ABCDabcd", false)>]
[<InlineData("ABCDefgh", true)>]
let ``Test if all characters are unique`` (input: string, allUnique: bool) =
    let result = WordleSolver.countDoubleCharacters input
    Assert.NotNull(result)
    Assert.Equal(allUnique, result.Length = 0)

[<Fact>]
let ``Get only words with unique characters from list`` () =
    let allWords = WordleSolver.allWords "words"

    let result =
        WordleSolver.wordsWithUniqueCharacters allWords

    Assert.NotNull(result)
    Assert.Equal(3440, Seq.length result)

[<Fact>]
let ``Return one random word with unique characters`` () =
    let allWords = WordleSolver.allWords "words"

    let words =
        WordleSolver.wordsWithUniqueCharacters allWords

    let result = WordleSolver.randomWord words
    Assert.NotNull(result)
    Assert.IsType<string>(result)

[<Fact>]
let ``Filter list of words to return only words without characters`` () =
    let allWords = [| "word1"; "word2"; "test" |]
    let characters = [| 'e'; 't' |]

    let result =
        WordleSolver.filterList allWords characters

    Assert.NotNull(result)

[<Fact>]
let ``Filter word list, remove words with non-existing characters`` () =
    let allWords = [| "word1"; "word2"; "test" |]

    let characterScore =
        [| { Character = 'e'
             Score = CharacterScore.DoesNotExist }
           { Character = 't'
             Score = CharacterScore.DoesNotExist } |]

    let characters =
        characterScore
        |> Array.map (fun a -> a.Character)
    
    let result =
        WordleSolver.filterList allWords characters

    Assert.NotNull result
    Assert.Equal(2, result.Length)

[<Fact>]
let ``Filter word list`` () =
    let allWords = [| "word1"; "word2"; "event"; "eatin" |]

    let characterScore =
        [| { Character = 'e'
             Score = CharacterScore.ExistsCorrectPosition}
           { Character = 't'
             Score = CharacterScore.ExistsButOtherPosition}
           { Character = 'a'
             Score = CharacterScore.DoesNotExist}
           { Character = 'b'
             Score = CharacterScore.DoesNotExist}
           { Character = 'c'
             Score = CharacterScore.DoesNotExist} |]

    let characters =
        characterScore
        |> Array.map (fun a -> a.Character)
    
    let result =
        WordleSolver.filterList allWords characters

    Assert.NotNull result
    Assert.Equal(2, result.Length)
