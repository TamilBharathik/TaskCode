using System.Collections.Generic;
using System.Threading.Tasks;
using backendtask.Model;

namespace backendtask.Repo
{
    public interface IDropdownRepository
    {
        Task<IEnumerable<Dropdown>> GetAll();
    }
}
