namespace OnlineBanking.Core.Models.Providers.Abstract
{
    public interface IUserTwoFactorHolder<TKey>
    {
        TKey Id { get; set; }
        string TwoFactorCode { get; set; }
    }
}
