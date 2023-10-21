using ErrorOr;

namespace ServiceErrors.Apartment;

public static class Errors
{
    public static class Apartment
    {
        /*public static Error InvalidName => Error.Validation(
            code: "Breakfast.InvalidName",
            description: $"Breakfast name must be at least {WebApplication5.DominModels.Apartment.MinNameLength}" +
                $" characters long and at most {Models.Breakfast.MaxNameLength} characters long.");

        public static Error InvalidDescription => Error.Validation(
            code: "Breakfast.InvalidDescription",
            description: $"Breakfast description must be at least {Models.Breakfast.MinDescriptionLength}" +
                $" characters long and at most {Models.Breakfast.MaxDescriptionLength} characters long.");*/

        public static Error NotFound => Error.NotFound(
            code: "Apartment.NotFound",
            description: "Apartment not found");

        public static Error OwnerDoesnNotExist => Error.NotFound(
            code: "Owner.Does.Not.Exist",
            description: "Owner Does Not Exist");
    }
}