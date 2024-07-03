using BarrelGame.Scripts.Interface;
using BarrelGame.Scripts.Struct;
using UnityEngine;

namespace BarrelGame.Scripts.Character
{
    public class KeyboardInput : IMovementInput
    {
        public MovementVector GetMovementInput()
        {
            var movementVector = new MovementVector();

            movementVector.MovementInput = new Vector3(
                                                    Input.GetAxis("Horizontal"),
                                                    0f,
                                                    Input.GetAxis("Vertical")
                                                    );

            return movementVector;
        }
    }
}