namespace Script.Util.Response
{
    public enum EResponse
    {
        Valid,
        InvalidMail,
        AccountNull,
        PasswordError
    }

    public class CustomResponse
    {
        public EResponse Response;
        public string ResponseMessage;
    }
}