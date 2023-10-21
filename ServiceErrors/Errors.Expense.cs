using ErrorOr;

namespace ServiceErrors.Expense;

public static class Errors
{
    public static class Expense
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
            code: "Expense.NotFound",
            description: "Expense not found");

        public static Error ApartmentDoesnNotExist => Error.NotFound(
            code: "Apartment.Does.Not.Exist",
            description: "Apartment Does Not Exist");
    }
}