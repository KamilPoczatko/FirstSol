namespace MySpot.Api.Exceptions
{
    public class InvalidEnitityIdException : CustomException
    {
        public InvalidEnitityIdException()
            : base("Enitity Id is invalid")
        {

        }
    }
}
