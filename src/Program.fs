﻿open Expecto
open EndToEndTests
open ParserTests
open TestsLexer
open Lexer
open Parser
open Combinator_runtime
open Helpers
open System.IO
open System

let expectoConfig = { defaultConfig with verbosity = Logging.LogLevel.Debug }

let inline printChain i =
    printf "%A\n------------------\n" i
    i

[<EntryPoint>]
let main argv =
    // Running tests - development

    
    lexerTestsWithExpecto() |> ignore
    parserTestsWithExpecto() |> ignore
    endToEndTestsWithExpecto() |> ignore
    
    printfn "%A" <| Interpret (Parse (tokeniser "def f x y = x - y \n f 2 3"))
    printfn "%A" <| Interpret (Parse (tokeniser "def f arr = Head (Tail arr) \n f [1,2,3]"))
    // printfn "%A" <| pRun pAst (tokeniser "def f x = x \n f 1") // -> handled in wrapper
    // printfn "%A" <| tokeniser "" // -> handled in wrapper
    
    // Running file - release
    (*
    let BuiltInCode = loadCode "src/mainlib/builtin.fpy"
    match [|"test_code.fpy"|] with 
    | [|path|] -> 
        let CombinedCode =
            try
                let UserCode = loadCode path
                Some <| BuiltInCode + "\n" + UserCode
            with
            | error -> 
                printf "File not found\n"
                None
        
        //Append built-in definitions to user code
        
        let result =
            CombinedCode
            |> Option.map tokeniser
            |> Option.map (pRun pAst)
            |> Option.map Interpret

        match result with
        | Some (Some res) -> printf "%s" <| PrintTree res
        | _ -> printf "Did not find evaluate.\n"
    | _ -> printf "Must enter a .fpy file to execute.\n"
    *)

    Console.ReadKey() |> ignore
    0