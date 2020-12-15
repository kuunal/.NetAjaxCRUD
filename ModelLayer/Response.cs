namespace ModelLayer
{
    public class Response<T>
    {

        public int StatusCode;
        public string Message;
        public T Data;

        public Response(T data, int status, string message){
            this.StatusCode = status;
            this.Message = message;
            this.Data = data;
        }
    }
}