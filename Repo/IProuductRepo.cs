
using backendtask.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backendtask.Repo
{
    public interface IProductRepo
    {
        Task<List<Product>> GetAll();
        public Task<Product> Create(Product Entry);
      

    }

      
  
}
