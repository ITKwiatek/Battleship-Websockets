namespace Battleship_Websockets.Model.Response
{
    public interface IResponse
    {
        public GameStatus GameStatus { get; set; }
        public PlayersTurn ActionReceiver { get; set; }
    }
}