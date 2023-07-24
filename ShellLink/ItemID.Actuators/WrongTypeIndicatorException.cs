using System;

namespace ShellLink.ItemID.Actuators
{
    [Serializable]
    public class WrongTypeIndicatorException : Exception
    {
        public ItemIDTypeIndicator ValidTypeIndicator { get; private set; }
        public ItemIDTypeIndicator WrongTypeIndicator { get; private set; }
        protected WrongTypeIndicatorException(string message) : base(message) { }
        protected WrongTypeIndicatorException(string message, Exception inner) : base(message, inner) { }
        protected WrongTypeIndicatorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public WrongTypeIndicatorException(
                ItemIDTypeIndicator validTypeIndicator,
                ItemIDTypeIndicator wrongTypeIndicator
            ) :
            base($"Type indicator is wrong. Accepted valur is {validTypeIndicator}, whereas current value is {wrongTypeIndicator}.")
        {
            ValidTypeIndicator = validTypeIndicator;
            ValidTypeIndicator = wrongTypeIndicator;
        }
    }
}