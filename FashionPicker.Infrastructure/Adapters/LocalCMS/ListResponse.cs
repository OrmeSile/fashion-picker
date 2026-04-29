namespace FashionPicker.Infrastructure.Adapters.LocalCMS;

internal record ListResponse<TObject>(
    IEnumerable<TObject> Data
);