using System.Text;

static class Program
{
    const int ROCK = 1;
    const int PAPER = 2;
    const int SCISSORS = 3;
    const int WIN = 6;
    const int DRAW = 3;
    const int LOSE = 0;
    const string SHOULD_WIN = "Z";
    const string SHOULD_LOSE = "X";
    const string SHOULD_DRAW = "Y";

    
    public static void Main()
    {
        var totalScorePartOne = 0;
        var totalScorePartTwo = 0;
        var sb = new StringBuilder();

        foreach(var line in File.ReadAllLines("input.txt"))
        {
            var moves = line.Split(" ");
            var roundTotalOne = CalculateRoundTotals(moves[0], moves[1]);
            var roundTotalTwo = CalculateRoundTotalsPartTwo(moves[0], moves[1]);
            //sb.AppendLine(roundTotal.ToString());
            totalScorePartOne += roundTotalOne;
            totalScorePartTwo += roundTotalTwo;
        }
        Console.WriteLine($"RPS total One {totalScorePartOne}");
        Console.WriteLine($"RPS total two {totalScorePartTwo}");
        //File.WriteAllText("out.csv", sb.ToString());
    }

    public static int CalculateRoundTotalsPartTwo(string p1, string p2)
    {
        if (IsPaper(p1))
        {
            switch (p2)
            {
                case SHOULD_WIN:
                    {
                        return SCISSORS + WIN;
                    }
                case SHOULD_LOSE:
                    {
                        return ROCK + LOSE;
                    }
                case SHOULD_DRAW:
                    {
                        return PAPER + DRAW;
                    }
            }
        }
        if (IsRock(p1))
        {
            switch (p2)
            {
                case SHOULD_WIN:
                    {
                        return PAPER + WIN;
                    }
                case SHOULD_LOSE:
                    {
                        return SCISSORS + LOSE;
                    }
                case SHOULD_DRAW:
                    {
                        return ROCK + DRAW;
                    }
            }
        }
        if (IsScissor(p1))
        {
            switch (p2)
            {
                case SHOULD_WIN:
                    {
                        return ROCK + WIN;
                    }
                case SHOULD_LOSE:
                    {
                        return PAPER + LOSE;
                    }
                case SHOULD_DRAW:
                    {
                        return SCISSORS + DRAW;
                    }
            }
        }
        throw new Exception($"Invalid input {p1}");
    }
    public static int CalculateRoundTotals(string p1, string p2)
    {
        if(IsRock(p1))
        {
            if (IsPaper(p2))
            {
                //win
                return PAPER + WIN;
            }

            if(IsScissor(p2))
            {
                //lose
                return SCISSORS + LOSE;
            }

            if (IsRock(p2))
            {
                return ROCK + DRAW;
            }
        }
        if (IsPaper(p1))
        {
            if (IsPaper(p2))
            {
                //win
                return PAPER + DRAW;
            }

            if (IsScissor(p2))
            {
                //lose
                return SCISSORS + WIN;
            }

            if (IsRock(p2))
            {
                return ROCK + LOSE;
            }
        }
        if (IsScissor(p1))
        {
            if (IsPaper(p2))
            {
                //win
                return PAPER + LOSE;
            }

            if (IsScissor(p2))
            {
                //lose
                return SCISSORS + DRAW;
            }

            if (IsRock(p2))
            {
                return ROCK + WIN;
            }
        }
        throw new Exception($"invalid input {p1}");
    }

    public static bool IsRock(string move)
    {
        return move is "A" or "X";
    }

    public static bool IsPaper(string move)
    {
        return move is "B" or "Y";
    }

    public static bool IsScissor(string move)
    {
        return move is "C" or "Z";
    }
}