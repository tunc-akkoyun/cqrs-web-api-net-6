using MediatR;

namespace Store.Application.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}