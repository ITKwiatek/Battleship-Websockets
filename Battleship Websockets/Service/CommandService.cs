using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship_Websockets.Service
{
    public static class CommandService
    {
        private const string Connect = "ConnId:";
        private const string Start = "Start";

        public static string RunCommand(string command)
        {
            switch (command)
            {
                case Connect:
                    System.Diagnostics.Debug.WriteLine("Run Connectiong...");
                    break;

                case Start:
                    System.Diagnostics.Debug.WriteLine("Start Game, Creating ConnId...");
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine($"Command: {command} not known");
                    break;
            }

            return "Implement me";
        }
    }
}
