module App.Update
open Elmish
open App.Model
open App.Message
let update msg model =
    match msg with
    | ChangeTitle t -> {model with title = t}, Cmd.none
    | ChangeAuthor a -> {model with author = a}, Cmd.none
    | ChangeDayLength d when d >= 12 -> {model with dayLength = d}, Cmd.none
    | ChangeDayLength _ -> model, Cmd.none
    | ChangeNightLength n when n >= 12 -> {model with nightLength = n}, Cmd.none
    | ChangeNightLength _ -> model, Cmd.none
    | ChangeLynchType l -> {model with lynchType = l}, Cmd.none