using MinivillesConsoleEdition;
// See https://aka.ms/new-console-template for more information

Console.SetBufferSize(200, 200);
//Console.SetWindowSize(100, 100);

Console.WriteLine("Entrez votre nom");
Game game = new Game([Console.ReadLine(), "AI"]);