module App.Model
open Elmish
open App.ModelLibrary
open App.Ability
open App.RoleFaction
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
type Model = {
    title: string
    author: string
    dayLength: int
    nightLength: int
    lynchType: LynchType
    roles: role list
    spawnList: (faction, roleSpawn) Map
    orderOfOperations: ((faction, role) Choice * ability) list
    messages: Message list
    
}
and SetupMsg =
    | ChangeTitle of string
    | ChangeAuthor of string
    | ChangeDayLength of int
    | ChangeNightLength of int
    | ChangeLynchType of LynchType
    static member invert model = function
        | ChangeTitle _ -> ChangeTitle model.title
        | ChangeAuthor _ -> ChangeAuthor model.author
        | ChangeDayLength _ -> ChangeDayLength model.dayLength
        | ChangeNightLength _ -> ChangeNightLength model.nightLength
        | ChangeLynchType _ -> ChangeLynchType model.lynchType
                
and [<Struct>] FactionMsg =
    | AddFaction of FactionToAdd: faction
    | RemoveFaction of FactionToRemove: faction
    | SetFactionColor of FactionForColor: faction * FactionColor: color
    | SetFactionName of FactionForName: faction * FactionName: string
    | SetFactionWincon of FactionForWincon: faction * Wincon: string
    static member invert = function
        | AddFaction f -> RemoveFaction f
        | RemoveFaction f -> AddFaction f
        | SetFactionColor (faction, _) -> SetFactionColor(faction, faction.color)
        | SetFactionName (faction, _) -> SetFactionName(faction, faction.name)
        | SetFactionWincon (faction, _) -> SetFactionWincon(faction, faction.wincon)
and [<Struct>] RoleMsg =
        | AddRole of RoleToAdd: role
        | RemoveRole of RoleToRemove: role
        | SetRoleColor of RoleForColor: role * RoleColor: color
        | SetRoleName of RoleForName: role * RoleName: string
        | AddRoleAbility of RoleForAbility: role * AbilityToAdd: ability
        | RemoveRoleAbility of RoleFromAbility: role * AbilityToRemove: ability
        //| EditRoleAbility of RoleForEditableAbility: role * AbilityEditMsg: abilityEditMessage
        static member invert = function
            | AddRole r -> RemoveRole r
            | RemoveRole r -> AddRole r
            | SetRoleColor (r, _) -> SetRoleColor (r, r |> Role.color)
            | SetRoleName (r, _) -> SetRoleName (r, r |> Role.name)
            | AddRoleAbility (r, a) -> RemoveRoleAbility(r, a)
            | RemoveRoleAbility(r, a) -> AddRoleAbility(r, a)
and MainMsg = struct end
and [<Struct>]
Message =
        | SetupMsg of SetupMsg: SetupMsg
        | FactionMsg of FactionMsg: FactionMsg
        | RoleMsg of RoleMsg: RoleMsg
        | CreateNewFaction
        | CreateNewRole
        | CreateNewRoleAbility of ToRole: role
        | CreateNewFactionAbility of ToFaction: faction
       
let standardRoles = []
let standardList = Map.ofList []
let standardOrder = []
let init() =
    {
        title = "S-FM Standard Mafia"
        author = "Author"
        dayLength = 24
        nightLength = 48
        lynchType = PluralityMajority
        roles = standardRoles
        spawnList = standardList
        orderOfOperations = standardOrder
        messages = []
    }, Cmd.none

