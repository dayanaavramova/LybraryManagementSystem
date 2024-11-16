namespace LibraryManagementSystem.Core.Contracts
{
    public interface ILibrarianService
    {
        Task<bool> ExistsByIdAsync(string userId);
        Task CreateAsync(string userId, string phoneNumber);
    }
}
