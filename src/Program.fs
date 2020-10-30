open App.Model
open App.View
open App.Update
open Elmish
open Elmish.React

let run() =
    Program.mkProgram init update view
    |> Program.withReactBatched "elmish-app"
    |> Program.withConsoleTrace
    |> Program.run
run()
