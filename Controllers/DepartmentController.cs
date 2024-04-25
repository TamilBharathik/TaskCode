using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using backendtask.Model;
using backendtask.Repositories;

namespace backendtask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet("ids")]
        public ActionResult<IEnumerable<int>> GetAllDepartmentIds()
        {
            var departmentIds = _departmentRepository.GetAllDepartmentIds();
            return Ok(departmentIds);
        }

        [HttpGet("{departmentId}/names")]
        public ActionResult<IEnumerable<Department>> GetDepartmentNamesForId(int departmentId)
        {
            var departmentNames = _departmentRepository.GetDepartmentNamesForId(departmentId);
            return Ok(departmentNames);
        }
    }
}
