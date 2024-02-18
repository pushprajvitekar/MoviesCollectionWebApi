namespace MovieCollectionWebApi.Auth
{
    public static class UserAuthorizationRequirement
    {
        public static SameUserAuthorizationRequirement SameUser = new SameUserAuthorizationRequirement();
    }
}
