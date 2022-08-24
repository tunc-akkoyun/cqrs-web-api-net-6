using MediatR;

namespace Store.Application.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}