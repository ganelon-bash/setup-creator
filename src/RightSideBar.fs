module App.RightSideBar
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props
open App.Model
let textField msg props children dispatch = 
    let message (e: Browser.Types.Event) = e.target?value |> msg in
    textarea [OnChange(fun e -> dispatch (message e)); yield! props] children
let spinner label value step min max msg dispatch =
    div [] [
        str label
        input [Type "Number"; Min min; Max max; Step step; OnChange (fun e -> e.target?value |> int |> msg |> dispatch); Value value; Cols 6]
    ]
let titleField title dispatch = 
    textField (string >> ChangeTitle >> SetupMsg) [ClassName "unresizableTextField"; Rows 1] [str title] dispatch
let authorField author dispatch =
    textField (string >> ChangeAuthor >> SetupMsg) [ClassName "unresizableTextField"; Rows 1] [str author] dispatch
let dayLengthField dayLength dispatch = spinner "Day Length: " dayLength 12 12 1200 (ChangeDayLength >> SetupMsg) dispatch
let nightLengthField nightLength dispatch = spinner "Night Length: " nightLength 12 12 1200 (ChangeNightLength >> SetupMsg) dispatch
let lynchPane lynchType dispatch =
    match lynchType with
    | Other c ->
        div [] [
            str "Lynch Type: "
            select [Value lynchType; OnChange (fun e -> e.target?value |> LynchType.ofString |> ChangeLynchType |> SetupMsg |> dispatch)] [
                option [Value Plurality] [str "Plurality"]
                option [Value Majority] [str "Majority"]
                option [Value PluralityMajority] [str "Plurality with Majority"]
                option [Value Condorcet] [str "Condorcet"]
                option [Value Kingmaker] [str "Kingmaker"]
                option [Value WeightedPlurality] [str "Weighted Purality"]
                option [Value (Other c)] [
                    str "Custom"
                ]
            ]
            div [] []
            textField (fun a -> a |> string |> LynchType.ofString |> ChangeLynchType |> SetupMsg) [ClassName "unresizableTextField"; Placeholder "Description"; Rows 4] [str c] dispatch
        ] 
    | _  ->
        div [] [
            str "Lynch Type: "
            select [Value lynchType; OnChange (fun e -> e.target?value |> LynchType.ofString |> ChangeLynchType |> SetupMsg |> dispatch)] [
                option [Value Plurality] [str "Plurality"]
                option [Value Majority] [str "Majority"]
                option [Value PluralityMajority] [str "Plurality with Majority"]
                option [Value Condorcet] [str "Condorcet"]
                option [Value Kingmaker] [str "Kingmaker"]
                option [Value WeightedPlurality] [str "Weighted Plurality"]
                option [Value (Other "")] [str "Custom"]
            ]
        ]
    
let rightSideBar title author dayLength nightLength lynchType dispatch =
    div [] [
        titleField title dispatch
        div [] []
        authorField author dispatch
        div [] []
        dayLengthField dayLength dispatch
        nightLengthField nightLength dispatch
        lynchPane lynchType dispatch
    ]