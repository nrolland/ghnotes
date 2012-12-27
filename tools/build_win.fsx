#I "../packages/FSharp.Formatting.1.0.7/lib/net40"
#load "../packages/FSharp.Formatting.1.0.7/literate/literate.fsx"
open FSharp.Literate
open System.IO


let generateDocs() =
  let source = __SOURCE_DIRECTORY__
  let template = Path.Combine(source, "template.html")
  let sources = Path.Combine(source, "../docs")
  let output = Path.Combine(source, "../output")
  let options = 
    "--reference:\"" + source + "\\..\\Packages\\FSharp.Formatting.1.0.7\\lib\\net40\\FSharp.CodeFormat.dll\" " +
    "--reference:\"" + source + "\\..\\Packages\\FSharp.Formatting.1.0.7\\lib\\net40\\FSharp.Markdown.dll\" " +
    "--reference:System.Web.dll"

  let jscss = Path.Combine(output, "content")
  Directory.CreateDirectory(jscss) |> ignore
  Directory.GetFiles((Path.Combine(source, "../packages/FSharp.Formatting.1.0.7/literate/content")))
  |> Array.map (fun f -> File.Copy(f, Path.Combine(jscss, Path.GetFileName f), true) ) |> ignore
  


  let projInfo =
      [ "page-description", """These are various notes of computer science subjects. They sometime have accompanying F# implementation 
                            They are authored in a litterate programming style and each document is a valid.""".Replace("\n","").Replace("                            ", "")
        "page-author", "Nicolas Rolland"
        "github-link", "https://github.com/nrolland/ghnotes"
        "project-name", "Github Science Notes" ]
  //let script = Path.Combine(source, "../docs/dynamic_prog.md")
  //Literate.ProcessScriptFile(script, template)
  Literate.ProcessDirectory
    ( sources, template, output, replacements = projInfo, 
      compilerOptions = options )

generateDocs()

