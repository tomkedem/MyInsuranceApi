
using Microsoft.EntityFrameworkCore;

public class InsurancePolicyRepository : IInsurancePolicyRepository
{
    private readonly AppDbContext _context;

    public InsurancePolicyRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<(IEnumerable<InsurancePolicyDto>, int)> GetPolicies(QueryObject queryObject)
    {
        var query = _context.InsurancePolicies.AsQueryable();

        if (queryObject.StartDate.HasValue)
        {
            query = query.Where(i => i.StartDate >= queryObject.StartDate.Value);
        }
        if (queryObject.UserID.HasValue)
        {
            query = query.Where(i => i.UserID >= queryObject.UserID.Value);
        }

        var page = await query
            .Select(i => new InsurancePolicyDto{
                ID = i.ID,
                PolicyNumber = i.PolicyNumber,
                InsuranceAmount = i.InsuranceAmount,
                StartDate = i.StartDate,
                EndDate = i.EndDate,
                UserID = i.UserID
            })
            .OrderBy(u => queryObject.OrderBy + (queryObject.IsAscending ? " ASC" : " DESC"))
            .Skip((queryObject.PageNumber - 1) * queryObject.PageSize)
            .Take(queryObject.PageSize)
            .ToListAsync();

        var total = query.Count();

        return (page,total);
    }
    public async Task<InsurancePolicy> GetPolicyById(int id)
    {
        return await _context.InsurancePolicies.FindAsync(id);
    }
    public async Task CreatePolicy(InsurancePolicy policy)
    {
        await _context.InsurancePolicies.AddAsync(policy);
        await _context.SaveChangesAsync();
    }       
    public async Task UpdatePolicy(InsurancePolicy policy)
    {
        // _context.Users.Update(user);
        var updatePolicy = _context.InsurancePolicies.FirstOrDefault(i => i.ID == policy.ID);
        updatePolicy.InsuranceAmount = policy.InsuranceAmount;
        updatePolicy.PolicyNumber = policy.PolicyNumber;    
        updatePolicy.StartDate = policy.StartDate;    
        updatePolicy.EndDate = policy.EndDate;    
        updatePolicy.UserID = policy.UserID;    

        await _context.SaveChangesAsync();

               
    }    
    public async Task DeletePolicy(int id)
    {
        var policy = await _context.InsurancePolicies.FindAsync(id);
        if (policy != null)
        {
            _context.InsurancePolicies.Remove(policy);
            await _context.SaveChangesAsync();
        }
    }
}