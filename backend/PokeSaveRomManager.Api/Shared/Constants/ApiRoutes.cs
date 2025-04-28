namespace PokeSaveRomManager.Api.Shared.Constants
{
    public static class ApiRoutes
    {
        private const string Base = "api/v{version:apiVersion}";

        public static class Types
        {
            public const string Root = Base + "/types";
        }

        public static class Items
        {
            public const string Root = Base + "/items";
        }
        public static class Categories
        {
            public const string Root = Base + "/categories";
        }

        public static class Natures
        {
            public const string Root = Base + "/natures";
        }

        public static class Stats
        {
            public const string Root = Base + "/stats";
        }

        public static class Abilities
        {
            public const string Root = Base + "/abilities";
            public const string Names = "names";
        }

        public static class Games
        {
            public const string Root = Base + "/games";
            public const string ById = Root + "/{gameId}";

            public const string Official = "official";
            public const string Generation = "generation";
            public const string Region = "region";

            public const string Types = ById + "/types";
            public const string Moves = ById + "/moves";
            public const string Abilities = ById + "/abilities";
        }

        public static class Moves
        {
            public const string Root = Base + "/moves";
            public const string Names = "names";

            public const string LearningMethods = Root + "/learning-methods";
        }

        public static class Pokemon
        {
            public const string Root = Base + "/pokemon";
        }

        public static class Users
        {
            public const string Auth = "/auth/login";
        }
    }
}
