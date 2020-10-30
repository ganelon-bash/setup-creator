module App.LynchType

[<Struct; StructuralEquality; StructuralComparison>] 
type LynchType =
    | Plurality
    | Majority
    | PluralityMajority
    | Condorcet
    | Kingmaker
    | WeightedPlurality
    | Other of Description: string
    static member toString = function
        | PluralityMajority -> "Plurality with Majority"
        | WeightedPlurality -> "Weighted Plurality"
        | Other d -> d
        | a -> sprintf "%A" a
    static member ofString = function
        | "Majority" -> Majority
        | "PluralityMajority" -> PluralityMajority
        | "Plurality" -> Plurality
        | "Condorcet" -> Condorcet
        | "Kingmaker" -> Kingmaker
        | "WeightedPlurality" -> WeightedPlurality
        | a -> Other a

