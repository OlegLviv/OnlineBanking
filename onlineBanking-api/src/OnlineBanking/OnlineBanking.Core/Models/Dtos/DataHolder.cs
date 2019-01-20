namespace OnlineBanking.Core.Models.Dtos
{
    public class DataHolder<TData> where TData : class
    {
        public TData Data { get; set; }

        public string Message { get; set; }

        public DataHolderStatus Status { get; set; }

        public static DataHolder<TData> CreateSuccess(TData data)
            => new DataHolder<TData>
            {
                Data = data,
                Status = DataHolderStatus.Success,
                Message = "Success"
            };

        public static DataHolder<TData> CreateFailure(string message, TData data = null)
            => new DataHolder<TData>
            {
                Data = data,
                Status = DataHolderStatus.Failure,
                Message = message
            };

        public static DataHolder<TData> CreateUnauthorized()
            => new DataHolder<TData>
            {
                Data = null,
                Status = DataHolderStatus.Unauthorized,
                Message = "Unauthorized"
            };
    }
}
