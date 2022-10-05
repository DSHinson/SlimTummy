namespace Generic.API.dto
{
    public class ResponseModel
    {
        public Boolean IsSuccess { get; set; }
        public Object Data { get; set; }
        public String Message { get; set; }
        public DateTime StartTimestamp { get; set; }
        public DateTime EndTimestamp { get; set; }

    }
}
