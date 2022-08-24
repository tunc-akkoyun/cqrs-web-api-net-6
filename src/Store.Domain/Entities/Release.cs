using System;
using Store.Domain.Primitives;

namespace Store.Domain.Entities;

public sealed class Release : EntityHash
{
    public Release(string version, string notes, DateTime releaseUTC)
    {
        Version = version;
        Notes = notes;
        ReleaseUTC = releaseUTC;
    }

    public string Version { get; private set; }
    public string Notes { get; private set; }
    public DateTime ReleaseUTC { get; set; }
}