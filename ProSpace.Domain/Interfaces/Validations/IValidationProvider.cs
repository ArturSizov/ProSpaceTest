namespace ProSpace.Domain.Interfaces.Validations
{
    public interface IValidationProvider<TItem> where TItem : class
    {
        /// <summary>
        /// Validate data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task ValidateAsync(TItem item);
    }
}
