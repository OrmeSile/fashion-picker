namespace FashionPicker.Core.Infra.Adapters.LocalCMS;

internal record ListResponse<TObject>(
    IEnumerable<TObject> Data
    );