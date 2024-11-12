using MinivillesConsoleEdition;
// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
Piles pile = new Piles();
foreach(Card c in pile.DrawPile.Keys)
    Console.WriteLine(c);