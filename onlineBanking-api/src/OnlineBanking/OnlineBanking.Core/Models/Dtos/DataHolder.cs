namespace OnlineBanking.Core.Models.Dtos
{
    public class DataHolder<TData>
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

        public static DataHolder<TData> CreateFailure(TData data, string message)
        => new DataHolder<TData>
        {
            Data = data,
            Status = DataHolderStatus.Failure,
            Message = message
        };
    }
}
