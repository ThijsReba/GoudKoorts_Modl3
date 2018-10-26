using System;

namespace View
{
    public class OutputView
    {
        public void ShowStart()
        {
            Console.WriteLine("|-----------------------------------------|");
            Console.WriteLine("|--------- Welkom bij Goudkoorts ---------|");
            Console.WriteLine("|-----------------------------------------|");
            Console.WriteLine();
            Console.WriteLine("|---------------- Uitleg -----------------|");
            Console.WriteLine("|--------- Druk op toets 1 t/m 5 ---------|");
            Console.WriteLine("|-------- op de rails te wisselen --------|");
            Console.WriteLine("|-----------------------------------------|");

            Console.WriteLine();
            Console.WriteLine("Druk op een toets om te beginnen.");
            Console.ReadKey();
        }

        public void ShowGameOver(Model.GameBoard game)
        {
            Console.Clear();
            Console.WriteLine("|-----------------------------------------|");
            Console.WriteLine("|--------------- GAMEOVER ----------------|");
            Console.WriteLine("|-----------------------------------------|");


            Console.WriteLine("|------ Je hebt " + game.TotalScore.ToString() + " punten gescoord -------|");
            Console.WriteLine();
            Console.ReadLine();
        }

        public void UpdateBoard(Model.GameBoard game)
        {
            Console.Clear();
            int nRows = game.Height;
            int nCols = game.Width;

            Model.Tile current = game.Origin;
            Model.Tile neighbourBelow = current.TileBelow;

            for (int r = 0; r < nRows; r++)
            {
                for (int c = 0; c < nCols; c++)
                {
                    Console.Write(current.ToChar());
                    current = current.TileToRight;
                }
                current = neighbourBelow;
                if (neighbourBelow != null)
                {
                    neighbourBelow = current.TileBelow;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("------- gebruik de toetsen 1 t/m 5 -------");
            Console.WriteLine("----------- Jouw score:    " + game.TotalScore.ToString() + "   -----------");

        }
    }
}