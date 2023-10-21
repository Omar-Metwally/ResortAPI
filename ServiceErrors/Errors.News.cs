using ErrorOr;

namespace ServiceErrors.News;

public static class Errors
{
    public static class News
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
            code: "News.NotFound",
            description: "News not found");

        public static Error ApartmentDoesnNotExist => Error.NotFound(
            code: "Apartment.Does.Not.Exist",
            description: "Apartment Does Not Exist");

        public static Error WrongDate => Error.Validation(
                    code: "Lease.End.Date.Must.Be.Later.Than.Start.Date",
                    description: "Lease End Date Must Be Later Than Start Date");

        public static Error UnderLease => Error.Validation(
            code: "The.Apartment.Is.Under.Lease.In.This.Period",
            description: "The Apartment Is Under Lease In This Period");


        /*public static Error WrongDate => Error.Validation(
            code: "Lease.End.Date.Must.Be.Later.Than.Start.Date",
            description: "Lease End Date Must Be Later Than Start Date");*/
    }
}