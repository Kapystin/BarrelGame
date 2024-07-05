using BarrelGame.Scripts.Interface;
using BarrelGame.Scripts.Struct;
using UnityEngine;

namespace BarrelGame.Scripts.Character
{
    public class TouchInput: IMovementInput
    {
        private Joystick _joystick;

        public void SetupJoystick(Joystick joystick)
        {
            _joystick = joystick;
        }
        
        public MovementVector GetMovementInput()
        {
            var movementVector = new MovementVector();

            movementVector.MovementInput = new Vector3(
                _joystick.Direction.x,
                0f,
                _joystick.Direction.y
            );

            return movementVector;
        }
    }
}