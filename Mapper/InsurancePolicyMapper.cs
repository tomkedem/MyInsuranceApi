public static class InsurancePolicyMapper
{
    public static InsurancePolicyDto ToInsurancePolicyDto(this InsurancePolicy insurancePolicy)
    {
        return new InsurancePolicyDto
        {
            ID = insurancePolicy.ID,
            PolicyNumber = insurancePolicy.PolicyNumber,
            InsuranceAmount = insurancePolicy.InsuranceAmount,
            StartDate = insurancePolicy.StartDate,
            EndDate = insurancePolicy.EndDate,
            UserID = insurancePolicy.UserID
        };
    }

    public static InsurancePolicy ToInsurancePolicyModel(this InsurancePolicyDto insurancePolicyDto)
    {
        return new InsurancePolicy
        {
            ID = insurancePolicyDto.ID,
            PolicyNumber = insurancePolicyDto.PolicyNumber,
            InsuranceAmount = insurancePolicyDto.InsuranceAmount,
            StartDate = insurancePolicyDto.StartDate,
            EndDate = insurancePolicyDto.EndDate,
            UserID = insurancePolicyDto.UserID
        };
    }
}