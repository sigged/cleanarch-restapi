namespace Mde.WishList.Api.Application.Common.Security
{
    public class Policies
    {
        public const string MustBeAdmin = nameof(MustBeAdmin);
        public const string MustBeCreator = nameof(MustBeCreator);
        public const string MustBeLastModifier = nameof(MustBeLastModifier);
    }
}
