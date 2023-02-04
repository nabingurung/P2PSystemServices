namespace ReadApi.Core.Common
{
    public class BaseEntity<PkId>
    {
        public PkId PKId { get; set; } = default!;
    }
}
