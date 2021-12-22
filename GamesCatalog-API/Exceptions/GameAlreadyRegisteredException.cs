using System;

namespace GamesCatalog_API.Exceptions
{
    public class GameAlreadyRegisteredException : Exception
    {
        public GameAlreadyRegisteredException() : base("This game is already registered")
        {

        }
    }
}
