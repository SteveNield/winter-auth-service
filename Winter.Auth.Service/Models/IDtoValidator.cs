namespace Winter.Auth.Service.Models
{
    public interface IDtoValidator<in T>
    {
        bool IsValid(T dtoToValidate);
    }
}