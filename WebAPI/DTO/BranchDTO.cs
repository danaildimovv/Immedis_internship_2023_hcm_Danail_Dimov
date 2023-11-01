namespace WebAPI.DTO
{
    public class BranchDTO
    {
        public int BranchId { get; set; }

        public string BranchName { get; set; } = null!;

        public int BranchCountryId { get; set; }
    }
}
