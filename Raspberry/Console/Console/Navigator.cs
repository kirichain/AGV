using GuidanceSystems;
using Boards;

namespace Navigators
{
    public class Navigator
    {
        public string nav_command;
        public Navigator()
        {
            Console.WriteLine("Navigator Init Done");
        }
        public void move(string direction)
        {
            nav_command = direction;
            Boards.Board.SendSerial(SerialReceiver.Motor_Controller, nav_command);
        }
        public void nav(Mode mode)
        {
            if (mode == Mode.Direct)
            {
                Console.WriteLine("Navigating in direct mode");
                move("forward");
            }
        }
    }
}