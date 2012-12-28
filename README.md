
The project aims to collect notes illustrated with some code, and provide an example on how to use [FSharp.Formatting library](http://tpetricek.github.com/FSharp.Formatting/)


Installation
------------

First install the dependencies with nuget.


You can run install.sh or the command

    nuget install packages.config -o Packages


Then you can run 

    fsi tools/build.fsx 

to compile the documents in docs/ folder
the html result of this repo should be [here](http://xquant.net/ghnotes/)


Usage
-----

The documents (and the generation scripts) are contained in the **src** folder.
The relevant markdown of literate scripts are placed under the **src/docs** folder
They are compiled using fsharpi ***src/tools/build.fsx*** script (which uses src/tools/template.html as well)
This yields the HTML files in the **ouput** folder, along with the content subfolder which contains the style and javascript for the tooltips.

To ease publication on Github pages, the **gh-pages** branch contains a script deploy.sh that copies the output folder's content at root level.

The workflow is therefore the following :

   - Edit pages in src/docs
   - Compile and inspect until satisfied
   - Commit to master
   - checkout gh-pages, deploy.sh, commit
   - ??
   - profit !

