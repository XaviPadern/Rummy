using System.Collections.Generic;

namespace RummyEnvironment
{
    public class RummyManager
    {
        private TokensLake tokenLake;

        public RummyManager()
        {
            this.Start();
        }

        private void Start()
        {
            // TODO:
            // Create all the tokens, put in the TokenLake and spread them among players.

            tokenLake = new TokensLake();
            tokenLake.CreateNew();
            tokenLake.ShuffleTokens();
        }
    }
}
