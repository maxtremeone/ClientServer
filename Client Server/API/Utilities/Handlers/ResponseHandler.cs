namespace API.Utilities.Handlers
{
    public class ResponseHandler <TEntity>
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public TEntity? Data { get; set; } //dikasih tanda tanya agar ketika datanya kosong tidak masuk exception, stelah ini ubah return di controllernya
    }
}
