using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/insurance-policies")]
public class InsurancePoliciesController : ControllerBase
{
    private readonly IInsurancePolicyRepository _insurancePolicyRepo;

    public InsurancePoliciesController(IInsurancePolicyRepository insurancePolicyRepository)
    {
        _insurancePolicyRepo = insurancePolicyRepository;
    }

    // Implement GET, POST, PUT, DELETE methods for InsurancePolicies using dependency injected _insurancePoliciesRepository
    [HttpGet]
    public async Task<IActionResult> GetPolicies([FromQuery] PolicyQueryParameters parameters)
    {
        DateTime? startDateValue = null;
        if (!string.IsNullOrEmpty(parameters.StartDate))
        {
            if (DateTime.TryParse(parameters.StartDate, out DateTime parsedDate))
            {
                startDateValue = parsedDate;
            }
            else
            {
                return BadRequest("Invalid start date format");
            }
        }

        QueryObject queryObject = new QueryObject
        {
            OrderBy = parameters.SortColumn,
            IsAscending = parameters.SortOrder == SortOrder.Ascending,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize,
            StartDate = startDateValue,
            UserID = parameters.UserID
        };
        var (insurancePolicies, totalCount) = await _insurancePolicyRepo.GetPolicies(queryObject);       
        var response = new
        {
            InsurancePolicies = insurancePolicies,
            TotalCount = totalCount
        };
        return Ok(response);
    }   
      
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var insurancePolicy = await _insurancePolicyRepo.GetPolicyById(id);
        if (insurancePolicy == null)
        {
            return NotFound();
        }
        return Ok(insurancePolicy);
    }

    [HttpPost]
    public async Task<IActionResult> AddPolicy([FromBody] InsurancePolicy insurancePolicy)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _insurancePolicyRepo.CreatePolicy(insurancePolicy);
        return CreatedAtAction(nameof(Get), new { id = insurancePolicy.ID }, insurancePolicy);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] InsurancePolicy insurancePolicy)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (id != insurancePolicy.ID)
        {
            return BadRequest();
        }
        await _insurancePolicyRepo.UpdatePolicy(insurancePolicy);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var insurancePolicy = await _insurancePolicyRepo.GetPolicyById(id);
        if (insurancePolicy == null)
        {
            return NotFound();
        }
        await _insurancePolicyRepo.DeletePolicy(insurancePolicy.ID);
        return NoContent();
    }
}

