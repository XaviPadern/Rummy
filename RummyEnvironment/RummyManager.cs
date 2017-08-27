using System.Collections.Generic;

namespace RummyEnvironment
{
    public class RummyManager
    {
        public const int InitialTokensForPlayer = 14;
        public static readonly string[] PlayerNames = { "Godzilla" };

        public ITokensLake tokensLake { get; private set; }
        public IBoard board { get; private set; }
        public List<IPlayer> players { get; private set; }

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
