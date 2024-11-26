using Microsoft.AspNetCore.Mvc;
using Victoralm.MAE.API.Models;
using Victoralm.MAE.API.UoW.Interfaces;

namespace Victoralm.MAE.API.Controllers;

[Route("[controller]")]
[ApiController]
public class MedicController : Controller
{
    private readonly ILogger<MedicController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public MedicController(ILogger<MedicController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var items = await _unitOfWork.Medics.GetMedicsAsync();

        if (items == null)
            return NotFound();

        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var item = await _unitOfWork.Medics.GetById(id);

        if (item == null)
            return NotFound();

        return Ok(item);
    }

    [HttpGet]
    [Route("GetBySpecialities")]
    public async Task<IActionResult> Get([FromQuery] List<Guid> ids)
    {
        var item = await _unitOfWork.Medics.GetMedicsBySpecialities(ids);

        if (item == null)
            return NotFound();

        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Medic medic)
    {
        if (ModelState.IsValid)
        {
            medic.Id = Guid.NewGuid();

            await _unitOfWork.Medics.Add(medic);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction("Get", new { medic.Id }, medic);
        }

        return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Medic medic)
    {
        await _unitOfWork.Medics.Upsert(medic);
        await _unitOfWork.CompleteAsync();

        // Following up the REST standards on update we need to return NoContent
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var item = await _unitOfWork.Medics.GetById(id);

        if (item == null)
            return BadRequest();

        await _unitOfWork.Medics.Delete(id);
        await _unitOfWork.CompleteAsync();

        return Ok(item);
    }
}
