(**
Dynamic programming 
===================================


Dynamic programming is equivalent to the following two concepts used together : _recursion_ and _deforestation_


Recursion
---------

Recursion allows to _express_ solutions that bends itself to it in a simpler way. It is merely a way to formulate a solution in terms of a **subproblem graph** of diminishing size. by itself, it does not reduce the complexity of computing the solution.



Deforestation
-------------


If subproblems appears multiple times in the subproblem graph, then storing intermediary results allows for a  dynamic folding of it, replacing the graph with its computed value. This is where the improvement of the dynamic programming comes from.


Two strategies are possible for this : 

- a **bottom up** approach, evaluating adjacents sub problems in a reverse topological order, that is tackling simpler problem first and guaranteing that simpler problems are all solved before increasing complexity.
This solution has low overhead but special care must be taken to the order of evaluation

- a **top down** approach, where we add a memoization steps, and that keep the natural. This allows unnessary branch, if any, to not be computed.


Both approaches yield the same asymptotical performance.



Examples
--------



Those examples are taken from the book [Introduction to algorithm][cormen].

[cormen]: http://en.wikipedia.org/wiki/Introduction_to_Algorithms "Cormen"


### Rod cutting 

- simple recursion
*)
(*** include: rodcutting ***)
(**

- bottom up
*)
(*** include: rodcuttingbottomup ***)
(**

- memoized recursion
*)
(*** include: rodcuttingrecmem ***)
(**


### Matrix product
- simple recursion
*)
(*** include: matrixproduct ***)
(**

- bottom up
*)
(*** include: matrixproductbu ***)
(**

- memoized recursion



### Longest Common subsequence

### Optimal binary search tree

*)



(*** define: rodcutting ***)
//rod cutting recursive - inefficient
let price = [|1;5;8;9;10;17;17;20;24;30|]

//the maximum price for a size n is the maximum among i=1..n of 
//(selling an entire piece if size i + the maximum price of an optimal subdivision for the remaining part) 
let rec rodcutting_rec  = function | 0 -> 0 | n -> Seq.init n (fun i -> price.[i] + rodcutting_rec (n-i-1)) |> Seq.max 
[1 .. 10] |> List.map (fun i -> i, rodcutting_rec i) 

(*** define: rodcuttingbottomup ***)
//rod cutting recursive - dynamic programming
//the layout of the ordering enforces access to stored value, and folding of the computation tree
let rodcutting_bu n = 
    let r = Array.init (price.Length + 1) (fun _ -> 0)
    let rec up k  = r.[k] <- Seq.init k (fun i -> price.[i] + r.[k-i-1]) |> Seq.max 
                    if k < n then up (k+1) else r.[k]
    up 1    
[1 .. 10] |> List.map (fun i -> i, rodcutting_bu i) 


(*** define: rodcuttingrecmem ***)
//rod cutting recursive with memoization - dynamic programming
//the memoization of each execution ensure the value storage without particular ordering
let memoize f = 
    let d =  System.Collections.Generic.Dictionary<_, _>()
    d, fun n -> if d.ContainsKey n then d.[n] 
                else d.Add(n, f n);d.[n]

let rodcutting_dp n =
    let rec rodcutting_rec  = function | 0 -> 0 | n -> Seq.init n (fun i -> price.[i] + rodcutting_rec (n-i-1)) |> Seq.max  
    let _, mf = memoize rodcutting_rec
    mf n
    
[1 .. 10] |> List.map (fun i -> i, rodcutting_dp i) 

(*** define: matrixproduct ***)
//rod cutting recursive - inefficient
let p = [|30;35;15;5;10;20;25|]

let tee f x = f x;x

//the cost of n*p . p*q is n*p*q   
//the minimum cost parenthesizing is the minimum for i = 1..n
//the min cost of parenthesizing 1..i together + (i+1).. together + multiplying both parts 
let rec matrixproduct_rec(i,j) = //assume 1 <= i,j <= n
    match j - i with 
    | n when n <= 0 -> 0 
    | n -> Seq.init n (fun k -> matrixproduct_rec(i,i+k) + p.[i-1]*p.[i+k]*p.[j] + matrixproduct_rec(i+k+1,j)) |> Seq.min 

let product xs ys = [ for x in xs do for y in ys do yield x,y ]
product [1 .. 6] [1 .. 6] |> List.map (fun i -> i, matrixproduct_rec i) 

(*** define: matrixproductbu ***)
module Seq = let inline minDef def items = if items |> Seq.isEmpty then def else items |> Seq.min
    
//we order the problem by the size of the subproblem j - i and solve problem of increasing size
// i j refers to the index of the matrix to be multiplied (1 based)
let matrixproduct_bu(ifinal,jfinal) = 
    let m = Array2D.init p.Length p.Length (fun _ _ -> 0)
    let rec up pbsize = 
        // we have (pbsizetotal - n) pbs of size n, and the index are i, j / j - i = n, and max j <= p.length
        Seq.init ((p.Length-1)-pbsize) ((+)1>> fun i-> i,i+pbsize) |> Seq.iter(fun(i,j) -> 
            m.[i,j] <- (Seq.init pbsize (fun k -> // for each possible split at i + k (k is 0 based offset)
                        m.[i,i+k] + p.[i-1]*p.[i+k]*p.[j] + m.[i+k+1,j])  //we compute the cost, accessing only pb of size  k < n and n - k - 1 < n 
                        |> Seq.minDef 0  )) // and take the min
        if pbsize < (jfinal-ifinal) then up (pbsize+1) else m.[ifinal,jfinal]
    up 0
    
product [1 .. 6] [1 .. 6] |> List.map (fun i -> i, matrixproduct_bu i) 

