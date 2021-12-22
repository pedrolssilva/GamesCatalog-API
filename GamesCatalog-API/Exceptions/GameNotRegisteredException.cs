using System;

namespace GamesCatalog_API.Exceptions
{
    public class GameNotRegisteredException : Exception
    {
        public GameNotRegisteredException() : base("This game is already registered")
        {

        }
    }
}
