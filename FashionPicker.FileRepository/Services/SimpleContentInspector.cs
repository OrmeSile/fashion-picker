using System.Collections.Immutable;
using MimeDetective;
using MimeDetective.Engine;

namespace FileRepository.Services;

public class SimpleContentInspector: IContentInspector
{
    private readonly IContentInspector _contentInspector;

    public SimpleContentInspector()
    {
        var builder = new ContentInspectorBuilder
        {
            Definitions = MimeDetective.Definitions.DefaultDefinitions.All()
        };
        _contentInspector =  builder.Build();
    }

    public ImmutableArray<DefinitionMatch> Inspect(ReadOnlySpan<byte> content)
    {
        return _contentInspector.Inspect(content);
    }
}