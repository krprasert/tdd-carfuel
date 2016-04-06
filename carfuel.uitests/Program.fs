open System
open canopy
open runner
open configuration
open reporters
 
let baseUrl = "http://localhost:1854" 
let userEmail = "user" + DateTime.Now.Ticks.ToString() + "@company.com"
let pwd = "Test999/*"
chromeDir <- "C:\\chromedriver" 
start firefox 
 
"Sign Up" &&& fun _ ->
    url (baseUrl + "/Account/Register")
    "#Email" << userEmail
    "#Password" << pwd
    "#ConfirmPassword" << pwd
    click "input[type=submit]"
    on baseUrl
 
"Log in" &&& fun _ ->
    url (baseUrl + "/Account/Login")
    "#Email" << userEmail
    "#Password" << pwd
    click "input[type=submit]"
    on baseUrl
 
"Click add link then go to create page" &&& fun _ ->
    url (baseUrl + "/cars")
    displayed "a#gotoAdd"
    click "a#gotoAdd"
    on (baseUrl + "/cars/create")
 
"Add new car" &&& fun _ ->
    let make = "Tesla " + DateTime.Now.Ticks.ToString()
    url (baseUrl + "/cars/create")
    "#Make" << make
    "#Model" << "Model 3"
    click "button#btnAdd"
 
    on (baseUrl + "/cars")
    "td" *= make
 
"Add the second car" &&& fun _ ->
    let make = "Tesla " + DateTime.Now.Ticks.ToString()
    url (baseUrl + "/cars/create")
    "#Make" << make
    "#Model" << "Model 3"
    click "button#btnAdd"
 
    on (baseUrl + "/cars")
    "td" *= make
 
"Add the third car should failed" &&& fun _ ->
    let make = "Tesla " + DateTime.Now.Ticks.ToString()
    url (baseUrl + "/cars/create")
    "#Make" << make
    "#Model" << "Model 3"
    click "button#btnAdd"
 
    on (baseUrl + "/cars")
    "td" *!= make
    contains "Cannot add more car" (read ".error")
 
run() 
//printfn "press [enter] to exit"
//System.Console.ReadLine() |> ignore 
quit()