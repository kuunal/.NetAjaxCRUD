namespace ModelLayer
{
    public class Response<T>
    {
        public T Data;

        public int StatusCode;
        public string Message;

        public Response(T data, int status, string message){
            this.Data = data;
            this.StatusCode = status;
            this.Message = message;
        }
    }
}