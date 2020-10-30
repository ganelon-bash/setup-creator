module App.RoleFaction
open App.ModelLibrary
open App.Ability

[<Struct; StructuralEquality; StructuralComparison>]
type chat =
    | DayChat
    | NightChat
    | Chat

[<Struct; StructuralEquality; StructuralComparison>]
type faction = {
    name: string
    wincon: string
    color: color
    ability: ability voption
}

and [<Struct; StructuralComparison; StructuralEquality>]
roleModifier =
    | VotingPower of VotingPower: int
    | AppearsAsRole of ApparentRole: role ref
    | CannotBeRoleblocked

and [<Struct; StructuralComparison; StructuralEquality>]
roleInternal =
    {
        name: string
        description: string
        defenseValue: integer
        roleModifiers: roleModifier list
    }
and [<Struct; StructuralEquality; StructuralComparison>]
factionalRoleModifier =
    | AppearsAsFaction of ApparentFaction: faction ref
    | KnowsOtherMembers of KnownRoles: role list
    | HasChat of ChattedRoles: role list  * chat: chat
and [<Struct; StructuralEquality; StructuralComparison>]
role =
    | Role of Role: roleInternal
    | FactionalRole of FactionalRole: roleInternal * Faction: faction * Wincon: string voption * Color: color voption
and [<Struct; StructuralEquality; StructuralComparison>]
roleSpawn =
    | KnownRole of role
    | HiddenRole of name: string * possibleRoles: role list * Faction: faction voption  
module Role =
    let name = function
        | Role r | FactionalRole(r, _, _, _) -> r.name
    let description = function
        | Role r | FactionalRole(r, _, _, _) -> r.description
    let defenseValue = function
        | Role r | FactionalRole(r, _, _, _) -> r.defenseValue
    let roleModifiers = function
        | Role r | FactionalRole(r, _, _, _) -> r.roleModifiers
    let faction = function
        | Role _ -> ValueNone
        | FactionalRole(_, f, _, _) -> ValueSome f
    let wincon = function
        | Role _ -> ValueNone
        | FactionalRole(_, _, w, _) -> w
    let color = function
        | Role _ -> White
        | FactionalRole(_, _, _, c) -> match c with ValueSome c -> c | _ -> White
module RoleSpawn =
    let name = function
        | KnownRole r -> r |> Role.name
        | HiddenRole (n, _, _) -> n
    let color = function
        | KnownRole r -> r |> Role.color
        | HiddenRole (_, _, f) -> match f with ValueSome f -> f.color | ValueNone -> White
    let faction = function
        | KnownRole r -> ValueNone
        | HiddenRole (_, _, f) -> f