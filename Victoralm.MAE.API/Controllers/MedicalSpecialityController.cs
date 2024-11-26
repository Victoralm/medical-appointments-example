using Microsoft.AspNetCore.Mvc;
using Victoralm.MAE.API.Models;
using Victoralm.MAE.API.UoW.Interfaces;

namespace Victoralm.MAE.API.Controllers;

[Route("[controller]")]
[ApiController]
public class MedicalSpecialityController : Controller
{
    private readonly ILogger<MedicalSpecialityController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public MedicalSpecialityController(ILogger<MedicalSpecialityController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var items = await _unitOfWork.MedicalSpecialities.GetMedicalSpecialtiesAsync();

        if (items == null)
            return NotFound();

        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var item = await _unitOfWork.MedicalSpecialities.GetById(id);

        if (item == null)
            return NotFound();

        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MedicalSpecialty medicalSpecialty)
    {
        if (ModelState.IsValid)
        {
            medicalSpecialty.Id = Guid.NewGuid();

            await _unitOfWork.MedicalSpecialities.Add(medicalSpecialty);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction("Get", new { medicalSpecialty.Id }, medicalSpecialty);
        }

        return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] MedicalSpecialty medicalSpecialty)
    {
        await _unitOfWork.MedicalSpecialities.Upsert(medicalSpecialty);
        await _unitOfWork.CompleteAsync();

        // Following up the REST standards on update we need to return NoContent
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var item = await _unitOfWork.MedicalSpecialities.GetById(id);

        if (item == null)
            return BadRequest();

        await _unitOfWork.MedicalSpecialities.Delete(id);
        await _unitOfWork.CompleteAsync();

        return Ok(item);
    }
}
