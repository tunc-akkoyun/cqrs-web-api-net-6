using System.Collections.Generic;

namespace Store.Domain.Primitives;

public sealed record PagedEntity<TEntity>(List<TEntity> Data, int Count);