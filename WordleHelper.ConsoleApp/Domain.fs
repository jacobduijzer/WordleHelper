namespace Domain

type CharacterScore =
   | DoesNotExist = 0
   | OtherPosition = 1
   | CorrectPosition = 2
   
type WordResult = {Character:char
                   Score:CharacterScore}