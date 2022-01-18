module WordleHelper.WordleSolver

open System
open System.IO
open System.Text
open System.Text.RegularExpressions
open Microsoft.VisualBasic.CompilerServices

type CharacterScore =
   | DoesNotExist = 0
   | OtherPosition = 1
   | CorrectPosition = 2
   
type WordResult = {Character:char
                   Score:CharacterScore}

let rec testCharacter character =
    Console.WriteLine()
    Console.Write $"(d)oes not exist, (o)ther position, (c)orrect position => '{character}' : "
    let input = Console.ReadKey().KeyChar
    match input with
    | 'd' -> {Character = character; Score = CharacterScore.DoesNotExist}
    | 'o' -> {Character = character; Score = CharacterScore.OtherPosition}
    | 'c' -> {Character = character; Score = CharacterScore.CorrectPosition}
    | _ -> testCharacter character

let testWord (word:string) =
    word.ToCharArray()
    |> Array.map testCharacter

let countDoubleCharacters (word:string) =
    word.ToLower().ToCharArray()
    |> Array.mapi(fun n g -> (n, g))
    |> Array.groupBy(fun (_,n)->  n)
    |> Array.filter(fun(_,n) -> n.Length > 1)
    
let allWords inputFile =
    File.ReadAllLines inputFile
    
let wordsWithUniqueCharacters words =
    words
    |> Seq.filter(fun word -> (countDoubleCharacters word).Length = 0)
    |> Seq.toArray
    
let hasWordCharacters word characters =
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
        |> Array.filter (fun word -> hasWordCharacters word nonExistingCharacters)
    let regex = regexBuilder result
    filteredWords
    |> Array.filter (fun word -> (Regex(regex, RegexOptions.Compiled).Match word).Success = true)

let randomWord words =
    words
    |> Seq.sortBy(fun _ -> Random().Next())
    |> Seq.head
    
    