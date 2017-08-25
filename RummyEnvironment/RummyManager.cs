using System.Collections.Generic;

namespace RummyEnvironment
{
    public class RummyManager
    {
        public const int InitialTokensForPlayer = 14;
        public static readonly string[] PlayerNames = { "Xavi" };

        private ITokensLake tokensLake;
        private IBoard board;
        private List<IPlayer> players;

        public RummyManager()
        {
            this.Start();
        }

        private void Start()
        {
            // Tokens lake creation.
            tokensLake = new TokensLake();
            tokensLake.CreateNew();
            tokensLake.ShuffleTokensList();

            // Board creation.
            board = new Board();

            // Players creation.
            players = new List<IPlayer>();
            foreach (string playerName in PlayerNames)
            {
                List<IToken> tokensForPlayer = tokensLake.GrabTokens(InitialTokensForPlayer);
                players.Add(new Player(playerName, tokensForPlayer));
            }
        }
    }
}
