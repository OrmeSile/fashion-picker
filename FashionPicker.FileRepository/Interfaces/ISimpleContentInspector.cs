using System.Collections.Immutable;
using MimeDetective;
using MimeDetective.Engine;

namespace FashionPicker.FileRepository.Interfaces;

public interface ISimpleContentInspector : IContentInspector
{
    public ImmutableArray<MimeTypeMatch> Inspect(MemoryStream content);
}