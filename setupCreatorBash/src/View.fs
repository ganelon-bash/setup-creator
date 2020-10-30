module App.View
open App.RightSideBar
open App.Model

let view model dispatch =
        RightSideBar.rightSideBar model.title model.author model.dayLength model.nightLength model.lynchType dispatch