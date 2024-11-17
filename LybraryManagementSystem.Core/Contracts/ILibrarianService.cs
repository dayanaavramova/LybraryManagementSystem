namespace LibraryManagementSystem.Core.Contracts
{
    public interface ILibrarianService
    {
        Task<bool> ExistsByIdAsync(string userId);
        Task CreateAsync(string userId, string phoneNumber);
        Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber);
        Task<bool> UserHasRoleAsync(string userId);
        Task<int?> GetLibrarianIdAsync(string userId);
    }
}
