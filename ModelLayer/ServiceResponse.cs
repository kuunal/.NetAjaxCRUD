namespace ModelLayer
{
    public class ServiceResponse<T>
    {
        public T Data;

        public int StatusCode;
        public string Message;

        public ServiceResponse(T data, int status, string message){
            this.Data = data;
            this.StatusCode = status;
            this.Message = message;
        }
    }
}