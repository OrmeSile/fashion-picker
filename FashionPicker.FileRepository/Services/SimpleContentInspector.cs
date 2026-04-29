using System.Collections.Immutable;
using FashionPicker.FileRepository.Interfaces;
using MimeDetective;
using MimeDetective.Engine;

namespace FileRepository.Services;

public class SimpleContentInspector : ISimpleContentInspector
{
    private readonly IContentInspector _contentInspector;

    public SimpleContentInspector()
    {
        var builder = new ContentInspectorBuilder
        {
            Definitions = MimeDetective.Definitions.DefaultDefinitions.All()
        };
        _contentInspector = builder.Build();
    }

    public ImmutableArray<DefinitionMatch> Inspect(ReadOnlySpan<byte> content)
    {
        return _contentInspector.Inspect(content);
    }

    public ImmutableArray<MimeTypeMatch> Inspect(MemoryStream content)
    {
        var results = _contentInspector.Inspect(content);
        content.Position = 0;
        return results.ByMimeType();
    }
}