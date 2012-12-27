(**
Dynamic programming 
===================================


Dynamic programming is equivalent to the folowwing two concepts used together : _recursion_ and _deforestation_


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



Those examples are taken from the book [Introduction to algorithm][cormen]

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


Matrix product

Longest Common subsequence

Optimal binary search tree

[cormen]:http://en.wikipedia.org/wiki/Introduction_to_Algorithms
*)

(*** define: rodcutting ***)
//rod cutting recursive - inefficient
let p = [|1;5;8;9;10;17;17;20;24;30|]

let rec rodcutting_rec  = function | 0 -> 0 | n -> Seq.init n (fun i -> p.[i] + rodcutting_rec (n-i-1)) |> Seq.max 
[1 .. 10] |> List.map (fun i -> i, rodcutting_rec i) 

(*** define: rodcuttingbottomup ***)
//rod cutting recursive - dynamic programming
//the layout of the ordering enforces access to stored value, and folding of the computation tree
let rodcutting_bu n = 
    let r = Array.init (p.Length + 1) (fun _ -> 0)
    let rec up k  = r.[k] <- Seq.init k (fun i -> p.[i] + r.[k-i-1]) |> Seq.max 
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
    let rec rodcutting_rec  = function | 0 -> 0 | n -> Seq.init n (fun i -> p.[i] + rodcutting_rec (n-i-1)) |> Seq.max  
    let _, mf = memoize rodcutting_rec
    mf n
    
[1 .. 10] |> List.map (fun i -> i, rodcutting_dp i) 



