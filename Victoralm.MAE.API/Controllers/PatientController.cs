using Microsoft.AspNetCore.Mvc;
using Victoralm.MAE.API.Models;
using Victoralm.MAE.API.UoW.Interfaces;

namespace Victoralm.MAE.API.Controllers;

[Route("[controller]")]
[ApiController]
public class PatientController : Controller
{
    private readonly ILogger<PatientController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public PatientController(ILogger<PatientController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var items = await _unitOfWork.Patients.GetPatientsAsync();

        if (items == null)
            return NotFound();

        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var item = await _unitOfWork.Patients.GetById(id);

        if (item == null)
            return NotFound();

        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Patient patient)
    {
        if (ModelState.IsValid)
        {
            patient.Id = Guid.NewGuid();

            await _unitOfWork.Patients.Add(patient);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction("Get", new { patient.Id }, patient);
        }

        return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Patient patient)
    {
        await _unitOfWork.Patients.Upsert(patient);
        await _unitOfWork.CompleteAsync();

        // Following up the REST standards on update we need to return NoContent
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var item = await _unitOfWork.Patients.GetById(id);

        if (item == null)
            return BadRequest();

        await _unitOfWork.Patients.Delete(id);
        await _unitOfWork.CompleteAsync();

        return Ok(item);
    }
}
