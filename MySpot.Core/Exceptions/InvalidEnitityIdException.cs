namespace MySpot.Core.Exceptions
{
    public class InvalidEnitityIdException : CustomException
    {
        public InvalidEnitityIdException()
            : base("Enitity Id is invalid")
        {

        }
    }
}
