using ErrorOr;

namespace ServiceErrors.Bill;

public static class Errors
{
    public static class Bill
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
            code: "Bill.NotFound",
            description: "Bill not found");

        public static Error ApartmentOrExpenseDoesnNotExist => Error.NotFound(
            code: "Apartment.Or.Expense.Does.Not.Exist",
            description: "Apartment Or Expense Does Not Exist");
    }
}