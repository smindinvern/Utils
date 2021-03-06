namespace smindinvern

module Utils =

    open System

    module List =
        let fold' f = function
            | (x::xs) -> List.fold f x xs
            | _ -> raise <| new InvalidOperationException()

        let inline cons (head: 'a) (tail: 'a list) = head::tail
        let uncons = function
            | [] -> raise <| InvalidOperationException()
            | x::xs -> (x, xs)

        let intersperse (a: 'a) (xs: 'a list) : 'a list =
            match xs with
            | first::second::rest -> first::(List.foldBack (fun x xs' -> a::x::xs') (second::rest) [])
            // TODO: Should this be an error condition?
            | _ -> xs

    module Seq =
        let uncons xs =
            if Seq.isEmpty xs then
                raise <| InvalidOperationException()
            else
                (Seq.head xs, Seq.tail xs)

        let fold' f xs =
            let (head, tail) = uncons xs
            Seq.fold f head tail

    let inline flip f x y = f y x
    let inline konst x _ = x
    let mkPair x y = (x,y)

    let inline maybe (def: 'b) (f: 'a -> 'b) (a: 'a option) : 'b =
        Option.fold (fun _ -> f) def a

