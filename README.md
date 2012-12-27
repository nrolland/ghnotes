
The project aims to collect notes illustrated with some code, and provide an example on how to use [FSharp.Formatting library](http://tpetricek.github.com/FSharp.Formatting/)
The authoritative documents are contained in the src folder.
After being compiled with [FSharp.Formatting](http://tpetricek.github.com/FSharp.Formatting/), they yields the HTML files at the root.



Installation
------------

First install the dependencies with nuget.


You can run install.sh or the command

    nuget install packages.config -o Packages


Then you can run 

    fsi tools/build.fsx 

to compile the documents in docs/ folder

