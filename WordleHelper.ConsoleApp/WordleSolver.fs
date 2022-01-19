module WordleHelper.WordleSolver

open System
open System.IO
open System.Text
open System.Text.RegularExpressions
open Domain

let readWordsFromFile inputFile =
    File.ReadAllLines inputFile
    
let selectRandomWord words =
    words
    |> Seq.sortBy(fun _ -> Random().Next())
    |> Seq.head
    
let rec enterCharacterScore character =
    Console.WriteLine()
    Console.Write $"Does (n)ot exist, (o)ther position, (c)orrect position => '{character}' : "
    let input = Console.ReadKey().KeyChar
    match input with
    | 'n' -> {Character = character; Score = CharacterScore.DoesNotExist}
    | 'o' -> {Character = character; Score = CharacterScore.OtherPosition}
    | 'c' -> {Character = character; Score = CharacterScore.CorrectPosition}
    | _ -> enterCharacterScore character

let enterWordScore (word:string) =
    word.ToCharArray()
    |> Array.map enterCharacterScore

let countCharacterOccurrences (word:string) =
    word.ToLower().ToCharArray()
    |> Array.mapi(fun n g -> (n, g))
    |> Array.groupBy(fun (_,n)->  n)
    |> Array.filter(fun(_,n) -> n.Length > 1)
    
let wordsWithUniqueCharacters words =
    words
    |> Seq.filter(fun word -> (countCharacterOccurrences word).Length = 0)
    |> Seq.toArray
    
let doesWordContainCharacters word characters =
    word
    |> String.forall (fun x -> not <| Array.contains x characters)
    
let regexBuilder (result:WordResult[]) : string =
  let regex = (
    let sb = StringBuilder()
    for item in result do
        (match item.Score with
        | CharacterScore.CorrectPosition -> sb.Append(item.Character)
        | CharacterScore.OtherPosition -> sb.Append($"[^{item.Character}]")
        | _ -> sb.Append("[a-z]")) |> ignore
    sb.ToString() )
  regex

let filterList (words:string[]) (result:WordResult[]) =
    let nonExistingCharacters =
        result
        |> Array.filter (fun item -> item.Score = CharacterScore.DoesNotExist)
        |> Array.map (fun item -> item.Character)
    let filteredWords =
        words
        |> Array.filter (fun word -> doesWordContainCharacters word nonExistingCharacters)
    let regex = regexBuilder result
    filteredWords
    |> Array.filter (fun word -> (Regex(regex, RegexOptions.Compiled).Match word).Success = true)


    
    