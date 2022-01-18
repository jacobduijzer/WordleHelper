module WordleHelper.WordleSolver

open System
open System.IO
open System.IO.Compression

// TODO: Remove fuckup when only allowing valid input
type CharacterScore =
   | DoesNotExist = 0
   | ExistsButOtherPosition = 1
   | ExistsCorrectPosition = 2
   | Fuckup = 3
   
type charResult = {Character:char
                   Score:CharacterScore}
   
let validCommand cmd = [ 'd'; 'o'; 'c' ] |> List.contains cmd

// TODO: only allow valid characters
let testCharacter n =
    Console.Write $"(D)oes not exist, Exists but (O)ther location, Exists (C)orrect position: {n}"
    let key =
        match Console.ReadKey().KeyChar with
        | 'd' -> (n, CharacterScore.DoesNotExist)
        | 'o' -> (n, CharacterScore.ExistsButOtherPosition)
        | 'c' -> (n, CharacterScore.ExistsCorrectPosition)
        | _ -> (n, CharacterScore.Fuckup)
    key

let testWord (word:string) =
    word.ToCharArray()
    |> Array.map (testCharacter)

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
    
let hasWordCharacters word characters =
    word
    |> String.forall (fun x -> not <| Array.contains x characters)

let filterList (words:string[]) (characters:char[]) =
    words
    |> Array.filter (fun word -> hasWordCharacters word characters)

let randomWord words =
    words
    |> Seq.sortBy(fun _ -> Random().Next())
    |> Seq.head
    
    