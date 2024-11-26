using Microsoft.AspNetCore.Mvc;
using Victoralm.MAE.API.Models;
using Victoralm.MAE.API.UoW.Interfaces;

namespace Victoralm.MAE.API.Controllers;

[Route("[controller]")]
[ApiController]
public class AppointmentController : Controller
{
    private readonly ILogger<PatientController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentController(ILogger<PatientController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var items = await _unitOfWork.Appointments.GetAppointmentsAsync();

        if (items == null)
            return NotFound();

        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var item = await _unitOfWork.Appointments.GetById(id);

        if (item == null)
            return NotFound();

        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Appointment appointment)
    {
        if (ModelState.IsValid)
        {
            appointment.Id = Guid.NewGuid();

            await _unitOfWork.Appointments.Add(appointment);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction("Get", new { appointment.Id }, appointment);
        }

        return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Appointment appointment)
    {
        await _unitOfWork.Appointments.Upsert(appointment);
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

        await _unitOfWork.Appointments.Delete(id);
        await _unitOfWork.CompleteAsync();

        return Ok(item);
    }
}
