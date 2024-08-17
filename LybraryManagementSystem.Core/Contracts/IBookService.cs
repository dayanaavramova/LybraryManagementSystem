using LibraryManagementSystem.Core.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<BookIndexServiceModel>> LastThreeBooks();
    }
}
