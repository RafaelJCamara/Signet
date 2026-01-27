namespace Signet.Api.Features.Common.UseCases
{
    public interface IUseCaseVoid<T>
    {
        ValueTask ExecuteAsync(T input, CancellationToken cancellationToken = default);
    }

    public interface IUseCaseOutputOnly<T>
    {
        ValueTask<T> ExecuteAsync(CancellationToken cancellationToken = default);
    }

    public interface IUseCase<TInput, TOutput>
    {
        ValueTask<TOutput> ExecuteAsync(Task input, CancellationToken cancellationToken = default);
    }
}
