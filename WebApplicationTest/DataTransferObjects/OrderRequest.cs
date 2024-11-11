namespace WebApplicationTest.DataTransferObjects
{
    public record OrderRequest(int CLientId, List<string> ProductIds);
}
