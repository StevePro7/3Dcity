//using System;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Input;

//namespace WindowsGame.Define.Inputs
//{
//    public interface IJoystickInput
//    {
//        void Initialize();
//        void Initialize(Byte theMaxPlayers, GamePadDeadZone theGamePadDeadZone);
//        void Update(GameTime gameTime);

//        void SetMotors(Single leftMotor, Single rightMotor);
//        void SetMotors(PlayerIndex playerIndex, Single leftMotor, Single rightMotor);
//        void ResetMotors();
//        void ResetMotors(PlayerIndex playerIndex);

//        PlayerIndex CurrPlayerIndex { get; }
//    }

//    public class JoystickInput : IJoystickInput
//    {
//        private GamePadState[] currGamePadState;
//        private GamePadState[] prevGamePadState;
//        private Byte maxPlayers;
//        private GamePadDeadZone gamePadDeadZone;

//        private const Byte MAX_PLAYERS = 4;

//        // http://xona.com/2010/05/03.html
//        // If not specified then IndependentAxes is the default.
//        // However, often circular it's better for 4-way motion.
//        public void Initialize()
//        {
//            Initialize(MAX_PLAYERS, GamePadDeadZone.Circular);
//        }
//        public void Initialize(Byte theMaxPlayers, GamePadDeadZone theGamePadDeadZone)
//        {
//            maxPlayers = theMaxPlayers;
//            gamePadDeadZone = theGamePadDeadZone;

//            currGamePadState = new GamePadState[maxPlayers];
//            prevGamePadState = new GamePadState[maxPlayers];
//        }

//        public void Update(GameTime gameTime)
//        {
//            for (Byte index = 0; index < maxPlayers; index++)
//            {
//                PlayerIndex playerIndex = (PlayerIndex) index;
//                currGamePadState[index] = GamePad.GetState(playerIndex, gamePadDeadZone);
//                prevGamePadState[index] = currGamePadState[index];
//            }
//        }

//        public void SetMotors(Single leftMotor, Single rightMotor)
//        {
//            SetMotors(CurrPlayerIndex, leftMotor, rightMotor);
//        }

//        public void SetMotors(PlayerIndex playerIndex, Single leftMotor, Single rightMotor)
//        {
//            Byte index = (Byte) playerIndex;
//            if (!currGamePadState[index].IsConnected)
//            {
//                return;
//            }

//            GamePad.SetVibration(playerIndex, leftMotor, rightMotor);
//        }

//        public void ResetMotors()
//        {
//            ResetMotors(CurrPlayerIndex);
//        }

//        public void ResetMotors(PlayerIndex playerIndex)
//        {
//            SetMotors(0, 0);
//        }

//        public PlayerIndex CurrPlayerIndex { get; private set; }
//    }
//}
