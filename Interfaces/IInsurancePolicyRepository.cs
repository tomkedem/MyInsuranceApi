public interface IInsurancePolicyRepository
{
    Task<(IEnumerable<InsurancePolicyDto>, int)> GetPolicies(QueryObject queryObject);
    Task<InsurancePolicy> GetPolicyById(int id);
    Task CreatePolicy(InsurancePolicy policy);
    Task UpdatePolicy(InsurancePolicy policy);
    Task DeletePolicy(int id);
}