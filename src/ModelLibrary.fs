module App.ModelLibrary
open System

[<Struct; StructuralEquality; CustomComparison>]
type integer =
    | Infinite
    | Int of value: int
    interface IComparable<integer> with
        member self.CompareTo other =
            match self, other with
            | Infinite, Infinite -> 0
            | Infinite, _ -> 1
            | _, Infinite -> -1
            | Int a, Int v -> compare a v
    interface IComparable with
        member self.CompareTo (object: obj) =
            match object with
            | :? integer as right -> compare self right
            | _ -> invalidArg "object" "not an integer"
            
    static member (+) (left, right) =
        match left, right with
        | Infinite, _ | _, Infinite -> Infinite
        | Int v, Int w -> Int (w + v)
    static member (-) (left, right) =
        match left, right with
        | Infinite, Int v -> Infinite
        | Int v, Int w -> Int (v - w)
        | _ -> invalidOp "Subtraction resulted in arithmetic error. This means you attempted to subtract infinity from something else (possibly infinity from infinity)."
    static member (*) (left, right) =
        match left, right with
        | Infinite, _ | _, Infinite -> Infinite
        | Int v, Int w -> Int (v * w)
    static member (/) (left, right) =
        match left, right with
        | Infinite, Int v -> Infinite
        | Int v, Infinite -> Int 0
        | Int v, Int w -> Int (v / w)
        | Infinite, Infinite -> invalidOp "Cannot divide infinity by infinity"
        
    

[<Struct; StructuralEquality; StructuralComparison>]
type color =
    | Red
    | Green
    | Blue
    | Yellow
    | Orange
    | Purple
    | White
    | Other of html: string
    static member color (red: byte) (green: byte) (blue: byte) =
        sprintf "#%s%s%s" (red.ToString("0X")) (green.ToString("0X")) (blue.ToString("0X"))

